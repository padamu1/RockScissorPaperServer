using SimulFactory.Common.Instance;

namespace SimulFactory.Game.Event
{
    public class C_StartMatching
    {
        public static void StartMatchingC(PcInstance pc)
        {
            MatchSystem.GetInstance().AddPcInsatnce(pc);
        }
    }
}
