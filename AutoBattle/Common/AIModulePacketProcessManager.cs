using RockScissorPaperServer.AutoBattle.Common.Base;
using RockScissorPaperServer.PacketSerializer.Model;
using SimulFactory.Common.Bean;
using SimulFactory.Core.Util;

namespace RockScissorPaperServer.AutoBattle.Common
{
    public class AIModulePacketProcessManager
    {
        /// <summary>
        /// AI Module은 서버로 부터 메시지를 받아서 처리하게 설정해야 함
        /// </summary>
        /// <param name="ai"></param>
        /// <param name="packet"></param>
        public static void ProcessData(AIModule ai, PacketData packet)                      // 클라이언트로부터 들어온 정보를 처리하기 위한 함수
        {
            byte eventCode = packet.EvCode;
            Dictionary<byte, object> param = packet.Data;

            // 데이터가 널이 아니라면 넣어줌
            if (param == null)
            {
                return;
            }
            try
            {
                switch ((Define.EVENT_CODE)eventCode)
                {
                    case Define.EVENT_CODE.MatchingSuccessS:
                        ai.GetPcPvp().SetMatchAccept(true);
                        break;
                    case Define.EVENT_CODE.MatchingResponseS:
                        // 매칭에 대한 결과
                        ai.GetPcPvp().GetMatch().SetDmg(ai.GetPcPvp().GetTeamNo(), Util.RInt(0,3));
                        break;
                    case Define.EVENT_CODE.MatchingCancelS:
                        ai.DespawnModule();
                        break;
                    case Define.EVENT_CODE.MatchingResultS:
                        // 승리 패배 결과
                        ai.DespawnModule();
                        break;
                    case Define.EVENT_CODE.UserBattleResponseS:
                        // 상대편이 낸 결과를 받음
                        break;
                    case Define.EVENT_CODE.RoundResultS:
                        // 해당 라운드의 결과를 받음
                        ai.GetPcPvp().GetMatch().SetDmg(ai.GetPcPvp().GetTeamNo(), Util.RInt(0, 3));
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }
    }
}
