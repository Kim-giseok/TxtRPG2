using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG2
{
    public class Skill : Character

    {
        public int manacost { get; set; }
        public Skill(int level, string name, int hp,int mp, int atk)
            : base(level, name, hp, mp, atk)
        {
            
        }
        public void Skill1(Enemy enemy)
        {
            Console.WriteLine("스킬1 사용");
            enemy.TakeDamage(Atk * 2);
        }
        public void Skill2(Enemy enemy)
        {
            Console.WriteLine("스킬2 사용");
            enemy.TakeDamage(Atk * 3);
        }
        public void Skill3(Enemy enemy)
        {
            Console.WriteLine("스킬3 사용");
            enemy.TakeDamage(Atk * 4);
        }
    }
}
