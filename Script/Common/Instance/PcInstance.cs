using SimulFactory.Common.Bean;
using SimulFactory.Core;
using SimulFactory.Game;
using SimulFactory.Game.Event;
using SimulFactory.Game.Matching;
using System.Net.Sockets;
using System.Text;

namespace SimulFactory.Common.Instance
{
    public class PcInstance : IDisposable
    {
        private WebSocketController socketController;

        private UserData userData;
        private PcPvp pcPvp;
        private MatchSystem? matchSystem;
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
            userData = new UserData(this);
            pcPvp = new PcPvp(this);
        }
        #region Getter
        public PcPvp GetPcPvp()
        {
            return pcPvp;
        }
        public UserData GetUserData()
        {
            return userData;
        }
        #endregion
        public void SendUserData()
        {
            S_UserInfo.UserInfoS(this);
        }
        public void SetMatchSystem(MatchSystem? matchSystem)
        {
            this.matchSystem = matchSystem;
            // 매칭 시스템에 들어갔을 때 유저에게 매칭시스템에 들어간 패킷 처리 혹은 들어가지 못했을 때 패킷 처리
        }
        public MatchSystem GetMatchSystem()
        {
            return matchSystem;
        }
        /// <summary>
        /// 유저 객체 사라질 때 호출
        /// </summary>
        public void Dispose()
        {
            if(pcPvp.GetMatch() != null)
            {
                Match match = pcPvp.GetMatch();
                match.UserDisconnect(this);
            }
            matchSystem?.RemovePcInstance(this);

            Console.WriteLine(GetUserData().UserName + " Client Disconnected");
        }
    }
}
