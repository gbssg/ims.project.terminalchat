using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;


namespace TerminalChatServer
{
    public class ServerList
    {
        public List<Server> Servers { get; } = new List<Server>();
    }
    public class Server
    {
        public string Ip { get; set; } 
        public string Port { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid UUID { get; set; } = Guid.CreateVersion7();
        public List<Channel> Channels { get; set; } 

        public Server(string _name, string _description, string _port , List<Channel> _channels) 
        { 
            this.Ip = GetLocalIpAddress();
            this.Port = _port;
            this.Name = _name;
            this.Description = _description;
            this.Channels = _channels;
        }

        public void UpdateChannel(Channel _channel)
        {
            int index = Channels.FindIndex(ch => ch.UUID == _channel.UUID);
            if (index != -1)
            {
                Channels[index] = _channel;
            } else
            {
                Channels.Add(_channel);
            }
        }
        public string GetLocalIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No Network adapters with an IPv4 address in the system!");
        }
    }

    public class Channel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid UUID { get; } = Guid.CreateVersion7();
        public Channel(string _name, string _description)
        {
            this.Name = _name;
            this.Description = _description;
        }

    }
}
