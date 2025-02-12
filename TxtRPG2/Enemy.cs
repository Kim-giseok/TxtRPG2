using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TxtRPG2;

namespace TxtRPG2
{

    internal class Enemy : Character // 인터페이스 상속
    {
        public Reward reward { get; set; }

        public Enemy(int level, string name, int hp, int mp, int atk, List<Skill> skills = null, int reward = 0) // 레벨, 이름, 체력, 공격력을 받는 생성자
            : base(level, name, hp, mp, atk) // 부모 생성자 호출
        {
            Skills = skills != null ? skills : new List<Skill>(); // 스킬이 없으면 빈 리스트로 초기화  
            this.reward = Reward.rewards[reward];
        }

        public Enemy(Enemy enemy) : base(enemy.Level, enemy.Name, enemy.Hp, enemy.Mp, enemy.Atk)
        {
            Skills = enemy.Skills;
            reward = enemy.reward;
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
