using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleSupport
{
    public class Menu
    {
        private readonly string _culture;                

        private Dictionary<string, Action> _actions;

        public Menu(Dictionary<string, Action> actions)
        {
            _culture = CultureInfo.CurrentCulture.ToString();

            var exitWord = _culture == "ru-RU" ? "Выход" : "Exit";
            
            _actions = actions;
            _actions.Add(exitWord, () => Environment.Exit(0));
        }
       
        /// <summary>
        /// Выполнение выбранного пункта меню
        /// </summary>
        public void ExecuteMenuItem()
        {
            try
            {
                while (true)
                {
                    var userChoice = _actions[ChooseMenuItem()];
                    if (userChoice != null)
                    {
                        userChoice.Invoke();
                        return;
                    }
                }
            }
            catch(Exception ex)
            {
                var errorMessage = _culture == "ru-RU" ? "Ошибка! Похоже что-то пошло не так..."
               : "Error! Looks like something went wrong...";
                Console.WriteLine(errorMessage);
                Console.ReadKey();
                Environment.Exit(-1);
            } 
        }

        /// <summary>
        /// Метод выбора пункта меню
        /// </summary>
        /// <param name="menuItems">Массив содержащий пункты меню</param>
        /// <returns>Возвращает число обозначающее выбранный пользователем пункт меню</returns>
        public string ChooseMenuItem()
        {
            var _menuItems = _actions.Keys.ToArray();


            int visibleChoice = 0; //счетчик выбранного пользователем пункта меню

            ConsoleKeyInfo buttonPressed; //нажимаемая пользователем клавиша

            while (true) //цикл использования меню
            {
                Console.Clear();

                for (int i = 0; i < _menuItems.Length; i++) //цикл для определения положения выбора пользователя
                {
                    if (i == visibleChoice)
                        Console.Write(
                            "> "); //отображение положения пользователя в меню если счетчик совпадает с номером строки
                    else
                        Console.Write(
                            "  "); //отображение пробела (пустого места) в меню если счетчик не совпадает с номером строки
                    Console.WriteLine(_menuItems[i]); //отображение пунктов меню
                }

                buttonPressed = Console.ReadKey();

                if (buttonPressed.Key == ConsoleKey.UpArrow && visibleChoice != 0)
                    visibleChoice--; //уменьшение счетчика если нажата клавиша "вверх" и счетчик не находится в максимально верхнем положении
                if (buttonPressed.Key == ConsoleKey.DownArrow && visibleChoice != _menuItems.Length - 1)
                    visibleChoice++; //увеличение счетчика если нажата клавиша "вниз" и счетчик не находится в максимально нижнем положении

                if (buttonPressed.Key == ConsoleKey.Enter) break;
            }

            return _menuItems[visibleChoice];
        }
                
    }

}
