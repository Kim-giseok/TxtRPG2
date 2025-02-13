using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG2
{
    class EquipQuest : Quest
    {
        public override string Goal { get => $"장비 {GoalCount}회 장착"; }

        public EquipQuest(string name, string descript, int goalCount, int nowCount = 0, State stat = State.Ready, int reward = 0) : base(name, descript, goalCount, nowCount, stat, reward)
        { }
    }
}
