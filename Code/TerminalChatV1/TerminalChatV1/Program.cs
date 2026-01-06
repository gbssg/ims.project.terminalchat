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
        static int windowWidth = 150;
        static int windowHeight = 40;

        static int tabInfocus = 0;
        static int MaxCharV = 29;
        static int MaxCharH = 121;

        //static List<Tab> tabs = new List<Tab>();

        static void Main()
        {
            SetupLocalUser setupLocalUser = new();
            FileManager fileManager = new();
            ReadWriteData readWriteData = new();


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
            KeyInputThread();
  
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
            string a = Console.ReadLine();
            if (a.ToLower().Equals("j")) 
            {
                // system diagnostics / debug

                Console.WriteLine();
                Console.WriteLine(Process.GetCurrentProcess());
                //Console.WriteLine(tabs);
                //Console.WriteLine(tabs.ElementAt(0));

                Console.Write("Press any key to continue normal Start.");
                string exitDebug = Console.ReadLine();
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
        static void KeyInputThread()
        {
            Thread keyInput = new Thread(() =>
            {
                while (true)
                {

                    var keyInfo = Console.ReadKey(intercept:true);
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.RightArrow:
                            //Console.WriteLine("pgup");
                            SwitchTab(1);
                            break;

                        case ConsoleKey.LeftArrow:
                            //Console.WriteLine("pgdn");
                            SwitchTab(0);
                            break;

                        case ConsoleKey.Enter:
                            SelectTab();
                            break;
                            


                    }


                    // if Key is ... then ... aktion bsp switch tabs.
                }
            });
            keyInput.Start();
        }

        static void Setup()
        {
            //User.SetUser();
            Message message = new Message();
            ServerList.Instance = new();
        }
        static void SelectTab()
        {

            Console.SetCursorPosition(0, 0);
            switch (tabInfocus)
            {
                case 0:
                    Console.WriteLine("1");
                    break;
                case 1:
                    Console.WriteLine("2");
                    break;
                case 2:
                    Console.WriteLine("3");
                    break;
                case 3:
                    Console.WriteLine("4");
                    break;
                case 4:
                    Console.WriteLine("5");
                    break;
            }
        }
        static void SwitchTab(int uod)
        {           //8, 10, 15, 11, 15, 8, 15, 13, 15, 4
            if (uod == 1) tabInfocus++;
            if (uod == 0) tabInfocus--;

            if (tabInfocus > 4) tabInfocus = 0;
            if (tabInfocus < 0) tabInfocus = 4;

            Console.BackgroundColor = ConsoleColor.Black;
            //DrawTabs();

            switch (tabInfocus)
            {
                case 0:
                    Console.SetCursorPosition(8, 29);
                    Console.BackgroundColor = ConsoleColor.Blue;
                    //DrawTab(tabs.ElementAt(0));
                    break;

                case 1:
                    Console.SetCursorPosition(33, 29);
                    Console.BackgroundColor = ConsoleColor.Blue;
                    //DrawTab(tabs.ElementAt(1));
                    break;

                case 2:
                    Console.SetCursorPosition(59, 29);
                    Console.BackgroundColor = ConsoleColor.Blue;
                    //DrawTab(tabs.ElementAt(2));
                    break;

                case 3:
                    Console.SetCursorPosition(82, 29);
                    Console.BackgroundColor = ConsoleColor.Blue;
                    //DrawTab(tabs.ElementAt(3));
                    break;

                case 4:
                    Console.SetCursorPosition(110, 29);
                    Console.BackgroundColor = ConsoleColor.Blue;
                    //DrawTab(tabs.ElementAt(4));
                    break;
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }
        static void resetCursor()
        {
            Console.SetCursorPosition(0, 0);
        }
        static void DrawTextbox()
        {
            int TBposY = MaxCharV - 5;
            
            string TBascii = "╔═══════════════════════════════════════════════════════════════════════════════════════════════════╩══════════════════╣" +
                           "\n║                                                                                                                      ║" +
                           "\n║                                                                                                                      ║" +
                           "\n║                                                                                                                      ║" +
                           "\n╚══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝";

            Console.SetCursorPosition(0, TBposY);
            Console.Write(TBascii);
            resetCursor();
        }
        static void DrawInfobox()
        {
            int IBasciiLength = 22;
            int IBasciiWidth = 21;
            string IBasciiTOP = "╔══════════════════╗";
            string IBasciiCenter = "║                  ║";
            Console.SetCursorPosition(MaxCharH - IBasciiWidth, 0);
            // Info box gets printed
            Console.Write(IBasciiTOP);
            for (int i = 0; i <= IBasciiLength; i++)
            {
                Console.SetCursorPosition(MaxCharH - IBasciiWidth, 1+i);
                Console.Write(IBasciiCenter);
                
            }
            resetCursor();
        }
        /*
        static void DrawTabs()
        {
            // Tabs spaceing is 8, tab1, 15, tab2, 15, tab3, 15, tab4, 15, tab5
            Console.SetCursorPosition(0, 29);
            int x = 8;
            foreach (Tab tab in tabs)
            { 
                Console.SetCursorPosition(x, 29);
                DrawTab(tab);
                x += 15 + tab.size;
            }
        }
        static void DrawTab(Tab tab)
        {
            Console.Write($"{tab.name}");
        }
        */
    }
}
