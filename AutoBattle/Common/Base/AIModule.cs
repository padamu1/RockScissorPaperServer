using RockScissorPaperServer.PacketSerializer.Model;
using SimulFactory.Common.Bean;
using SimulFactory.Common.Instance;
using SimulFactory.Core;
using SimulFactory.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockScissorPaperServer.AutoBattle.Common.Base
{
    public class AIModule : PcInstance
    {
        private Define.MATCH_TYPE matchType;
        public AIModule(WebSocketController socketController) : base(socketController)
        {
            Console.WriteLine("AI Module created");
        }
        public void SetMatchType(Define.MATCH_TYPE matchType)
        {
            this.matchType = matchType;
        }
        /// <summary>
        /// AIModule의 정보를 설정하기 위한 함수
        /// </summary>
        protected override void SetupUser()
        {
            userData = new UserData(this);
            pcPvp = new PcPvp(this);
            this.userData.UserName = LoginUtil.MakeUserNickName();
            this.userData.UserNo = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }
        public void SetRating(int rating)
        {
            pcPvp.GetNormalPvpDto().Rating = rating;
        }
        /// <summary>
        /// AIModule은 패킷에 대한 처리를 할 필요가 없으므로 전용 프로세스 매니저를 통해 처리
        /// </summary>
        /// <param name="packetData"></param>
        public override void SendPacket(PacketData packetData)
        {
            AIModulePacketProcessManager.ProcessData(this, packetData);
        }
        /// <summary>
        /// 게임이 종료되었을 때 queue에 반환하도록 설정
        /// </summary>
        public void DespawnModule()
        {
            Console.WriteLine("AI Returned");
            AutoBattleManager.GetInstance().ReturnAIModule(this);
        }
    }
}
