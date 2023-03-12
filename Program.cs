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
        Console.WriteLine("Quit this program : \"Quit\" \n Service Start : \"Start\" \n Service Stop : \"Stop\"");
        switch (Console.ReadLine())
        {
            case "Stop":
                Console.WriteLine("Service Stop");
                NetworkStarter.GetInstance().ServiceStop();
                Start();
                break;
            case "Start":
                Console.WriteLine("Service Start");
                NetworkStarter.GetInstance().ServiceStart();
                Start();
                break;
            case "Quit":
                Console.WriteLine("esc : program end");
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
        Console.WriteLine("Service will Started");
        NetworkStarter.GetInstance().ServiceStart();
        ApplicationManager.Start();
    }
}