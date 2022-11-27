using System.Collections;
using System.Net;
using System.Net.Sockets;

using SimulFactory.Core;
using SimulFactory.Core.Base;
using SimulFactory.Game;

public enum ApplicationState
{
    Run = 0,
    Stop = 1,
}
public class ApplicationManager : ThreadBase
{
    public ApplicationState State = ApplicationState.Run;
    protected override void ThreadAction()
    {
        base.ThreadAction();

        bool isServerStop = false;
        while (!isServerStop)
        {
            Console.Write("\nServer For ROCK_SCISSOR_PAPER\n");
            Console.WriteLine("서버를 종료하시려면 \"Quit\" \n서비스를 시작하려면 \"Start\" \n서비스를 종료하려면 \"Stop\" 를 입력해주세요.");
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
        State = ApplicationState.Stop;
    }
}
public class Program
{
    public static void Main()
    {
        
        NetworkStarter.GetInstance();
        ApplicationManager applicationManager = new ApplicationManager();
        applicationManager.ThreadStart(1);
        while (true)
        {
            if(applicationManager.State == ApplicationState.Stop)
            {
                break;
            }
        }
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