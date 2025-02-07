namespace TxtRPG2
{
    public enum Job
    {
        Warrior, //전사
        Archer //궁수
    }

    public class Player : Character //플레이어 클래스 . 캐릭터 인터페이스 상속
    {
        public Job Job { get; }
        public int Def { get; set; }
        public int Gold { get; set; }

        public Player(string name, Job job, int level)
        {
            Name = name;
            Job = job;
            Level = level;
            SetJob(job);
        }

        private void SetJob(Job job)
        {
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

            Console.WriteLine(ConsoleUtility.GetInput); //입력값을 받아오는 메서드



            /*  메인에서
                //플레이어 객체 생성
                Console.Write("플레이어 이름을 입력하세요: ");
                string playerName = Console.ReadLine();  // 플레이어 이름 입력 받기
                Console.WriteLine("직업을 선택하세요:");
                Console.WriteLine("1. 전사 (Warrior)");
                Console.WriteLine("2. 궁수 (Archer)");
                int jobChoice = int.Parse(Console.ReadLine());
                Player player;

                // 직업에 맞는 객체 생성
                if (jobChoice == 1)
                {
                player = new Warrior(playerName, 1); // 전사
                }
                else if (jobChoice == 2)
                {
                player = new Archer(playerName, 1); // 궁수
                }
                else
                {
                Console.WriteLine("잘못된 입력입니다.");
                return;
                }

                //상태 보기 메서드 호출
                player.Status();
            */

        }

    }
}
