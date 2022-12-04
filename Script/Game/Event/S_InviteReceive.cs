using SimulFactory.Common.Bean;
using SimulFactory.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Game.Event
{
    public class S_InviteReceive
    {
        public static EventData Data(Define.RECEIVE_DATA_TYPE type, long code)
        {
            Dictionary<byte, object> param = new Dictionary<byte, object>();
            param.Add(0, (int)type);
            param.Add(0, code);
            return new EventData((byte)Define.EVENT_CODE.InviteReceiveS, param);
        }
    }
}
