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
    public class S_UserName
    {
        public static EventData Data(PcInstance pc, bool result)
        {
            Dictionary<byte, object> dic = new Dictionary<byte, object>();
            dic.Add(0, result);
            if(result)
            {
                dic.Add(1, pc.GetUserData().UserName);
            }
            return new EventData((byte)Define.EVENT_CODE.UserNameS, dic);
        }
    }
}
