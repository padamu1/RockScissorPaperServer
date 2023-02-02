using RockScissorPaperServer.Script.Common.Dto;
using SimulFactory.Common.Bean;
using SimulFactory.Common.Instance;
using SimulFactory.Common.Thread;
using SimulFactory.Core.Sql;
using SimulFactory.Game.Event;

namespace SimulFactory.Game.Matching.Mode
{
    public class NormalMatch : MatchThread
    {
        public NormalMatch(MatchSystem matchSystem) : base(matchSystem)
        {

        }
        /// <summary>
        /// 노멀 매치는 점수 계산이 들어감
        /// </summary>
        /// <param name="winTeamNos"></param>
        protected override void SendGameResult(List<int> winTeamNos)
        {
            int matchType = (int)Define.MATCH_TYPE.Normal;
            // 결과 각 유저에게 전송
            foreach (KeyValuePair<int, PcInstance> pc in pcDic)
            {
                NormalPvpDto normalPvpDto = pc.Value.GetPcPvp().GetNormalPvpDto();
                if (winTeamNos.Contains(pc.Value.GetPcPvp().GetTeamNo()))
                {
                    normalPvpDto.Rating += (int)(Define.K_FACTOR * (1 - (eloDic[pc.Key])));
                    normalPvpDto.WinCount++;
                    pc.Value.SendPacket(S_MatchingResult.Data(true, normalPvpDto, matchType));
                }
                else
                {
                    normalPvpDto.Rating += (int)(Define.K_FACTOR * (0 - (eloDic[pc.Key])));
                    normalPvpDto.DefeatCount++;
                    pc.Value.SendPacket(S_MatchingResult.Data(false, normalPvpDto, matchType));

                }
                pc.Value.GetPcPvp().SetMatch(null);
            }
            // 노말 데이터 설정
            PcPvpSql.UpdateUserNormalPvpSql(pcDic.Values.ToArray());
            base.SendGameResult(winTeamNos);
        }
    }
}
