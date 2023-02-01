using SimulFactory.Common.Bean;
using SimulFactory.Common.Instance;
using SimulFactory.Common.Thread;
using SimulFactory.Core.Sql;
using SimulFactory.Core;
using SimulFactory.Core.Util;
using SimulFactory.Game.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockScissorPaperServer.Script.Common.Dto;

namespace SimulFactory.Game.Matching.Mode
{
    public class MultiMatch : MatchThread
    {
        public MultiMatch(MatchSystem matchSystem) : base(matchSystem)
        {
            
        }
        /// <summary>
        /// 멀티 게임은 점수 계산이 필요없음
        /// </summary>
        /// <param name="winTeamNos"></param>
        protected override void SendGameResult(List<int> winTeamNos)
        {
            int matchType = (int)Define.MATCH_TYPE.Multi;
            // 결과 각 유저에게 전송
            foreach (KeyValuePair<int, PcInstance> pc in pcDic)
            {
                MultiPvpDto multiPvpDto = pc.Value.GetPcPvp().GetMultiPvpDto();
                if (winTeamNos.Contains(pc.Value.GetPcPvp().GetTeamNo()))
                {
                    multiPvpDto.WinCount++;
                    pc.Value.SendPacket(S_MatchingResult.Data(true, multiPvpDto, matchType));
                }
                else
                {
                    multiPvpDto.DefeatCount++;
                    pc.Value.SendPacket(S_MatchingResult.Data(false, multiPvpDto, matchType));

                }
                pc.Value.GetPcPvp().SetMatch(null);
            }
            // 멀티 데이터 설정
            //PcPvpSql.UpdateUserPvpSql(pcDic.Values.ToArray());
            base.SendGameResult(winTeamNos);
        }
    }
}
