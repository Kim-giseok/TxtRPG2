using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG2
{
    internal class Reward
    {
        public int Exp { get; set; }
        public Item[] Items { get; set; }
        public int Gold { get; set; }

        public Reward(int exp = 1, Item[] items = null, int gold = 100)
        {
            Exp = exp;
            Items = items;
            Gold = gold;
        }

        public void ApplyReWard(Player player)
        {
            player.Exp += Exp;
            player.Gold += Gold;
            if (Items != null)
            {
                foreach (Item item in Items)
                {
                    player.inven.AddItem(item);
                }
            }
        }
    }
}
