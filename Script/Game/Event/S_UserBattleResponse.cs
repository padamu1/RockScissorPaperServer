using SimulFactory.Common.Bean;
using SimulFactory.Common.Instance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Game.Event
{
    public class S_UserBattleResponse
    {
        public static void UserBattleResponseS(PcInstance pc, int buttonNo)
        {
            Dictionary<byte, object> param = new Dictionary<byte, object>();
            param.Add(0, buttonNo);
            pc.SendPacket((byte)Define.EVENT_CODE.UserBattleResponseS, param);
        }
    }
}
