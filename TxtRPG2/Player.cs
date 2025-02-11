namespace TxtRPG2
{

    public class Player : Character //플레이어 클래스 . 캐릭터 인터페이스 상속
    {
        public string Job { get; }
        public int Def { get; set; }
        public int Gold { get; set; }

        public Player(string name, int level, string job, int atk, int def, int hp = 100, int mp = 50, int gold = 1500) : base(level, name, hp, mp, atk)
        {
            Job = job;
            Def = def;
            Gold = gold;
        }

        public void Status() //플레이어의 상태를 보여주는 메서드
        {
            Console.WriteLine("상태 보기\n캐릭터의 정보가 표시됩니다.");
            Console.WriteLine($"\nlv. {Level:D2}");
            Console.WriteLine($"{Name} ({Job})");
            Console.WriteLine($"공격력 : {Atk}");
            Console.WriteLine($"방어력 : {Def}");
            Console.WriteLine($"체력 : {Hp}");
            Console.WriteLine($"Gold : {Gold} G");
            Console.WriteLine("\n0.나가기\n");
            /*  메인에서
                플레이어 객체 생성
                Player player = new Player(playerName, 1, "전사", 10, 5, 100, 1500);

                상태 보기 메서드 호출
                player.Status();
            */
        }

    }
}
