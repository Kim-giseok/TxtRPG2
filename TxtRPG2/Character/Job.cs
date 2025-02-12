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
        public Warrior(string name, int level = 1, int atk = 10, int def = 15, int hp = 100, int mp = 50, int gold = 1500) : base(name, level, "전사", atk, def, hp, mp, gold)
        {
            Skills.Add(Skill.skills[0]);
        }
    }

    internal class Archer : Player
    {
        public Archer(string name, int level = 1, int atk = 20, int def = 10, int hp = 100, int mp = 50, int gold = 1500) : base(name, level, "궁수", atk, def, hp, mp, gold)
        {
            Skills.Add(Skill.skills[1]);
        }
    }
}