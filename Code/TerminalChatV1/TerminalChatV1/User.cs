using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TerminalChatClient
{
    internal class User
    {
        public string? name {  get; set; }
        public DateTime creationTime;
    
        public static User SetUser()
        {
            // Path gets assembled manualy to enshure compadability between linux, mac and windows devices
            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string parentfoldername = "TerminalChatCLI";
            string foldername = "Data"; 
            string filename = "user.json";
            string path = Path.Combine(appdata, parentfoldername, foldername, filename);

            var user = new User // create Userclass to serialize into json
            {
                name = "placeholder",
                creationTime = DateTime.Now
            };

            Console.WriteLine("Please Set a Username");
            do
            {
                user.name = Console.ReadLine();
                Console.Clear();
                if (user.name == string.Empty)
                    Console.WriteLine("Please set a Username");
            } 
            while (string.IsNullOrWhiteSpace(user.name));

            string jsonString = JsonSerializer.Serialize(user);
            if (Directory.Exists(Path.Combine(appdata, parentfoldername)) && File.Exists(path))
            // if the appdata directory, datafolder or file doesn't exist it will create new ones, if they exist the json will get saved into the file.
            {
                Console.WriteLine(jsonString);
                File.WriteAllText(path, jsonString);
            }
            else
            {
                try
                {

                    Directory.CreateDirectory(Path.Combine(appdata, parentfoldername, foldername));
                    File.Create(path).Close();
                    Console.WriteLine(path);

                }
                catch (Exception e) 
                {
                    Console.WriteLine(e.Message);
                }
            }


                //Console.WriteLine($"User saved at: {path}");
            Console.ReadKey();

            return user;
        }
    }

}
