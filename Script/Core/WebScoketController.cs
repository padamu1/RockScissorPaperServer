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
            pc = new PcInstance(this);
        }
        public void SendPacket(byte eventCode, Dictionary<byte, object> param)
        {
            dataFormat.eventCode = eventCode;
            dataFormat.data = param;
            SendData(ByteUtillity.ObjectToByte(dataFormat));
        }
    }
}
