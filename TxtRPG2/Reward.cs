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
            new Reward(1, 50, [new HpPotion(HpPotion.hpPotions[0])]),
            new Reward(2, 150, [new HpPotion(HpPotion.hpPotions[0]), new Weapon(Weapon.weapons[0])]),
            new Reward(5, 500, [new HpPotion(HpPotion.hpPotions[0]), new MpPotion(MpPotion.mpPotions[0])])
        };
        public int Exp { get; set; }
        public int Gold { get; set; }
        Item[] Items { get; set; }

        public Reward(int exp = 1, int gold = 100, Item[] items = null)
        {
            Exp = exp;
            Gold = gold;
            Items = items == null ? Array.Empty<Item>() : items;
        }

        public void ApplyReWard(Player player, Dictionary<string, int> items)
        {
            player.GainExp(Exp);
            player.Gold += Gold;
            foreach (var item in Items)
            {
                player.inven.AddItem(item);
                if (items.ContainsKey(item.Name))
                {
                    items[item.Name] += 1;
                }
                else
                {
                    items.Add(item.Name, 1);
                }
            }
        }
    }
}
