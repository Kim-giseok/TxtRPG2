namespace TxtRPG2
{

    internal class Player : Character //플레이어 클래스 . 캐릭터 인터페이스 상속
    {
        public string Job { get; }
        public override float Atk { get => base.Atk + (inven.EWeapon != null ? inven.EWeapon.Atk : 0); }
        public int Def { get => BaseDef + (inven.EAmor != null ? inven.EAmor.Def : 0); }
        public int Gold { get; set; }

        public int BaseDef { get; set; }

        public int Exp { get; set; }

        public Inventory inven { get; set; } = new Inventory();

        public Player(string name, int level, string job, float atk, int def, int hp = 100, int mp = 50, int gold = 1500) : base(level, name, hp, mp, atk)
        {
            Job = job;
            BaseDef = def;
            Gold = gold;
        }

        public void Status() //플레이어의 상태를 보여주는 메서드
        {
            ConsoleUtility.WriteLine("상태 보기", ConsoleColor.Yellow);
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();
            Console.WriteLine($"lv. {Level:D2}");
            Console.WriteLine($"{Name} ({Job})");
            Console.Write($"공격력 : {Atk}");
            if(inven.EWeapon != null)
            {
                ConsoleUtility.WriteLine($" (+{inven.EWeapon.Atk})", ConsoleColor.Red);
            }
            else
            {
                Console.WriteLine();
            }
            Console.Write($"방어력 : {Def}");
            if(inven.EAmor != null)
            {
                ConsoleUtility.WriteLine($" (+{inven.EAmor.Def})", ConsoleColor.Red);
            }
            else
            {
                Console.WriteLine();
            }
            Console.WriteLine($"체력 : {Hp}/100");
            Console.WriteLine($"마력 : {Mp}/50");
            Console.WriteLine($"Gold : {Gold} G");
        }

        static Dictionary<int, int> LevelTable = new Dictionary<int, int>
        {
            { 1, 10 }, { 2, 35 }, { 3, 65 }, {4,100}
        };
        public void GainExp(int exp)
        {
            Exp += exp;
            if (!LevelTable.ContainsKey(Level))
            {
                Exp = LevelTable[LevelTable.Count];
            }
            if (Exp >= LevelTable[Level])
            {
                Level++;
                base.Atk += 0.5f;
                BaseDef++;
            }
        }
    }
}
