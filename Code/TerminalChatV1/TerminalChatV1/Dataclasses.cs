using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TerminalChatClient
{
    public class SetupUser
    {
        public string name {  get; set; }
    }
    public class SetupUserList
    {
        public List<SetupUser> users { get; set; } = new List<SetupUser>();
    }
    public class User 
    {
        public string username { get; set; }
        public DateTime UserJoinDate { get; set; }

    }
    public class Channel 
    {
        public int channelId { get; set; }
        public string ChannelName { get; set; }
        public string channelDescription { get; set; }

    }
    public class Message : ISendable
    {
        public Guid ServerUUID { get; }
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
    public class MessageLog
    {
        public List<Message> messages { get; set; } = new List<Message>();
    }
    public class Server : ISendable
    {
        public Guid UUID { get; }
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
    // singelton, one list only
    public class ServerList
    {

        public static ServerList? Instance;

        public static List<Server> servers = new List<Server>();
        
        // constructor
        public ServerList() { }
    }
}
