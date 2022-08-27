using System.Collections;
using System.Net;
using System.Net.Sockets;

using SimulFactory.Core;
public class Program
{
    public static void Main()
    {
        ConnectionManager cm = new ConnectionManager("127.0.0.1",80);
        bool isServerStop = false;
        while (!isServerStop)
        {
            Console.Write("\n\n\n\nServer For RockScissorPaper\n");
            Console.WriteLine("서버를 종료하시려면 \"quit\"를 입력해주세요.");
            switch (Console.ReadLine())
            {
                case "quit":
                    isServerStop = true;
                    break;
                default:
                    break;
            }
        }
    }
}