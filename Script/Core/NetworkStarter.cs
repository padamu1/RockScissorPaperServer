using SimulFactory.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Core
{
    public class NetworkStarter
    {
        public ConnectionManager ConnectionManager { get; set; }
        public NetworkStarter()
        {
            ConnectionManager = new ConnectionManager(3000);
            Thread matchThread = new Thread(MatchSystem.GetInstance().Matching);
            matchThread.IsBackground = true;
            matchThread.Start();
        }
    }
}
