using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TerminalChatServer
{
    public class ServerDataCrud
    {
        public string directoryPath { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TerminalChatServer");
        public string serverListPath { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TerminalChatServer", "servers.json");

        public void SetupAppDir()
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            if (!File.Exists(serverListPath))
            {
                using (File.Create(serverListPath)) { };
            }
        }

        public ServerList GetServers()
        {
            string jsonText = File.ReadAllText(serverListPath);
            /*
            if (jsonText.Length == 0)
            {
                ServerList sltemp = new();
                var opt = new JsonSerializerOptions { WriteIndented = true };
                string jsonText2 = JsonSerializer.Serialize(sltemp, opt);
                return sltemp;
            }
            */
            ServerList serverList = JsonSerializer.Deserialize<ServerList>(jsonText);

            return serverList;
        }
        public string GetServersAsJson()
        {
            string jsonText = File.ReadAllText(serverListPath);
            /*
            if (jsonText.Length == 0)
            {
                ServerList sltemp = new();
                var opt = new JsonSerializerOptions { WriteIndented = true };
                string jsonText2 = JsonSerializer.Serialize(sltemp, opt);
                return sltemp;
            }
            */
            return jsonText;
        }


        public void AddServer(Server _server)
        {
            ServerList serverList = GetServers();

            var opt = new JsonSerializerOptions { WriteIndented = true };
            string jsonText = JsonSerializer.Serialize(serverList, opt);
            Console.WriteLine(jsonText);
            //Console.WriteLine(serverList.ToString());
            
            serverList.Servers.Add(_server);
            //Console.WriteLine(serverList.Servers);

            opt = new JsonSerializerOptions { WriteIndented = true };
            jsonText = JsonSerializer.Serialize(serverList, opt);
            
            Console.Write(jsonText);
            File.WriteAllText(serverListPath, jsonText);
        }

        public void DeleteServer(Server _server)
        {
            ServerList serverList = GetServers();
            serverList.Servers.Remove(_server);

            var opt = new JsonSerializerOptions { WriteIndented = true };
            string jsonText = JsonSerializer.Serialize(serverList, opt);

            File.WriteAllText(serverListPath, jsonText);
        }

        public void UpdateServer(Server _server)
        {
            ServerList serverList = GetServers();

            int index = serverList.Servers.FindIndex(sv => sv.UUID == _server.UUID);
            if (index != -1)
            {
                serverList.Servers[index] = _server;
            } else
            {
                AddServer(_server);
                //Some sort of Warning! To be implemented! not mvp
            }

            var opt = new JsonSerializerOptions { WriteIndented = true };
            string jsonText = JsonSerializer.Serialize(serverList, opt);

            File.WriteAllText(serverListPath, jsonText);
        }
        public Server GetServer(Guid _UUID)
        {
            ServerList serverList = GetServers();
            int index = serverList.Servers.FindIndex(sv => sv.UUID == _UUID);
            if (index != -1)
            {
                return serverList.Servers[index];
            } else
            {
                throw new Exception("Could not find Server identified by this UUID!");
                //Some sort of ErrorHandling
            }
        }
    }
}
