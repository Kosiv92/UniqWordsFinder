using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSupport.Core
{
    public class FileReader
    {
        private readonly string _directory;

        private readonly string _filePath;

        private string[] _linesResult;

        public FileReader(string directory)
        {
            _directory = directory;
            _filePath = directory + "\\example.txt";
        }

        /// <summary>
        /// Путь к директории с рабочим файлом
        /// </summary>
        public string Directory
        {
            get { return _directory; }
        }

        /// <summary>
        /// Полный путь к рабочему файлу
        /// </summary>
        public string FilePath
        {
            get { return _filePath; }
        }

        /// <summary>
        /// Получить результаты чтения файла в виде массива строк
        /// </summary>
        /// <returns>Массив строк</returns>
        public string[] GetReadResultInLines()
        {
            if (_linesResult == null)
            {
                return _linesResult = File.ReadAllLines(_filePath, UnicodeEncoding.UTF8);
            }
            return _linesResult;
        }



    }
}
