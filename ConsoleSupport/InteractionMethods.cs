using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSupport
{
    public static class InteractionMethods
    {
        private static string fileName = "\\example.txt";

        private static string directory, filePath;

        public static string InputPathToFile()
        {            
            bool isFileExist;

            Console.Write($"Укажите путь к папке, хранящей файл с именем \"{fileName}\" в формате \"D:\\Directory\": ");

            do
            {
                directory = Console.ReadLine();
                filePath = directory + fileName;
                isFileExist = File.Exists(filePath);
                if (!isFileExist)
                {
                    Console.WriteLine($"Файл \"{fileName}\" отсутствует по указанному пути. Необходимо указать путь к папке с файлом в формате \"D:\\Directory\\\"");
                    Console.Write("Попробуйте снова: ");
                }
                else
                {
                    Console.WriteLine("Файл найден. Начните обработку нажатием любой клавиши");
                    Console.ReadKey();
                }
            } while (!isFileExist);

            return directory;
        }

        public static void PrintOutInfo()
        {
            Console.WriteLine($"Полный путь к файлу: {filePath}");            
        }

    }
    
}
