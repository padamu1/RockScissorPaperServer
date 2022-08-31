using SimulFactory.Common.Instance;
using SimulFactory.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Game.Event
{
    public class S_Login
    {
        public static void Login_S()
        {
            DataFormat dataFormat = new DataFormat();
            dataFormat.evCode = 0;
            PcInstance.GetInstance().GetWebSocketController().SendData(ByteUtillity.ObjectToByte(dataFormat));
        }
    }
}
