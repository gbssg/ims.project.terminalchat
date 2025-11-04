namespace TerminalChatClient
{
    public class FileManager
    {
        public string appData {  get; } = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public string directoryPath { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "TerminalChatCLI");
        public string setupUserPath { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "TerminalChatCLI" + "user.json");
        public string serverListPath { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "TerminalChatCLI" + "serverlist.json");

        public void SetupAppDir()
        {
            // checks if appData directory exists, if not creates one
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // checks if user file exists, if not creates one
            if (!File.Exists(setupUserPath))
            {
                using (File.Create(setupUserPath)) { }
            }

            // checks if serverlist file exists, if not creates one
            if (!File.Exists(serverListPath))
            {
                using (File.Create(serverListPath)) { }
            }
        }

        public void CreateServerProfile(Server server)
        {
            string name = server.UUID.ToString();
            string filename = "Serverprofile_" + name;
            string profilePath = Path.Combine(directoryPath + filename);

            // creating server profile directory
            Directory.CreateDirectory(profilePath);

            // creating profile specific datafiles
            using (File.Create(Path.Combine(profilePath, "serverprofile.json"))) { }
            using (File.Create(Path.Combine(profilePath, "messagelog.json"))) { }

        }
        public string GetServerProfilePath(Guid serverUUID) 
        {
            string serverprofile = "Serverprofile_" + serverUUID.ToString();
            string path = Path.Combine(directoryPath, serverprofile, "serverprofile.json");

            if (Path.Exists(path))
            {
                return path;
            }
            else
            {
                return "File not found!";
            }

        }
        public string GetMessageLogPath(Guid serverUUID)
        {
            string serverprofile = "Serverprofile_" + serverUUID.ToString();
            string path = Path.Combine(directoryPath, serverprofile, "massagelog.json");

            if (Path.Exists(path))
            {
                return path;
            }
            else
            {
                return "File not found!";
            }
        }
        /*
        // redundant functions, paths are public static readonly variables
        public string GetUserPath()
        {

            if (Path.Exists(userPath))
            {
                return userPath;
            }
            else
            {
                return "File not found!";
            }
        }
        public string GetServerlistPath()
        {
            if (Path.Exists(serverListPath))
            {
                return serverListPath;
            }
            else
            {
                return "File not found!";
            }
        }
        */

    }
}
