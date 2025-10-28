using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TerminalChatClient;

namespace TerminalChatClient
{
    public class ReadWriteData
    {
        public List<Server> serverList { get; set; }
        public List<User> userList { get; set; }
        public List<Message> messageLog { get; set; }
        public Filemanager fm { get; set; } = new Filemanager();

        public void AddServer(Server server)
        {
            int index = ServerList.servers.FindIndex(sv => sv.UUID == server.UUID);
            if (index != -1)
            {
                ServerList.servers[index] = server;
            }else
            {
                ServerList.servers.Add(server);
            }
            // serialize obect into json string
            string jsonText = JsonSerializer.Serialize(ServerList.servers);

            File.WriteAllText(fm.GetServerlistPath(), jsonText);
        }

        // read local saved data
        public ServerList GetServerList()
        {
            // get json string with Filemanager
            string jsonText = File.ReadAllText(fm.GetServerlistPath());

            // deserialize json string into serverlist object
            ServerList serverList = JsonSerializer.Deserialize<ServerList>(jsonText);

            return serverList;
        }
        public Server GetServerProfile(Guid serverUUID)
        {
            // get json string with Filemanager
            string jsonText = File.ReadAllText(fm.GetServerProfilePath(serverUUID));

            // deserialize json string into server object
            Server server = JsonSerializer.Deserialize<Server>(jsonText);

            return server;
        }
        public MessageLog GetMessageLog(Guid serverUUID)
        {
            // get json string with Filemanager
            string jsonText = File.ReadAllText(fm.GetMessageLogPath(serverUUID));

            // deserialize json string into server object
            MessageLog messages = JsonSerializer.Deserialize<MessageLog>(jsonText);

            return messages;
        }
    }
}
