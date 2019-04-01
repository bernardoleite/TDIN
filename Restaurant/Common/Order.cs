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
        public int TableId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public int TotalPrice { get; set; }
        public enum State { Not_Processed, Processing, Finished, Delivered, Closed }
        public State StateProperty { get; set; }

        public Order(int type, int tableId, Product product, int qty)
        {
            Type = type;
            this.TableId = tableId;
            this.Product = product;
            this.Quantity = qty;   
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