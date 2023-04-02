using ConsoleSupport;
using FileSupport.Core;
using FileSupport.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniqWordsFinder
{
    public class ConsoleClient
    {
        Dictionary<string, Action> _actions;

        FileReader fileReader;

        FileWriter fileWriter;

        IDataHandler dataHandler;

        public ConsoleClient()
        {
            _actions = new Dictionary<string, Action>();
            fileReader = new FileReader(InteractionMethods.InputPathToFile());
        }

        public Dictionary<string, Action> Actions => _actions;

        public void AddActions()
        {
            _actions.Clear();
            _actions.Add("Посмотреть информацию о файле", InteractionMethods.PrintOutInfo);
            dataHandler = new SyncRawDataHandler(fileReader.GetReadResultInLines());
            Action handleFile = () =>
            {
                fileWriter = new FileWriter(fileReader.Directory, dataHandler.HandleData());
                try
                {
                    Process.Start("notepad", fileWriter.ResultPath);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                    Environment.Exit(0);
                }
                Console.WriteLine($"Работа приложения завершена. Найдено {dataHandler.UniqueWordsFound} уникальных слов. Результат сохранены в файле \"result.txt\"");
                Console.ReadKey();
                Environment.Exit(0);
            };
            _actions.Add("Обработать файл", handleFile);
        }


    }
}
