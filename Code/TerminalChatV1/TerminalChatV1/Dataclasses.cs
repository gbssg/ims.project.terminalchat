using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TerminalChatClient;

namespace TerminalChatClient
{
    public class SetupUser
    {
        public string Name {  get; set; }
    }

    public class LocalUsers
    {
        public List<SetupUser> SetupUsers { get; set; } = new List<SetupUser>();
    }

    public class User 
    {
        public string UserName { get; set; }
        public DateTime UserJoinDate { get; set; }
    }

    public class Channel 
    {
        public int ChannelId { get; set; }
        public string ChannelName { get; set; }
        public string ChannelDescription { get; set; }
    }

    public class Message
    {
        public Guid ServerUUID { get; }
        public string ServerIp { get; set; }
        public int ChannelId { get; set; }
        public User Sender { get; set; }
        public DateTime Timestamp {  get; set; }
        public string Body { get; set; }
    }

    public class MessageLog
    {
        public List<Message> Messages { get; set; } = new List<Message>();
    }

    public class Server
    {
        public Guid UUID { get; }
        public string ServerIp { get; set; }
        public int Port { get; set; }
        public string ServerName {  get; set; }
        public List<Channel> Channels { get; set; } = new List<Channel>();
        public List<User> Users { get; set; } = new List<User>();
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
