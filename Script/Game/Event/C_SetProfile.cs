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
            string faceColor = (string)param[0];
            string eyeForm = (string)param[1];
            string eyeColor = (string)param[2];
            string mouthForm = (string)param[3];
            UserDBSql.SetUserProfile(pc.GetUserData().UserNo, faceColor, eyeForm, eyeColor, mouthForm);
        }
    }
}
