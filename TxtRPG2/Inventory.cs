using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG2
{
    internal class Inventory
    {
        public Dictionary<string, Potion> Potions { get; set; }
        public List<Item> Equips { get; set; }
        public Weapon EWeapon { get; set; }
        public Amor EAmor { get; set; }

        public Inventory()
        {
            Equips = new List<Item>();
            Potions = new Dictionary<string, Potion>();
        }

        public enum Showmode { Idle, Equip, Sell }
        public void ShowItems(Showmode mode = Showmode.Idle)
        {
            if (mode != Showmode.Sell)
            {
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine();
            }
            Console.WriteLine("[아이템 목록]");
            Console.WriteLine();

            if (mode == Showmode.Idle && Potions.Count > 0)
            {
                Console.WriteLine("포션");
                Console.WriteLine();
                {
                    foreach (var potion in Potions)
                    {
                        Console.Write("- ");
                        potion.Value.ApearInfo();
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine("장비");
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

        public void ShowInven()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("인벤토리");
                ShowItems();

                Console.WriteLine();
                Console.WriteLine("1. 장착 관리");
                Console.WriteLine("0. 나가기");
                switch (ConsoleUtility.GetInput(0, 1))
                {
                    case 0:
                        return;
                    case 1:
                        ShowEquip();
                        break;
                }
            }
        }

        public void ShowEquip()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("인벤토리 - 장착관리");
                ShowItems(Showmode.Equip);

                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                int choice = ConsoleUtility.GetInput(0, 1);
                switch (choice)
                {
                    case 0:
                        return;
                    default:
                        Equip(choice - 1);
                        break;
                }
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
            else
            {
                if (Potions.ContainsKey(item.Name))
                    Potions[item.Name].count += ((Potion)item).count;
                else if (item.GetType() == typeof(HpPotion))
                    Potions.Add(item.Name, new HpPotion((HpPotion)item));
                else if (item.GetType() == typeof(MpPotion))
                    Potions.Add(item.Name, new MpPotion((MpPotion)item));
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
