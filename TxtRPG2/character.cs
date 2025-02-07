using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG2
{
    interface ICharacter
    {
        string Name { get; set; }
        int Hp { get; set; }
        int Atk { get; set; }
        int Level { get; set; }
        void TakeDamage(int Damage);

    }

    public class Character : ICharacter
    {
        public string Name { get; set; }
        public int Hp { get; set; }
        public int Atk { get; set; }
        public int Level { get; set; }
        public Character(string name, int hp, int atk, int level)
        {
            Name = name;
            Hp = hp;
            Atk = atk;
            Level = level;
        }

        public void TakeDamage(int Damage)
        {
            // 공격력의 ±10% 변동을 적용한 최종 공격력 계산
            Damage = (int)Math.Ceiling(Damage * (1 + (new Random().NextDouble() * 0.2 - 0.1)));
            //finalAtk = (int)Damage; // 데미지를 int로 변환
            Console.WriteLine($"{Name}이 {Damage}만큼의 피해를 받았습니다.(기준 공격력: {Atk})");
            
            Hp -= Damage;
            if (Hp <= 0)
            {
                Hp=0;
                Console.WriteLine($"{Name}이 죽었습니다.");
            }
            else
            {
                Console.WriteLine($"{Name}남은 체력: {Hp}");
            }
        }

    }
}
