using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG2
{
    internal class Inventory
    {
        public List<Item> Equips { get; set; }
        public Weapon EWeapon { get; set; }
        public Amor EAmor { get; set; }

        public Inventory()
        {
            Equips = new List<Item>();
        }

        public void ShowInfo (bool equip = false)
        {
            Console.WriteLine("[아이템 목록]");
            Console.WriteLine();
            for (int i = 0; i < Equips.Count; i++)
            {
                Console.Write("- ");
                if (EWeapon == Equips[i] || EAmor == Equips[i])
                {
                    Console.Write(" [E]");
                }
                Equips[i].ApearInfo(true);
            }
        }
    }
}
