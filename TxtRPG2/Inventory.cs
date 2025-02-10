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

        public void ShowInfo(bool equip = false)
        {
            Console.WriteLine("[아이템 목록]");
            Console.WriteLine();
            for (int i = 0; i < Equips.Count; i++)
            {
                Console.Write("- ");
                if (equip)
                {
                    Console.Write($"{i + 1}. ");
                }
                if (EWeapon == Equips[i] || EAmor == Equips[i])
                {
                    Console.Write("[E] ");
                }
                Equips[i].ApearInfo(true);
            }
        }

        public void Equip(int idx)
        {
            if (Equips[idx].GetType() == typeof(Weapon))
            {
                if (EWeapon == Equips[idx])
                {
                    EWeapon = null;
                }
                else
                {
                    EWeapon = (Weapon)Equips[idx];
                }
            }
            else if (Equips[idx].GetType() == typeof(Amor))
            {
                if (EAmor == Equips[idx])
                {
                    EAmor = null;
                }
                else
                {
                    EAmor = (Amor)Equips[idx];
                }
            }
        }
    }
}
