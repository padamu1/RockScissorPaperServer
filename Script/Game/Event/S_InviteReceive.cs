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
    public class S_InviteReceive
    {
        public static PacketData Data(Define.RECEIVE_DATA_TYPE type, long code)
        {
            Dictionary<byte, object> param = new Dictionary<byte, object>();
            param.Add(0, (int)type);
            param.Add(1, code);
            return new PacketData((byte)Define.EVENT_CODE.InviteReceiveS, param);
        }
        public static PacketData Data(Define.RECEIVE_DATA_TYPE type, long code, PcInstance pc)
        {
            Dictionary<byte, object> param = new Dictionary<byte, object>();
            param.Add(0, (int)type);
            param.Add(1, code);
            param.Add(2, pc.GetUserData().UserName);
            return new PacketData((byte)Define.EVENT_CODE.InviteReceiveS, param);
        }
    }
}
