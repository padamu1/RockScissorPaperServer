using SimulFactory.Common.Bean;
using SimulFactory.Common.Instance;
using SimulFactory.Core.Sql;
using SimulFactory.Game;
using System.Collections;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
namespace SimulFactory.Core
{
    public class WebSocketController : DataProcessing
    {
        public WebSocketController(TcpClient tcpClient) : base(tcpClient)
        {
            pc = new PcInstance(this);  // 유저가 접속하면 PcInstance를 생성해줌
            messageStream.BeginRead(dataBuffer, 0, dataBuffer.Length, OnReadData, null);
        }
        /// <summary>
        /// pc에 데이터를 전달하기 위한 함수
        /// </summary>
        /// <param name="eventCode"></param>
        /// <param name="param"></param>
        public void SendPacket(byte eventCode, Dictionary<byte, object> param)
        {
            dataFormat.eventCode = eventCode;
            dataFormat.data = param;
            SendData(ByteUtillity.ObjectToByte(dataFormat));
        }
    }
}
