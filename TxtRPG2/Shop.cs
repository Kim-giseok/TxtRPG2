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
        public Item[] Items { get; private set; }

        public Shop(Player player)
        {
            this.player = player;
            Items =
            [
                new Amor("수련자 갑옷", 5, "수련에 도움을 주는 갑옷입니다.", 1000),
                new Weapon("낡은 검", 2, "쉽게 볼 수 있는 낡은 검 입니다.", 600),
                new HpPotion("체력 포션", 30, "잃은 체력을 회복하는 포션입니다.", 100)
            ];
        }

        void ShowItems(bool buyMode = false)
        {
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G");
            Console.WriteLine();

            //판매일 경우 플래이어가 소유중인 아이템을 보여줍니다.

            Console.WriteLine("[아이템 목록]");
            Console.WriteLine();
            for (int i = 0; i < Items.Length; i++)
            {
                Console.Write("- ");
                Items[i].ApearInfo(true);
            }
        }

        public void ShopEnter()
        {
        }

        void BuyItem()
        {

        }

        void SellItem()
        {

        }
    }
}
