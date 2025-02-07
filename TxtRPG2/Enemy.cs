using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TxtRPG2;

namespace TxtRPG2
{
    
    public class Enemy : Character // 인터페이스 상속
    {
        public bool IsDead { get; set; }// 사망여부

        public Enemy(int level, string name, int hp, int atk) // 레벨, 이름, 체력, 공격력을 받는 생성자
        {
            this.Level = level;
            this.Name = name;
            this.Hp = hp;
            this.Atk = atk;
            this.IsDead = false;
        }

        public void TakeDamage(int Damage)
        {
            if (IsDead)
            {
                Console.WriteLine($"{Name}은 이미 죽었습니다.");
                return;
            }
            // 공격력의 ±10% 변동을 적용한 최종 공격력 계산
            Damage = (int)Math.Ceiling(Damage * (1 + (new Random().NextDouble() * 0.2 - 0.1)));
            Console.WriteLine($"{Name}이 {Damage}만큼의 피해를 받았습니다.(기준 공격력: {Atk})");
            
            Hp -= Damage;
            if (Hp <= 0)
            {
                Hp = 0;
                IsDead = true;
                Console.WriteLine($"{Name}이 죽었습니다.");
            }
            else
            {
                Console.WriteLine($"남은 체력: {Hp - Damage}");
            }
        }

        public void ApearInfo()
        {
            if (IsDead)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"{Name} Dead");

            }
            else
            {
                Console.WriteLine($"Lv.{Level} 이름: {Name} 체력: {Hp} 공격력: {Atk}");
            }

            Console.ResetColor();
        }
    }
}