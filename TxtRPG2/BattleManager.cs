using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TxtRPG2
{
    internal class BattleManager
    {
        Player player;
        Enemy[] Enemys;
        Enemy[] spawn;

        public int EnterHp { get; private set; }
        public bool Victory { get => player.Hp != 0; }

        public BattleManager(Player player)
        {
            this.player = player;
            Enemys =
            [
                new Enemy(2, "미니언", 15, 5),
                new Enemy(3, "공허충", 10, 9),
                new Enemy(5, "대포미니언", 25, 8)
            ];
            EnterHp = player.Hp;
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
                spawn[i].AppearInfo();
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
            // 입장시 플레이어의 Hp저장
            EnterHp = player.Hp;
            // 1~4마리의 랜덤한 수의 적 출현
            spawn = new Enemy[new Random().Next(4)];

            for (int i = 0; i < spawn.Length; i++)
            {
                int idx = new Random().Next(Enemys.Length);
                spawn[i] = new Enemy(Enemys[idx].Level, Enemys[idx].Name, Enemys[idx].Hp, Enemys[idx].Atk);
            }

            while (true)
            {
                //전투종료 판정
                int i = 0;
                for (; i < spawn.Length; i++)
                {
                    if (!spawn[i].IsDead)
                    {
                        break;
                    }
                }
                if (!Victory || i == spawn.Length)
                {
                    break;
                }

                ShowInfos();
                // 선택지 표시/선택
                switch (ConsoleUtility.GetInput(1, 1))
                {
                    case 1:
                        PlayerTurn();
                        EnemyTurn();
                        break;
                }
            }
            // 전투 종료 후 결과화면 출력
            Result();
        }

        void Attack(Character actor, Character target)
        {
            int damage = (int)(actor.Atk * new Random().Next(90, 110) / 100f + 0.5f);
            int Hp = target.Hp;
            target.TakeDamage(damage);
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Battle!!");
                Console.WriteLine();

                Console.WriteLine($"{actor.Name}의 공격!");
                Console.WriteLine($"Lv.{target.Level} {target.Name}을(를) 맞췄습니다. [데미지 : {damage}]");

                Console.WriteLine();
                Console.WriteLine($"Lv.{target.Level} {target.Name}");
                if (target.Hp == 0)
                {
                    Console.WriteLine($"HP {Hp} -> dead");
                }
                else
                {
                    Console.WriteLine($"HP {Hp} -> {target.Hp}");
                }

                Console.WriteLine();
                Console.WriteLine("0. 다음");
                switch (ConsoleUtility.GetInput(0, 0))
                {
                    case 0: return;
                }
            }
        }

        void PlayerTurn()
        {
            while (true)
            {
                ShowInfos(true);

                Console.WriteLine("0. 취소");

                int choice = ConsoleUtility.GetInput(0, spawn.Length);
                switch (choice)
                {
                    case 0: return;
                    default:
                        if (spawn[choice - 1].IsDead)
                        {
                            Console.WriteLine("잘못된 입력입니다.");
                            Thread.Sleep(500);
                            break;
                        }
                        else
                        {
                            Attack(player, spawn[choice - 1]);
                            return;
                        }
                }
            }
        }

        void EnemyTurn()
        {
            foreach (var monster in spawn)
            {
                if (!monster.IsDead)
                {
                    Attack(monster, player);
                }
            }
        }

        void Result()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Battle!! - Result");
                Console.WriteLine();
                if (Victory)
                {
                    Console.WriteLine("Victory");
                    Console.WriteLine();
                    Console.WriteLine($"던전에서 몬스터 {spawn.Length}마리를 잡았습니다.");
                }
                else
                {
                    Console.WriteLine("You Lose");
                }
                Console.WriteLine();
                Console.WriteLine($"Lv.{player.Level} {player.Name}");
                Console.WriteLine($"HP {EnterHp} -> {player.Hp}");

                Console.WriteLine();
                Console.WriteLine("0. 다음");
                switch (ConsoleUtility.GetInput(0, 0))
                {
                    case 0: return;
                }
            }
        }
    }
}