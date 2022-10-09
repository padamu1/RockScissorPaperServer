using SimulFactory.Common.Instance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Game.Event
{
    public class C_UserBattleButtonClicked
    {
        public static void UserBattleButtonClickedC(PcInstance pc, Dictionary<byte,object> param)
        {
            int buttonNo = Convert.ToInt32(param[0]);
            // 버튼 클릭
            pc.GetPcPvp().GetMatch().SetDmg(pc.GetPcPvp().GetTeamNo(),buttonNo);
        }
    }
}
