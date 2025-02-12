using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG2
{
    internal class Shop
    {
        Player player;
        public Item[] Items { get; private set; } =
        {
                new Amor(Amor.amors[0]),
                new Weapon(Weapon.weapons[0]),
                new HpPotion(HpPotion.hpPotions[0]),
                new MpPotion(MpPotion.mpPotions[0])
        };

        public Shop(Player player)
        {
            this.player = player;
        }

        public Shop(Player player, bool[] sells)
        {
            this.player = player;
            for (int i = 0; i < sells.Length; i++)
            {
                Items[i].IsSold = sells[i];
            }
        }

        enum ShopMode { Idle, Buy, Sell };
        void ShowItems(ShopMode mode = ShopMode.Idle)
        {
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.Write($"{player.Gold}");
            ConsoleUtility.WriteLine(" G", ConsoleColor.DarkRed);
            Console.WriteLine();

            //판매일 경우 플래이어가 소유중인 아이템을 보여줍니다.
            if (mode == ShopMode.Sell)
            {
                player.inven.ShowItems(Inventory.Showmode.Sell);
                return;
            }

            Console.WriteLine("[아이템 목록]");
            Console.WriteLine();
            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i].IsSold)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
                Console.Write("- ");
                if (mode == ShopMode.Buy)
                {
                    Console.Write($"{i + 1} ");
                }
                Items[i].ApearInfo(Item.ApearMode.Buy);
                Console.ResetColor();
            }
        }

        public void ShopEnter()
        {
            while (true)
            {
                Console.Clear();
                ConsoleUtility.WriteLine("상점", ConsoleColor.Yellow);
                //보유골드와 상점의 아이템들을 보여줍니다.
                ShowItems();

                Console.WriteLine();
                Console.WriteLine("1. 아이템 구매");
                Console.WriteLine("2. 아이템 판매");
                Console.WriteLine("0. 나가기");
                switch (ConsoleUtility.GetInput(0, 2))
                {
                    case 0:
                        return;
                    case 1:
                        BuyItem();
                        break;
                    case 2:
                        SellItem();
                        break;
                }
            }
        }

        void BuyItem()
        {
            while (true)
            {
                Console.Clear();
                ConsoleUtility.WriteLine("상점 - 아이템 구매", ConsoleColor.Yellow);
                //보유골드와 상점의 아이템들을 보여줍니다.
                ShowItems(ShopMode.Buy);

                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                int choice = ConsoleUtility.GetInput(0, Items.Length);
                switch (choice)
                {
                    case 0:
                        return;
                    default:
                        //고른 아이템을 구매합니다.
                        if (Items[choice - 1].IsSold) //이미 구매했을 경우
                        { Console.WriteLine("이미 구매한 아이템입니다."); }
                        else if (Items[choice - 1].Price <= player.Gold) //금액이 충분한 경우
                        {
                            player.Gold -= Items[choice - 1].Price;
                            player.inven.AddItem(Items[choice - 1]);
                            Items[choice - 1].IsSold = true;
                            Console.WriteLine("구매를 완료했습니다.");
                        }
                        else //금액이 부족한 경우
                        { Console.WriteLine("Gold 가 부족합니다."); }
                        Thread.Sleep(500);
                        break;
                }
            }
        }

        void SellItem()
        {
            while (true)
            {
                Console.Clear();

                ConsoleUtility.WriteLine("상점 - 아이템 판매", ConsoleColor.Yellow);
                //보유골드와 자신의 아이템들을 보여줍니다.
                ShowItems(ShopMode.Sell);

                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                int choice = ConsoleUtility.GetInput(0, player.inven.Equips.Count);
                switch (choice)
                {
                    case 0:
                        return;
                    default:
                        //플레이어의 아이템을 판매합니다.
                        player.Gold += player.inven.Equips[choice - 1].Price * 80 / 100;
                        player.inven.DelItem(choice - 1);
                        Console.WriteLine("판매가 완료되었습니다.");
                        Thread.Sleep(500);
                        break;
                }
            }
        }
    }
}
