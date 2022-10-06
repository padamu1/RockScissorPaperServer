using SimulFactory.Common.Thread;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Game.Matching
{
    public class CardMatch : MatchThread
    {
        public CardMatch():base()
        {

        }
        protected override int CheckRoundResult()
        {
            return base.CheckRoundResult();
        }
        protected override void EndRound(int winTeamNo = 0)
        {
            base.EndRound(winTeamNo);
        }
    }
}
