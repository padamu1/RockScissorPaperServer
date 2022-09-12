using SimulFactory.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Common.Thread
{
    public class MatchThread : ThreadBase
    {
        public MatchThread():base()
        {
            // 생성자
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
