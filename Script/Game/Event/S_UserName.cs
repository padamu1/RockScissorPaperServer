using SimulFactory.Common.Bean;
using SimulFactory.Common.Instance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Game.Event
{
    public class S_UserName
    {
        public static void UserNameS(PcInstance pc, bool result)
        {
            Dictionary<byte, object> dic = new Dictionary<byte, object>();
            dic.Add(0, result);
            if(result)
            {
                dic.Add(1, pc.GetUserData().UserName);
            }
            pc.SendPacket((byte)Define.EVENT_CODE.UserNameS, dic);
        }
    }
}
