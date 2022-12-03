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
    public class S_UserBattleResponse
    {
        public static EventData Data(PcInstance pc, Dictionary<string, int> enemyResult)
        {
            Dictionary<byte, object> param = new Dictionary<byte, object>();
            param.Add(0, enemyResult);
            return new EventData((byte)Define.EVENT_CODE.UserBattleResponseS, param);
        }
    }
}
