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
        public float Atk { get; set; }
        public int Def { get; set; }
        public string Job { get; set; }
        public int Gold { get; set; }


        //인벤토리 정보
        public struct DEquip
        {
            public string type { get; set; }
            public string name { get; set; }
            public bool equip { get; set; }
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
        public bool[] DSells { get; set; }

        //던전 정보
        public int DFloor { get; set; }

        //퀘스트 정보
        public struct DQuest
        {
            public int nowCount { get; set; }
            public int state { get; set; }
        }
        public DQuest[] Quests { get; set; }

        static DEquip[] SaveEquips(Inventory inven)
        {
            DEquip[] equips = new DEquip[inven.Equips.Count];
            for (int i = 0; i < equips.Length; i++)
            {
                equips[i].type = inven.Equips[i].GetType().ToString().Split(".")[1];
                equips[i].name = inven.Equips[i].Name;
                equips[i].equip =
                (
                    inven.EWeapon == inven.Equips[i] ||
                    inven.EAmor == inven.Equips[i]
                );
            }
            return equips;
        }

        static DPotion[] SavePotions(Inventory inven)
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

        static bool[] SaveSells(Shop shop)
        {
            bool[] sells = new bool[shop.Items.Length];
            for (int i = 0; i < sells.Length; i++)
            {
                sells[i] = shop.Items[i].IsSold;
            }
            return sells;
        }

        static DQuest[] SaveQuests()
        {
            DQuest[] quests = new DQuest[QuestBoard.Quests.Length];
            for (int i = 0; i < quests.Length; i++)
            {
                quests[i].nowCount = QuestBoard.Quests[i].NowCount;
                quests[i].state = (int)QuestBoard.Quests[i].Stat;
            }
            return quests;
        }

        public static void Save(Player player, Shop shop, BattleManager dungeon,  string path = "save.json")
        {
            SaveData save = new SaveData()
            {
                Name = player.Name,
                Level = player.Level,
                Hp = player.Hp,
                Mp = player.Mp,
                Atk = ((Character)player).Atk,
                Def = player.BaseDef,
                Job = player.Job,
                Gold = player.Gold,

                Equips = SaveEquips(player.inven),
                Potions = SavePotions(player.inven),

                DSells = SaveSells(shop),

                DFloor = dungeon.Floor,

                Quests = SaveQuests()
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

        public static void Load(out Player player, out Shop shop, out BattleManager dungeon, out QuestBoard qb, string path = "save.json")
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
                    player = new Warrior(load.Name, load.Level, load.Atk, load.Def, load.Hp, load.Mp, load.Gold);
                    break;
                case "궁수":
                    player = new Archer(load.Name, load.Level, load.Atk, load.Def, load.Hp, load.Mp, load.Gold);
                    break;
                default:
                    throw new Exception();
            }

            qb = new QuestBoard(player);

            Item[] iteml;
            for (int i = 0; i < load.Equips.Length; i++)
            {
                switch (load.Equips[i].type)
                {
                    case "Weapon":
                        iteml = Weapon.weapons;
                        break;
                    case "Amor":
                        iteml = Amor.amors;
                        break;
                    default:
                        throw new Exception();
                }
                foreach (var item in iteml)
                {
                    if (item.Name == load.Equips[i].name)
                    {
                        player.inven.AddItem(item);
                        if (load.Equips[i].equip)
                        {
                            player.inven.Equip(i);
                        }
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
                        throw new Exception();
                }
                foreach (var item in iteml)
                {
                    if (item.Name == potion.name)
                    {
                        for (int i = 0; i < potion.count; i++)
                        {
                            player.inven.AddItem(item);
                        }
                        break;
                    }
                }
            }

            shop = new Shop(player, load.DSells);

            dungeon = new BattleManager(player, load.DFloor);

            for (int i = 0; i < load.Quests.Length; i++)
            {
                Quest.State stat = (Quest.State)load.Quests[i].state;
                switch(QuestBoard.Quests[i])
                {
                    case KillQuest:
                        QuestBoard.Quests[i] = new KillQuest((KillQuest)QuestBoard.Quests[i], load.Quests[i].nowCount, stat);
                        break;
                    case EquipQuest:
                        QuestBoard.Quests[i] = new EquipQuest((EquipQuest)QuestBoard.Quests[i], load.Quests[i].nowCount,  stat);
                        break;
                    case GrowQuest:
                        QuestBoard.Quests[i] = new GrowQuest((GrowQuest)QuestBoard.Quests[i], load.Quests[i].nowCount, stat);
                        break;
                    default:
                        throw new Exception();
                }
            }

            Console.WriteLine("저장데이터를 불러왔습니다.");
            Console.ReadKey();
        }
    }
}
