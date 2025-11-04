using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TerminalChatClient;

namespace TerminalChatClient
{
    public class ReadWriteData
    {
        public FileManager fm { get; set; } = new FileManager();
        public SetupUserList setupUserlist { get; set; } = new SetupUserList();

        // adds or updates a server in the serverlist
        public void UpdateServerlist(Server server)
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

            // overrides previous 
            File.WriteAllText(fm.serverListPath, jsonText);
        }

        public ServerList ReadServerList()
        {
            // get json string with Filemanager
            string jsonText = File.ReadAllText(fm.serverListPath);

            // deserialize json string into serverlist object
            ServerList serverList = JsonSerializer.Deserialize<ServerList>(jsonText);

            return serverList;
        }

        public void UpdateServerProfile(Server server)
        {
            string jsonText = JsonSerializer.Serialize(server);

            File.WriteAllText(fm.GetServerProfilePath(server.UUID), jsonText);
        }

        public Server ReadServerProfile(Guid serverUUID)
        {
            // get json string with Filemanager
            string jsonText = File.ReadAllText(fm.GetServerProfilePath(serverUUID));

            // deserialize json string into server object
            Server server = JsonSerializer.Deserialize<Server>(jsonText);

            return server;
        }
        public void UpdateSetupUserlist(SetupUser _setupUser)
        {
            // get index of setupUser if 
            int index = setupUserlist.users.IndexOf(_setupUser);

            if (index != -1) 
            {
                setupUserlist.users.Add(_setupUser);
            }
            else
            {
                setupUserlist.users[index] = _setupUser;
            }
            string jsonText = JsonSerializer.Serialize(setupUserlist);

            File.WriteAllText(fm.setupUserPath, jsonText);
        }

        public SetupUserList ReadSetupUserlist()
        {
            string jsonText = File.ReadAllText(fm.setupUserPath);

            SetupUserList users = JsonSerializer.Deserialize<SetupUserList>(jsonText);
            
            return users;
        }

        public void UpdateMessagelog(Message _message)
        {
            // uses ReadMessageLog(Guid serverUUID); to get existing message log
            MessageLog ml = ReadMessageLog(_message.ServerUUID);

            // adds current message to the messages list
            ml.messages.Add(_message);

            // serializses the ml object into json
            string jsonText = JsonSerializer.Serialize(ml);

            // writes the json text onto the path provided by the fm.GetMessageLogPath(_message.ServerUUID)
            File.WriteAllText(fm.GetMessageLogPath(_message.ServerUUID),jsonText);
        }

        public MessageLog ReadMessageLog(Guid serverUUID)
        {
            // get json string with Filemanager
            string jsonText = File.ReadAllText(fm.GetMessageLogPath(serverUUID));

            // deserialize json string into server object
            MessageLog messageLog = JsonSerializer.Deserialize<MessageLog>(jsonText);

            return messageLog;
        }
    }
}
