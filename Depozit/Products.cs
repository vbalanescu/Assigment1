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
    public partial class Products : Form
    {
        internal User user;


        public Products()
        {
            InitializeComponent();
            dgvProducts.AutoGenerateColumns = false;
        }

        private void Products_Load(object sender, EventArgs e)
        {
                UserOperations bl = new UserOperations();
                dgvProducts.DataSource = bl.GetProductsForUser();
        }
    }
}
