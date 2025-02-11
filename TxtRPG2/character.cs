using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG2
{
    interface ICharacter
    {
        string Name { get; set; }
        int Hp { get; set; }
        int Atk { get; set; }
        int Mp { get; set; }
        int Level { get; set; }
        List<Skill> Skills { get; set; }
        void TakeDamage(int Damage);
        

    }

    public class Character : ICharacter
    {
        public string Name { get; set; }
        public int Hp { get; set; }
        public int Atk { get; set; }
        public int Level { get; set; }
        public int Mp { get; set; }
        public List<Skill> Skills { get; set; } = new List<Skill>();
        public bool IsDead { get => Hp <= 0; }
        private int bleedDamage = 0; //지속피해
        private int effectDuration = 0; // 지속시간
        private int attackBuff = 0;
        

        public Character(int level, string name, int hp, int mp, int atk)
        {
            Level = level;
            Name = name;
            Hp = hp;
            Mp = mp;
            Atk = atk;
            
        }

        public void TakeDamage(int Damage)
        {
            Hp -= Damage;
            if (Hp <= 0)
            {
                Hp = 0;
            }
        }

        // 지속피해 효과 적용
        public void ApplyBleed(int damagePerTurn, int duration)
        {
            bleedDamage = damagePerTurn;
            effectDuration = duration;
            Console.WriteLine($"{Name}가 {duration}턴 동안 {damagePerTurn}의 출혈 피해를 받습니다!");
        }

        // 기절 효과 적용
        public void ApplyStun(int duration)
        {
            effectDuration = duration;
            Console.WriteLine($"{Name}가 {duration}턴 동안 기절 상태에 빠졌습니다!");
        }

        // 버프 효과 적용 (공격력 증가)
        public void ApplyBuff(int atkIncrease, int duration)
        {
            attackBuff = atkIncrease;
            effectDuration = duration;
            Console.WriteLine($"{Name}의 공격력이 {atkIncrease}만큼 증가합니다! (지속 {duration}턴)");
        }

        // 매 턴 상태 이상 처리
        public void ProcessStatusEffects()
        {
            if (effectDuration > 0)
            {
                effectDuration--;
                Console.WriteLine($"{Name}가 기절 상태로 행동할 수 없습니다! (남은 턴: {effectDuration})");
                return; // 기절 상태에서는 행동 불가
            }

            if (effectDuration > 0)
            {
                Hp -= bleedDamage;
                effectDuration--;
                Console.WriteLine($"{Name}가 지속 피해를 입습니다! (-{bleedDamage} HP, 남은 턴: {effectDuration})");
                
            }

            if (effectDuration > 0)
            {
                effectDuration--;
                if (effectDuration == 0)
                {
                    Console.WriteLine($"{Name}의 공격력 버프가 사라졌습니다.");
                    attackBuff = 0;
                }
            }
        }

        // 🛠 공격 시, 버프된 공격력 적용
        public int GetAttackPower()
        {
            return Atk + attackBuff;
        }



    }
}
