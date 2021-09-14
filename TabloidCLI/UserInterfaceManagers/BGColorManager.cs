using System;
using System.Collections.Generic;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    public class BGColorManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        

        public BGColorManager(IUserInterfaceManager parentUI)
        {
            _parentUI = parentUI;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Background Color Menu");
            Console.WriteLine(" 1) Dark Blue");
            Console.WriteLine(" 2) Dark Magenta");
            Console.WriteLine(" 3) Dark Green");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    DarkBlue();
                    return this;

                case "2":
                    DarkMagenta();
                    return this;

                case "3":
                    DarkGreen();
                    return this;

                case "0":
                    return _parentUI;

                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
                    
            }
        }

        private void DarkBlue()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();
        }

        private void DarkMagenta()
        {
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.Clear();
        }

        private void DarkGreen()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.Clear();
        }
    }
}
