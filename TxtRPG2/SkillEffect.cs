using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG2
{
    public enum SkillType
    {
        Damage, // 데미지
        Bleed,  // 지속피해
        Buff,   // 강화
        Stun  // 약화
    }
    public class SkillEffect// 스킬 부가 효과 클래스
    {
        public SkillType Type { get; set; }
        public int Value { get; set; }
        public int Duration { get; set; }
        public SkillEffect(SkillType type, int value, int duration)
        {
            Type = type;
            Value = value;
            Duration = duration;
        }
    }
    
}
