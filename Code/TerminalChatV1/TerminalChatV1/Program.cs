using System.Collections;
using System.Net.Sockets; 
using System.Text;

namespace TerminalChatV1
{
    internal class Program
    {
        static string serverIp;
        static int port;
        static string[] tabs = { "TextBox", "TB1", "TB2", "TB3", "TB4" };
        static int tabsIndex = 0;

        static void Main()
        {
            KeyInputThread();
            /*
            Connect();

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
            */
        }
        static void Connect()
        {
            Console.Write("Gib die Server-IP ein: ");
            serverIp = Console.ReadLine() ?? "127.0.0.1"; // Standard ist localhost

            port = 5000;
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
            if (tabsIndex >= 4) tabsIndex = 0;
            else ++tabsIndex;
            Console.WriteLine(tabs[tabsIndex]);

        }
            
    }
}
