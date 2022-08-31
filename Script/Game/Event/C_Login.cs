using SimulFactory.Common.Instance;

namespace SimulFactory.Game.Event
{
    public class C_Login
    {
        public static void LoginC(Dictionary<byte,object> param)
        {
            // 정보 받은 후 로그인 처리
            UserData.GetInstance().UserId = (string)param[0];
            UserData.GetInstance().UserName = (string)param[1];
            S_Login.Login_S();
        }
    }
}
