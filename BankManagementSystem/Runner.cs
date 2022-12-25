using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem
{
    public class Runner
    {
        public static void Main(String[] args)
        {
            Login login = new Login();
            login.loginScreen();
        }
    }
}
