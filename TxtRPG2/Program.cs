namespace TxtRPG2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //깃 연동 확인(김기석)
            //깃 연동 확인(권수민)
            //깃 연동 확인(양원준)
            //222
            Console.WriteLine("Hello, World!");
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
        }
    }
}
