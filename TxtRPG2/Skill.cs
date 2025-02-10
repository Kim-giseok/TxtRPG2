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
        private static Random rand = new();
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

        public void Use(Character actor, Character target, Enemy[] spawn) //플레이어용 스킬 사용
        {
            if (!CanUseSkill(actor)) return;

            actor.Mp -= ManaCost;
            Console.WriteLine($"{actor.Name}가 {Name}을(를) 사용했다!");

            List<Enemy> finalTargets = SelectTargets(target, spawn);
            ApplyDamage(actor, finalTargets);
        }

        private bool CanUseSkill(Character actor) // 스킬 사용 가능 여부
        {
            if (actor.Mp < ManaCost)
            {
                Console.WriteLine($"{actor.Name}의 마나가 부족하여 {Name}을 사용할 수 없습니다.");
                return false;
            }
            return true;
        }

        private List<Enemy> SelectTargets(Character target, Enemy[] spawn) // 스킬 타겟 선택
        {
            List<Enemy> possibleTargets = spawn.Where(e => !e.IsDead && e != target).ToList();
            List<Enemy> finalTargets = new() { (Enemy)target };

            for (int i = 0; i < Range - 1 && possibleTargets.Count > 0; i++)
            {
                int randomIndex = rand.Next(possibleTargets.Count);
                finalTargets.Add(possibleTargets[randomIndex]);
                possibleTargets.RemoveAt(randomIndex);
            }
            return finalTargets;
        }

        private void ApplyDamage(Character actor, List<Enemy> targets) // 스킬 데미지 연산
        {
            int damage = (int)(actor.Atk * DamageMultiplier);
            foreach (var eTarget in targets)
            {
                eTarget.TakeDamage(damage);
                
            }
        }

    }
}
