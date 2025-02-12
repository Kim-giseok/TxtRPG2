using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG2
{
    internal class Item
    {
        public static Item[] items = { };

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

        public Item(Item item)
        {
            Name = item.Name;
            Description = item.Description;
            Price = item.Price;
            IsSold = false;
        }

        public enum ApearMode { Idle, Buy, Sell };
        public virtual void ApearInfo(ApearMode mode = ApearMode.Idle)
        {
            int sl = Console.CursorLeft;
            Console.Write($"{Name}");
            Console.SetCursorPosition(sl + 15, Console.CursorTop);
            Console.Write($" |\t\t+\t|");
            sl = Console.CursorLeft;
            Console.Write($" {Description}");
            Console.SetCursorPosition(sl + 50, Console.CursorTop);
            switch (mode)
            {
                case ApearMode.Idle:
                    Console.WriteLine();
                    break;
                case ApearMode.Buy:
                    if (IsSold)
                    {
                        Console.WriteLine($"\t| 판매완료");
                    }
                    else
                    {
                        Console.WriteLine($"\t| {Price} G");
                    }
                    break;
                case ApearMode.Sell:
                    Console.WriteLine($"\t| {Price * 80 / 100} G");
                    break;
            }
        }
    }
}
