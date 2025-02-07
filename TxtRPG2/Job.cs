using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG2
{
    public class Warrior : Character
    {
        public Warrior(string name, int level, int hp, int gold)
        {
            Name = name;
            Level = level;
            Job = "전사";
            Atk = 10;  // 전사 공격력
            Def = 15;  // 전사 방어력
            Hp = 100;
            Gold = gold;
        }

        public string Job { get; set; }
        public int Def { get; set; }
        public int Gold { get; set; }
    }

    public class Archer : Character
    {
        public Archer(string name, int level, int hp, int gold)
        {
            Name = name;
            Level = level;
            Job = "궁수";
            Atk = 20;  // 궁수 공격력
            Def = 10;   // 궁수 방어력
            Hp = 80;
            Gold = gold;
        }

        public string Job { get; set; }
        public int Def { get; set; }
        public int Gold { get; set; }
    }
}
