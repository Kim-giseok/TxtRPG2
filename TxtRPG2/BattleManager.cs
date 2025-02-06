using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG2
{
    internal class BattleManager
    {
        Player player;
        Enemy[] Enemys;
        public bool Victory { get => player.Hp != 0; }

        public BattleManager(Player player)
        {
            player = player;
            Enemys =
            [
                new Enemy(2, "미니언", 15, 5),
                new Enemy(3, "공허충", 10, 9),
                new Enemy(5, "대포미니언", 25, 8)
            ]
        }

        public void Battle()
        {

        }
    }
}
