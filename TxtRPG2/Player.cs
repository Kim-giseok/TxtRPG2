namespace TxtRPG2
{

    internal class Player : Character //플레이어 클래스 . 캐릭터 인터페이스 상속
    {
        public string Job { get; }
        public override int Atk { get => base.Atk + (inven.EWeapon != null ? inven.EWeapon.Atk : 0); }
        public int Def { get => BaseDef + (inven.EAmor != null ? inven.EAmor.Def : 0); }
        public int Gold { get; set; }

        public int BaseDef { get; set; }

        public int Exp { get; set; }

        public Inventory inven { get; set; } = new Inventory();

        public Player(string name, int level, string job, int atk, int def, int hp = 100, int mp = 50, int gold = 1500) : base(level, name, hp, mp, atk)
        {
            Job = job;
            BaseDef = def;
            Gold = gold;
        }

        public void Status() //플레이어의 상태를 보여주는 메서드
        {
            Console.WriteLine("상태 보기\n캐릭터의 정보가 표시됩니다.");
            Console.WriteLine($"\nlv. {Level:D2}");
            Console.WriteLine($"{Name} ({Job})");
            Console.WriteLine($"공격력 : {Atk}" + (inven.EWeapon != null ? $" (+{inven.EWeapon.Atk})" : ""));
            Console.WriteLine($"방어력 : {Def}" + (inven.EAmor != null ? " (+{inven.EAmor.Def})" : ""));
            Console.WriteLine($"체력 : {Hp}/100");
            Console.WriteLine($"마력 : {Mp}/50");
            Console.WriteLine($"Gold : {Gold} G");
            Console.WriteLine("\n0.나가기\n");
        }
    }
}
