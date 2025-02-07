using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG2
{
    internal class Item
    {
        public string Name { get; }
        public int Atk { get; }
        public int Def { get; }
        public string Description { get; }
        public int Price { get; }
        public bool IsSold { get; }

        public Item (string name, int atk, int def, string description, int price)
        {
            Name = name;
            Atk = atk;
            Def = def;
            Description = description;
            Price = price;
            IsSold = false;
        }

        public virtual void ApearInfo() { }
    }
}
