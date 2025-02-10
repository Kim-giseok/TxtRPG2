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

        public Potion(Potion potion) : base(potion)
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
    }

    internal interface IHpPotion
    {
        public int Hp { get; }
    }

    internal class HpPotion : Potion, IHpPotion
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

        public override void ApearInfo(bool select = false)
        {
            int sl = Console.CursorLeft;
            base.ApearInfo(select);
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
