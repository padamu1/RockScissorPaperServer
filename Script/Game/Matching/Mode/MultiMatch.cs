using SimulFactory.Common.Bean;
using SimulFactory.Common.Thread;
using SimulFactory.Core.Util;
using SimulFactory.Game.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Game.Matching.Mode
{
    public class MultiMatch : MatchThread
    {
        public MultiMatch(MatchSystem matchSystem) : base(matchSystem)
        {

        }
    }
}
