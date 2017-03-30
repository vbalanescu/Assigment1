using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Depozit.DAL;
using Depozit.Models;
using System.Configuration;

namespace Depozit.BL
{
    public class UserOperations
    {
        public User Login(string userName, string password)
        {
            DataAccess dal = new DataAccess();
            User user = dal.GetUser(userName);
            if (user!=null)
            {
                Security secure = new Security();
                if(secure.VerifyHash(password, user.Password))
                {
                    return user;
                }
            }
            return null;
        }

        public void AddUser(User user)
        {
            Security secure = new Security();
            user.Password = secure.HashSHA1(user.Password);

            DataAccess dal = new DataAccess();
            dal.AddUser(user);
        }

        public IList<Product> GetProductsForUser()
        {
            DataAccess dal = new DataAccess();
            return dal.GetProductsForUser();
        }

        public IList<Order> GetOrdersForUser()
        {
            DataAccess dal = new DataAccess();
            return dal.GetOrdersForUser();
        }

        public void AddProduct(Product p)
        {
            DataAccess dal = new DataAccess();
            dal.AddProduct(p);
        }

        public bool AddOrder(Order o)
        {
            if (o.Product.Stoc >= o.Size)
            {
                DataAccess dal = new DataAccess();
                dal.AddOrder(o);
                o.Product.Stoc -= o.Size;
                dal.UpdateProduct(o.Product);
            }
            else
                return false;
            return true;
        }

        public bool EditOrder(Order o)
        {
            DataAccess dal = new DataAccess();
            dal.EditOrder(o);
            //o.Product.Stoc -= o.Size;
            //dal.UpdateProduct(o.Product);
            
            return true;
        }

        public void DeleteProduct(string p)
        {
            DataAccess dal = new DataAccess();
            dal.DeleteProduct(p);
        }

        public void DeleteOrder(int id)
        {
            DataAccess dal = new DataAccess();
            dal.DeleteOrder(id);
        }

        public void UpdateProduct(Product p)
        {
            DataAccess dal = new DataAccess();
            dal.UpdateProduct(p);
        }

       
    }
}
