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
            foreach(KeyValuePair<int, Common.Instance.PcInstance> calculatedTeam in userRating)
            {
                int teamRating = userRating[1].GetPcPvp().GetRating();

                int enemyRating = 0; 
                foreach (KeyValuePair<int, Common.Instance.PcInstance> team in userRating)
                {
                    // 키가 같으면 스킵
                    if(calculatedTeam.Key == team.Key)
                    {
                        continue;
                    }
                    enemyRating += team.Value.GetPcPvp().GetRating();
                }
                float teamProbability = Probability(teamRating, enemyRating / userRating.Count - 1);
                eloDic.Add(calculatedTeam.Key, teamProbability);
            }
        }
        private static float Probability(int ratingA, int ratingB)
        {
            return 1 / (1 + MathF.Pow(10f, (ratingB - ratingA) / 400f));
        }
        public static Define.ROCK_SCISSOR_PAPER GetRSPResultV2(Dictionary<int, int> roundResponseDic)
        {
            int rockCount = 0;
            int paperCount = 0;
            int scissorCount = 0;
            foreach (KeyValuePair<int, int> response in roundResponseDic)
            {
                switch ((Define.ROCK_SCISSOR_PAPER)response.Value)
                {
                    case Define.ROCK_SCISSOR_PAPER.Rock:
                        rockCount++;
                        break;
                    case Define.ROCK_SCISSOR_PAPER.Paper:
                        paperCount++;
                        break;
                    case Define.ROCK_SCISSOR_PAPER.Scissor:
                        scissorCount++;
                        break;
                }
            }

            if(scissorCount == 0)
            {
                if(rockCount == roundResponseDic.Count || paperCount == roundResponseDic.Count)
                {
                    return Define.ROCK_SCISSOR_PAPER.Tie;
                }
                return Define.ROCK_SCISSOR_PAPER.Paper;
            }
            else if(rockCount == 0)
            {
                if (paperCount == roundResponseDic.Count || scissorCount == roundResponseDic.Count)
                {
                    return Define.ROCK_SCISSOR_PAPER.Tie;
                }
                return Define.ROCK_SCISSOR_PAPER.Scissor;
            }
            else if (paperCount == 0)
            {
                if (scissorCount == roundResponseDic.Count || rockCount == roundResponseDic.Count)
                {
                    return Define.ROCK_SCISSOR_PAPER.Tie;
                }
                return Define.ROCK_SCISSOR_PAPER.Rock;
            }
            else
            {
                return Define.ROCK_SCISSOR_PAPER.Tie;
            }

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
