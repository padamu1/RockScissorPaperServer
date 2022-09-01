using SimulFactory.Common.Instance;

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
        private List<PcInstance> readyMatchList;
        public MatchSystem()
        {
            matchList = new List<PcInstance>();
            addMatchList = new List<PcInstance>();
            removeMatchList = new List<PcInstance>();
            readyMatchList = new List<PcInstance>();
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
                lock (addMatchList)
                {
                    foreach (PcInstance instance in removeMatchList)
                    {
                        matchList.Remove(instance);
                    }
                    removeMatchList.Clear();
                }
                // 매칭 전 정렬
                matchList.OrderBy(x => x.pcPvp.rating);
                // 실제 로직 처리
                for (int count = 0; count < matchList.Count - 1;) 
                {
                    bool matchSuccess = false;
                    if(matchSuccess)
                    {
                        count += 2;
                    }
                    else
                    {
                        count += 1;
                    }
                }
                Thread.Sleep(3000);
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
