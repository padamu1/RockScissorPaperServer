using SimulFactory.Common.Instance;
using SimulFactory.Core.Base;
using SimulFactory.Game.Matching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Common.Thread
{
    public class MatchThread : ThreadBase
    {
        private Match match;
        public MatchThread(Match match):base()
        {
            this.match = match;
        }
        protected override void ThreadAction()
        {
            base.ThreadAction();
        }
        public override ThreadBase Clone()
        {
            return this;
        }
        public override string GetThreadName()
        {
            return "MatchTread";
        }
    }
}
