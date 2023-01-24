using RockScissorPaperServer.PacketSerializer.Model;
using SimulFactory.Common.Bean;
using SimulFactory.Common.Instance;
using SimulFactory.Core;
using SimulFactory.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Game.Event
{
    public class S_Login
    {
        public static PacketData Data(PcInstance pc,bool result)
        {
            Dictionary<byte, object> dic = new Dictionary<byte, object>(); // 주석
            dic.Add(0, result);
            if(result)
            {
                dic.Add(1, pc.GetUserData().UserNo);
                dic.Add(2, pc.GetUserData().UserName);
            }
            return new PacketData((byte)Define.EVENT_CODE.LoginS, dic);
        }
    }
}
