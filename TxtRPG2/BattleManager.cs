using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG2
{
    internal class BattleManager
    {
        Player player;
        Enemy[] Enemys;
        public bool Victory { get => player.Hp != 0; }

        public BattleManager(Player player)
        {
            player = player;
            Enemys =
            [
                new Enemy(2, "미니언", 15, 5),
                new Enemy(3, "공허충", 10, 9),
                new Enemy(5, "대포미니언", 25, 8)
            ]
        }

        public void Battle()
        {
            // 1~4마리의 랜덤한 수의 적 출현
            Enemy[] spawn = new Enemy[new Random().Next(4)];

            foreach (var enemy in spawn)
            {
                int idx = new Random().Next(Enemys.Length);
                enemy = new Enemy(Enemys[idx].Level, Enemys[idx].Name, Enemys[idx].Hp, Enemys[idx].Atk);
            }

            // 적들의 정보 출력
            foreach (var enemy in spawn)
            {
                Console.WriteLine(enemy);
            }

            // 플레이어의 레벨 이름(직업) \n 체력/최대체력 표시
            Console.WriteLine();
            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{player.Level}\t{player.Name} ({player.Job})");
            Console.WriteLine($"HP {player.Hp:D3}/100");

            // 선택지 표시/선택
            Console.WriteLine("1. 공격");
            GameManager.select(out byte choice);
            switch(choice)
            {
                case 1:
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
            }
        }
    }
}
