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
        public static EventData Data(PcInstance pc, Define.RECEIVE_DATA_TYPE type, bool result)
        {
            Dictionary<byte, object> param = new Dictionary<byte, object>();
            param.Add(0, ((int)type));
            param.Add(1, result);
            return new EventData((byte)Define.EVENT_CODE.FriendRequestS, param);
        }
        public static EventData Data(PcInstance pc, Define.RECEIVE_DATA_TYPE type, List<FriendRequestDto> friendRequestDtoList)
        {
            Dictionary<byte, object> param = new Dictionary<byte, object>();
            param.Add(0, ((int)type));
            param.Add(1, friendRequestDtoList);
            return new EventData((byte)Define.EVENT_CODE.FriendRequestS, param);
        }
    }
}
