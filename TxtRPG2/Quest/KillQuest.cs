using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG2
{
    class KillQuest : Quest
    {
        public string Target { get; }
        public override string Goal { get => $"{Target} {GoalCount}마리 처치"; }

        public KillQuest(string name, string descript, string target, int reward, int goalCount, int nowCount = 0, State stat = State.Ready) : base(name, descript, reward, goalCount, nowCount, stat)
        {
            Target = target;
        }

        public void Triger(Enemy target)
        {
            if (target.Name == Target)
            {
                Triger();
            }
        }
    }
}
