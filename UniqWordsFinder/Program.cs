using ConsoleSupport;
using FileSupport.Core;
using FileSupport.Interfaces;
using System.Diagnostics;

namespace UniqWordsFinder
{
    internal class Program
    {        

        static void Main(string[] args)
        {
            var fileReader = new FileReader(InteractionMethods.InputPathToFile()); //считываем файл
            IDataHandler dataHandler = new RawDataHandler(fileReader.GetReadResultInLines()); //передаем результат обработчику
            var client = new ConsoleClient(dataHandler);

            //для вызова метода путем рефлексии
            //client.CallPrivateMethod(); 
                        
            client.Execute();            
        }                
    }
}