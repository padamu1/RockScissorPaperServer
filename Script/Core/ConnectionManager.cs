using System.Net;
using System.Net.Sockets;

namespace SimulFactory.Core
{
    public class ConnectionManager
    {
        private readonly TcpListener tcpListener;
        public Dictionary<TcpClient, WebSocketController> clients;
        public ConnectionManager(string address, int port)
        {
            tcpListener = new TcpListener(IPAddress.Parse(address), port);
            clients = new Dictionary<TcpClient,WebSocketController>();
            tcpListener.Start();
            //비동기 Listening 시작
            tcpListener.BeginAcceptTcpClient(OnAcceptClient, null);
        }

        private void OnAcceptClient(IAsyncResult ar)
        {
            TcpClient client = tcpListener.EndAcceptTcpClient(ar);
            Console.WriteLine("Client Connected");

            if(clients.ContainsKey(client))
            {
                clients[client].Dispose();
                clients.Remove(client);
            }

            WebSocketController webSocketController = new WebSocketController(client);
            clients.Add(client, webSocketController);
            //다음 클라이언트를 대기
            tcpListener.BeginAcceptTcpClient(OnAcceptClient, null);
        }
    }
}
