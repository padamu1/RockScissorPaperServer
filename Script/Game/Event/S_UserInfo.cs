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
    public class S_UserInfo
    {
        public static PacketData Data(PcInstance pc)
        {
            Dictionary<byte, object> param = new Dictionary<byte, object>();
            param.Add(0, pc.GetUserData().UserName);
            param.Add(1, pc.GetPcPvp().GetRating());
            param.Add(2, pc.GetPcPvp().GetWinCount());
            param.Add(3, pc.GetPcPvp().GetDefeatCount());
            return new PacketData((byte)Define.EVENT_CODE.UserInfoS, param);
        }
    }
}
