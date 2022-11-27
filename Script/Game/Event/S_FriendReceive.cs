using SimulFactory.Common.Bean;
using SimulFactory.Common.Instance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Game.Event
{
    public class S_FriendReceive
    {
        public static void FriendReceiveS(PcInstance pc, Define.FRIEND_RECEIVE_DATA_TYPE type , bool result)
        {
            Dictionary<byte, object> param = new Dictionary<byte, object>();
            param.Add(0, type);
            param.Add(1, result);
            pc.SendPacket((byte)Define.EVENT_CODE.FriendReceiveS, param);
        }
    }
}
