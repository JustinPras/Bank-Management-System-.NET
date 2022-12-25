using System.Collections;
using System.Text.RegularExpressions;

namespace BankManagementSystem
{
    /* The class Accounts is responsible for most of the functionality within the program.
     * It handles the different functions a user would want to make on their bank account/s.  */
    public class Accounts
    {
        private List<Account> accounts;
        private string[] allText;

        /* The constructor Accounts() is responsible for creating a List "accounts".
         * And reading existing account text files saved within the "Accounts" folder and adding them to the accounts List. */
        public Accounts()
        {
            accounts = new List<Account>();
            List<String> filePaths = Directory.EnumerateFiles("../../../Accounts").ToList();
            if (filePaths != null)
            {
                foreach (string filePath in filePaths)
                {
                    allText = File.ReadAllLines(filePath);
                    int accountNumber = readInt(0);
                    double accountBalance = readDouble(1);
                    string userFirstName = readString(2);
                    string userLastName = readString(3);
                    string userAddress = readString(4);
                    string userphone = readString(5);
                    string userEmail = readString(6);
                    if (allText.Length <= 7) 
                    {
                        accounts.Add(new Account(accountNumber, accountBalance, userFirstName, userLastName, userAddress, userphone, userEmail));
                    }
                    else
                    {
                        List<String> transactionArr = new List<string>();
                        for (int i = 7; i < allText.Length; i++)
                        {
                            transactionArr.Add(allText[i]);
                        }
                        accounts.Add(new Account(accountNumber, accountBalance, userFirstName, userLastName, userAddress, userphone, userEmail, transactionArr));
                    }
                    
                }
            }
            Console.ReadKey();
        }

        /* searchAccount() prompts the "SEARCH AN ACCOUNT" function interface and allows the user to search for an account */ 
        public void searchAccount()
        {
            string notDone = "y";
            do
            {
                Console.Clear();
                Program.printInterface("SEARCH AN ACCOUNT");
                Console.WriteLine("\t\t|                     ENTER THE DETAILS                      |");
                Console.WriteLine("\t\t|                                                            |");
                Console.Write("\t\t|    Account Number: ");
                int cursorX = Console.CursorLeft;
                int cursorY = Console.CursorTop;
                Console.WriteLine("\t\t                             |");
                Console.WriteLine("\t\t|____________________________________________________________|");
                Console.SetCursorPosition(cursorX, cursorY);
                string searchedNumberString = Console.ReadLine();
                try
                {
                    int searchedNumber = Convert.ToInt32(searchedNumberString);
                    if (getAccount(searchedNumber) != null)
                    {
                        Console.WriteLine("\n\n\t\t  Account found!");
                        Program.printInterface("ACCOUNT DETAILS");
                        getAccount(searchedNumber).displayAccountDetails();
                        Console.Write("\n\t\t  Check another account (y/n)? ");
                        notDone = Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("\n\n\t\t  Account not found!");
                        Console.Write("\t\t  Check another account (y/n)? ");
                        notDone = Console.ReadLine();
                    }
                }
                catch (FormatException)
                {
                    Console.Write("\n\n\t\t  Please enter a valid account number.");
                    Console.Write("\n\t\t  Would you like to search for another account (y/n)? ");
                    notDone = Console.ReadLine();
                }
                catch (OverflowException)
                {
                    Console.Write("\n\n\t\t  Please enter an account number under 11 digits.");
                    Console.Write("\n\t\t  Would you like to search for another account (y/n)? ");
                    notDone = Console.ReadLine();
                }
            } while (!notDone.Equals("n"));
        }

        /* desposit() prompts the "DEPOSIT" function interface and allows the user to deposit money into account. */
        public void deposit()
        {
            string notDone = "y";
            do
            {
                Console.Clear();
                Console.WriteLine("\t\t ____________________________________________________________");
                Console.WriteLine("\t\t|                                                            |");
                Console.WriteLine("\t\t|                          DEPOSIT                           |");
                Console.WriteLine("\t\t|____________________________________________________________|");
                Console.WriteLine("\t\t|                     ENTER THE DETAILS                      |");
                Console.WriteLine("\t\t|                                                            |");
                Console.Write("\t\t|    Account Number: ");
                int accNocursorX = Console.CursorLeft;
                int accNocursorY = Console.CursorTop;
                Console.WriteLine("\t\t                             |");
                Console.Write("\t\t|    Amount: $");
                int amountCursorX = Console.CursorLeft;
                int amountCursorY = Console.CursorTop;
                Console.WriteLine("\t\t                                     |");
                Console.WriteLine("\t\t|____________________________________________________________|");
                Console.SetCursorPosition(accNocursorX, accNocursorY);

                string searchedNumberString = Console.ReadLine();
                try
                {
                    int searchedNumber = Convert.ToInt32(searchedNumberString);
                    Account account = getAccount(searchedNumber);
                    if (account != null)
                    {

                        Console.Write("\n\n\t\t  Account Found! Enter the amount...");
                        int cursorX = Console.CursorLeft;
                        int cursorY = Console.CursorTop;
                        Console.SetCursorPosition(amountCursorX, amountCursorY);
                        string amountString = Console.ReadLine();
                        Console.SetCursorPosition(cursorX, cursorY);
                        try
                        {
                            double amount = Convert.ToDouble(amountString);
                            account.deposit(amount);
                            Console.Write("\n\n\t\t  Deposit successfull!");
                            Console.Write("\n\t\t  Press any key to continue...");
                            Console.ReadKey();
                            break;
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                    else
                    {
                        Console.WriteLine("\n\n\t\t  Account not found!");
                        Console.Write("\t\t  Retry (y/n)? ");
                        notDone = Console.ReadLine();
                    }
                }
                catch (FormatException)
                {
                    Console.Write("\n\n\t\t  Please enter a valid account number.");
                    Console.Write("\n\t\t  Would you like to search for another account (y/n)? ");
                    notDone = Console.ReadLine();
                }
                catch (OverflowException)
                {
                    Console.Write("\n\n\t\t  Please enter an account number under 11 digits.");
                    Console.Write("\n\t\t  Would you like to search for another account (y/n)? ");
                    notDone = Console.ReadLine();
                }

            } while (!notDone.Equals("n"));
        }

        /* withdraw() prompts the "WITHDRAW" function interface and allows the user to withdraw from an account.
         Furthermore, an error will occur if the user wishes to withdraw more than they can from an accuont. */
        public void withdraw()
        {
            string notDone = "y";
            do
            {
                Console.Clear();
                Program.printInterface("WITHDRAW");
                Console.WriteLine("\t\t|                     ENTER THE DETAILS                      |");
                Console.WriteLine("\t\t|                                                            |");
                Console.Write("\t\t|    Account Number: ");
                int accNocursorX = Console.CursorLeft;
                int accNocursorY = Console.CursorTop;
                Console.WriteLine("\t\t                             |");
                Console.Write("\t\t|    Amount: $");
                int amountCursorX = Console.CursorLeft;
                int amountCursorY = Console.CursorTop;
                Console.WriteLine("\t\t                                     |");
                Console.WriteLine("\t\t|____________________________________________________________|");
                Console.SetCursorPosition(accNocursorX, accNocursorY);

                string searchedNumberString = Console.ReadLine();
                try
                {
                    int searchedNumber = Convert.ToInt32(searchedNumberString);
                    Account account = getAccount(searchedNumber);
                    if (account != null)
                    {

                        Console.Write("\n\n\t\t  Account Found! Enter the amount...");
                        int cursorX = Console.CursorLeft;
                        int cursorY = Console.CursorTop;
                        Console.SetCursorPosition(amountCursorX, amountCursorY);
                        string amountString = Console.ReadLine();
                        Console.SetCursorPosition(cursorX, cursorY);
                        try
                        {
                            double amount = Convert.ToDouble(amountString);
                            if (account.canWithdraw(amount))
                            {
                                account.withdraw(amount);
                                Console.Write("\n\n\t\t  Withdrawal successfull!");
                                Console.Write("\n\t\t  Press any key to continue...");
                                Console.ReadKey();
                                break;
                            }
                            else
                            {
                                Console.Write("\n\t\t  Insufficient funds in account.");
                                Console.Write("\n\n\t\t  Press any key to re-enter amount to withdraw...");
                                Console.ReadKey();
                            }

                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                    else
                    {
                        Console.WriteLine("\n\n\t\t  Account not found!");
                        Console.Write("\t\t  Retry (y/n)? ");
                        notDone = Console.ReadLine();
                    }
                }
                catch (FormatException)
                {
                    Console.Write("\n\n\t\t  Please enter a valid account number.");
                    Console.Write("\n\t\t  Would you like to search for another account (y/n)? ");
                    notDone = Console.ReadLine();
                }
                catch (OverflowException)
                {
                    Console.Write("\n\n\t\t  Please enter an account number under 11 digits.");
                    Console.Write("\n\t\t  Would you like to search for another account (y/n)? ");
                    notDone = Console.ReadLine();
                }
            } while (!notDone.Equals("n"));
        }

        /* showStatement() prompts the "STATEMENT" function interface and allows the user to display an account's banking details, including their last 5 transactions. */
        public void showStatement()
        {
            string notDone = "y";
            do
            {
                Console.Clear();
                Program.printInterface("STATEMENT");
                Console.WriteLine("\t\t|                     ENTER THE DETAILS                      |");
                Console.WriteLine("\t\t|                                                            |");
                Console.Write("\t\t|    Account Number: ");
                int cursorX = Console.CursorLeft;
                int cursorY = Console.CursorTop;
                Console.WriteLine("\t\t                             |");
                Console.WriteLine("\t\t|____________________________________________________________|");
                Console.SetCursorPosition(cursorX, cursorY);
                string searchedNumberString = Console.ReadLine();
                try
                {
                    int searchedNumber = Convert.ToInt32(searchedNumberString);
                    Account account = getAccount(searchedNumber);
                    if (account != null)
                    {
                        Console.WriteLine("\n\n\t\tAccount found! The statement is displayed below...");
                        Program.printInterface("SIMPLE BANKING SYSTEM");
                        Console.WriteLine("\t\t|    Account Statement                                       |");
                        account.displayAccountStatement();
                        Console.Write("\n\t\t  Email statement (y/n)? ");
                        if (Console.ReadLine() == "y")
                        {
                            account.sendEmailStatement();
                            Console.Write("\n\t\t  Email sent successfully!...");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\n\n\t\t  Account not found!");
                    }
                    Console.Write("\n\n\t\t  Check another account (y/n)? ");
                    notDone = Console.ReadLine();
                }
                catch (FormatException)
                {
                    Console.Write("\n\n\t\t  Please enter a valid account number.");
                    Console.Write("\n\t\t  Would you like to search for another account (y/n)? ");
                    notDone = Console.ReadLine();
                }
                catch (OverflowException)
                {
                    Console.Write("\n\n\t\t  Please enter an account number under 11 digits.");
                    Console.Write("\n\t\t  Would you like to search for another account (y/n)? ");
                    notDone = Console.ReadLine();
                }
            } while (!notDone.Equals("n"));
        }

        /* deleteAccount() prompts the "DELETE AN ACCOUNT" function interface and allows the user to delete an account from the banking system database. */
        public void deleteAccount()
        {
            string notDone = "y";
            do
            {
                Console.Clear();
                Program.printInterface("DELETE AN ACCOUNT");
                Console.WriteLine("\t\t|                     ENTER THE DETAILS                      |");
                Console.WriteLine("\t\t|                                                            |");
                Console.Write("\t\t|    Account Number: ");
                int cursorX = Console.CursorLeft;
                int cursorY = Console.CursorTop;
                Console.WriteLine("\t\t                             |");
                Console.WriteLine("\t\t|____________________________________________________________|");
                Console.SetCursorPosition(cursorX, cursorY);
                string searchedNumberString = Console.ReadLine();
                try
                {
                    int searchedNumber = Convert.ToInt32(searchedNumberString);
                    Account account = getAccount(searchedNumber);
                    if (account != null)
                    {
                        Console.WriteLine("\n\n\t\tAccount found! Details displayed below...");
                        Program.printInterface("ACCOUNT DETAILS");
                        account.displayAccountDetails();
                        Console.Write("\n\t\t  Delete (y/n)? ");
                        if (Convert.ToChar(Console.ReadLine()) == 'y')
                        {
                            accounts.Remove(account);
                            File.Delete("../../../Accounts/" + account.getAccountNumber() + ".txt");
                            Console.Write("\n\t\t  Account Deleted!");
                            Console.Write("\n\t\t  Press any key to continue...");
                            Console.ReadKey();
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("\n\n\t\tAccount not found!");
                        Console.Write("\t\tSearch for another account (y/n)? ");
                        notDone = Console.ReadLine();
                    }
                }
                catch (FormatException)
                {
                    Console.Write("\n\n\t\t Please enter a valid account number.");
                    Console.Write("\n\t\t Would you like to search for another account (y/n)? ");
                    notDone = Console.ReadLine();
                }
                catch (OverflowException)
                {
                    Console.Write("\n\n\t\t Please enter an account number under 11 digits.");
                    Console.Write("\n\t\t Would you like to search for another account (y/n)? ");
                    notDone = Console.ReadLine();
                }
            } while (!notDone.Equals("n"));
        }

        private double readDouble(int i)
        {
            string[] stringArr = allText[i].Split('|');
            return Convert.ToDouble(stringArr[1]);
        }

        private int readInt(int i)
        {
            string[] stringArr = allText[i].Split('|');
            return Convert.ToInt32(stringArr[1]);
        }

        private String readString(int i)
        {
            string[] stringArr = allText[i].Split('|');
            return stringArr[1];
        }

        /* createAccount() prompts the "CReATE A NEW ACCOUNT" function interface and allows the user to enter details to create a new bank account. */
        public void createAccount()
        {
            bool error = false;
            string correct;
            string userEmail = "temp";
            string userFirstName, userLastName, userAddress, userPhoneString;
            char[] phoneArr;

            do
            {
                Console.Clear();
                Program.printInterface("CREATE A NEW ACCOUNT");
                Console.WriteLine("\t\t|                     ENTER THE DETAILS                      |");
                Console.WriteLine("\t\t|                                                            |");
                Console.Write("\t\t|    First Name: ");

                int firstCursorX = Console.CursorLeft;
                int firstCursorY = Console.CursorTop;
                Console.Write("                                            |");

                Console.Write("\n\t\t|    Last Name: ");
                int lastCursorX = Console.CursorLeft;
                int lastCursorY = Console.CursorTop;
                Console.Write("                                             |");

                Console.Write("\n\t\t|    Address: ");
                int addressCursorX = Console.CursorLeft;
                int addressCursorY = Console.CursorTop;
                Console.Write("                                               |");

                Console.Write("\n\t\t|    Phone: ");
                int phoneCursorX = Console.CursorLeft;
                int phoneCursorY = Console.CursorTop;
                Console.Write("                                                 |");

                Console.Write("  \n\t\t|    Email: ");
                int emailCursorX = Console.CursorLeft;
                int emailCursorY = Console.CursorTop;
                Console.Write("                                                 |");

                Console.WriteLine("\n\t\t|____________________________________________________________|");

                Console.SetCursorPosition(firstCursorX, firstCursorY);
                userFirstName = Console.ReadLine();
                Console.SetCursorPosition(lastCursorX, lastCursorY);
                userLastName = Console.ReadLine();
                Console.SetCursorPosition(addressCursorX, addressCursorY);
                userAddress = Console.ReadLine();
                Console.SetCursorPosition(phoneCursorX, phoneCursorY);
                string tempPhoneString = Console.ReadLine();
                userPhoneString = String.Concat(tempPhoneString.Where(c => !Char.IsWhiteSpace(c))); //remove all whitespaces
                phoneArr = userPhoneString.ToCharArray();
                try
                {
                    int userPhone = Convert.ToInt32(userPhoneString);
                }
                catch (FormatException)
                {
                    Console.WriteLine("\n\n\n\t\t You must enter a valid phone number.");
                    error = true;
                    break;
                }
                catch (OverflowException)
                {
                    Console.WriteLine("\n\n\n\t\t You must enter a valid phone number under 11 digits.");
                    error = true;
                    break;
                }

                Console.SetCursorPosition(emailCursorX, emailCursorY);
                userEmail = Console.ReadLine();
                Console.Write("\n\n\t\t Is this information correct (y/n)? ");
                correct = Console.ReadLine();
            } while (!correct.Equals("y"));

            if (verifyDetails(userFirstName, userLastName, userAddress, userPhoneString, userEmail))
            {
                Account account = new Account(userFirstName, userLastName, userAddress, userPhoneString, userEmail);
                accounts.Add(account);
                Console.Write("\n\t\t Account Created! Details will be provided via email.");
                Console.Write("\n\t\t Account number is: {0}", account.getAccountNumber());
                Console.Write("\n\n\t\t Press any key to continue...");
                Console.ReadKey();
            }
            else if (error)
            {
                Console.Write("\n\t\t Press any key to continue...");
                Console.ReadKey();
            }
            else
            {
                Console.Write("\n\t\t Please enter information in a valid format.");
                Console.Write("\n\n\t\t Press any key to continue...");
                Console.ReadKey();
            }
        }

        /* verifyDetails(string userFirstName, string userLastName, string userAddress, int userPhone, string userEmail) is used by createAccount(),
         * to verify that the information inputted by the user is in the valid format. */
        private bool verifyDetails(string userFirstName, string userLastName, string userAddress, string userPhone, string userEmail)
        {
            if (Regex.IsMatch(userFirstName, "^[a-zA-Z]+$")
                && Regex.IsMatch(userLastName, "^[a-zA-Z]+$")
                && Regex.IsMatch(userAddress, @"^(?=.{1,}$)[A-Za-z0-9-,]+(?:\s[A-Za-z0-9-,]+)*$")
                && userPhone.Length <= 10
                && Regex.IsMatch(userEmail, "^(?=.{1,}$)[A-Za-z0-9-,.]+@(?:[A-Za-z0-9-,.]+)*$"))
            {
                return true;
            }
            else
            {
                return false;
            }   
        }

        /* getAccount(int accountNumber) is used by multiple methods within the class Accounts to allow that respective method to access the details of a specific account */
        public Account getAccount(int accountNumber)
        {
            foreach (Account account in accounts)
            {
                if (accountNumber == account.getAccountNumber())
                {
                    return account;
                }
            }
            return null;
        }
    }
}