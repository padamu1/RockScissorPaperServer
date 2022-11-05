using SimulFactory.Common.Bean;
using SimulFactory.Game.Matching.Mode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Game.Matching
{
    public class MultiMatchSystem : MatchSystem
    {
        static readonly Lazy<MultiMatchSystem> instanceHolder = new Lazy<MultiMatchSystem>(() => new MultiMatchSystem());
        public static MultiMatchSystem GetInstance()
        {
            return instanceHolder.Value;
        }
        protected override void CheckSearchUser()
        {
            base.CheckSearchUser();

            // 매칭 전 정렬
            matchSearchList.OrderBy(x => x.GetPcPvp().GetRating());
            // 실제 로직 처리
            for (int count = 0; count < matchSearchList.Count - 1;)
            {
                //bool matchSuccess = false;
                if (matchSearchList[count + 1].GetPcPvp().GetRating() - matchSearchList[count].GetPcPvp().GetRating() <= Define.DEFAULT_SEARCH_RATING + matchSearchList[count].GetPcPvp().GetWaitCount() * Define.INCREASE_SEARCH_RATING)
                {
                    MultiMatch match = new MultiMatch(this);
                    match.AddPcInstance(matchSearchList[count]);
                    RemovePcInstance(matchSearchList[count]);
                    match.AddPcInstance(matchSearchList[count + 1]);
                    RemovePcInstance(matchSearchList[count + 1]);
                    match.CalculateEloRating();
                    readyMatchList.Add(match);
                    count += 2;
                }
                else
                {
                    matchSearchList[count].GetPcPvp().SetWaitCount(matchSearchList[count].GetPcPvp().GetWaitCount() + 1);
                    count += 1;
                }
            }
        }
    }
}
