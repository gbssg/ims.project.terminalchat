using System.Text.Json;

namespace TerminalChatServer
{
    public class ServerSetup
    {
        static dataCrud dc = new dataCrud();
        public Server ServerSetupPrompt()
        {
            bool loop = true;
            string serverJson;
            do
            {
                loop = false;
                // Enter Server-Json to create a server
                Console.WriteLine("Enter server-json:");
                serverJson = Console.ReadLine();

                try
                {

                    Server deserializeTest = JsonSerializer.Deserialize<Server>(serverJson);
                    dc.AddServer(deserializeTest);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Could not read Json, wrong format, try again:");
                    loop = true;
                }

            } while (loop);

            return JsonSerializer.Deserialize<Server>(serverJson);
            
            
        }
    }
}