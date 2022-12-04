using SimulFactory.Common.Bean;
using SimulFactory.Common.Instance;
using SimulFactory.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Game.Event
{
    public class S_MatchingResult
    {
        public static EventData Data(PcInstance pc, bool isWin)
        {
            Dictionary<byte, object> param = new Dictionary<byte, object>();
            if(isWin)
            {
                param.Add(0, true);
                pc.GetPcPvp().UpdateMatchResult(true , 10);
            }
            else
            {
                param.Add(0, false);
                pc.GetPcPvp().UpdateMatchResult(false , -10);
            }
            param.Add(1, pc.GetPcPvp().GetRating());
            param.Add(2, pc.GetPcPvp().GetWinCount());
            param.Add(3, pc.GetPcPvp().GetDefeatCount());

            return new EventData((byte)Define.EVENT_CODE.MatchingResultS, param);
        }
    }
}
