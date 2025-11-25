using System.Text.Json;

namespace TerminalChatServer
{
    public class ServerSetup
    {
        static dataCrud dc = new dataCrud();
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
        public void ServerSetupPrompt()
        {
            string enteredName = Console.ReadLine();
            string enteredDescription = Console.ReadLine();
            int amountOfChannels = ReadInt(0, 10);

        }
        // takes max and min expected integer value and prompts the user to enter a number, and returns it if fits the parameters descriptions.
        public int ReadInt(int expectedMin, int expectedMax)
        {
            bool loop = true;
            int convertedInt;

            do
            {
                string? input = Console.ReadLine();

                try
                {
                    Convert.ToInt32(input);
                    convertedInt = Convert.ToInt32(input);
                
                    if (convertedInt > expectedMax)
                    {
                        Console.WriteLine($"Please enter an integer smaller then {expectedMax}");
                    } 
                    else if (convertedInt < expectedMin)
                    {
                        Console.WriteLine($"Please enter an integer bigger than {expectedMin}");
                    }else
                    {
                        loop = false;
                    }
                }
                catch
                {
                    Console.WriteLine($"Please enter an integer bigger than {expectedMin} but smaller then {expectedMax}");
                }
                convertedInt = Convert.ToInt32(input);

            } while (loop);
            return convertedInt;
        }
        //takes min and max length of a string and prompts the user to enter one, returns the string if it fits the parameters description
        public string ReadString(int maxLength, int minLength)
        {
            bool loop = true;
            string input;

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