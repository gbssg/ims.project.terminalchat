using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using TerminalChatClient;

namespace TerminalChatV1
{


    //Placeholder Message Class

    internal class Program
    {

        static string? serverIp;
        static int port;

        //static List<Tab> tabs = new List<Tab>();

        static void Main()
        {
            SetupLocalUser setupLocalUser = new();
            FileManager fileManager = new();
            ReadWriteData readWriteData = new();
            ClientTcpConnection clientTcpConnection = new();


            SetupUser slu = new();
            slu.Name = "test";

            //Debug();
            //readWriteData.OverrideSetupUserList(slu);
            Console.WriteLine(File.ReadAllText(fileManager.setupUserPath));
            //readWriteData.ReadSetupUserlist();
            Console.ReadKey();
            fileManager.SetupAppDir();
            setupLocalUser.UserSetupPrompt();

            Setup();
            clientTcpConnection.ListenToServer();

            //KeyInputThread();
  
        }
        static void Connect()
        {
            Console.Write("Gib die Server-IP ein: ");
            serverIp = Console.ReadLine() ?? "127.0.0.1"; // Standard ist localhost

            port = 5000;
        }
        public void Debug()
        {
            Console.Write("Enter Debug?(j/n)");
            string? a = Console.ReadLine();
            if (a.ToLower().Equals("j")) 
            {
                // system diagnostics / debug

                Console.WriteLine();
                Console.WriteLine(Process.GetCurrentProcess());
                //Console.WriteLine(tabs);
                //Console.WriteLine(tabs.ElementAt(0));

                Console.Write("Press any key to continue normal Start.");
                string? exitDebug = Console.ReadLine();
            }
            Console.Clear();
        }

        static void Chatfunction()
        {
            try
            {
                using (TcpClient client = new TcpClient(serverIp, port))
                {
                    Console.WriteLine($"Verbunden mit {serverIp}:{port}");
                    NetworkStream stream = client.GetStream();

                    // Empfängt Nachrichten vom Server
                    Thread receiveThread = new Thread(() =>
                    {
                        byte[] buffer = new byte[1024];
                        while (true)
                        {
                            int bytesRead = stream.Read(buffer, 0, buffer.Length);

                            if (bytesRead == 0) break;

                            string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                            Console.WriteLine("\nClient: " + receivedMessage);
                            Console.Write("Du: ");
                        }
                    });
                    receiveThread.Start();


                    while (true)
                    {
                        Console.Write("Du: ");
                        string message = Console.ReadLine() ?? "";

                        byte[] buffer = Encoding.UTF8.GetBytes(message);
                        stream.Write(buffer, 0, buffer.Length);

                        if (message.ToLower() == "exit") break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Fehler: " + e.Message);
                Console.ReadLine();
            }
        }
        //static void KeyInputThread()
        //{
        //    Thread keyInput = new Thread(() =>
        //    {
        //        while (true)
        //        {

        //            var keyInfo = Console.ReadKey(intercept:true);
        //            switch (keyInfo.Key)
        //            {
        //                case ConsoleKey.RightArrow:
        //                    //Console.WriteLine("pgup");
        //                    SwitchTab(1);
        //                    break;

        //                case ConsoleKey.LeftArrow:
        //                    //Console.WriteLine("pgdn");
        //                    SwitchTab(0);
        //                    break;

        //                case ConsoleKey.Enter:
        //                    SelectTab();
        //                    break;
                            


        //            }


        //            // if Key is ... then ... aktion bsp switch tabs.
        //        }
        //    });
        //    keyInput.Start();
        //}

        static void Setup()
        {
            //User.SetUser();
            Message message = new Message();
            ServerList.Instance = new();
        }
        
    }
}
