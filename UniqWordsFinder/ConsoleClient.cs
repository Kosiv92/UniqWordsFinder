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
        IDataHandler _dataHandler;

        public ConsoleClient(IDataHandler dataHandler)
        {
            _dataHandler = dataHandler;
        }

        public void ExecuteInConsole()
        {
            var result = _dataHandler.HandleData();
            var _fileWriter = new FileWriter(InteractionMethods.directory, result);
            try
            {
                _fileWriter.SaveResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                Environment.Exit(0);
            }
            Process.Start("notepad", _fileWriter.ResultPath);
            Console.WriteLine($"Работа приложения завершена. Найдено {_dataHandler.UniqueWordsFound} уникальных слов. Результат сохранены в файле \"result.txt\"");
            Console.ReadKey();
            Environment.Exit(0);
        }

    }
}
