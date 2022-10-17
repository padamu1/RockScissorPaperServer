using SimulFactory.Common.Instance;
using SimulFactory.Game;
namespace SimulFactory.Core
{
    public class NetworkStarter
    {
        static readonly Lazy<NetworkStarter> instanceHolder = new Lazy<NetworkStarter>(() => new NetworkStarter());
        public static NetworkStarter GetInstance()
        {
            return instanceHolder.Value;
        }
        public ConnectionManager ConnectionManager { get; set; }
        private bool isThreadRun;
        private Thread networkThread;
        private Thread matchThread;

        public NetworkStarter()
        {

            // DB서버 확인
            SqlController.CheckSqlConnection();

            // 서버 시작 전 준비 작업
            PcListInstance.GetInstance();

            // 매칭 서버 준비
            matchThread = new Thread(MatchSystem.GetInstance().Matching);
            matchThread.IsBackground = true;
            matchThread.Start();

            ConnectionManager = new ConnectionManager(3000);
            // 서버 시작
        }
        public void NetWorkThreadAction()
        {
            ConnectionManager.StartTcpListen();
            while (isThreadRun)
            {

            }
            ConnectionManager.StopTcpListen();
        }

        public void ServiceStop()
        {
            isThreadRun = false;
            Console.WriteLine("service stopped");
        }
        public void ServiceStart()
        {
            isThreadRun = true;
            networkThread = new Thread(NetWorkThreadAction);
            networkThread.IsBackground = true;
            networkThread.Start();
        }
    }
}
