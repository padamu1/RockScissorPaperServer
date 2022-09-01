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
            S_Login.Login_S();
        }
    }
}
