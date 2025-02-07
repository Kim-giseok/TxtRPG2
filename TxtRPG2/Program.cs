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
            //실행 확인 메인메서드
            Console.Write("플레이어의 이름을 입력해주세요 : ");
            string playerName = Console.ReadLine();

            Player player = new Player(playerName, 1, "전사", 10, 5, 100, 1500); // Player 객체를 한 번만 생성

            bool isRunning = true;

            while (isRunning)
            {
                Console.Write("1. 상태 보기\n2. 데미지 받기\n>> ");
                string input = Console.ReadLine();

                if (input == "1")
                {
                    player.Status(); // 상태 보기
                }
                else if (input == "2")
                {
                    player.TakeDamage(10); // 데미지 받기
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }
        }
    }
}
