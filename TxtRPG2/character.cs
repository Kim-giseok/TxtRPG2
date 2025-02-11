using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG2
{
    public class Character
    {
        public string Name { get; set; }
        public int Hp { get; set; }
        public int Mp { get; set; }
        public virtual int Atk { get; set; }
        public int Level { get; set; }
        public List<Skill> Skills { get; set; }
        public bool IsDead { get => Hp <= 0; }

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
            int damage = (int)(Atk * new Random().Next(90, 110) / 100f + 0.5f);
            int Hp = target.Hp;

            target.TakeDamage(damage);

            while (true)
            {
                Console.Clear();

                Console.WriteLine("Battle!!");
                Console.WriteLine();
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
            Mp -= skill.ManaCost;
            int damage = (int)(Atk * skill.DamageMultiplier);

            int[] Hp = new int[targets.Length];
            for (int i = 0; i < Hp.Length; i++)
            {
                Hp[i] = targets[i].Hp;
                targets[i].TakeDamage(damage);
            }
            while (true)
            {
                Console.Clear();

                Console.WriteLine("Battle!!");
                Console.WriteLine();
                Console.WriteLine($"{Name}은 {skill.Name}을 사용했다.!");

                for (int i = 0; i < targets.Length; i++)
                {
                    Console.WriteLine($"Lv.{targets[i].Level} {targets[i].Name}에게 {damage}의 피해를 입혔습니다. ");
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
    }
}
