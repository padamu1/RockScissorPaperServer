using SimulFactory.Common.Bean;
using SimulFactory.Common.Instance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Game.Event
{
    public class S_UserInfo
    {
        public static void UserInfoS(PcInstance pc)
        {
            Dictionary<byte, object> param = new Dictionary<byte, object>();
            param.Add(0, pc.GetUserData().UserName);
            param.Add(1, pc.GetPcPvp().GetRating());
            param.Add(2, pc.GetPcPvp().GetWinCount());
            param.Add(3, pc.GetPcPvp().GetDefeatCount());
            pc.SendPacket((byte)Define.EVENT_CODE.UserInfoS, param);
        }
    }
}
