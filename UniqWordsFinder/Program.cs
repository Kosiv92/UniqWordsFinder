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
            var client = new ConsoleClient();
            client.AddActions();
            var menu = new Menu(client.Actions);
            menu.ExecuteMenuItem();
        }                
    }
}