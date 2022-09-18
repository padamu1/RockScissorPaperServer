using SimulFactory.Common.Bean;
using SimulFactory.Common.Instance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Game.Event
{
    public class S_StartMatching
    {
        public static void StartMatchingS(PcInstance pc)
        {
            Dictionary<byte, object> param = new Dictionary<byte, object>();
            param.Add(0, true);
            pc.SendPacket((byte)Define.EVENT_CODE.StartMatchingS, param);
        }
    }
}
