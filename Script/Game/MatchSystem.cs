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
        private List<PcInstance> matchList;
        private List<PcInstance> addMatchList;
        private List<PcInstance> removeMatchList;
        private List<Match> readyMatchList;
        public MatchSystem()
        {
            matchList = new List<PcInstance>();
            addMatchList = new List<PcInstance>();
            removeMatchList = new List<PcInstance>();
            readyMatchList = new List<Match>();
        }
        public void Matching()
        {
            while(true)
            {
                lock (addMatchList)
                {
                    foreach(PcInstance instance in addMatchList)
                    {
                        matchList.Add(instance);
                    }
                    addMatchList.Clear();
                }
                lock (removeMatchList)
                {
                    foreach (PcInstance instance in removeMatchList)
                    {
                        matchList.Remove(instance);
                    }
                    removeMatchList.Clear();
                }
                lock(matchList)
                {
                    // 매칭 전 정렬
                    matchList.OrderBy(x => x.pcPvp.Rating);
                    // 실제 로직 처리
                    for (int count = 0; count < matchList.Count - 1;)
                    {
                        bool matchSuccess = false;
                        if (matchSuccess)
                        {
                            Match match = new Match();
                            match.AddPcInstance(matchList[count]);
                            match.AddPcInstance(matchList[count + 1]);
                            match.SendMatchSuccess();
                            count += 2;
                        }
                        else
                        {
                            count += 1;
                        }
                    }
                }
                lock(readyMatchList)
                {
                    foreach (Match match in readyMatchList)
                    {
                        switch (match.CheckUserWaitState())
                        {
                            case Define.MATCH_STATE.MATCH_START_WAIT:   // 매칭 시작 대기중
                                break;
                            case Define.MATCH_STATE.MATCH_START_SUCCESS:// 매칭 시작 성공
                                match.SendMatchStartResult(Define.MATCH_STATE.MATCH_START_SUCCESS);
                                break;
                            case Define.MATCH_STATE.MATCH_START_FAILED: // 매칭 시작 실패
                                match.SendMatchStartResult(Define.MATCH_STATE.MATCH_START_FAILED);
                                break;
                        }
                    }
                }
                Thread.Sleep(Define.MATCH_SYSTEM_DELAY_TIME);
            }
        }
        public void AddPcInsatnce(PcInstance pcInstance)
        {
            lock(addMatchList)
            {
                addMatchList.Add(pcInstance);
            }
        }
        public void RemovePcInstance(PcInstance pcInstance)
        {
            lock(removeMatchList)
            {
                removeMatchList.Add(pcInstance);
            }
        }
    }
}
