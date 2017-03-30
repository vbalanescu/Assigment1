using System;

namespace Depozit.Models
{
    public class Order
    {
        public int ID { get; set; }
        public string Customer { get; set; }
        public User user { get; set; }
        public string Status { get; set; }
        public int Size { get; set; }
        public string address { get; set; }
        public Product Product { get; set; }
        public int IdP { get; set; }
        public DateTime ShippingDate{ get; set; }
        public Order() { }
    }
}
