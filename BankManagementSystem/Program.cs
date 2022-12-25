using System;

namespace BankManagementSystem
{

    /* The class Program is responsible for displaying the Main Menu of the program.
     * Furthermore, it also handles the user's inputs of deciding what function they would like to perform e.g Creating a new account */
    public class Program
    {

        int userChoiceInput;
        Accounts accounts = new Accounts();
        public void mainMenu()
        {

            do
            {
                Console.Clear();
                printInterface("WELCOME TO SIMPLE BANKING SYSTEM");
                Console.WriteLine("\t\t|                                                            |");
                Console.WriteLine("\t\t|      1. Create a new account                               |");
                Console.WriteLine("\t\t|      2. Search for an account                              |");
                Console.WriteLine("\t\t|      3. Deposit                                            |");
                Console.WriteLine("\t\t|      4. Withdraw                                           |");
                Console.WriteLine("\t\t|      5. A/C statement                                      |");
                Console.WriteLine("\t\t|      6. Delete Account                                     |");
                Console.WriteLine("\t\t|      7. Exit                                               |");
                Console.WriteLine("\t\t|____________________________________________________________|");
                Console.Write("\t\t|    Enter your choice (1-7): ");
                int cursorX = Console.CursorLeft;
                int cursorY = Console.CursorTop;
                Console.Write("                               |");
                Console.WriteLine("\n\t\t|____________________________________________________________|");
                Console.SetCursorPosition(cursorX, cursorY);
                try
                {
                    userChoiceInput = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException f)
                {
                    continue;
                }


                switch (userChoiceInput)
                {
                    case 1:
                        accounts.createAccount();
                        break;
                    case 2:
                        accounts.searchAccount();
                        break;
                    case 3:
                        accounts.deposit();
                        break;
                    case 4:
                        accounts.withdraw();
                        break;
                    case 5:
                        accounts.showStatement();
                        break;
                    case 6:
                        accounts.deleteAccount();
                        break;
                    case 7:
                        break;
                    default:
                        mainMenu();
                        break;
                }
            } while (userChoiceInput != 7);
        }

        /* printInterface(String title) is used throughout the program as a template for creating the "header" of each function's respective GUI */
        public static void printInterface(String title)
        {
            Console.WriteLine("\t\t ____________________________________________________________");
            Console.WriteLine("\t\t|                                                            |");
            if (title.Length%2 == 0)
            {
                Console.WriteLine("\t\t|" + new string(' ', (60 - title.Length) / 2) + title + new string(' ', (60 - title.Length) / 2) + "|");
            }
            else
            {
                Console.WriteLine("\t\t|" + new string(' ', (60 - title.Length) / 2) + title + new string(' ', (61 - title.Length) / 2) + "|");
            }
            Console.WriteLine("\t\t|____________________________________________________________|");
        }
    }
}