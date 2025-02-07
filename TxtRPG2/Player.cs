namespace TxtRPG2
{

    public class Player : Character //플레이어 클래스 . 캐릭터 인터페이스 상속
    {
        public string Job { get; }
        public int Def { get; set; }
        public int Gold { get; set; }

        public Player(string name, int level, string job, int atk, int def, int hp, int gold)
        {
            Name = name;
            Level = level;
            Job = job;
            Atk = atk;
            Def = def;
            Hp = hp;
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
            Console.Write("\n0.나가기\n>> ");

            string input = Console.ReadLine();
            if (input == "0")
            {
                Console.WriteLine("상태보기 창을 닫습니다.");
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }
}
