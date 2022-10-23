using SimulFactory.Common.Bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Core.Util
{
    public class MatchUtil
    {
        public static void SetPvpEloRating(Dictionary<int, Common.Instance.PcInstance> userRating, ref Dictionary<int, float> eloDic)
        {
            int teamARating = userRating[1].GetPcPvp().GetRating();
            int teamBRating = userRating[2].GetPcPvp().GetRating();

            float teamAProbability = Probability(teamARating, teamBRating);
            float teamBProbability = Probability(teamBRating, teamARating);

            eloDic.Add(1, teamAProbability);
            eloDic.Add(2, teamBProbability);
        }
        private static float Probability(int ratingA, int ratingB)
        {
            return 1 / (1 + MathF.Pow(10f, (ratingB - ratingA) / 400f));
        }
        public static int GetRSPResult(Dictionary<int,int> roundResponseDic)
        {
            int winTeamNo = 0;
            switch ((Define.ROCK_SCISSOR_PAPER)roundResponseDic[1])
            {
                case Define.ROCK_SCISSOR_PAPER.Rock:
                    switch ((Define.ROCK_SCISSOR_PAPER)roundResponseDic[2])
                    {
                        case Define.ROCK_SCISSOR_PAPER.Rock:
                            winTeamNo = 3;
                            break;
                        case Define.ROCK_SCISSOR_PAPER.Scissor:
                            winTeamNo = 1;
                            break;
                        case Define.ROCK_SCISSOR_PAPER.Paper:
                            winTeamNo = 2;
                            break;
                    }
                    break;
                case Define.ROCK_SCISSOR_PAPER.Scissor:
                    switch ((Define.ROCK_SCISSOR_PAPER)roundResponseDic[2])
                    {
                        case Define.ROCK_SCISSOR_PAPER.Rock:
                            winTeamNo = 2;
                            break;
                        case Define.ROCK_SCISSOR_PAPER.Scissor:
                            winTeamNo = 3;
                            break;
                        case Define.ROCK_SCISSOR_PAPER.Paper:
                            winTeamNo = 1;
                            break;
                    }
                    break;
                case Define.ROCK_SCISSOR_PAPER.Paper:
                    switch ((Define.ROCK_SCISSOR_PAPER)roundResponseDic[2])
                    {
                        case Define.ROCK_SCISSOR_PAPER.Rock:
                            winTeamNo = 1;
                            break;
                        case Define.ROCK_SCISSOR_PAPER.Scissor:
                            winTeamNo = 2;
                            break;
                        case Define.ROCK_SCISSOR_PAPER.Paper:
                            winTeamNo = 3;
                            break;
                    }
                    break;
            }
            return winTeamNo;
        }
    }
}
