using Depozit.BL;
using Depozit.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Depozit
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            txtPassword.PasswordChar = '*';
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            UserOperations bl = new UserOperations();
            try { 
            User user = bl.Login(txtUser.Text, txtPassword.Text);

                if (user.IsAdmin)
                {
                    Admin adminForm = new Admin();
                    adminForm.Show();
                }
                else
                {
                    UserInterface ui = new UserInterface();
                    ui.user = user;
                    ui.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Date de logare invalide");
            }
        }
    }
}
