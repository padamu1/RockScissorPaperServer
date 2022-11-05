using SimulFactory.Common.Instance;
using SimulFactory.Core.Sql;
using SimulFactory.Game;
using SimulFactory.Game.Matching;

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
        private Thread normalMatchThread;
        private Thread multiMatchThread;

        public NetworkStarter()
        {

            // DB서버 확인
            SqlController.CheckSqlConnection();

            // 서버 시작 전 준비 작업
            PcListInstance.GetInstance();

            // 매칭 서버 준비
            normalMatchThread = new Thread(NormalMatchSystem.GetInstance().Matching);
            normalMatchThread.IsBackground = true;
            normalMatchThread.Start();

            multiMatchThread = new Thread(MultiMatchSystem.GetInstance().Matching);
            multiMatchThread.IsBackground = true;
            multiMatchThread.Start();

            ConnectionManager = new ConnectionManager(3000);
            // 서버 시작
        }
        public void NetWorkThreadAction()
        {
            // DB 로드
            //LoadData();

            // TCP Listen 시작
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
        public void LoadData()
        {
            ShopItemTable.GetInstance().LoadShopTable();
        }
    }
}
