using SimulFactory.Common.Instance;
using SimulFactory.Game;
namespace SimulFactory.Core
{
    public class NetworkStarter
    {
        public ConnectionManager ConnectionManager { get; set; }
        public NetworkStarter()
        {
            ConnectionManager = new ConnectionManager(3000);
            PcListInstance.GetInstance();
            Thread matchThread = new Thread(MatchSystem.GetInstance().Matching);
            matchThread.IsBackground = true;
            matchThread.Start();
        }
    }
}
