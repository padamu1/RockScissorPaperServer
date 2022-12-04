using Newtonsoft.Json;
using SimulFactory.Common.Bean;
using SimulFactory.Common.Dto;
using SimulFactory.Common.Instance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Game.Event
{
    public class S_FriendRequest
    {
        public static void FriendRequestS(PcInstance pc, Define.FRIEND_RECEIVE_DATA_TYPE type, bool result)
        {
            Dictionary<byte, object> param = new Dictionary<byte, object>();
            param.Add(0, ((int)type));
            param.Add(1, result);
            pc.SendPacket((byte)Define.EVENT_CODE.FriendRequestS, param);
        }
        public static void FriendRequestS(PcInstance pc, Define.FRIEND_RECEIVE_DATA_TYPE type, List<FriendRequestDto> friendRequestDtoList)
        {
            if (friendRequestDtoList.Count == 0)
            {
                return;
            }
            Dictionary<byte, object> param = new Dictionary<byte, object>();
            param.Add(0, ((int)type));
            param.Add(1, JsonConvert.SerializeObject(friendRequestDtoList));
            pc.SendPacket((byte)Define.EVENT_CODE.FriendRequestS, param);
        }
    }
}
