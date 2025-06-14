using SimulFactory.Common.Instance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Game.Event
{
    public class C_UserName
    {
        public static void UserNameC(PcInstance pc, Dictionary<byte,object> param)
        {
            string changeUserName = (string)param[0];
            pc.GetUserData().ChangeUserName(changeUserName);
        }
    }
}
