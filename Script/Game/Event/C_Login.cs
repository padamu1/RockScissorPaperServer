using SimulFactory.Common.Instance;
using SimulFactory.Core.Util;

namespace SimulFactory.Game.Event
{
    public class C_Login
    {
        public static void LoginC(PcInstance pc, Dictionary<byte,object> param)
        {
            // 정보 받은 후 유저 데이터에 저장
            pc.GetUserData().UserNo = (long)param[0];
            //pc.GetUserData().LoginType = Convert.Int32(param[0]);
            //pc.GetUserData().UserNo = (long)param[1];
            //pc.GetUserData().GoogleToken = (string)param[2];

            // 유저 확인
            LoginUtil.CheckLogin(pc);
        }
    }
}
