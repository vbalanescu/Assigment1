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
    public partial class Orders : Form
    {
        internal bool Admin;
        internal User user;
        private IList<Order> ordersList;

        public Orders()
        {
            InitializeComponent();
            dgvOrders.AutoGenerateColumns = false;
        }

        public Orders(IList<Order> ordersList)
        {
            this.ordersList = ordersList;
            Admin = true;
            InitializeComponent();
            dgvOrders.AutoGenerateColumns = false;
        }

        private void Orders_Load(object sender, EventArgs e)
        {
                if (Admin)
                    dgvOrders.DataSource = ordersList;
                else
                {
                    UserOperations bl = new UserOperations();
                    dgvOrders.DataSource = bl.GetOrdersForUser();
                }
        }

    }
}
