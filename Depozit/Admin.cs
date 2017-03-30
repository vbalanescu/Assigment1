using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Depozit.BL;
using Depozit.Models;
using Depozit.DAL;

namespace Depozit
{
    public partial class Admin : Form
    {
        IList<User> list;
        IList<Order> OrdersList;
        public Admin()
        {

            InitializeComponent();
            DataAccess d = new DataAccess();
            list = d.GetUsers();

            foreach (User u in list)
            comboBox1.Items.Add(u.ID);
            comboBox1.SelectedIndex = 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            User user = new User();
            user.UserName = txtUserName.Text;
            user.Password = txtPassword.Text;
            user.firstName = txtFirstName.Text;
            user.lastName = txtFirstName.Text;
            user.IsAdmin = chkAdmin.Checked;
            user.DateOfBirth = dtpDateOfBirth.Value;

            UserOperations bl = new UserOperations();
            bl.AddUser(user);

            MessageBox.Show("Operation succesful");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataAccess d = new DataAccess();
            OrdersList = d.GetOrdersById(Int32.Parse(comboBox1.Text));

            Orders orders = new Orders(OrdersList);
            orders.Show();
        }
    }
}
