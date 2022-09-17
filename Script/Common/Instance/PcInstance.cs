using SimulFactory.Common.Bean;
using SimulFactory.Core;
using SimulFactory.Game;
using System.Net.Sockets;
using System.Text;

namespace SimulFactory.Common.Instance
{
    public class PcInstance
    {
        private WebSocketController socketController;

        private UserData userData;
        private PcPvp pcPvp;

        public PcInstance(WebSocketController socketController)
        {
            this.socketController = socketController;
            SetupUser();
        }
        public void SendPacket(byte evCode, Dictionary<byte, object> param)
        {
            socketController.SendPacket(evCode,param);
        }
        /// <summary>
        /// 유저 로그인 시 로딩되어야 할 정보들
        /// </summary>
        private void SetupUser()
        {
            userData = new UserData();
            pcPvp = new PcPvp(this);
        }
        #region Gettser
        public PcPvp GetPcPvp()
        {
            return pcPvp;
        }
        public UserData GetUserData()
        {
            return userData;
        }
        #endregion
    }
}
