using SimulFactory.Common.Instance;
using SimulFactory.Core.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Game.Event
{
    public class C_LoadProfile
    {
        public static void LoadProfileC(PcInstance pc, Dictionary<byte, object> param)
        {
            string loadUserName = (string)param[0];
            long loadUserNo = UserDBSql.GetUserNoByName(loadUserName);
            pc.SendPacket(S_LoadProfile.Data(loadUserName, UserDBSql.GetUserProfile(loadUserNo)));
        }
    }
}
