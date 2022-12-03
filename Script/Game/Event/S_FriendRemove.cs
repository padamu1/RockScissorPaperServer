using SimulFactory.Common.Bean;
using SimulFactory.Common.Instance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Game.Event
{
    public class S_FriendRemove
    {
        public static void FriendRemoveS(PcInstance pc, Define.FRIEND_RECEIVE_DATA_TYPE type, bool result)
        {
            Dictionary<byte, object> param = new Dictionary<byte, object>();
            param.Add(0, (int)type);
            param.Add(1, result);
            pc.SendPacket((int)Define.EVENT_CODE.FriendRemoveS, param);
        }
        public static void FriendRemoveS(PcInstance pc, Define.FRIEND_RECEIVE_DATA_TYPE type, string userName)
        {
            Dictionary<byte, object> param = new Dictionary<byte, object>();
            param.Add(0, (int)type);
            param.Add(1, userName);
            pc.SendPacket((int)Define.EVENT_CODE.FriendRemoveS, param);
        }
    }
}
