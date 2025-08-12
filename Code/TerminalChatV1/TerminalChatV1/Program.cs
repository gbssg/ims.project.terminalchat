using System.Net.Sockets;
using System.Text;

namespace TerminalChatV1
{
    public class Box
    {
        public int id;
        public string name;
        public bool infocus;
    }
    public class Tab
    {
        public int id; 
        public string name;
        public bool selected;
    }
    //Placeholder Message Class
    public class Message
    {
        public int id;
        public int sender;
        public int reciever;
        public DateTime timestamp;
        public string body;
    }
    internal class Program
    {
        static string serverIp;
        static int port;

        static int boxInfocus = 0;
        static int MaxCharV = 30;
        static int MaxCharH = 121;

        static void Main()
        {
            // Create Boxes and Boxlist
            List<Box> Boxes = new List<Box>();
            Box textBox = new Box();
            Boxes.Add(textBox);

            Box infoBox = new Box();
            Boxes.Add(infoBox);
            
            Box messageBox = new Box();
            Boxes.Add(messageBox);

            Console.WriteLine(Boxes);

            // Create Tabs and Tablist
            List<Tab> tabs = new List<Tab>();
            Tab serverList = new Tab();
            tabs.Add(serverList);

            Tab channelList = new Tab();
            tabs.Add(channelList);

            Tab userlist = new Tab();
            tabs.Add(userlist);

            Tab notification = new Tab();
            tabs.Add(notification);
            Console.WriteLine(tabs);
            Thread.Sleep(1000);
            Setup();
            // TODO: create Tabs
            //Connect();
            //KeyInputThread();
            //Chatfunction();
            Console.ReadLine();

        }
        static void Connect()
        {
            Console.Write("Gib die Server═IP ein: ");
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
        static void NextTab()
        {
            if (boxInfocus >= 4) boxInfocus = 0;
            else ++boxInfocus;
            //Console.WriteLine(tabs[boxInfocus]);

        }
        static void Setup()
        {
            DrawTextbox();
            DrawInfobox();

        }
        static void DrawTextbox()
        {
            int TBposY = MaxCharV - 6;
            
            string TBascii = "╔═══════════════════════════════════════════════════════════════════════════════════════════════════╩══════════════════╣" +
                           "\n║                                                                                                                      ║" +
                           "\n║                                                                                                                      ║" +
                           "\n║                                                                                                                      ║" +
                           "\n╚══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝";

            Console.SetCursorPosition(0, TBposY);
            Console.Write(TBascii);


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
        }
        static void DrawMessage(Message message)
        {
            // Class created but no media to display, not testable rn
            Console.WriteLine($"[{message.timestamp}]{message.sender}:{message.body}");


        }
        static void DrawTabs()
        {

        }

    }
}
