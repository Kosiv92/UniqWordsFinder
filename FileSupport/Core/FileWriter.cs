using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSupport.Core
{
    public class FileWriter
    {
        private readonly string _directory;

        private ICollection<KeyValuePair<string, int>> _result;

        public FileWriter(string directory, ICollection<KeyValuePair<string, int>> result)
        {
            _directory = directory;
            _result = result;
        }

        public string ResultPath => _directory + "\\result.txt";

        public void SaveResult()
        {
            using (StreamWriter sw = new StreamWriter(ResultPath, false, System.Text.Encoding.UTF8))
            {
                foreach (var pair in _result)
                {
                    sw.WriteLine($"{pair.Key} - {pair.Value};");
                }
            }
        }
    }
}
