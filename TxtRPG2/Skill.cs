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
        private static Random rand = new();
        public string Name { get; }
        public int ManaCost { get; }
        public double DamageMultiplier { get; }
        public int Range { get; set; }
        public List<SkillEffect> sEffect { get; set; } // Changed type to List<SkillEffect>

        public Skill(string name, int manaCost, double damageMultiplier, int range, List<SkillEffect> effects)
        {
            Name = name;
            ManaCost = manaCost;
            DamageMultiplier = damageMultiplier;
            Range = range;
            sEffect = effects;
        }

        public void Use(Character actor, Character target, Character[] allCharacter) //플레이어용 스킬 사용
        {
            if (!CanUseSkill(actor)) return;

            actor.Mp -= ManaCost;
            Console.WriteLine($"{actor.Name}가 {Name}을(를) 사용했다!");

            List<Character> finalTargets = SelectTargets(target, allCharacter);
            ApplyDamage(actor, finalTargets);
        }

        public bool CanUseSkill(Character actor) // 스킬 사용 가능 여부>> 관련 부분 수정해야됨
        {
            if (actor.Mp < ManaCost)
            {
                Console.WriteLine($"{actor.Name}의 마나가 부족하여 {Name}을 사용할 수 없습니다.");
                return false;
            }
            return true;
        }

        private List<Character> SelectTargets(Character target, Character[] allCharacter) // 스킬 타겟 선택
        {
            List<Character> possibleTargets = allCharacter.Where(c => !c.IsDead && c != target).ToList();// 죽은 적과  선택 대상은 제외
            List<Character> finalTargets = new() { (Character)target };

            for (int i = 0; i < Range - 1 && possibleTargets.Count > 0; i++)
            {
                int randomIndex = rand.Next(possibleTargets.Count);
                finalTargets.Add(possibleTargets[randomIndex]);
                possibleTargets.RemoveAt(randomIndex);
            }
            return finalTargets;
        }

        private void ApplyDamage(Character actor, List<Character> targets) // 스킬 데미지 연산
        {
            int damage = (int)(actor.Atk * DamageMultiplier);
            foreach (var eTarget in targets)
            {
                eTarget.TakeDamage(damage);
                Console.WriteLine($"{damage}의 스킬피해");
                ApplyEffect(eTarget);

            }
        }
        private void ApplyEffect(Character target)
        {
            foreach (var effect in sEffect)
            {
                switch (effect.Type)
                {
                    case SkillType.Bleed:
                        target.ApplyBleed(effect.Value, effect.Duration);
                        break;
                    case SkillType.Stun:
                        target.ApplyStun(effect.Duration);
                        break;
                    case SkillType.Buff:
                        target.ApplyBuff(effect.Value, effect.Duration);
                        break;
                }
            }
        }
    }
}
