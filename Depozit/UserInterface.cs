using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Depozit.Models;
using Depozit.BL;
using Depozit.DAL;
namespace Depozit
{

    public partial class UserInterface : Form
    {
        public User user;
        IList<Product> list;
        public UserInterface()
        {
            InitializeComponent();
            DataAccess d = new DataAccess();
            list = d.GetProductsForUser();
            
            foreach (Product p in list)
            comboBox1.Items.Add(p.Name);
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Products products = new Products();
            products.user = user;
            products.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Orders orders = new Orders();
            orders.user = user;
            orders.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Product p = new Product();
                p.Name = textBox1.Text;
                p.Price = Int32.Parse(textBox2.Text);
                p.Stoc = Int32.Parse(textBox3.Text);
                p.Size = Int32.Parse(textBox4.Text);

                UserOperations bl = new UserOperations();
                bl.AddProduct(p);

                MessageBox.Show("Operation succesful");
            }
            catch (Exception) { MessageBox.Show("Introduceti datele corect!"); }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {

                if (textBox1.Text == "") {
                    MessageBox.Show("Introduceti datele corect!");
                    return;
                }
                UserOperations bl = new UserOperations();
                bl.DeleteProduct(textBox1.Text);
                MessageBox.Show("Operation succesful");
            }
            catch (Exception) { MessageBox.Show("Introduceti datele corect!"); }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                Product p = new Product();
                p.Name = textBox1.Text;
                p.Price = Int32.Parse(textBox2.Text);
                p.Stoc = Int32.Parse(textBox3.Text);
                p.Size = Int32.Parse(textBox4.Text);

                UserOperations bl = new UserOperations();
                bl.UpdateProduct(p);

                MessageBox.Show("Operation succesful");
            }
            catch (Exception) { MessageBox.Show("Introduceti datele corect!"); }
        }

        private void button6_Click(object sender, EventArgs e)

        {
            try
            {
                Order o = new Order();
                o.Customer = textBox6.Text;
                o.ShippingDate = this.dtpDateOfBirth.Value;
                o.address = textBox8.Text;
                o.Status = textBox9.Text;
                o.Product = list.ElementAt(comboBox1.SelectedIndex);
                o.Size = Int32.Parse(textBox10.Text);
                o.user = user;
                UserOperations bl = new UserOperations();

                if (bl.AddOrder(o))
                    MessageBox.Show("Operation succesful");
                else
                    MessageBox.Show("Stoc Insuficient!");
            }
            catch (Exception) { MessageBox.Show("Introduceti datele corect!"); }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {

                if (textBox5.Text == "")
                {
                    MessageBox.Show("Introduceti datele corect!");
                    return;
                }
                UserOperations bl = new UserOperations();
                bl.DeleteOrder(Int32.Parse(textBox5.Text));
                MessageBox.Show("Operation succesful");
            }
            catch (Exception) { MessageBox.Show("Introduceti datele corect!"); }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                Order o = new Order();
                o.ID = Int32.Parse(textBox5.Text);
                o.Customer = textBox6.Text;
                o.ShippingDate = this.dtpDateOfBirth.Value;
                o.address = textBox8.Text;
                o.Status = textBox9.Text;
                o.Product = list.ElementAt(comboBox1.SelectedIndex);
                o.Size = Int32.Parse(textBox10.Text);
                o.user = user;
                UserOperations bl = new UserOperations();

                if (bl.EditOrder(o))
                    MessageBox.Show("Operation succesful");
                else
                    MessageBox.Show("Stoc Insuficient!");
            }
            catch (Exception) { MessageBox.Show("Introduceti datele corect!"); }
        }
    }
}
