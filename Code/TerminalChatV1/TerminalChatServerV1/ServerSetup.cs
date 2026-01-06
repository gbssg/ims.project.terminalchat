using System.Text.Json;

namespace TerminalChatServer
{
    public class ServerSetup
    {
        static ServerDataCrud dc = new ServerDataCrud(); 
        public Server ServerSetupPromptByJSON()
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

        public Server ServerSetupPrompt()
        {
            Server server = new Server();
            Console.WriteLine("Enter the server's name:");
            string? enteredName = ReadString(64, 8);
            Console.WriteLine("Enter the servers description, no linebreakes:");
            string? enteredDescription = ReadString(128, 8);
            Console.WriteLine("Enter Server Port: (make shure the port isn't in use by any other service, check in your firewall settings)");
            int enteredPort = ReadInt(1, 65536);
            Console.WriteLine("Enter amount of channels: (max: 10, the more you choose the more you have to configure)");
            int amountOfChannels = ReadInt(0, 10);
            if (amountOfChannels > 1) 
            {
                server.Name = enteredName;
                server.Description = enteredDescription;
                server.Port = enteredPort;
                server.Channels = SetupChannelRecursive(amountOfChannels);
            } else
            {
                server.Name = enteredName;
                server.Description = enteredDescription;
                server.Port = enteredPort;
                server.Channels.Add(SetupChannel());
            }
            return server;
        }

        public Channel SetupChannel()
        {
            Console.WriteLine("Enter the channel's name:");
            string? enteredName = ReadString(64, 8);
            Console.WriteLine("Enter the channel's description, no linebreakes:");
            string? enteredDescription = ReadString(128, 8);
            Channel c = new Channel(enteredName, enteredDescription);
            return c;
        }

        public List<Channel> SetupChannelRecursive(int amountOfChannels)
        {
            List<Channel> channels = new List<Channel>();
            for (int i = 0; i < amountOfChannels; i++)
            {
                channels.Add(SetupChannel());
            }
            return channels;
        }

        // takes max and min expected integer value and prompts the user to enter a number, and returns it if fits the parameters descriptions.
        static int ReadInt(int expectedMin, int expectedMax)
        {
            bool wholeLoop = true;
            int convertedInt;
            string? input;
            do
            {
                input = Console.ReadLine();
                // check if string can convert to int32
                try
                {
                    int? testconv = Convert.ToInt32(input);
                    Console.WriteLine("succeded!");
                    if (testconv != null && testconv>=expectedMin && testconv<=expectedMax)
                    {
                        wholeLoop = false;
                    }
                    else
                    {
                        Console.WriteLine($"Please enter a Number between {expectedMin} and {expectedMax}");
                    }
                }
                catch
                {

                    Console.WriteLine($"Please enter a Number between {expectedMin} and {expectedMax}");
                }

            } while (wholeLoop);
            convertedInt = Convert.ToInt32(input);
            return convertedInt;
        }

        //takes min and max length of a string and prompts the user to enter one, returns the string if it fits the parameters description
        public string ReadString(int maxLength, int minLength)
        {
            bool loop = true;
            string? input;

            do
            {
                input = Console.ReadLine();
                if (input.Length > minLength && input.Length < maxLength)
                {
                    loop = false;
                }
                else
                {
                    Console.WriteLine($"Please enter a String longer than {minLength} and shorter than {maxLength}!");
                }
            } while (loop);
            return input;
        }
    }
}