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
    public class S_MatchingSuccess
    {
        public static PacketData Data(List<object> users)
        {
            Dictionary<byte, object> param = new Dictionary<byte, object>();
            param.Add(0, users);
            Console.WriteLine("매칭 성공 메시지 보냄");
            return new PacketData((byte)Define.EVENT_CODE.MatchingSuccessS, param);
        }
    }
}
