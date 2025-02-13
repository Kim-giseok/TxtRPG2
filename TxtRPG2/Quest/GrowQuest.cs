using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG2
{
    class GrowQuest : Quest
    {
        public Player player { get; }
        public override string Goal { get => $"Lv. {GoalCount}까지 달성"; }
        public override int NowCount { get => player.Level; }

        public GrowQuest(string name, string descript, Player player, int reward, int goalCount, int nowCount = 0, State stat = State.Ready) : base(name, descript, reward, goalCount, nowCount, stat)
        {
            this.player = player;
        }
    }
}
