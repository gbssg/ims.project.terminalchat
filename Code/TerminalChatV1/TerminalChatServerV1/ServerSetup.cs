using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalChatServer
{
    public class ServerSetup
    {
        public Server ServerSetupPrompt()
        {
            //MVP: Simple Userinterface nothing fancy
            Console.WriteLine("Enter a server name:");
            string serverName = Console.ReadLine();

            Console.WriteLine("Enter a server description:");
            string serverDescription = Console.ReadLine();

            Console.WriteLine("Enter the server port:");
            string serverPort = Console.ReadLine();

            Console.WriteLine($"Enter a channel name:");
            string channelName = Console.ReadLine();

            Console.WriteLine($"Enter a channel description:");
            string channelDescription = Console.ReadLine();

            Channel c1 = new Channel(channelName, channelDescription);
            List<Channel> channels = new();
            channels.Add(c1);
            Server _server = new Server(serverName, serverDescription, serverPort, channels);
            return _server;
        }
    }
}
