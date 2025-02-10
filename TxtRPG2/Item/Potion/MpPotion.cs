using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG2
{
    internal interface IMpPotion
    {
        public int Mp { get; }
    }

    internal class MpPotion : Potion, IMpPotion
    {
        public int Mp { get; }

        public MpPotion(string name, int Mp, string description, int price) : base(name, description, price)
        {
            this.Mp = Mp;
        }

        public MpPotion(MpPotion potion) : base(potion)
        {
            Mp = potion.Mp;
        }

        public override void ApearInfo(bool sell = false)
        {
            int sl = Console.CursorLeft;
            base.ApearInfo(sell);
            Console.SetCursorPosition(sl + 18, Console.CursorTop - 1);
            Console.Write($"마력회복\t+ {Mp}");
            Console.SetCursorPosition(0, Console.CursorTop + 1);
        }

        public override void Use(Player player)
        {
            int cnt = count;
            base.Use(player);

            if (cnt != 0)
            {
                int baseMp = player.Hp;
                player.Hp += Mp;
                if (player.Hp > 50)
                {
                    player.Hp = 50;
                }
                Console.WriteLine($"{player.Name}은 체력을 {player.Hp - baseMp}만큼 회복했습니다.");
            }
        }
    }
}
