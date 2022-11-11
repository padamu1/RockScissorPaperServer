using SimulFactory.Game.Matching.Mode;
using SimulFactory.Common.Bean;
using SimulFactory.Common.Instance;
using SimulFactory.Game.Event;
using SimulFactory.Game.Matching;
namespace SimulFactory.Game
{
    public class MatchSystem
    {
        protected List<PcInstance> matchSearchList;
        protected List<PcInstance> addMatchList;
        protected List<PcInstance> removeMatchList;
        protected List<Match> removeReadyMatchList;
        protected List<Match> readyMatchList;
        protected int defaultSearchCount = 0;
        protected int searchCount = 0;
        protected int minSearchCount = 0;
        protected bool decreaseSearchCount = false;

        public MatchSystem()
        {
            matchSearchList = new List<PcInstance>();
            addMatchList = new List<PcInstance>();
            removeMatchList = new List<PcInstance>();
            removeReadyMatchList = new List<Match>();
            readyMatchList = new List<Match>();
        }
        public void Matching()
        {
            while (true)
            {
                lock (addMatchList)
                {
                    foreach (PcInstance instance in addMatchList)
                    {
                        instance.SetMatchSystem(this);
                        Console.WriteLine(instance.GetUserData().UserNo + " 매칭 리스트 추가");
                        matchSearchList.Add(instance);
                    }
                    addMatchList.Clear();
                }
                lock (removeMatchList)
                {
                    foreach (PcInstance instance in removeMatchList)
                    {
                        Console.WriteLine(instance.GetUserData().UserNo + " 매칭 리스트 삭제");
                        matchSearchList.Remove(instance);
                        instance.SetMatchSystem(null);
                    }
                    removeMatchList.Clear();
                }
                lock (matchSearchList)
                {
                    // 검색 카운트를 낮추어야 하는지 판단.
                    if(decreaseSearchCount)
                    {
                        // 검색 카운트를 낮추어야 한다면, 최소보다 큰지 판단
                        if(searchCount > minSearchCount)
                        {
                            searchCount--;
                        }
                    }
                    else
                    {
                        searchCount = defaultSearchCount;
                    }
                    CheckSearchUser(searchCount);

                }
                lock (removeReadyMatchList)
                {
                    foreach (Match match in removeReadyMatchList)
                    {
                        readyMatchList.Remove(match);
                    }
                }
                lock (readyMatchList)
                {
                    foreach (Match match in readyMatchList)
                    {
                        switch (match.GetMatchState())
                        {
                            case Define.MATCH_STATE.MATCH_READY:
                                // 매칭 대기 상태
                                switch (match.CheckUserWaitState())
                                {
                                    case Define.MATCH_READY_STATE.MATCH_START_BEFORE_WAIT:
                                        Console.WriteLine("매칭 성공 시작 대기중 ...");
                                        break;
                                    case Define.MATCH_READY_STATE.MATCH_START_WAIT:
                                        // 매칭 시작 대기중
                                        Console.WriteLine("매칭 시작 대기중 ...");
                                        break;
                                    case Define.MATCH_READY_STATE.MATCH_START_SUCCESS:
                                        // 매칭 시작 성공
                                        match.SendMatchStartResult(Define.MATCH_READY_STATE.MATCH_START_SUCCESS);
                                        Console.WriteLine("매칭 성공 ...");
                                        break;
                                    case Define.MATCH_READY_STATE.MATCH_START_FAILED:
                                        // 매칭 시작 실패
                                        match.SendMatchStartResult(Define.MATCH_READY_STATE.MATCH_START_FAILED);
                                        Console.WriteLine("매칭 실패 ...");
                                        break;
                                }
                                break;
                            case Define.MATCH_STATE.MATCH_START:
                                // 매칭 진행중 상태
                                break;
                        }
                    }
                }
                Thread.Sleep(Define.MATCH_SYSTEM_DELAY_TIME);
            }
        }
        public void AddPcInsatnce(PcInstance pcInstance)
        {
            lock (addMatchList)
            {
                if (!matchSearchList.Contains(pcInstance))
                {
                    pcInstance.GetPcPvp().SetWaitCount(0);
                    addMatchList.Add(pcInstance);
                }
            }
        }
        /// <summary>
        /// 매칭 중인 유저 리스트에서 제거
        /// </summary>
        /// <param name="pcInstance"></param>
        public void RemovePcInstance(PcInstance pcInstance)
        {
            lock (removeMatchList)
            {
                if (matchSearchList.Contains(pcInstance))
                {
                    removeMatchList.Add(pcInstance);
                    S_MatchingCancel.MatchingCancelS(pcInstance);
                }
            }
        }
        public void RemoveReadyMatchList(Match match)
        {
            lock (readyMatchList)
            {
                if (readyMatchList.Contains(match))
                {
                    removeReadyMatchList.Add(match);
                }
            }
        }
        protected virtual void CheckSearchUser(int searchCount)
        {
            // 매칭 전 정렬
            matchSearchList.OrderBy(x => x.GetPcPvp().GetRating());
            // 실제 로직 처리
            for (int count = 0; count < matchSearchList.Count - (searchCount - 1);)
            {
                //bool matchSuccess = false;
                if (matchSearchList[count + 1].GetPcPvp().GetRating() - matchSearchList[count].GetPcPvp().GetRating() <= Define.DEFAULT_SEARCH_RATING + matchSearchList[count].GetPcPvp().GetWaitCount() * Define.INCREASE_SEARCH_RATING)
                {
                    NormalMatch match = new NormalMatch(this);
                    match.AddPcInstance(matchSearchList[count]);
                    RemovePcInstance(matchSearchList[count]);
                    match.AddPcInstance(matchSearchList[count + 1]);
                    RemovePcInstance(matchSearchList[count + 1]);
                    match.CalculateEloRating();
                    readyMatchList.Add(match);
                    count += searchCount;
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
