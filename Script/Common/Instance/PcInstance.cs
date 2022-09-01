using SimulFactory.Common.Bean;
using SimulFactory.Core;
using SimulFactory.Game;
using System.Net.Sockets;
using System.Text;

namespace SimulFactory.Common.Instance
{
    public class PcInstance
    {
        public UserData UserData { get; set; }
        public PcPvp pcPvp { get; set; }
        private WebSocketController socketController;
        public PcInstance(WebSocketController socketController)
        {
            this.socketController = socketController;
            UserData = new UserData();
            pcPvp = new PcPvp();
        }
        public void SendPacket(byte evCode, Dictionary<byte, object> param)
        {
            socketController.SendPacket(evCode,param);
        }
    }
}
