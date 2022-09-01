using SimulFactory.Common.Bean;
using SimulFactory.Core;
using SimulFactory.Game;
using System.Net.Sockets;
using System.Text;

namespace SimulFactory.Common.Instance
{
    public class PcInstance : WebSocketController
    {
        public UserData UserData { get; set; }
        public PcPvp pcPvp { get; set; }
        public PcInstance(TcpClient tcpClient) : base(tcpClient)
        {
            UserData = new UserData();
            pcPvp = new PcPvp();
        }

        #region 데이터 처리
        protected override bool ProcessClientRequest(int dataSize)
        {
            bool fin = (dataBuffer[0] & 0b10000000) != 0;   // 혹시 false일 경우 다음 데이터와 이어주는 처리를 해야 함
            bool mask = (dataBuffer[1] & 0b10000000) != 0;  // 클라이언트에서 받는 경우 무조건 true
            PayloadDataType opcode = (PayloadDataType)(dataBuffer[0] & 0b00001111); // enum으로 변환

            int msglen = dataBuffer[1] - 128; // Mask bit가 무조건 1라는 가정하에 수행
            int offset = 2;     //데이터 시작점
            if (msglen == 126)  //길이 126 이상의 경우
            {
                msglen = BitConverter.ToInt16(new byte[] { dataBuffer[3], dataBuffer[2] });
                offset = 4;
            }
            else if (msglen == 127)
            {
                // 이 부분은 구현 안 함. 나중에 필요한 경우 구현
                Console.WriteLine("Error: over int16 size");
                return true;
            }

            if (mask)
            {
                byte[] decoded = new byte[msglen];
                //마스킹 키 획득
                byte[] masks = new byte[4] { dataBuffer[offset], dataBuffer[offset + 1], dataBuffer[offset + 2], dataBuffer[offset + 3] };
                offset += 4;

                for (int i = 0; i < msglen; i++)    //마스크 제거
                {
                    decoded[i] = (byte)(dataBuffer[offset + i] ^ masks[i % 4]);
                }

                switch (opcode)
                {
                    case PayloadDataType.Text:
                        MessageManager.ProcessData(this, ByteUtillity.ByteToObject(decoded));
                        SendData(Encoding.UTF8.GetBytes("Success!"), PayloadDataType.Text);
                        break;
                    case PayloadDataType.Binary:
                        //Binary는 아무 동작 없음
                        break;
                    case PayloadDataType.ConnectionClose:
                        //받은 요청이 서버에서 보낸 요청에 대한 응답이 아닌 경우에만 실행
                        if (State != WebSocketState.CloseSent)
                        {
                            SendCloseRequest(1000, "Graceful Close");
                            State = WebSocketState.Closed;
                        }
                        Dispose();      // 소켓 닫음
                        return false;
                    default:
                        Console.WriteLine("Unknown Data Type");
                        break;
                }
            }
            else
            {
                // 마스킹 체크 실패
                Console.WriteLine("Error: Mask bit not valid");
            }

            return true;
        }
        #endregion
    }
}
