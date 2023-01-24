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
    public class S_UserBattleResponse
    {
        public static PacketData Data(PcInstance pc, Dictionary<string, int> enemyResult)
        {
            Dictionary<byte, object> param = new Dictionary<byte, object>();
            List<object> list = new List<object>();
            foreach(var result in enemyResult)
            {
                List<object> data = new List<object>()
                {
                    result.Key,
                    result.Value
                };
                list.Add(data);
            }
            param.Add(0, list);
            return new PacketData((byte)Define.EVENT_CODE.UserBattleResponseS, param);
        }
    }
}
