using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homiepet_Corner_Sales_and_Inventory_Management_System.Classes
{
    public static class UserCredentials
    {
        public static string Username { get; set; } = "admin";

        public static string Password
        {
            get => Properties.Settings.Default.Password;
            set
            {
                Properties.Settings.Default.Password = value;
                Properties.Settings.Default.Save();
            }
        }
    }

}
