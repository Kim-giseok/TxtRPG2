using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG2
{
    internal class Potion : Item
    {
        public override bool IsSold { get => false; }
        public int count { get; set; }

        public Potion(string name, string description, int price) : base(name, description, price)
        {
            count = 1;
        }

        public Potion(Potion potion, bool copy = false) : base(potion, copy)
        {
            count = potion.count;
        }

        public virtual void Use(Player player)
        {
            if (count == 0)
            {
                Console.WriteLine("포션이 없습니다.");
                return;
            }
            Console.WriteLine($"{player.Name}은 {Name}을 사용했습니다.");
            count--;
        }

        public override void ApearInfo(bool sell = false)
        {
            int sl = Console.CursorLeft;
            base.ApearInfo(sell);
            if (!sell)
            {
                Console.SetCursorPosition(sl + 80, Console.CursorTop - 1);
                Console.WriteLine($"\t| {count}");
            }
        }
    }
}
