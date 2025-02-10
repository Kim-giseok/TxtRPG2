using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG2
{
    internal interface IArmor
    {
        public int Def { get; }
    }

    internal class Amor : Item, IArmor
    {
        public int Def { get; }

        public Amor(string name, int def, string description, int price) : base(name, description, price)
        {
            Def = def;
        }

        public Amor(Amor amor) : base(amor)
        {
            Def = amor.Def;
        }

        public override void ApearInfo(bool inShop = false)
        {
            Console.Write($"{Name}\t| 방어력 + {Def} | {Description}");
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
            else
            {
                Console.WriteLine();
            }
        }
    }
}
