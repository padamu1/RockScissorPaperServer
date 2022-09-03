using SimulFactory.Common.Instance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Game.Match
{
    public class Match
    {
        private List<PcInstance> pcList; // 현재 매칭된 유저가 들어가 있는 객체
        public Match()
        {
            pcList = new List<PcInstance>();
        }
    }
}
