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
    public class S_MatchingCancel
    {
        public static EventData Data()
        {
            return new EventData((byte)Define.EVENT_CODE.MatchingCancelS, new Dictionary<byte, object>());
        }
    }
}
