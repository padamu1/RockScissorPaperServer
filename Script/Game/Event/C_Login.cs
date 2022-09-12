using SimulFactory.Common.Instance;

namespace SimulFactory.Game.Event
{
    public class C_Login
    {
        public static void LoginC(PcInstance pc, Dictionary<byte,object> param)
        {
            // 정보 받은 후 로그인 처리
            pc.GetUserData().UserId = (string)param[0];
            pc.GetUserData().UserName = (string)param[1];
            pc.GetPcPvp().Rating = Convert.ToInt32(param[2]);
            pc.GetPcPvp().WinCount = Convert.ToInt32(param[3]);
            pc.GetPcPvp().DefeatCount = Convert.ToInt32(param[4]);
            // 유저 확인
            bool result = true;
            if (PcListInstance.GetInstance().CheckUser(pc))
            {
                PcListInstance.GetInstance().AddInstance(pc);
            }
            else
            {
                result = false;
            }
            S_Login.LoginS(pc,result);
        }
    }
}
