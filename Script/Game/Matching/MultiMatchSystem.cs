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
        /// <summary>
        /// 멀티 매칭일 경우 재정의 필요
        /// </summary>
        /// <param name="searchCount"></param>
        protected override void CheckSearchUser(int searchCount)
        {
            // 매칭 전 정렬 -> 점수 순으로 정렬
            matchSearchList.OrderBy(x => x.GetPcPvp().GetRating());

            // 실제 로직 처리 -> 현재 두명이 잡히도록 되어있음
            for (int count = 0; count < matchSearchList.Count - 2;)
            {
                //bool matchSuccess = false;
                if (matchSearchList[count + 2].GetPcPvp().GetRating() - matchSearchList[count].GetPcPvp().GetRating() <= Define.DEFAULT_SEARCH_RATING + matchSearchList[count].GetPcPvp().GetWaitCount() * Define.INCREASE_SEARCH_RATING)
                {
                    MultiMatch match = new MultiMatch(this);
                    match.AddPcInstance(matchSearchList[count]);
                    RemovePcInstance(matchSearchList[count]);
                    match.AddPcInstance(matchSearchList[count + 1]);
                    RemovePcInstance(matchSearchList[count + 1]);
                    match.AddPcInstance(matchSearchList[count + 2]);
                    RemovePcInstance(matchSearchList[count + 2]);
                    match.CalculateEloRating();
                    readyMatchList.Add(match);
                    count += 3;
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
