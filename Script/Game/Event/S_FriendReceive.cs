using RockScissorPaperServer.PacketSerializer.Model;
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
    public class S_FriendReceive
    {
        public static PacketData Data(Define.RECEIVE_DATA_TYPE type , bool result)
        {
            Dictionary<byte, object> param = new Dictionary<byte, object>();
            param.Add(0, type);
            param.Add(1, result);
            return new PacketData((byte)Define.EVENT_CODE.FriendReceiveS, param);
        }
        public static PacketData Data(Define.RECEIVE_DATA_TYPE type , string otherUserName, bool result)
        {
            Dictionary<byte, object> param = new Dictionary<byte, object>();
            param.Add(0, type);
            param.Add(1, otherUserName);
            param.Add(2, result);
            return new PacketData((byte)Define.EVENT_CODE.FriendReceiveS, param);
        }
    }
}
