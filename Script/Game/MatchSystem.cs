using SimulFactory.Common.Bean;
using SimulFactory.Common.Instance;
using SimulFactory.Game.Matching;
namespace SimulFactory.Game
{
    public class MatchSystem
    {
        static readonly Lazy<MatchSystem> instanceHolder = new Lazy<MatchSystem>(() => new MatchSystem());
        public static MatchSystem GetInstance()
        {
            return instanceHolder.Value;
        }
        private List<PcInstance> matchSearchList;
        private List<PcInstance> addMatchList;
        private List<PcInstance> removeMatchList;
        private List<Match> removeReadyMatchList;
        private List<Match> readyMatchList;
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
                    }
                    removeMatchList.Clear();
                }
                lock (matchSearchList)
                {
                    // 매칭 전 정렬
                    matchSearchList.OrderBy(x => x.GetPcPvp().GetRating());
                    // 실제 로직 처리
                    for (int count = 0; count < matchSearchList.Count - 1;)
                    {
                        //bool matchSuccess = false;
                        if (count + 1 <= matchSearchList.Count - 1)
                        {
                            Match match = new Match();
                            match.AddPcInstance(matchSearchList[count]);
                            RemovePcInstance(matchSearchList[count]);
                            match.AddPcInstance(matchSearchList[count + 1]);
                            RemovePcInstance(matchSearchList[count + 1]);
                            readyMatchList.Add(match);
                            count += 2;
                        }
                        else
                        {
                            count += 1;
                        }
                    }
                }
                lock (removeReadyMatchList)
                {
                    foreach(Match match in removeReadyMatchList)
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
                            case Define.MATCH_STATE.MATCH_READY: // 매칭 대기 상태
                                switch (match.CheckUserWaitState())
                                {
                                    case Define.MATCH_READY_STATE.MATCH_START_BEFORE_WAIT:
                                        Console.WriteLine("매칭 성공 시작 대기중 ...");
                                        break;
                                    case Define.MATCH_READY_STATE.MATCH_START_WAIT:   // 매칭 시작 대기중
                                        Console.WriteLine("매칭 시작 대기중 ...");
                                        break;
                                    case Define.MATCH_READY_STATE.MATCH_START_SUCCESS:// 매칭 시작 성공
                                        match.SendMatchStartResult(Define.MATCH_READY_STATE.MATCH_START_SUCCESS);
                                        Console.WriteLine("매칭 성공 ...");
                                        break;
                                    case Define.MATCH_READY_STATE.MATCH_START_FAILED: // 매칭 시작 실패
                                        match.SendMatchStartResult(Define.MATCH_READY_STATE.MATCH_START_FAILED);
                                        Console.WriteLine("매칭 실패 ...");
                                        break;
                                }
                                break;
                            case Define.MATCH_STATE.MATCH_START: // 매칭 진행중 상태
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
                    addMatchList.Add(pcInstance);
                }
            }
        }
        public void RemovePcInstance(PcInstance pcInstance)
        {
            lock (removeMatchList)
            {
                if (matchSearchList.Contains(pcInstance))
                {
                    removeMatchList.Add(pcInstance);
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
    }
}
