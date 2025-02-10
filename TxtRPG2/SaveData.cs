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
        public string Name { get; set; }
        public int Level { get; set; }
        public int Hp { get; set; }
        public int Mp { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public string Job { get; set; }
        public int Gold { get; set; }

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

        public static void Save(Player player, string path = "save.json")
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
                Gold = player.Gold
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

        public static void Load(out Player player, string path = "save.json")
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
            Console.WriteLine("저장데이터를 불러왔습니다.");
            Thread.Sleep(500);
        }
    }
}
