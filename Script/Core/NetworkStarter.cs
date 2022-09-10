using SimulFactory.Common.Instance;
using SimulFactory.Game;
namespace SimulFactory.Core
{
    public class NetworkStarter
    {
        public ConnectionManager ConnectionManager { get; set; }
        public NetworkStarter()
        {
            // 서버 시작 전 준비 작업
            SqlController.GetInstance();
            PcListInstance.GetInstance();

            // 매칭 서버 준비
            Thread matchThread = new Thread(MatchSystem.GetInstance().Matching);
            matchThread.IsBackground = true;
            matchThread.Start();
            
            // 서버 시작
            ConnectionManager = new ConnectionManager(3000);
        }
    }
}
