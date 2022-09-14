using SimulFactory.Common.Bean;
using SimulFactory.Common.Instance;
using SimulFactory.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Game.Event
{
    public class S_Login
    {
        public static void LoginS(PcInstance pc,bool result)
        {
            Dictionary<byte, object> dic = new Dictionary<byte, object>();
            dic.Add(0, result);
            if(result)
            {
                dic.Add(1, pc.GetUserData().UserNo);
            }
            pc.SendPacket((byte)Define.EVENT_CODE.LoginS, dic);
        }
    }
}
