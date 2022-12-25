using System;
using System.Net;
using System.Net.Mail;

namespace BankManagementSystem
{

    /* The class Account is where the details of each bank account is stored.
     * The class creates accounts while also creating a text file where all the account's details are stored.
     * Furthermore, the class is responsible for handling transactions (deposits, withdrawals) */
    public class Account
    {
        private int accountNumber;

        private string userFirstName, userLastName, userAddress, userEmail, userPhone;
        private double accountBalance;
        private List<String> transactionArr;

        public Account(int accountNumber, double accountBalance, string userFirstName, string userLastName, string userAddress, string userPhone, string userEmail, List<String> transactionArr)
        {
            this.accountNumber = accountNumber;
            this.userFirstName = userFirstName;
            this.userLastName = userLastName;
            this.userAddress = userAddress;
            this.userEmail = userEmail;
            this.userPhone = userPhone;
            this.accountBalance = accountBalance;
            this.transactionArr = transactionArr;
        }

        public Account(int accountNumber, double accountBalance, string userFirstName, string userLastName, string userAddress, string userPhone, string userEmail)
        {
            this.accountNumber = accountNumber;
            this.userFirstName = userFirstName;
            this.userLastName = userLastName;
            this.userAddress = userAddress;
            this.userEmail = userEmail;
            this.userPhone = userPhone;
            this.accountBalance = accountBalance;
        } 

        public Account(string userFirstName, string userLastName, string userAddress, string userPhone, string userEmail)
        {
            this.userFirstName = userFirstName;
            this.userLastName = userLastName;
            this.userAddress = userAddress;
            this.userPhone = userPhone;
            this.userEmail = userEmail;
            accountBalance = 0;
            transactionArr = new List<string>();

            List<String> filePaths = Directory.EnumerateFiles("../../../Accounts").ToList();
            accountNumber = 100001;
            foreach (string filePath in filePaths)
            {
                if (filePath.Contains(Convert.ToString(accountNumber)))
                {
                    accountNumber++;
                }
                else
                {
                    continue;
                }
            }
            createAccountFile();
        }

        public void displayAccountStatement()
        {
            string statementAccNo = "Account No: " + accountNumber;
            string statementBalance = "Account Balance: " + accountBalance.ToString("C2");
            string statementFirstName = "First Name: " + userFirstName;
            string statementLastName = "Last Name: " + userLastName;
            string statementAddress = "Adress: " + userAddress;
            string statementPhone = "Phone: " + userPhone;
            string statementEmail = "Email: " + userEmail;
            Console.WriteLine("\t\t|                                                            |");
            Console.WriteLine("\t\t|    " + statementAccNo + new string(' ', 56 - statementAccNo.Length) + "|");
            Console.WriteLine("\t\t|    " + statementBalance + new string(' ', 56 - statementBalance.Length) + "|");
            Console.WriteLine("\t\t|    " + statementFirstName + new string(' ', 56 - statementFirstName.Length) + "|");
            Console.WriteLine("\t\t|    " + statementLastName + new string(' ', 56 - statementLastName.Length) + "|");
            Console.WriteLine("\t\t|    " + statementAddress + new string(' ', 56 - statementAddress.Length) + "|");
            Console.WriteLine("\t\t|    " + statementPhone + new string(' ', 56 - statementPhone.Length) + "|");
            Console.WriteLine("\t\t|    " + statementEmail + new string(' ', 56 - statementEmail.Length) + "|");
            if (transactionArr != null)
            {
                Console.WriteLine("\t\t|                                                            |");
                Console.WriteLine("\t\t|                                                            |");
                Console.WriteLine("\t\t|    Last 5 transactions...                                  |");
                Console.WriteLine("\t\t|                                                            |");

                int count = 0;
                int i = transactionArr.Count  - 1;
                while (i != -1 && count != 5)
                {
                    string[] statementTransaction = transactionArr[i].Split('|');
                    if (statementTransaction[1].Equals("Withdraw"))
                    {
                        statementTransaction[1] = " Withdrew:  ";
                    }
                    else if (statementTransaction[1].Equals("Deposit"))
                    {
                        statementTransaction[1] = " Deposited: ";
                        
                    }
                   
                    Console.WriteLine("\t\t|    " + statementTransaction[0]
                                                  + statementTransaction[1] 
                                                  + statementTransaction[2]
                                                  + new string(' ', 12 - statementTransaction[2].Length) + "Balance: " + statementTransaction[3]
                                                  + new string(' ', 13 - statementTransaction[3].Length) 
                                                  + "|");
                    count++;
                    i--;
                } 
            }
            Console.WriteLine("\t\t|____________________________________________________________|");

        }

        public void displayAccountDetails()
        {
            string statementAccNo = "Account No: " + accountNumber;
            string statementBalance = "Account Balance: " + accountBalance.ToString("C2");
            string statementFirstName = "First Name: " + userFirstName;
            string statementLastName = "Last Name: " + userLastName;
            string statementAddress = "Adress: " + userAddress;
            string statementPhone = "Phone: " + userPhone;
            string statementEmail = "Email: " + userEmail;
            Console.WriteLine("\t\t|                                                            |");
            Console.WriteLine("\t\t|    " + statementAccNo + new string(' ', 56 - statementAccNo.Length) + "|");
            Console.WriteLine("\t\t|    " + statementBalance + new string(' ', 56 - statementBalance.Length) + "|");
            Console.WriteLine("\t\t|    " + statementFirstName + new string(' ', 56 - statementFirstName.Length) + "|");
            Console.WriteLine("\t\t|    " + statementLastName + new string(' ', 56 - statementLastName.Length) + "|");
            Console.WriteLine("\t\t|    " + statementAddress + new string(' ', 56 - statementAddress.Length) + "|");
            Console.WriteLine("\t\t|    " + statementPhone + new string(' ', 56 - statementPhone.Length) + "|");
            Console.WriteLine("\t\t|    " + statementEmail + new string(' ', 56 - statementEmail.Length) + "|");
            Console.WriteLine("\t\t|____________________________________________________________|");

        }

        public bool canWithdraw(double amount)
        {
            return amount <= accountBalance;
        }

        public void withdraw(double amount)
        {
            accountBalance -= amount;


            string[] fileText = File.ReadAllLines("../../../Accounts/" + accountNumber + ".txt");
            fileText[1] = "Account Balance|" + accountBalance;
            File.WriteAllLines("../../../Accounts/" + accountNumber + ".txt", fileText);

            string today = DateTime.Now.ToString("dd-MM-yyyy");
            string delimiter = "|";
            File.AppendAllText("../../../Accounts/" + accountNumber + ".txt", today + delimiter + "Withdraw" + delimiter + amount.ToString("C2") + delimiter + accountBalance.ToString("C2"));

            transactionArr.Add(today + delimiter + "Withdraw" + delimiter + amount.ToString("C2") + delimiter + accountBalance.ToString("C2"));
        }

        public void deposit(double amount)
        {

            accountBalance += amount;

            string[] fileText = File.ReadAllLines("../../../Accounts/" + accountNumber + ".txt");
            fileText[1] = "Account Balance|" + accountBalance;
            File.WriteAllLines("../../../Accounts/" + accountNumber + ".txt", fileText);

            string today = DateTime.Now.ToString("dd-MM-yyyy");
            string delimiter = "|";
            File.AppendAllText("../../../Accounts/" + accountNumber + ".txt", today + delimiter + "Deposit" + delimiter + amount.ToString("C2") + delimiter + accountBalance.ToString("C2"));

            transactionArr.Add(today + delimiter + "Deposit" + delimiter + amount.ToString("C2") + delimiter + accountBalance.ToString("C2"));
        }

        private void createAccountFile()
        {
            string filePath = "../../../Accounts/" + accountNumber + ".txt";
            string delimiter = "|";

            string fileAccountNumber = "AccountNo" + delimiter + accountNumber;
            string fileAccountBalance = "\nAccount Balance" + delimiter + Math.Round(accountBalance, 2);
            string fileFirstName = "\nFirst Name" + delimiter + userFirstName;
            string fileLastName = "\nLast Name" + delimiter + userLastName;
            string fileAddress = "\nAddress" + delimiter + userAddress;
            string filePhone = "\nPhone" + delimiter + userPhone;
            string fileEmail = "\nEmail" + delimiter + userEmail;

            sendEmail();

            File.WriteAllText(filePath, fileAccountNumber + fileAccountBalance + fileFirstName + fileLastName + fileAddress + filePhone + fileEmail);
        }

        public int getAccountNumber()
        {
            return this.accountNumber;
        }

        //Sends an email to the user's email address they entered into the system. The contents of the email are the user's account details
        private void sendEmail()
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("juice16642@gmail.com", "ptwixroogzwhoajz"),
                EnableSsl = true,
            };
            string emailBody = "AccountNo: " + accountNumber +
                                "\nAccount Balance: $" + Math.Round(accountBalance, 2) +
                                "\nFirst Name: " + userFirstName +
                                "\nLast Name: " + userLastName +
                                "\nAddress: " + userAddress +
                                "\nPhone: " + userPhone +
                                "\nEmail: " + userEmail;
            try
            {
                smtpClient.Send("Bank Management System<juice16642@gmail.com>", userEmail, "Your Account Details", emailBody);
            }
            catch (Exception)
            {
                Console.Write("Email doesn't exist");
            }
        }

        public void sendEmailStatement()
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("juice16642@gmail.com", "ptwixroogzwhoajz"),
                EnableSsl = true,
            };
            string emailBody = "AccountNo: " + accountNumber +
                                "\nAccount Balance: $" + Math.Round(accountBalance, 2) +
                                "\nFirst Name: " + userFirstName +
                                "\nLast Name: " + userLastName +
                                "\nAddress: " + userAddress +
                                "\nPhone: " + userPhone +
                                "\nEmail: " + userEmail;
            if (transactionArr != null)
            {
                int count = 0;
                int i = transactionArr.Count - 1;
                while (i != -1 && count != 5)
                {
                    string[] statementTransaction = transactionArr[i].Split('|');
                    if (statementTransaction[1].Equals("Withdraw"))
                    {
                        statementTransaction[1] = " Withdrew:  ";
                    }
                    else if (statementTransaction[1].Equals("Deposit"))
                    {
                        statementTransaction[1] = " Deposited: ";

                    }
                    emailBody = emailBody + "\n" + statementTransaction[0] + new string(' ', 4)
                                                 + statementTransaction[1]
                                                 + statementTransaction[2]
                                                 + new string(' ', 12 - statementTransaction[2].Length) + "Balance: " + statementTransaction[3];
                    count++;
                    i--;
                }
            }

            try
            {
                smtpClient.Send("Bank Management System<juice16642@gmail.com>", userEmail, "Your Account Statement", emailBody);
            }
            catch (Exception)
            {
                Console.Write("Email doesn't exist");
            }
        }
    }
}