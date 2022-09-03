using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Common.Bean
{
    public class PcPvp
    {
        public int Rating { get; set; }
        public int WinCount { get; set; }
        public int DefeatCount { get; set; }
        public int MatchId { get; set; }
        public bool IsWin { get; set; }
        public bool MatchAccept { get; set; }
        public PcPvp()
        {
            IsWin = false;
        }
    }
}
