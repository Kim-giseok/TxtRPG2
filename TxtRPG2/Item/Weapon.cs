using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG2
{
    internal interface IWeapon
    {
        public int Atk { get; }
    }

    internal class Weapon : Item, IWeapon
    {
        public int Atk { get; }

        public Weapon(string name, int atk, string description, int price) : base(name, description, price)
        {
            Atk = atk;
        }

        public Weapon(Weapon weapon) : base(weapon)
        {
            Atk = weapon.Atk;
        }

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
            else
            {
                Console.WriteLine();
            }
        }
    }
}
