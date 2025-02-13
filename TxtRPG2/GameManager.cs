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
        Player player; // 캐릭터를 상속받도록 변경
        BattleManager battleManager;

        Shop shop;

        QuestBoard questBoard;

        public GameManager()
        {
            try
            {
                SaveData.Load(out player, out shop, out battleManager);
            }
            catch (Exception)
            {
                Console.WriteLine("저장된 데이터가 없습니다. 새로운 게임을 시작합니다.");
                Console.ReadKey();
                Console.Clear();
                player = ChooseJob();  // 직업 선택 후 player에 저장
                shop = new Shop(player);
                battleManager = new BattleManager(player);
            }
            questBoard = new QuestBoard(player);
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
                    player = new Warrior(playerName);  // 전사 생성
                }
                else if (jobChoice == 2)
                {
                    // 궁수 선택 시
                    Console.WriteLine("궁수를 선택하셨습니다.");
                    player = new Archer(playerName);  // 궁수 생성
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
                Console.WriteLine("5. 퀘스트");
                Console.WriteLine("6. 저장");
                Console.WriteLine("0. 종료");

                int input = ConsoleUtility.GetInput(0, 6);
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
                        player.inven.ShowInven();
                        break;
                    case 4:
                        ConsoleUtility.Loading();
                        shop.ShopEnter();
                        break;
                    case 5:
                        ConsoleUtility.Loading();
                        questBoard.ShowInfo();
                        break;
                    case 6:
                        SaveData.Save(player, shop, battleManager);
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
            Console.WriteLine();
            Console.WriteLine("0.나가기");
            ConsoleUtility.GetInput(0, 0);
        }

        public void EnterBattle()
        {
            while (true)
            {
                ConsoleUtility.Loading();

                Console.Clear();
                Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
                Console.WriteLine("이제 전투를 시작할 수 있습니다.");

                Console.WriteLine();
                Console.WriteLine("1. 상태 보기");
                Console.Write($"2. 전투 시작 (현재 진행 : ");
                ConsoleUtility.Write($"{battleManager.Floor}", ConsoleColor.DarkRed);
                Console.WriteLine("층)");
                Console.WriteLine("3. 회복 아이템");
                Console.WriteLine("0. 나가기");
                switch (ConsoleUtility.GetInput(0, 3))
                {
                    case 0:
                        return;
                    case 1:
                        ShowStat();
                        break;
                    case 2:
                        ConsoleUtility.Loading();
                        battleManager.Battle();
                        break;
                    case 3:
                        ConsoleUtility.Loading();
                        player.inven.UsePotion(player);
                        break;
                }
            }
        }
    }
}