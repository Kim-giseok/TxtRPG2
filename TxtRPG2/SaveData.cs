using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TxtRPG2
{
    [Serializable]
    internal class SaveData
    {
        //플레이어의 정보
        public string Name { get; set; }
        public int Level { get; set; }
        public int Hp { get; set; }
        public int Mp { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public string Job { get; set; }
        public int Gold { get; set; }


        //인벤토리 정보
        public struct DEquip
        {
            public string type { get; set; }
            public string name { get; set; }
        }
        public DEquip[] Equips { get; set; }

        public struct DPotion
        {
            public string type { get; set; }
            public string name { get; set; }
            public int count { get; set; }
        }
        public DPotion[] Potions { get; set; }

        //상점 정보

        static DEquip[] SaveEquips(Inventory inven)
        {
            DEquip[] equips = new DEquip[inven.Equips.Count];
            for (int i = 0; i < equips.Length; i++)
            {
                equips[i].type = inven.Equips[i].GetType().ToString().Split(".")[1];
                equips[i].name = inven.Equips[i].Name;
            }
            return equips;
        }

        static DPotion[] SavePotions (Inventory inven)
        {
            DPotion[] potions = new DPotion[inven.Potions.Count];
            var list = new List<Potion>(inven.Potions.Values);
            for (int i = 0; i < potions.Length; i++)
            {
                potions[i].type = list[i].GetType().ToString().Split(".")[1];
                potions[i].name = list[i].Name;
                potions[i].count = list[i].count;
            }
            return potions;
        }

        public static void Save(Player player, Inventory inven, string path = "save.json")
        {
            SaveData save = new SaveData()
            {
                Name = player.Name,
                Level = player.Level,
                Hp = player.Hp,
                Mp = player.Mp,
                Atk = player.Atk,
                Def = player.Def,
                Job = player.Job,
                Gold = player.Gold,

                Equips = SaveEquips(inven),
                Potions = SavePotions(inven)
            };

            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                IncludeFields = true,
            };
            string jString = JsonSerializer.Serialize(save, options);

            File.WriteAllText(path, jString);
            Console.WriteLine("저장 완료");
            Thread.Sleep(500);
        }

        public static void Load(out Player player, out Inventory inven, string path = "save.json")
        {
            string jString = File.ReadAllText(path);

            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                IncludeFields = true
            };
            SaveData load = JsonSerializer.Deserialize<SaveData>(jString, options);

            switch (load.Job)
            {
                case "전사":
                    player = new Warrior(load.Name, load.Level, load.Hp, load.Gold);
                    break;
                case "궁수":
                    player = new Archer(load.Name, load.Level, load.Hp, load.Gold);
                    break;
                default:
                    player = new Warrior(load.Name, load.Level, load.Hp, load.Gold);
                    break;
            }

            inven = new Inventory();
            Item[] iteml;
            foreach (var equips in load.Equips)
            {
                switch (equips.type)
                {
                    case "Weapon":
                        iteml = Weapon.weapons;
                        break;
                    case "Amor":
                        iteml = Amor.amors;
                        break;
                    default:
                        iteml = Item.items;
                        break;
                }
                foreach (var item in iteml)
                {
                    if (item.Name == equips.name)
                    {
                        inven.AddItem(item);
                    }
                    break;
                }
            }
            foreach (var potion in load.Potions)
            {
                switch (potion.type)
                {
                    case "HpPotion":
                        iteml = HpPotion.hpPotions;
                        break;
                    case "MpPotion":
                        iteml = MpPotion.mpPotions;
                        break;
                    default:
                        iteml = Item.items;
                        break;
                }
                foreach (var item in iteml)
                {
                    if (item.Name == potion.name)
                    {
                        for (int i = 0; i < potion.count; i++)
                        {
                            inven.AddItem(item);
                        }
                        break;
                    }
                }
            }

            Console.WriteLine("저장데이터를 불러왔습니다.");
            Thread.Sleep(500);
        }
    }
}
