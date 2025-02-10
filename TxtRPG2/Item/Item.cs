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
        public string Description { get; }
        public int Price { get; }
        public bool IsSold { get; set; }

        public Item(string name, string description, int price)
        {
            Name = name;
            Description = description;
            Price = price;
            IsSold = false;
        }

        public Item(Item item)
        {
            Name = item.Name;
            Description = item.Description;
            Price = item.Price;
            IsSold = false;
        }

        public virtual void ApearInfo(bool inShop = false)
        {
            Console.Write($"{Name}\t| \t\t | {Description}");
            if (inShop)
            {
                if (IsSold)
                {
                    Console.WriteLine($"\t| 판매완료");
                }
                else
                {
                    Console.WriteLine($"\t| {Price} G");
                }
            }
        }
    }
}
