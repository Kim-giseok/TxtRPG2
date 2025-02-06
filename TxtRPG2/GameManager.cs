using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG2
{
    class Program
    {
        static void Main(string[] args)
        {
            // 게임 시작 인사
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다. \n이제 전투를 시작할 수 있습니다.");
            Select();
        }

        static void Select()
        {
            while (true)
            {   //로딩 중 표시
                ConsoleUtility.Loading();

                Console.Clear();
                // 메뉴 출력
                Console.WriteLine("\n무엇을 하시겠습니까?");
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 전투 시작");
                Console.WriteLine("0. 종료");
                Console.Write("선택 (1, 2, 0): ");

                string userPlay = Console.ReadLine();

                // 입력한 값에 대한 출력

                switch (userPlay)
                {
                    case "1":
                        ShowStat();
                        break;
                    case "2":
                        EnterBattle();
                        break;
                    case "0":
                        Console.WriteLine("게임을 종료합니다. 안녕히 가세요!");
                        return;
                    default:
                        Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                        break;
                }
            }
        }
    }
}
