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

    internal class HpPotion : Potion, IHpPotion
    {
        public static HpPotion[] hpPotions =
        {
            new HpPotion("체력 포션", 30, "잃은 체력을 약간 회복하는 포션입니다.", 100)
        };

        public int Hp { get; }

        public HpPotion(string name, int hp, string description, int price) : base(name, description, price)
        {
            Hp = hp;
        }

        public HpPotion(HpPotion potion) : base(potion)
        {
            Hp = potion.Hp;
        }

        public override void ApearInfo(ApearMode mode = ApearMode.Idle)
        {
            int sl = Console.CursorLeft;
            base.ApearInfo(mode);
            Console.SetCursorPosition(sl + 18, Console.CursorTop - 1);
            Console.Write($"체력회복\t+ {Hp}");
            Console.SetCursorPosition(0, Console.CursorTop + 1);
        }

        public override void Use(Player player)
        {
            int cnt = count;
            base.Use(player);

            if (cnt != 0)
            {
                int baseHp = player.Hp;
                player.Hp += Hp;
                if (player.Hp > 100)
                {
                    player.Hp = 100;
                }
                Console.WriteLine($"{player.Name}은 체력을 {player.Hp - baseHp}만큼 회복했습니다.");
            }
        }
    }
}
