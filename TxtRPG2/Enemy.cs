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
       // public bool IsDead { get => Hp <= 0; }// 사망여부
        public List<Skill> Skills { get; set; }
        public Enemy(int level, string name, int hp, int mp, int atk) // 레벨, 이름, 체력, 공격력을 받는 생성자
            : base(level, name, hp, mp, atk) // 부모 생성자 호출
        {
            Skills = new List<Skill>
            {
                new Skill("깨물기", 3, 3, 1, new List<SkillEffect> { new SkillEffect(SkillType.Bleed, 5, 3) }),
                new Skill("연속 찌르기", 1, 2, 1,new List<SkillEffect> { new SkillEffect(SkillType.Damage, 2, 0) }),
                new Skill("강하게 내려치기",5,7,1,new List<SkillEffect> {new SkillEffect(SkillType.Stun, 2 ,1 )})
            };
        }

        public void TakeDamage(int Damage)
        {
            Hp -= Damage;
            if (Hp <= 0)
            {
                Hp = 0;
            }
        }
        
        public void EnemySkill(Enemy enemy , Player player, Character[] allCharacter)// 적의 스킬 사용
        {
            Random rand = new Random();
            int randSkill = rand.Next(0, Skills.Count);
            Skill skill = Skills[randSkill];
            skill.Use(enemy,player, allCharacter);
        }

        public void AppearInfo()// 적의 정보 출력
        {
            if (IsDead)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Lv.{Level} {Name} Dead");

            }
            else if(IsStun)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Lv.{Level} {Name} Stun");
            }
            else
            {
                Console.WriteLine($"Lv.{Level} {Name} | 체력: {Hp} | 공격력: {Atk} |");
            }

            Console.ResetColor();
        }

        


    }
}

