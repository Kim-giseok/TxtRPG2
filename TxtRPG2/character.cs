using System.Reflection.Metadata.Ecma335;
using System.Threading;

namespace TxtRPG2
{
    public class Character
    {
        public string Name { get; set; }
        public int Hp { get; set; }
        public int Mp { get; set; }
        public int Atk { get; set; }
        public int Level { get; set; }
        public List<Skill> Skills { get; set; } = new List<Skill>();
        public bool IsDead { get => Hp <= 0; }
        public StatusEffect CurrentStatus { get; set; } = StatusEffect.None;
        private int statusEffectTurn = 0;

        public Character(int level, string name, int hp, int mp, int atk)
        {
            Level = level;
            Name = name;
            Hp = hp;
            Mp = mp;
            Atk = atk;
            Skills = new List<Skill>();

        }

        public void Attack(Character target)
        {
            // 스턴 상태인 경우 공격 불가
            if (IsStunned())
            {
                Console.WriteLine($"{Name}은(는) 기절 상태라 공격할 수 없습니다!");
                Thread.Sleep(300);
                return;
            }

            int damage = (int)(Atk * new Random().Next(90, 110) / 100f + 0.5f);
            int Hp = target.Hp;

            target.TakeDamage(damage);

            while (true)
            {
                Console.Clear();

                Console.WriteLine("Battle!!");
                Console.WriteLine();
                ProcessStatusEffect();
                Console.WriteLine($"{Name}의 공격!");
                Console.WriteLine($"Lv.{target.Level} {target.Name}에게 {damage}의 피해를 입혔습니다. ");
                Console.WriteLine();
                Console.WriteLine($"Lv.{target.Level} {target.Name}");

                if (target.Hp == 0)
                {
                    Console.WriteLine($"HP {Hp} -> dead");
                }
                else
                {
                    Console.WriteLine($"HP {Hp} -> {target.Hp}");
                }

                Console.WriteLine();
                Console.WriteLine("0. 다음");
                switch (ConsoleUtility.GetInput(0, 0))
                {
                    case 0: return;
                }
            }
        }

        public void UseSkill(Skill skill, Character[] targets)
        {
            // 스턴 상태인 경우 스킬 사용 불가
            if (IsStunned())
            {
                Console.WriteLine($"{Name}은(는) 기절 상태라 스킬을 사용할 수 없습니다!");
                Thread.Sleep(300);
                return;
            }

            Mp -= skill.ManaCost;
            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine();
            Console.WriteLine($"{Name}은(는) {skill.Name}을 사용했다!");


            int damage = (int)(Atk * skill.DamageMultiplier);
            int[] Hp = new int[targets.Length];

            for (int i = 0; i < targets.Length; i++)
            {
                Hp[i] = targets[i].Hp;
                ProcessStatusEffect();
                targets[i].TakeDamage(damage);
                Console.WriteLine($"Lv.{targets[i].Level} {targets[i].Name}에게 {damage}의 피해를 입혔다!");


            }

            // 상태 이상 효과가 있는 경우 적용
            if (skill.Effect.HasValue && skill.Effect.Value != StatusEffect.None)
            {
                foreach (var target in targets)
                {
                    target.ApplyStatusEffect(skill.Effect.Value, skill.EffectDuration);
                }
            }

            Console.WriteLine();
            for (int i = 0; i < targets.Length; i++)
            {
                Console.WriteLine($"Lv.{targets[i].Level} {targets[i].Name}");
                if (targets[i].Hp == 0)
                {
                    Console.WriteLine($"HP {Hp[i]} -> dead");
                }
                else
                {
                    Console.WriteLine($"HP {Hp[i]} -> {targets[i].Hp}");
                }
            }

            Console.WriteLine();
            Console.WriteLine("0. 다음");
            switch (ConsoleUtility.GetInput(0, 0))
            {
                case 0: return;
            }
        }



        public void ShowSkills()
        {
            for (int i = 0; i < Skills.Count; i++)
            {
                Console.Write($"{i + 1}. ");
                Skills[i].ShowInfo();
            }
        }

        public void TakeDamage(int Damage)
        {
            Hp -= Damage;
            if (Hp <= 0)
            {
                Hp = 0;
            }
        }

        public void ApplyStatusEffect(StatusEffect effect, int duration)
        {
            if (effect == StatusEffect.None) return;

            CurrentStatus = effect;
            statusEffectTurn = duration;

            Console.WriteLine($"{Name}은(는) {effect}상태가 되었다! [지속{statusEffectTurn}]");
        }

        public void ProcessStatusEffect() // 상태이상 적용
        {
            if (statusEffectTurn > 0)
            {
                ConsoleColor defaultColor = Console.ForegroundColor; // 기존 색상 저장

                switch (CurrentStatus)
                {
                    case StatusEffect.Poison:
                        int poisonDamage = Math.Max(3, (int)(Hp * 0.05)); // 최소 3 피해
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine($"{Name}은(는) 독으로 인해 {poisonDamage}의 피해를 입었다!");
                        TakeDamage(poisonDamage);
                        Thread.Sleep(300);
                        break;

                    case StatusEffect.Stun:
                        IsStunned();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"{Name}은(는) 기절 상태라 행동할 수 없다!");
                        Thread.Sleep(300);
                        break;

                    case StatusEffect.Burn:
                        int burnDamage = Math.Max(8, (int)(Atk * 0.5)); // 최소 8 피해
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"{Name}은(는) 화상으로 인해 {burnDamage}의 피해를 입었다!");
                        TakeDamage(burnDamage);
                        Thread.Sleep(300);
                        break;

                    case StatusEffect.Bleed:
                        int bleedDamage = Math.Max(2, (int)(Hp * 0.01)); // 최소 2 피해
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine($"{Name}은(는) 출혈로 인해 {bleedDamage}의 피해를 입었다!");
                        TakeDamage(bleedDamage);
                        Thread.Sleep(300);
                        break;
                }

                Console.ForegroundColor = defaultColor; // 색상 원래대로 돌리기
                statusEffectTurn--;
                Thread.Sleep(300);

                // 상태 이상 해제
                if (statusEffectTurn == 0)
                {
                    Console.WriteLine($"{Name}의 {CurrentStatus} 상태 이상이 해제되었다!");
                    Thread.Sleep(300);
                    CurrentStatus = StatusEffect.None;
                }
            }
        }

        public bool IsStunned()
        {
            Thread.Sleep(300);
            return CurrentStatus == StatusEffect.Stun && statusEffectTurn > 0;
        }
    }
}
