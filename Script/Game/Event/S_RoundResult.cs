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
    public class S_RoundResult
    {
        public static PacketData Data(List<object> winUserNames)
        {
            Dictionary<byte, object> param = new Dictionary<byte, object>();
            //param.Add(0, (int)winUserResult);
            //param.Add(1, pc.GetPcPvp().GetCardNo());
            // 2번에 카드 리스트 넣어서 보냄
            param.Add(0, winUserNames);
            return new PacketData((byte)Define.EVENT_CODE.RoundResultS, param);
        }
    }
}
