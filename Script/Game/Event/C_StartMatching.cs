using SimulFactory.Common.Bean;
using SimulFactory.Common.Instance;
using SimulFactory.Game.Matching;

namespace SimulFactory.Game.Event
{
    public class C_StartMatching
    {
        public static void StartMatchingC(PcInstance pc, Dictionary<byte,object> param)
        {
            switch((Define.MATCH_TYPE)(int)param[0])
            {
                case Define.MATCH_TYPE.Normal:
                    NormalMatchSystem.GetInstance().AddPcInsatnce(pc);
                    break;
                case Define.MATCH_TYPE.Multi:
                    MultiMatchSystem.GetInstance().AddPcInsatnce(pc);
                    break;
                case Define.MATCH_TYPE.Card:
                    break;
            }
            
        }
    }
}
