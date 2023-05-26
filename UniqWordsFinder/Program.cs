using ConsoleSupport;
using FileSupport.Core;
using FileSupport.Interfaces;
using System.Diagnostics;

namespace UniqWordsFinder
{
    internal class Program
    {

        static async Task Main(string[] args)
        {
            var fileReader = new FileReader(InteractionMethods.InputPathToFile()); //считываем файл

            var webClient = new WebServiceClient("https://localhost:7217/Text");
            if (!webClient.IsServerAvailable().Result)
            {
                Console.WriteLine("Server is not available. Application will be closed");
                Console.ReadKey();
                Environment.Exit(0);
            };

            IDataHandler dataHandler = new WebDataHandler(webClient, fileReader.GetReadResultInLines());                        
                        
            //IDataHandler dataHandler = new RawDataHandler(fileReader.GetReadResultInLines()); //передаем результат обработчику
            var client = new ConsoleClient(dataHandler);                       

            client.Execute();
        }
    }
}