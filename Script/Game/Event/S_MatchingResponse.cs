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
    public class S_MatchingResponse
    {
        public static PacketData Data(int result)
        {
            Dictionary<byte, object> param = new Dictionary<byte, object>();
            param.Add(0, result);
            return new PacketData((byte)Define.EVENT_CODE.MatchingResponseS, param);
        }
    }
}
