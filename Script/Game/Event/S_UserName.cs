﻿using RockScissorPaperServer.PacketSerializer.Model;
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
        public static PacketData Data(PcInstance pc, long code)
        {
            Dictionary<byte, object> dic = new Dictionary<byte, object>();
            dic.Add(0, code);
            if(code == 0)
            {
                dic.Add(1, pc.GetUserData().UserName);
            }
            return new PacketData((byte)Define.EVENT_CODE.UserNameS, dic);
        }
    }
}
