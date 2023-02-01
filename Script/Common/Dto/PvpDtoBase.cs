using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockScissorPaperServer.Script.Common.Dto
{
    public class PvpDtoBase
    {
        public int Rating {get;set;}
        public int WinCount {get;set;}
        public int DefeatCount { get; set; }
    }
}
