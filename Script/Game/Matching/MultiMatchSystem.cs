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
        public MultiMatchSystem()
        {
            defaultSearchCount = Define.MULTI_MODE_SEARCH_USER_COUNT;
            minSearchCount = Define.MULTI_MODE_SEARCH_USER_MIN_COUNT;
        }
        /// <summary>
        /// 멀티 매칭일 경우 재정의 필요
        /// </summary>
        /// <param name="searchCount"></param>
        protected override void CheckSearchUser(int searchCount)
        {
            // 매칭 전 정렬 -> 점수 순으로 정렬
            matchSearchList.OrderBy(x => x.GetPcPvp().GetRating());

            // 실제 로직 처리
            for (int count = 0; count < matchSearchList.Count - searchCount - 1;)
            {
                //bool matchSuccess = false;
                if (matchSearchList[count + searchCount].GetPcPvp().GetRating() - matchSearchList[count].GetPcPvp().GetRating() <= Define.DEFAULT_SEARCH_RATING + matchSearchList[count].GetPcPvp().GetWaitCount() * Define.INCREASE_SEARCH_RATING)
                {
                    MultiMatch match = new MultiMatch(this);
                    for(int index = count; index < count + searchCount; index++)
                    {
                        match.AddPcInstance(matchSearchList[index]);
                        RemovePcInstance(matchSearchList[index]);
                    }
                    match.CalculateEloRating();
                    readyMatchList.Add(match);
                    count += searchCount;
                }
                else
                {
                    matchSearchList[count].GetPcPvp().SetWaitCount(matchSearchList[count].GetPcPvp().GetWaitCount() + 1);
                    count += 1;

                    // 멀티 매칭 중 카운트가 최대 대기 카운트 보다 크다면, 유저 3명, 유저 2명에 대한 매칭이 이루어지도록 설정
                    if (matchSearchList[count].GetPcPvp().GetWaitCount() >= Define.MULTI_MATCH_WAIT_COUNT)
                    {
                        this.decreaseSearchCount = true;
                    }
                }
            }
        }
    }
}
