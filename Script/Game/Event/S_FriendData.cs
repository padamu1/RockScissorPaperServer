using Newtonsoft.Json;
using RockScissorPaperServer.PacketSerializer.Model;
using SimulFactory.Common.Bean;
using SimulFactory.Common.Dto;
using SimulFactory.Common.Instance;
using SimulFactory.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Game.Event
{
    public class S_FriendData
    {
        public static PacketData Data(List<FriendDto> friendDtoList)
        {
            if(friendDtoList.Count == 0)
            {
                return null;
            }
            Dictionary<byte, object> param = new Dictionary<byte, object>();
            param.Add(0, JsonConvert.SerializeObject(friendDtoList));
            return new PacketData((byte)Define.EVENT_CODE.FriendDataS, param);
        }
    }
}
