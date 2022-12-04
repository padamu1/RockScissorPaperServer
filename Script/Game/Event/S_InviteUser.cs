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
    public class S_InviteUser
    {
        public static EventData Data(Define.RECEIVE_DATA_TYPE type, long errorCode)
        {
            Dictionary<byte, object> param = new Dictionary<byte, object>();
            param.Add(0, (long)type);
            param.Add(1, errorCode);
            return new EventData((byte)Define.EVENT_CODE.InviteUserS, param);
        }
        public static EventData Data(Define.RECEIVE_DATA_TYPE type, PcInstance otherUser)
        {
            Dictionary<byte, object> param = new Dictionary<byte, object>();
            param.Add(0, (long)type);
            param.Add(1, otherUser.GetUserData().UserName);
            param.Add(2, otherUser.GetUserData().UserNo);
            return new EventData((byte)Define.EVENT_CODE.InviteUserS, param);
        }
    }
}
