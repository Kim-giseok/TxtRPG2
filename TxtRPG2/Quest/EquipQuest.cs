using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG2
{
    class EquipQuest : Quest
    {
        public override string Goal { get => $"장비 {GoalCount}회 장착"; }

        public EquipQuest(string name, string descript, int reward, int goalCount, int nowCount = 0, State stat = State.Ready) : base(name, descript, reward, goalCount, nowCount, stat)
        { }
        public EquipQuest(EquipQuest quest, int nowCount, State stat) : base(quest, nowCount, stat)
        { }
    }
}
