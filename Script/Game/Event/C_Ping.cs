using SimulFactory.Common.Instance;
using SimulFactory.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Game.Event
{
    public class C_Ping
    {
        public static void PingC(PcInstance pc)
        {
            pc.GetUserData().PingTime = Util.GetServerTime();
        }
    }
}
