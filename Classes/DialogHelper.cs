using Guna.UI2.WinForms;
using Homiepet_Corner_Sales_and_Inventory_Management_System.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homiepet_Corner_Sales_and_Inventory_Management_System.Classes
{
    public class DialogHelper
    {
        public static void ShowInfo(string message, string title = "Information")
        {
            Guna2MessageDialog dialog = new Guna2MessageDialog
            {
                Style = MessageDialogStyle.Light,
                Buttons = MessageDialogButtons.OK,
                Caption = title,
                Text = message,
                Icon = MessageDialogIcon.Information
                

            };

            dialog.Show();
        }

        public static void ShowOk(string message, string title = "Message")
        {
            Guna2MessageDialog dialog = new Guna2MessageDialog
            {
                Style = MessageDialogStyle.Light,
                Buttons = MessageDialogButtons.OK,
                Caption = title,
                Text = message,
                Icon = MessageDialogIcon.None
            };

            dialog.Show();
        }

        public static bool ShowYesNo(string message, string title = "Confirm")
        {
            Guna2MessageDialog dialog = new Guna2MessageDialog
            {
                Style = MessageDialogStyle.Light,
                Buttons = MessageDialogButtons.YesNo,
                Caption = title,
                Text = message,
                Icon = MessageDialogIcon.Question
            };

            DialogResult result = dialog.Show();

            if (result == DialogResult.Yes)
            {
                // Do something when user clicks Yes
                // Example: log, close form, save data, etc.

                return true;
            }
            else
            {
                // Do something when user clicks No

                return false;
            }

        }
    }
}
