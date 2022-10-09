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
    }
}
