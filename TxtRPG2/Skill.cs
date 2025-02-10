using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TxtRPG2
{
    public class Skill
    {
        public string Name { get; }
        public int ManaCost { get; }
        public int DamageMultiplier { get; }
        public int Range { get; set; }

        public Skill(string name, int manaCost, int damageMultiplier, int range)
        {
            Name = name;
            ManaCost = manaCost;
            DamageMultiplier = damageMultiplier;
            Range = range;
        }

        public void Use(Character actor, Character target, Enemy[] spawn)
        {
            if (actor.Mp >= ManaCost)
            {
                actor.Mp -= ManaCost;
                int damage = actor.Atk * DamageMultiplier;
                Console.WriteLine($"{actor.Name}가 {Name}을(를) 사용했다!");

                // 추가 타격 대상 찾기 (선택된 적 + 랜덤한 추가 적)
                List<Enemy> possibleTargets = spawn.Where(e => !e.IsDead && e != target).ToList();
                List<Enemy> finalTargets = new() { (Enemy)target };
                Random rand = new Random();
                for (int i = 0; i < Range - 1 && possibleTargets.Count > 0; i++)
                {
                    int randomIndex = rand.Next(possibleTargets.Count);
                    finalTargets.Add(possibleTargets[randomIndex]);
                    possibleTargets.RemoveAt(randomIndex);
                }
                // 선택된 모든 적에게 피해 적용
                foreach (var eTarget in finalTargets)
                {
                    

                    eTarget.TakeDamage(damage);
                    Console.WriteLine($"{eTarget.Name}에게 {damage} 피해!");
                }
            }
        }
        
    }
}
