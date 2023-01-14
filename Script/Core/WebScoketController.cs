using SimulFactory.Common.Bean;
using SimulFactory.Common.Instance;
using SimulFactory.Core.Util;
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
            try
            {
                messageStream.BeginRead(dataBuffer, 0, dataBuffer.Length, OnReadData, null);
            }
            catch(Exception e)
            {
                PcListInstance.GetInstance().RemoveInstance(pc.GetUserData().UserNo);
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// pc에 데이터를 전달하기 위한 함수
        /// </summary>
        /// <param name="eventCode"></param>
        /// <param name="param"></param>
        public void SendPacket(EventData eventData) => SendData(ByteUtillity.ObjectToByte(eventData));
    }
}
