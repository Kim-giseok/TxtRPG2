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
