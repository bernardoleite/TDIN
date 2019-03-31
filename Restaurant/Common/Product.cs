using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [Serializable]
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public enum Type { Food, Drink };

        public Product(int Identifier, string Name, float Price){
            this.Id = Identifier;
            this.Name = Name;
            this.Price = Price;
        }
    }

}