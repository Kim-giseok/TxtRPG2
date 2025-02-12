using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TxtRPG2
{
    public class Skill//스킬 관리 클래스
    {
        public static Skill[] skills =
        {
            new Skill("알파 스트라이크", 10, 2f, 1),
            new Skill("더블 스트라이크", 15, 1.5f, 2),
            new Skill("보이드 어택", 7, 1.5f, 1),
            new Skill("포격", 15, 1.5f, 2),
            new Skill("강타", 15, 1.5f, 1)
        };
        public string Name { get; }
        public int ManaCost { get; }
        public float DamageMultiplier { get; }
        public int Range { get; set; }

        public Skill(string name, int manaCost, float damageMultiplier, int range)
        {
            Name = name;
            ManaCost = manaCost;
            DamageMultiplier = damageMultiplier;
            Range = range;
        }

        public void ShowInfo()
        {
            int l = Console.CursorLeft;
            Console.WriteLine($"{Name} - MP {ManaCost}");
            Console.SetCursorPosition(l, Console.CursorTop);
            if (Range == 1)
            {
                Console.WriteLine($"공격력 * {DamageMultiplier} 로 하나의 적을 공격합니다.");
            }
            else
            {
                Console.WriteLine($"공격력 * {DamageMultiplier} 로 {Range}명의 적을 랜덤으로 공격합니다.");
            }
        }
    }
}
