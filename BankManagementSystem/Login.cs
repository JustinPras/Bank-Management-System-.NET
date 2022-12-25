using System;

namespace BankManagementSystem
{
    /* The class Login is responsible for displaying the Login Menu of the program.
     * Furthermore, it also handles the validation of the username and password by comparing it to the saved information in login.txt */

    public class Login
    {

        string inputUserName, inputPassword;
        string[,] userDB;

        private void checkCredentials()
        {
            string credentials = inputUserName + "|" + inputPassword;
            string[] fileText = File.ReadAllLines("../../../login.txt");
            foreach (string text in fileText)
            {
                if (credentials.Equals(text))
                {
                    Console.Write("\n\n\n\t\t Valid credentials!...     Please enter");
                    Program program = new Program();
                    program.mainMenu();
                    return;
                }
            }
            Console.Write("\n\n\n\t\t Invalid credentials!...   Please re-enter");
            Console.ReadKey(true);
            inputUserName = null;
            inputPassword = null;
            loginScreen();
        }

        public void loginScreen()
        {
            Console.Clear();
            Program.printInterface("WELCOME TO SIMPLE BANKING SYSTEM");
            Console.WriteLine("\t\t|                      LOGIN TO START                        |");
            Console.WriteLine("\t\t|                                                            |");
            Console.Write("\t\t|      User Name: ");
            int loginCursorX = Console.CursorLeft;
            int loginCursorY = Console.CursorTop;
            Console.Write("                                           |");
            Console.Write("\n\t\t|      Password: ");
            int passCursorX = Console.CursorLeft;
            int passCursorY = Console.CursorTop;
            Console.Write("                                            |");

            Console.WriteLine("\n\t\t|____________________________________________________________|\n");
            Console.SetCursorPosition(loginCursorX, loginCursorY);
            inputUserName = Console.ReadLine();

            Console.SetCursorPosition(passCursorX, passCursorY);

            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    inputPassword += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && inputPassword.Length > 0)
                    {
                        inputPassword = inputPassword.Substring(0, (inputPassword.Length - 1));
                        Console.Write("\b \b");
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                }
            } while (true);
            checkCredentials();
        }
    }
}