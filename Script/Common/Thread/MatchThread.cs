using SimulFactory.Common.Bean;
using SimulFactory.Common.Instance;
using SimulFactory.Core.Base;
using SimulFactory.Game.Matching;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Common.Thread
{
    using System.Threading;
    public class MatchThread : ThreadBase
    {
        private Match match;
        private bool threadRun;
        public MatchThread(Match match):base()
        {
            this.match = match;
        }
        protected override void ThreadAction()
        {
            threadRun = true;
            while (threadRun)
            {
                match.MatchRoundChecker();
                Thread.Sleep(delayTime);
            }
        }
        public override void Dispose()
        {
            threadRun = false;
            base.Dispose();
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
