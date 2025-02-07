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
        private Player player;
        private BattleManager battleManager;

        public GameManager()
        {
            player = new player("전사",100,10,5);
            battleManager = new battleManager(player);
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
                Console.WriteLine("0. 종료");
                Console.Write("선택 (1, 2): ");

                int input = ConsoleUtility.GetInput(1, 2);

                // 입력한 값에 대한 출력

                switch (input)
                {
                    case 1:
                        ShowStat();
                        break;
                    case 2:
                        EnterBattle();
                        break;

                        // 입력한 값에 대한 출력


                }
            }
        }
        public void ShowStat()
        {
            ConsoleUtility.Loading();

            Console.Clear();

            player.Status();

            ConsoleUtility.GetInput(0, 0);
            StartScene();



        }
        public void EnterBattle()
        {
            ConsoleUtility.Loading();

            Console.Clear();

            battleManager.Battle();

            ConsoleUtility.GetInput(0, 0);
            StartScene();
        }
    }
}