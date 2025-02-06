namespace TxtRPG2
{
    namespace TxtRPG2
    {
        internal class Program
        {

            public interface IPlayer //플레이어 인터페이스 정의
            {
                public string Name { get; }
                public int Level { get; }
                public int Atk { get; }
                public int Def { get; }
                public int HP { get; set; }
                public void TakeDamage(int damage);
            }
            public class Player : IPlayer //플레이어 클래스 . IPlayer 인터페이스 상속
            {
                public string Name { get; }
                public int Level { get; }
                public string Job { get; }
                public int Atk { get; }
                public int Def { get; }
                public int HP { get; set; }
                public int Gold { get; set; }

                public Player(string name, int level, string job, int atk, int def, int hp, int gold)
                {
                    Name = name;
                    Level = 1;
                    Job = job;
                    Atk = 10;
                    Def = 5;
                    HP = 100;
                    Gold = gold;
                }

                public void TakeDamage(int damage) //플레이어가 데미지 받을때 호출되는 메서드
                {
                    Console.WriteLine($"HP {HP} -> {HP - damage}");
                    HP -= damage;
                    if (HP < 0)
                    {
                        HP = 0;
                    }
                    Console.Write("0. 메뉴화면으로\n>> ");
                    string input = Console.ReadLine();
                    if (input == "0")
                    {
                        Console.WriteLine("메뉴화면으로 나갑니다.");
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                    }
                }

            }

            static void Status(Player player) //플레이어의 상태를 보여주는 메서드
            {
                Console.WriteLine("상태 보기\n캐릭터의 정보가 표시됩니다.");
                Console.WriteLine($"\nlv. {player.Level:D2}");
                Console.WriteLine($"{player.Name} ({player.Job})");
                Console.WriteLine($"공격력 : {player.Atk}");
                Console.WriteLine($"방어력 : {player.Def}");
                Console.WriteLine($"체력 : {player.HP}");
                Console.WriteLine($"Gold : {player.Gold} G");
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

            static void Main(string[] args) //메인 메서드
            {  
                Console.Write("플레이어의 이름을 입력해주세요 : ");
                string playerName = Console.ReadLine();

                bool isRunning = true;

                while (isRunning)

                {
                    Player player = new Player(playerName, 1, "전사", 10, 5, 100, 1500);
                    Console.Write("1. 상태 보기\n2. 데미지 받기\n>> ");
                    string input = Console.ReadLine();

                    if (input == "1")
                    {
                        Status(player);
                    }
                    else if (input == "2")
                    {
                        player.TakeDamage(10);
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                    }
                }
            }
        }
    }

}
