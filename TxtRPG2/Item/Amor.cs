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

        public override void ApearInfo(bool sell = false)
        {
            int sl = Console.CursorLeft;
            base.ApearInfo(sell);
            Console.SetCursorPosition(sl + 18, Console.CursorTop - 1);
            Console.Write($"방어력\t+ {Def}");
            Console.SetCursorPosition(0, Console.CursorTop + 1);
        }
    }
}
