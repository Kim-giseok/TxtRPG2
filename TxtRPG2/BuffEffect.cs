using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG2
{
    public class BuffEffect
    {
        public string Name { get; }
        public int Duration { get; private set; } // 지속 턴 수
        public int AtkModifier { get; } // 공격력 변화량
        public int HpRegen { get; } // 매 턴 HP 회복량
        public bool IsDebuff { get; } // 디버프 여부

        public BuffEffect(string name, int duration, int atkModifier = 0, int hpRegen = 0, bool isDebuff = false)
        {
            Name = name;
            Duration = duration;
            AtkModifier = atkModifier;
            HpRegen = hpRegen;
            IsDebuff = isDebuff;
        }

        public void ReduceDuration()
        {
            Duration--;
        }
    }
}
