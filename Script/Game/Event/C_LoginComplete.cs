using SimulFactory.Common.Instance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Game.Event
{
    public class C_LoginComplete
    {
        public static void LoginCompleteC(PcInstance pc, Dictionary<byte, object> param)
        {
            pc.SendUserData();
        }
    }
}
