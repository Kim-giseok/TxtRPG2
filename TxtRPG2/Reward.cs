using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG2
{
    internal class Reward
    {
        public static Reward[] rewards =
        {
            new Reward(1, 100),
            new Reward(1, 50),
            new Reward(2, 150),
            new Reward(5, 500)
        };
        public int Exp { get; set; }
        public int Gold { get; set; }

        public Reward(int exp = 1, int gold = 100)
        {
            Exp = exp;
            Gold = gold;
        }

        public void ApplyReWard(Player player)
        {
            player.GainExp(Exp);
            player.Gold += Gold;
        }
    }
}
