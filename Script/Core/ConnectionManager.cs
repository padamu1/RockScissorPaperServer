using System.Net;
using System.Net.Sockets;

namespace SimulFactory.Core
{
    public class ConnectionManager
    {
        private readonly TcpListener tcpListener;
        public Dictionary<TcpClient, WebSocketController> clients;
        public ConnectionManager(int port)
        {
            tcpListener = new TcpListener(IPAddress.Any, port);
            clients = new Dictionary<TcpClient,WebSocketController>();
        }

        private void OnAcceptClient(IAsyncResult ar)
        {
            try
            {
                TcpClient client = tcpListener.EndAcceptTcpClient(ar);
                Console.WriteLine("Client Connected");

                //if(clients.ContainsKey(client))
                //{
                //    clients[client].Dispose();
                //    clients.Remove(client);
                //}

                WebSocketController webSocketController = new WebSocketController(client);
                //다음 클라이언트를 대기
                tcpListener.BeginAcceptTcpClient(OnAcceptClient, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public void StartTcpListen()
        {
            tcpListener.Start();
            //비동기 Listening 시작
            tcpListener.BeginAcceptTcpClient(OnAcceptClient, null);
        }

        public void StopTcpListen()
        {
            try
            {
                tcpListener.Server.Shutdown(SocketShutdown.Both);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                tcpListener.Server.Close();
                tcpListener.Stop();
            }
        }
    }
}
