using System.Collections;
using System.Net;
using System.Net.Sockets;

using SimulFactory.Core;
using SimulFactory.Core.Base;
using SimulFactory.Game;

public class ApplicationManager
{
    public static void Start()
    {
        Console.WriteLine("서버를 종료하시려면 \"Quit\" \n서비스를 시작하려면 \"Start\" \n서비스를 종료하려면 \"Stop\" 를 입력해주세요.");
        switch (Console.ReadLine())
        {
            case "Stop":
                Console.WriteLine("서비스가 종료됩니다.");
                NetworkStarter.GetInstance().ServiceStop();
                Start();
                break;
            case "Start":
                Console.WriteLine("서비스가 시작됩니다.");
                NetworkStarter.GetInstance().ServiceStart();
                Start();
                break;
            case "Quit":
                Console.WriteLine("esc : 프로그램이 종료됩니다.");
                break;
            default:
                break;
        }
    }
}
public class Program
{
    public static void Main()
    {
        
        NetworkStarter.GetInstance();
        ApplicationManager.Start();
        /*
        bool isServerStop = false;
        while (!isServerStop)
        {
            Console.Write("\nServer For ROCK_SCISSOR_PAPER\n");
            Console.WriteLine("서버를 종료하시려면 \"quit\"를 입력해주세요.");
            switch (Console.ReadLine())
            {
                case "Stop":
                    Console.WriteLine("서비스가 종료됩니다.");
                    NetworkStarter.GetInstance().ServiceStop();
                    break;
                case "Start":
                    Console.WriteLine("서비스가 시작됩니다.");
                    NetworkStarter.GetInstance().ServiceStart();
                    break;
                case "Quit":
                    Console.WriteLine("프로그램이 종료됩니다.");
                    isServerStop = true;
                    break;
                default:
                    break;
            }
        }
        */
    }
}