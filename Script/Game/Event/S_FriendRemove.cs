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
    public class S_FriendRemove
    {
        public static PacketData Data(Define.RECEIVE_DATA_TYPE type, bool result, string friendName)
        {
            Dictionary<byte, object> param = new Dictionary<byte, object>();
            param.Add(0, (int)type);
            param.Add(1, result);
            param.Add(2, friendName);
            return new PacketData((byte)Define.EVENT_CODE.FriendRemoveS, param);
        }
        public static PacketData Data(Define.RECEIVE_DATA_TYPE type, string userName)
        {
            Dictionary<byte, object> param = new Dictionary<byte, object>();
            param.Add(0, (int)type);
            param.Add(1, userName);
            return new PacketData((byte)Define.EVENT_CODE.FriendRemoveS, param);
        }
    }
}
