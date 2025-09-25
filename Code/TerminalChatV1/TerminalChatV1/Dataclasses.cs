using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TerminalChatClient
{
    internal class User 
    {
        public string username { get; set; }
        public DateTime UserJoinDate { get; set; }

    }
    internal class Channel 
    {
        public int channelId { get; set; }
        public string ChannelName { get; set; }
        public string channelDescription { get; set; }

    }
    internal class Message : ISendable
    {
        public string serverIndex { get; set; }
        public string serverIp { get; set; }
        public int channelId { get; set; }
        public User sender { get; set; }
        public DateTime timestamp {  get; set; }
        public string body { get; set; }

        public string ToJson()
        {
            var options = new JsonSerializerOptions { WriteIndented = true }; // makes json pretty
            return JsonSerializer.Serialize(this, options); // writes json string

        }
    }
    internal class Server : ISendable
    {
        public int serverIndex { get; set; }
        public string serverIp { get; set; }
        public string serverName {  get; set; }
        public List<Channel> channels { get; set; } = new List<Channel>();
        public List<User> users { get; set; } = new List<User>();
        public string ToJson()
        {
            var options = new JsonSerializerOptions { WriteIndented = true }; // makes json pretty
            return JsonSerializer.Serialize(this, options); // writes json string
        }
    }
    internal class ServerList : ISendable
    {
        public List<Server> servers { get; set; } = new List<Server>();

        public string ToJson()
        {
            var options = new JsonSerializerOptions { WriteIndented = true }; // makes json pretty
            return JsonSerializer.Serialize(this, options); // writes json string
        }
    }
}
