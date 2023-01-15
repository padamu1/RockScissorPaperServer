using SimulFactory.Common.Instance;
using SimulFactory.Core.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Game.Event
{
    public class C_SetProfile
    {
        public static void SetProfileC(PcInstance pc, Dictionary<byte, object> param)
        {
            string profileKey = (string)param[0];
            UserDBSql.SetUserProfile(pc.GetUserData().UserNo, profileKey);
        }
    }
}
