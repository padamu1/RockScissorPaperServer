using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockScissorPaperServer.Script.Common.Dto
{
    public class NormalPvpDto : PvpDtoBase
    {
        public NormalPvpDto(int rating) 
        { 
            Rating = rating;
        }
    }
}
