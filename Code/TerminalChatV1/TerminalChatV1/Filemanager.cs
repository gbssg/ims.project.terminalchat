using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalChatClient
{
    public class Filemanager
    {
        public string appData {  get; } = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public string directoryPath { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "TerminalChatCLI");
        public string userPath { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "TerminalChatCLI" + "user.json");
        public string serverListPath { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "TerminalChatCLI" + "serverlist.json");
        public void SetupAppDir()
        {
            // checks if appData directory exists, if not creates one
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // checks if user file exists, if not creates one
            if (!File.Exists(userPath))
            {
                using (File.Create(userPath)) { }
            }

            // checks if serverlist file exists, if not creates one
            if (!File.Exists(serverListPath))
            {
                using (File.Create(serverListPath)) { }
            }
        }
        public void CreateServerProfile(Server server)
        {
            int index = server.serverIndex;
            string filename = "Serverprofile_" + index;
            string profilePath = Path.Combine(directoryPath + filename);

            // creating server profile directory
            Directory.CreateDirectory(profilePath);

            // creating profile specific datafiles
            using (File.Create(Path.Combine(profilePath, "serverprofile.json"))) { }
            using (File.Create(Path.Combine(profilePath, "messagelog.json"))) { }

        }
        public string GetServerProfilePath(Server server) 
        {
            string filename = "Serverprofile_" + server.serverIndex;
            string path = Path.Combine(directoryPath, filename, "serverprofile.json");

            if (Path.Exists(path))
            {
                return path;
            }
            else
            {
                return "File not found!";
            }

        }
        public string GetMessagelogPath(Server server)
        {
            string filename = "Serverprofile_" + server.serverIndex;
            string path = Path.Combine(directoryPath, filename, "massagelog.json");

            if (Path.Exists(path))
            {
                return path;
            }
            else
            {
                return "File not found!";
            }
        }
    }
}
