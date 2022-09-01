using SimulFactory.Common.Instance;

namespace SimulFactory.Game.Event
{
    public class C_Login
    {
        public static void LoginC(PcInstance pc, Dictionary<byte,object> param)
        {
            // 정보 받은 후 로그인 처리
            pc.UserData.UserId = (string)param[0];
            pc.UserData.UserName = (string)param[1];
            pc.pcPvp.rating = Convert.ToInt32(param[2]);
            pc.pcPvp.winCount = Convert.ToInt32(param[3]);
            pc.pcPvp.defeatCount = Convert.ToInt32(param[4]);

            S_Login.Login_S(pc);
        }
    }
}
