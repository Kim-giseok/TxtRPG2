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
        public List<Skill> Skills { get; set; } // 스킬 리스트
        public Enemy(int level, string name, int hp, int mp, int atk, List<Skill> skills = null) // 레벨, 이름, 체력, 공격력을 받는 생성자
            : base(level, name, hp, mp, atk) // 부모 생성자 호출
        {
             Skills = skills != null ? new List<Skill>(skills) : new List<Skill>(); // 스킬이 없으면 빈 리스트로 초기화  
        }

        public void TakeDamage(int Damage)
        {
            Hp -= Damage;
            if (Hp <= 0)
            {
                Hp = 0;
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
                Console.WriteLine($"Lv.{Level} {Name} | 체력: {Hp}");
            }
            Console.ResetColor();
        }
    }
}
