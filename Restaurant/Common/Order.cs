using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [Serializable]
    public class Order
    {
        public int type { get; set; }
        public int tableId { get; set; }
        public Product product { get; set; }
        public int quantity { get; set; }
        public int totalPrice { get; set; }
        public enum State { Not_Processed, Processing, Finished, Delivered, Closed }
        public State StateProperty { get; set; }

        public Order(int type, int tableId, Product product, int qty)
        {
            Type = type;
            this.tableId = tableId;
            this.product = product;
            this.quantity = qty;
            
        }

        public int Type
        {
            get
            {
                return type;
            }
            set
            {
                if (value < 0)
                    type = 999;
                else
                    type = value;
            }
        }


    }


}