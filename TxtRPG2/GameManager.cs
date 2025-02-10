using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG2
{
    class GameManager
    {
        private Player player; // 캐릭터를 상속받도록 변경
        private BattleManager battleManager;

        Inventory inven;
        Shop shop;

        public GameManager()
        {
            player = ChooseJob();  // 직업 선택 후 player에 저장
            if (player != null)
            {
                battleManager = new BattleManager(player);
            }
            inven = new Inventory();
            shop = new Shop(player, inven);
        }
        public Player ChooseJob()
        {
            Player player = null;

            Console.Write("플레이어 이름을 입력하세요: ");
            string playerName = Console.ReadLine();

            while (player == null)
            {

                // 직업 선택
                Console.WriteLine("직업을 선택하세요: 1. 전사, 2. 궁수");

                int jobChoice;
                bool isValidInput = int.TryParse(Console.ReadLine(), out jobChoice);

                // 잘못된 입력시
                if (!isValidInput || (jobChoice != 1 && jobChoice != 2))
                {
                    Console.WriteLine("잘못된 입력입니다. 1 또는 2를 입력해주세요.");
                    continue; // 잘못된 입력일 경우 다시 입력받음
                }

                // Player 객체 생성
                if (jobChoice == 1)
                {
                    // 전사 선택 시
                    Console.WriteLine("전사를 선택하셨습니다.");
                    player = new Warrior(playerName, 1, 100, 0);  // 전사 생성
                }
                else if (jobChoice == 2)
                {
                    // 궁수 선택 시
                    Console.WriteLine("궁수를 선택하셨습니다.");
                    player = new Archer(playerName, 1, 80, 0);  // 궁수 생성
                }
            }

            Console.WriteLine("직업 선택 완료! 아무 키나 눌러서 진행하세요.");
            Console.ReadKey();

            return player;
        }


        public void StartScene()
        {
            while (true)
            {   //로딩 중 표시
                ConsoleUtility.Loading();

                Console.Clear();
                // 메뉴 출력
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 전투 시작");
                Console.WriteLine("3. 인벤토리");
                Console.WriteLine("4. 상점");
                Console.WriteLine("0. 종료");

                int input = ConsoleUtility.GetInput(0, 4);

                // 입력한 값에 대한 출력

                switch (input)
                {
                    case 0:
                        return;
                    case 1:
                        ShowStat();
                        break;
                    case 2:
                        EnterBattle();
                        break;
                    // 입력한 값에 대한 출력
                    case 3:
                        ConsoleUtility.Loading();
                        inven.ShowInven();
                        break;
                    case 4:
                        ConsoleUtility.Loading();
                        shop.ShopEnter();
                        break;
                }
            }
        }

        public void ShowStat()
        {
            ConsoleUtility.Loading();

            Console.Clear();

            if (player is Player)
            {
                (player as Player).Status(); // Status 호출
            }
            else
            {
                Console.WriteLine("잘못된 타입입니다.");
            }

            ConsoleUtility.GetInput(0, 0);
            StartScene();
        }

        public void EnterBattle()
        {
            ConsoleUtility.Loading();

            Console.Clear();

            battleManager.Battle();

            StartScene();
        }
    }
}