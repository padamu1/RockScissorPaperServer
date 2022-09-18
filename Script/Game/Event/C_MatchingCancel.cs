using SimulFactory.Common.Instance;
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
            MatchSystem.GetInstance().RemovePcInstance(pc);
        }
    }
}
