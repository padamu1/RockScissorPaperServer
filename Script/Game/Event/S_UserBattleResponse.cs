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
        public static void UserBattleResponseS(PcInstance pc, Dictionary<string, int> enemyResult)
        {
            Dictionary<byte, object> param = new Dictionary<byte, object>();
            param.Add(0, enemyResult);
            pc.SendPacket((byte)Define.EVENT_CODE.UserBattleResponseS, param);
        }
    }
}
