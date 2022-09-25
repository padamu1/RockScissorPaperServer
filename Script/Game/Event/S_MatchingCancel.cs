using SimulFactory.Common.Bean;
using SimulFactory.Common.Instance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Game.Event
{
    public class S_MatchingCancel
    {
        public static void MatchingCancelS(PcInstance pc)
        {
            pc.SendPacket((byte)Define.EVENT_CODE.MatchingCancelS, new Dictionary<byte, object>());
        }
    }
}
