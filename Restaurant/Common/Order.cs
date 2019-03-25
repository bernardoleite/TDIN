using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [Serializable]
    class Order
    {
        public int ID { get; set; }
        public Table table { get; set; }
        public Product product { get; set; }
        public int quantity { get; set; }
        public int totalPrice { get; set; }
        public enum State { Not_Processed, Processing, Finished, Delivered, Closed }

        public Order(Table table, Product product, int qty)
        {

        }

    }


}