using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TxtRPG2;

namespace TxtRPG2
{
    internal class Warrior : Player
    {
        public Warrior(string name, int level, int hp = 100, int mp = 50, int gold = 1500) : base(name, level, "전사", 10, 15, hp, mp, gold)
        {
            Skills.Add(Skill.skills[0]);
        }
    }

    internal class Archer : Player
    {
        public Archer(string name, int level, int hp = 100, int mp = 50, int gold = 1500) : base(name, level, "궁수", 20, 10, hp, mp, gold)
        {
            Skills.Add(Skill.skills[1]);
        }
    }
}