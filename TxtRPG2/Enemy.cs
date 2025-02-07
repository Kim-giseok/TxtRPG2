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
            : base(level, name, hp, atk) // 부모 생성자 호출
        {

            this.IsDead = false;
        }

        public void TakeDamage(int Damage)
        {

            Hp -= Damage;
            if (Hp <= 0)
            {
                Hp = 0;
                IsDead = true;
                if (IsDead)
                {
                    return;
                }
            }
        }

        public void AppearInfo()// 적의 정보 출력
        {
            if (IsDead)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Lv.{Level} {Name} Dead");

            }
            else
            {
                Console.WriteLine($"Lv.{Level} 이름: {Name} 체력: {Hp} 공격력: {Atk}");
            }

            Console.ResetColor();
        }
    }
}