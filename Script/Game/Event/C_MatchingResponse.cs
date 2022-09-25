using SimulFactory.Common.Instance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Game.Event
{
    public class C_MatchingResponse
    {
        public static void MatchingResponseC(PcInstance pc, Dictionary<byte, object> param)
        {
            pc.GetPcPvp().SetMatchAccept((bool)param[0]);
        }
    }
}
