using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG2
{
    internal class Weapon : Item
    {
        public Weapon(string name, int atk, string description, int price, int isSoid) : base(name, atk, 0, description, price)
        { }

        public override void ApearInfo(bool inShop = false)
        {
            Console.Write($"{Name}\t| 공격력 + {Atk} | {Description}");
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
