using System;

namespace Common
{
    [Serializable]
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public enum Type { Food, Drink };
    }
}