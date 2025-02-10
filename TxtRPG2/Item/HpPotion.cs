using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG2
{
    internal interface IHpPotion
    {
        public int Hp { get; }
    }

    internal class HpPotion : Item, IHpPotion
    {
        public int Hp { get; }

        public HpPotion(string name, int hp, string description, int price) : base(name, description, price)
        {
            Hp = hp;
        }

        public HpPotion(HpPotion potion) : base(potion)
        {
            Hp = potion.Hp;
        }

        public override void ApearInfo(bool inShop = false)
        {
            Console.Write($"{Name}\t| 체력회복 + {Hp} | {Description}");
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
