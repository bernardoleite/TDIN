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
        public enum Type { FOOD, DRINK }
        public Type TypeProperty { get; set; }

        public Product(int Identifier, string Name, float Price, Type type){
            this.Id = Identifier;
            this.Name = Name;
            this.Price = Price;
            this.TypeProperty = type;
        }
    }

}