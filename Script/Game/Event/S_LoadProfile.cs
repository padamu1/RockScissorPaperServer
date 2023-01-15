using Newtonsoft.Json;
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
        public static EventData Data(string name, string key)
        {
            Dictionary<byte, object> param = new Dictionary<byte, object>();
            param.Add(0, name);
            param.Add(1, key);
            return new EventData((byte)Define.EVENT_CODE.LoadProfileS, param);
        }
    }
}
