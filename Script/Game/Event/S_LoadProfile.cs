using Newtonsoft.Json;
using RockScissorPaperServer.PacketSerializer.Model;
using SimulFactory.Common.Bean;
using SimulFactory.Common.Dto;
using SimulFactory.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Game.Event
{
    public class S_LoadProfile
    {
        public static PacketData Data(string name, string profileJson)
        {
            Dictionary<byte, object> param = new Dictionary<byte, object>();
            param.Add(0, name);
            param.Add(1, profileJson);
            return new PacketData((byte)Define.EVENT_CODE.LoadProfileS, param);
        }
    }
}
