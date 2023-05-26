using ConsoleSupport;
using FileSupport.Core;
using FileSupport.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UniqWordsFinder
{
    public class ConsoleClient
    {
        IDataHandler _dataHandler;

        private TimeSpan _elapsedTime;

        private Stopwatch _stopwatch;

        public ConsoleClient(IDataHandler dataHandler)
        {
            _dataHandler = dataHandler;
            _stopwatch = new Stopwatch();
            _elapsedTime = TimeSpan.Zero;
        }

        public string ElapsedTime => String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            _elapsedTime.Hours, _elapsedTime.Minutes, _elapsedTime.Seconds,
            _elapsedTime.Milliseconds / 10);

        public void Execute()
        {
            _stopwatch.Start();
            var result = _dataHandler.HandleData();
            _stopwatch.Stop();

            _elapsedTime = _stopwatch.Elapsed;
            
            SendResultToWriter(result);
        }

        public void CallPrivateMethod()
        {
            _stopwatch.Start();

            var type = _dataHandler.GetType();

            var methods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);

            var method = methods.Where(m => m.Name.Contains("Single")).FirstOrDefault();

            var result = (Dictionary<string, int>)method.Invoke(_dataHandler, new object[] { });

            _stopwatch.Stop();

            _elapsedTime = _stopwatch.Elapsed;

            SendResultToWriter(result);
        }

        private void SendResultToWriter(Dictionary<string, int> result)
        {
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
            Console.WriteLine($"Работа приложения завершена. Найдено {_dataHandler.UniqueWordsFound} уникальных слов. Результат сохранены в файле \"result.txt\"\n" +
                $"Время обработки исходного файла заняло: {ElapsedTime}");
            Console.ReadKey();
            Environment.Exit(0);
        }

    }
}
