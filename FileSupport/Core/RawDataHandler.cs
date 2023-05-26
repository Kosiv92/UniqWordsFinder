using FileSupport.Extensions;
using FileSupport.Interfaces;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace FileSupport.Core
{
    public class RawDataHandler : IDataHandler
    {
        private readonly string[] _rawStringsData;               

        private string[] _punctuationMarks = new string[] { ".", ",", "!", "?", "\"", "«", "»", ":", "(", ")", "•", "-", "–", ",", ".", Environment.NewLine, "\t", "=", "’", "“", "№",
                                                   "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", ";", "–", "…", "…", " ", "  ", "#", "&", "[", "]", "„"};

        Dictionary<string, int> _uniqueWords;

        ConcurrentDictionary<string, int> _concurrentUniqueWords;

        public RawDataHandler(string[] rawStringsData)
        {
            _rawStringsData = rawStringsData;
            _uniqueWords = new Dictionary<string, int>();
            _concurrentUniqueWords = new ConcurrentDictionary<string, int>();            
        }

        public int UniqueWordsFound => _uniqueWords.Count;                

        /// <summary>
        /// Подсчет уникальных слов в анализируемом файле (закрытый синхронный метод для работы с рефлексией)
        /// </summary>
        /// <returns>Словарь с уникальными словами в качестве ключей и количеством их повторений в файле в качестве значений</returns>
        private Dictionary<string, int> HandleDataSingleTR()
        {
            string[] words;

            for (int i = 0; i < _rawStringsData.Length; i++)
            {
                words = _rawStringsData[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for (int y = 0; y < words.Length; y++)
                {
                    words[y] = words[y].DeleteSupplySymbols(_punctuationMarks).ToLower();
                    if (String.IsNullOrEmpty(words[y]) || String.IsNullOrWhiteSpace(words[y])) continue;
                    if (_uniqueWords.ContainsKey(words[y])) _uniqueWords[words[y]]++;
                    else _uniqueWords.Add(words[y], 1);
                }
                Array.Clear(words);
            }
            return _uniqueWords.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }

        /// <summary>
        /// Подсчет уникальных слов в анализируемом файле с использованием многопоточности
        /// </summary>
        /// <returns>Словарь с уникальными словами в качестве ключей и количеством их повторений в файле в качестве значений</returns>
        public Dictionary<string, int> HandleData()
        {
            var result = Parallel.ForEach(_rawStringsData, SaveWordsToConcurrentDictionary);

            return _uniqueWords = _concurrentUniqueWords.OrderByDescending(x=> x.Value).ToDictionary(x => x.Key, x => x.Value);
        }

        private void SaveWordsToConcurrentDictionary(string str)
        {
            string[] words = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string word in words)
            {
                string wordForWork = word.ToLower().DeleteSupplySymbols(_punctuationMarks);

                if (String.IsNullOrEmpty(wordForWork) || String.IsNullOrWhiteSpace(wordForWork))
                {
                    return;
                }

                _concurrentUniqueWords.AddOrUpdate(wordForWork, 1, (wordForWork, u) => u + 1);
            }
        }
    }
}
