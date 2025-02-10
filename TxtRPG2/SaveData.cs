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
        public struct Playerinfo
        {
            public string name;
            public int level;
            public int Hp;
            public int Mp;
            public int Atk;
            public int Def;
            public string Job;
            public int Gold;
        }
        public Playerinfo PlayerData;

        //public bool[] ShopItems;

        //public struct Iteminfo
        //{
        //    public string type;
        //    public string Name;
        //    public int[] Stat;
        //    public string Description;
        //    public int Price;
        //}
        //public Iteminfo[] Items;

        public SaveData(Player player)
        {
            PlayerData.name = player.Name;
            PlayerData.level = player.Level;
            PlayerData.Hp = player.Hp;
            PlayerData.Mp = player.Mp;
            PlayerData.Atk = player.Atk;
            PlayerData.Def = player.Def;
            PlayerData.Job = player.Job;
            PlayerData.Gold = player.Gold;
        }

        public static void Save(SaveData save, string path)
        {
            var options = new JsonSerializerOptions { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
            string jString = JsonSerializer.Serialize(save, options);

            File.WriteAllText(path, jString);
            Console.WriteLine("저장 완료");
            Thread.Sleep(500);
        }

        public static void Load(string path, out Player player)
        {
            string jString = File.ReadAllText(path);

            SaveData load = JsonSerializer.Deserialize<SaveData>(jString);
            switch(load.PlayerData.Job)
            {
                case "전사":
                    player = new Warrior(load.PlayerData.name, load.PlayerData.level, load.PlayerData.Hp, load.PlayerData.Gold);
                    break;
                case "궁수":
                    player = new Archer(load.PlayerData.name, load.PlayerData.level, load.PlayerData.Hp, load.PlayerData.Gold);
                    break;
                default:
                    player = new Warrior(load.PlayerData.name, load.PlayerData.level, load.PlayerData.Hp, load.PlayerData.Gold);
                    break;
            }
            Console.WriteLine("저장데이터를 불러왔습니다.");
            Thread.Sleep(500);
        }
    }
}
