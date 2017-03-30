    using System;
using System.Collections.Generic;
using System.Configuration;
using Depozit.Models;
using MySql.Data.MySqlClient;

namespace Depozit.DAL
{
    public class DataAccess
    {
        private string connString;

        public DataAccess()
        {
            connString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        }

        public User GetUser(string userName)
        {

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string statement = "SELECT * FROM Users where UserName=\""+ userName +"\";";
                
                MySqlCommand cmd = new MySqlCommand(statement,conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    {
                        User user = new User();
                        user.ID = reader.GetInt32("Id");
                        user.UserName = reader.GetString("UserName");
                        user.Password = reader.GetString("Password");
                        user.firstName = reader.GetString("FirstName");
                        user.lastName = reader.GetString("LastName");
                        user.IsAdmin = reader.GetBoolean("IsAdmin");//
                        user.DateOfBirth = reader.GetDateTime("DateOfBirth");

                        return user;
                    }
                }
            }

            return null;
        }

        public IList<Order> GetOrdersById(int user)
        {
            IList<Order> orderList = new List<Order>();

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string statement = "SELECT * FROM orders where user = @id";

                MySqlCommand cmd = new MySqlCommand(statement, conn);
                cmd.Parameters.AddWithValue("@id", user);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Order order = new Order();
                        order.ID = reader.GetInt32("id");
                        order.Customer = reader.GetString("customer");
                        order.ShippingDate = reader.GetDateTime("shippingDate");
                        order.address = reader.GetString("address");
                        order.Status = reader.GetString("status");
                        order.Product = new Product();
                        order.Product.ID = reader.GetInt32("idP");
                        order.Size = reader.GetInt32("number");
                        order.user = new User();
                        order.IdP = order.Product.ID;
                        order.user.ID = reader.GetInt32("user");
                        orderList.Add(order);

                    }
                }
            }
            return orderList;
        }

        public IList<User> GetUsers()
        {
            IList<User> userList = new List<User>();

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string statement = "SELECT * FROM users;";

                MySqlCommand cmd = new MySqlCommand(statement, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        User user = new User();
                        user.ID = reader.GetInt32("Id");
                        user.UserName = reader.GetString("UserName");
                        userList.Add(user);
                    }
                }
            }
            return userList;
        }

        public IList<Order> GetOrdersForUser()
        {
            IList<Order> orderList = new List<Order>();

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string statement = "SELECT * FROM orders;";

                MySqlCommand cmd = new MySqlCommand(statement, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Order order = new Order();
                        order.ID = reader.GetInt32("id");
                        order.Customer = reader.GetString("customer");
                        order.ShippingDate = reader.GetDateTime("shippingDate");
                        order.address = reader.GetString("address");
                        order.Status = reader.GetString("status");
                        order.Product = new Product();
                        order.Product.ID = reader.GetInt32("idP");
                        order.Size = reader.GetInt32("number");
                        orderList.Add(order);
                        order.user = new User();
                        order.IdP = order.Product.ID;
                        order.user.ID = reader.GetInt32("user");
                    }
                }
            }
            return orderList;
        }

        public void AddProduct(Product p)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO products(name, price, stoc, size) VALUES(@name, @price, @stoc, @size)";
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@name", p.Name);
                cmd.Parameters.AddWithValue("@price", p.Price);
                cmd.Parameters.AddWithValue("@stoc", p.Stoc);
                cmd.Parameters.AddWithValue("@size", p.Size);

                cmd.ExecuteNonQuery();
            }
        }



        public void DeleteProduct(string name)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM `products` WHERE `products`.`name` = @name";
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@name", name);

                cmd.ExecuteNonQuery();
            }
        }


        public void DeleteOrder(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM `orders` WHERE `id` = @id";
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateProduct(Product p)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "UPDATE `products` SET `price` = @price, `stoc` = @stoc, `size` = @size WHERE `products`.`name` = @name;";
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@name", p.Name);
                cmd.Parameters.AddWithValue("@price", p.Price);
                cmd.Parameters.AddWithValue("@stoc", p.Stoc);
                cmd.Parameters.AddWithValue("@size", p.Size);

                cmd.ExecuteNonQuery();
            }
        }

        public void AddUser(User user)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO Users(UserName, Password, FirstName, LastName, IsAdmin, DateOfBirth) VALUES(@username, @password, @firstname, @lastname, @isadmin, @dateofbirth)";
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@username", user.UserName);
                cmd.Parameters.AddWithValue("@password", user.Password);
                cmd.Parameters.AddWithValue("@firstname", user.firstName);
                cmd.Parameters.AddWithValue("@lastname", user.lastName);
                cmd.Parameters.AddWithValue("@isadmin", user.IsAdmin);
                cmd.Parameters.AddWithValue("@dateofbirth", user.DateOfBirth);

                cmd.ExecuteNonQuery();
            }
        }



        public IList<Product> GetProductsForUser()
        {
            IList<Product> productList = new List<Product>();

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string statement = "SELECT * FROM products;";

                MySqlCommand cmd = new MySqlCommand(statement, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Product product = new Product();
                        product.ID = reader.GetInt32("id");
                        product.Name = reader.GetString("name");
                        product.Price = reader.GetInt32("price");
                        product.Stoc = reader.GetInt32("stoc");
                        product.Size = reader.GetInt32("size");
                        productList.Add(product);
                    }
                }
            }
            return productList;
        }
        public void AddOrder(Order order)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO orders(customer, shippingDate, address, status, idP, number, user) VALUES(@customer, @shippingDate, @address, @status, @idP, @number, @user)";
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@customer", order.Customer);
                cmd.Parameters.AddWithValue("@shippingDate", order.ShippingDate);
                cmd.Parameters.AddWithValue("@address", order.address);
                cmd.Parameters.AddWithValue("@status", order.Status);
                cmd.Parameters.AddWithValue("@idP", order.Product.ID);
                cmd.Parameters.AddWithValue("@number", order.Size);
                cmd.Parameters.AddWithValue("@user", order.user.ID);

                cmd.ExecuteNonQuery();
            }
        }
        public void EditOrder(Order order)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
             
                cmd.CommandText = "UPDATE `products` SET  `user` = @user, `idP` = @idP, `status` = @status, `customer` = @customer, `shippingDate` = @shippindDate, `address` = @address , `number` = @number WHERE `id` = @id";
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@id", order.ID);
                cmd.Parameters.AddWithValue("@customer", order.Customer);
                cmd.Parameters.AddWithValue("@shippingDate", order.ShippingDate);
                cmd.Parameters.AddWithValue("@address", order.address);
                cmd.Parameters.AddWithValue("@status", order.Status);
                cmd.Parameters.AddWithValue("@idP", order.Product.ID);
                cmd.Parameters.AddWithValue("@number", order.Size);
                cmd.Parameters.AddWithValue("@user", order.user.ID);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
