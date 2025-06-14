﻿using Newtonsoft.Json;
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
    public class S_FriendRequest
    {
        public static PacketData Data(PcInstance pc, Define.RECEIVE_DATA_TYPE type, bool result)
        {
            Dictionary<byte, object> param = new Dictionary<byte, object>();
            param.Add(0, ((int)type));
            param.Add(1, result);
            return new PacketData((byte)Define.EVENT_CODE.FriendRequestS, param);
        }
        public static PacketData Data(PcInstance pc, Define.RECEIVE_DATA_TYPE type, bool isList, List<FriendRequestDto> friendRequestDtoList)
        {
            if (friendRequestDtoList.Count == 0)
            {
                return null;
            }
            Dictionary<byte, object> param = new Dictionary<byte, object>();
            param.Add(0, ((int)type));
            param.Add(1, (bool)isList);
            param.Add(2, JsonConvert.SerializeObject(friendRequestDtoList));
            return new PacketData((byte)Define.EVENT_CODE.FriendRequestS, param);
        }
    }
}
