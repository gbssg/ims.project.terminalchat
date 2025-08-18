using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using TerminalChatClient;

namespace TerminalChatV1
{


    //Placeholder Message Class
    public class Message
    {
        public int id;
        public string? sender;
        public string? reciever;
        public DateTime timestamp;
        public string? body;
    }
    internal class Program
    {
        static string? serverIp;
        static int port;

        static int tabInfocus;
        static int MaxCharV = 29;
        static int MaxCharH = 121;

        static List<Box> boxes = new List<Box>();
        static List<Tab> tabs = new List<Tab>();

        static void Main()
        {
            // Create Boxes and Boxlist
            Box textBox = new Box();
            boxes.Add(textBox);

            Box infoBox = new Box();
            boxes.Add(infoBox);
            
            Box messageBox = new Box();
            boxes.Add(messageBox);


            // Create Tabs and Tablist
            Tab serverList = new Tab();
            serverList.name = "Serverlist";
            serverList.size = 10;
            tabs.Add(serverList);

            Tab channelList = new Tab();
            channelList.name = "Channellist";
            channelList.size = 11;
            tabs.Add(channelList);

            Tab userlist = new Tab();
            userlist.name = "Userlist";
            userlist.size = 8;
            tabs.Add(userlist);

            Tab notifications = new Tab();
            notifications.name = "Notifications";
            notifications.size = 13;
            tabs.Add(notifications);

            Tab exit = new Tab();
            exit.name = "exit";
            exit.size = 4;
            tabs.Add(exit);


            Debug();
            Setup();
            KeyInputThread();




            // TODO: 
            //    - make Tabs functional
            //    - seperate function into classes, cleanup main function
            //

            //Connect();
            //KeyInputThread();
            //Chatfunction();
            //Console.ReadLine();

        }
        static void Connect()
        {
            Console.Write("Gib die Server-IP ein: ");
            serverIp = Console.ReadLine() ?? "127.0.0.1"; // Standard ist localhost

            port = 5000;
        }
        static void Debug()
        {
            Console.Write("Enter Debug?(j/n)");
            string a = Console.ReadLine();
            if (a.ToLower().Equals("j")) 
            {
                // system diagnostics / debug

                Console.WriteLine(Process.GetCurrentProcess());
                Console.WriteLine(boxes);
                Console.WriteLine(tabs);
                Console.WriteLine(tabs.ElementAt(0));

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

                    }


                    // if Key is ... then ... aktion bsp swithch tabs.
                }
            });
            keyInput.Start();
        }
        static void SwitchTab(int uod)
        {           //8, 10, 15, 11, 15, 8, 15, 13, 15, 4
            if(uod == 1) tabInfocus++;
            if(uod == 0) tabInfocus--;

            if (tabInfocus > 4) tabInfocus = 0;
            if (tabInfocus < 0) tabInfocus = 4;

            Console.BackgroundColor = ConsoleColor.Black;
            DrawTabs();

            switch (tabInfocus)
            {
                case 0:
                    Console.SetCursorPosition(8, 29);
                    Console.BackgroundColor = ConsoleColor.Blue;
                    DrawTab(tabs.ElementAt(0));
                    break;

                case 1:
                    Console.SetCursorPosition(33, 29);
                    Console.BackgroundColor = ConsoleColor.Blue;
                    DrawTab(tabs.ElementAt(1));
                    break;

                case 2:
                    Console.SetCursorPosition(59, 29);
                    Console.BackgroundColor = ConsoleColor.Blue;
                    DrawTab(tabs.ElementAt(2));
                    break;

                case 3:
                    Console.SetCursorPosition(82, 29);
                    Console.BackgroundColor = ConsoleColor.Blue;
                    DrawTab(tabs.ElementAt(3));
                    break;

                case 4:
                    Console.SetCursorPosition(110, 29);
                    Console.BackgroundColor = ConsoleColor.Blue;
                    DrawTab(tabs.ElementAt(4));
                    break;
            }
            Console.BackgroundColor = ConsoleColor.Black;



        }
        static void Setup()
        {
            Message message = new Message();
            DrawTextbox();
            DrawTabs();
            DrawInfobox();
            DrawMessage(message);

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
        static void DrawMessage(Message message)
        {
            // Class created but no media to display, not testable rn
            if(message.sender == null || message.body == null)
            {
                Console.WriteLine($"[00:00]Sender:Message");
            }
            else
            {
                Console.WriteLine($"[{message.timestamp.Hour}:{message.timestamp.Minute}]{message.sender}:{message.body}");
            }
        }
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
    }
}
