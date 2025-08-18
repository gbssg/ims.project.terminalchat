using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net.Sockets;
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

        static int boxInfocus = 0;
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



            // TODO: 
            //    - make Tabs functional
            //    - seperate function into classes, cleanup main function
            //

            //Connect();
            //KeyInputThread();
            //Chatfunction();
            Console.ReadLine();

        }
        static void Connect()
        {
            Console.Write("Gib die Server-IP ein: ");
            serverIp = Console.ReadLine() ?? "127.0.0.1"; // Standard ist localhost

            port = 5000;
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

                    ConsoleKey Key = Console.ReadKey(intercept:true).Key;
                    Console.Write(Key.ToString());
                    switch (Key)
                    {
                        case ConsoleKey.Tab:
                            NextTab();
                                break;
                    }


                    // if Key is ... then ... aktion bsp swithch tabs.
                }
            });
            keyInput.Start();
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

                Console.Write("Press any key to continue normal Start.");
                string exitDebug = Console.ReadLine();
            }
            Console.Clear();
        }
        static void NextTab()
        {
            if (boxInfocus >= 4) boxInfocus = 0;
            else ++boxInfocus;
            //Console.WriteLine(tabs[boxInfocus]);

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
            Console.SetCursorPosition(0, 29);
            foreach (Tab tab in tabs)
            {
                DrawTab(tab);
            }
        }
        static void DrawTab(Tab tab)
        {
            Console.Write($"\t{tab.name}\t");
        }
    }
}
