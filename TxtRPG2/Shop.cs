﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG2
{
    internal class Shop
    {
        Player player;
        public Item[] Items { get; private set; }
        Inventory inven;

        public Shop(Player player, Inventory inven)
        {
            this.player = player;
            Items =
            [
                new Amor("수련자 갑옷", 5, "수련에 도움을 주는 갑옷입니다.", 1000),
                new Weapon("낡은 검", 2, "쉽게 볼 수 있는 낡은 검 입니다.", 600),
                new HpPotion("체력 포션", 30, "잃은 체력을 회복하는 포션입니다.", 100)
            ];
            this.inven = inven;
        }

        enum ShopMode { Idle, Buy, Sell };
        void ShowItems(ShopMode mode = ShopMode.Idle)
        {
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G");
            Console.WriteLine();

            //판매일 경우 플래이어가 소유중인 아이템을 보여줍니다.
            if (mode == ShopMode.Sell)
            {
                inven.ShowItems(Inventory.Showmode.Sell);
                return;
            }

            Console.WriteLine("[아이템 목록]");
            Console.WriteLine();
            for (int i = 0; i < Items.Length; i++)
            {
                Console.Write("- ");
                if (mode == ShopMode.Buy)
                {
                    Console.Write($"{i + 1} ");
                }
                Items[i].ApearInfo(true);
            }
        }

        public void ShopEnter()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("상점");
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
                Console.WriteLine("상점 - 아이템 구매");
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
                            inven.AddItem(Items[choice - 1]);
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

                Console.WriteLine("상점 - 아이템 판매");
                //보유골드와 자신의 아이템들을 보여줍니다.
                ShowItems(ShopMode.Sell);

                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                int choice = ConsoleUtility.GetInput(0, inven.Equips.Count);
                switch (choice)
                {
                    case 0:
                        return;
                    default:
                        //플레이어의 아이템을 판매합니다.
                        player.Gold += inven.Equips[choice - 1].Price;
                        inven.DelItem(choice - 1);
                        Console.WriteLine("판매가 완료되었습니다.");
                        Thread.Sleep(500);
                        break;
                }
            }
        }
    }
}
