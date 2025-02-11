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
        public virtual bool IsSold { get; set; }

        public Item(string name, string description, int price)
        {
            Name = name;
            Description = description;
            Price = price;
            IsSold = false;
        }

        public Item(Item item, bool copy = false)
        {
            Name = item.Name;
            Description = item.Description;
            Price = copy ? item.Price : item.Price * 80 / 100;
            IsSold = false;
        }

        public virtual void ApearInfo(bool sell = false)
        {
            int sl = Console.CursorLeft;
            Console.Write($"{Name}");
            Console.SetCursorPosition(sl + 15, Console.CursorTop);
            Console.Write($" |\t\t+\t|");
            sl = Console.CursorLeft;
            Console.Write($" {Description}");
            Console.SetCursorPosition(sl + 50, Console.CursorTop);
            if (sell)
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
            else
            {
                Console.WriteLine();
            }
        }
    }
}
