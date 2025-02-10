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

        public enum Showmode { Idle, Equip, Sell}
        public void ShowInfo(Showmode mode = Showmode.Idle)
        {
            Console.WriteLine("[아이템 목록]");
            Console.WriteLine();
            for (int i = 0; i < Equips.Count; i++)
            {
                Console.Write("- ");
                if (mode != Showmode.Idle)
                {
                    Console.Write($"{i + 1}. ");
                }
                if (EWeapon == Equips[i] || EAmor == Equips[i])
                {
                    Console.Write("[E] ");
                }
                Equips[i].ApearInfo(mode == Showmode.Sell);
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

        public void AddItem(Item item)
        {
            if (item.GetType() == typeof(Weapon))
            {
                Equips.Add(new Weapon((Weapon)item));
            }
            else if (item.GetType() == typeof(Amor))
            {
                Equips.Add(new Amor((Amor)item));
            }
        }

        public void DelItem(int idx)
        {
            if (EWeapon == Equips[idx])
            {
                EWeapon = null;
            }
            if (EAmor == Equips[idx])
            {
                EAmor = null;
            }
            Equips.RemoveAt(idx);
        }
    }
}
