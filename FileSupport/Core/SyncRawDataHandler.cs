using FileSupport.Extensions;
using FileSupport.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSupport.Core
{
    public class SyncRawDataHandler : IDataHandler
    {
        private readonly string[] _rawStringsData;

        private string[] _punctuationMarks = new string[] { ".", ",", "!", "?", "\"", "«", "»", ":", "(", ")", "•", "-", "–", ",", ".", Environment.NewLine, "\t", "=", "’", "“", "№",
                                                   "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", ";", "–", "…", "…", " ", "  ", "#", "&", "[", "]", "„"};

        Dictionary<string, int> _uniqueWords;

        public SyncRawDataHandler(string[] rawStringsData)
        {
            _rawStringsData = rawStringsData;
            _uniqueWords = new Dictionary<string, int>();
        }

        public int UniqueWordsFound => _uniqueWords.Count;

        /// <summary>
        /// Подсчет уникальных слов в анализируемом файле
        /// </summary>
        /// <returns>Словарь с уникальными словами в качестве ключей и количеством их повторений в файле в качестве значений</returns>
        public Dictionary<string, int> HandleData()
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
    }
}
