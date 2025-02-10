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

        public override void ApearInfo(bool sell = false)
        {
            int sl = Console.CursorLeft;
            base.ApearInfo(sell);
            Console.SetCursorPosition(sl + 18, Console.CursorTop - 1);
            Console.Write($"공격력\t+ {Atk}");
            Console.SetCursorPosition(0, Console.CursorTop + 1);
        }
    }
}
