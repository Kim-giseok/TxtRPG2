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
        Enemy[] spawn;
        public bool Victory { get => player.Hp != 0; }

        public BattleManager(Player player)
        {
            player = player;
            Enemys =
            [
                new Enemy(2, "미니언", 15, 5),
                new Enemy(3, "공허충", 10, 9),
                new Enemy(5, "대포미니언", 25, 8)
            ];
        }

        void ShowInfos(bool select = false)
        {
            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine();
            // 적들의 정보 출력
            for (int i = 0; i < spawn.Length; i++)
            {
                if (select)
                {
                    Console.Write($"{i + 1} ");
                }
                Console.WriteLine(spawn[i]);
            }

            // 플레이어의 레벨 이름(직업) \n 체력/최대체력 표시
            Console.WriteLine();
            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{player.Level}\t{player.Name} ({player.Job})");
            Console.WriteLine($"HP {player.Hp:D3}/100");
            Console.WriteLine();
        }

        public void Battle()
        {
            // 1~4마리의 랜덤한 수의 적 출현
            spawn = new Enemy[new Random().Next(4)];

            for (int i = 0; i < spawn.Length; i++)
            {
                int idx = new Random().Next(Enemys.Length);
                spawn[i] = new Enemy(Enemys[idx].Level, Enemys[idx].Name, Enemys[idx].Hp, Enemys[idx].Atk);
            }

            while (true)
            {
                ShowInfos();
                // 선택지 표시/선택
                Console.WriteLine("1. 공격");
                GameManager.Select(out byte choice);
                switch (choice)
                {
                    case 1:
                        PlayerTurn();
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        Thread.Sleep(500);
                        break;
                }
            }
        }

        void PlayerTurn()
        {
            while (true)
            {
                ShowInfos(true);

                Console.WriteLine("0. 취소");

                if (GameManager.Select(out byte choice) && choice == 0)
                {
                    break;
                }
                try
                {
                    if (spawn[choice - 1].IsDead)
                    {
                        throw new Exception("");
                    }

                    PlayerAttack(choice -1);
                    return;
                }
                catch (Exception e)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(500);
                }
            }
        }

        void PlayerAttack(int idx)
        {
            int damage = player.Atk * new Random().Next(90, 110) / 100;
            int Hp = spawn[idx].Hp;
            spawn[idx].GetDamage(damage);
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Battle!!");
                Console.WriteLine();

                Console.WriteLine($"{player.Name}의 공격!");
                Console.WriteLine($"Lv.{spawn[idx].Level} {spawn[idx].Name}을(를) 맞췄습니다. [데미지 : {damage}]");

                Console.WriteLine();
                Console.WriteLine($"Lv.{spawn[idx].Level} {spawn[idx].Name}");
                if (spawn[idx].IsDead)
                {
                    Console.WriteLine($"HP {Hp} -> dead");
                }
                else
                {
                    Console.WriteLine($"HP {Hp} -> {spawn[idx].Hp}");
                }

                Console.WriteLine();
                Console.WriteLine("0. 다음");
                if (GameManager.Select(out byte choice) && choice == 0)
                {
                    MonsterTurn();
                    break;
                }
                Console.WriteLine("잘못된 입력입니다.");
                Thread.Sleep(500);
            }
        }

        void MonsterTurn()
        { }
    }
}
