using SimulFactory.Common.Instance;
using SimulFactory.Game.Matching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Game.Event
{
    public class C_MatchingCancel
    {
        public static void MatchingCancel(PcInstance pc)
        {
            pc.GetMatchSystem()?.RemovePcInstance(pc);
        }
    }
}
