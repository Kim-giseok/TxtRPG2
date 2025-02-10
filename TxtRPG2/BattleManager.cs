using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace TxtRPG2
{
    internal class BattleManager
    {
        Player player;
        Enemy[] Enemys;
        Enemy[] spawn;
        Skill[] Skills;

        public int EnterHp { get; private set; }
        public bool Victory { get => player.Hp != 0; }

        public BattleManager(Player player)
        {
            this.player = player;
            Enemys =
            [
                new Enemy(2, "미니언", 15,10, 5),//레벨, 이름, 체력,마나, 공격력
                new Enemy(3, "공허충", 10,10, 9),
                new Enemy(5, "대포미니언", 25,10, 8)
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
            Console.WriteLine($"HP {player.Hp:D3}/100 Mp {player.Mp:D3}/20");
            Console.WriteLine();
        }

        public void Battle()
        {
            // 입장시 플레이어의 Hp저장
            EnterHp = player.Hp;
            // 1~4마리의 랜덤한 수의 적 출현
            spawn = new Enemy[new Random().Next(1, 4)];

            for (int i = 0; i < spawn.Length; i++)
            {
                int idx = new Random().Next(Enemys.Length);
                spawn[i] = new Enemy(Enemys[idx].Level, Enemys[idx].Name, Enemys[idx].Hp, Enemys[idx].Mp, Enemys[idx].Atk);
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
                Console.WriteLine("1. 공격");
                Console.WriteLine("2. 스킬");
                int choice = ConsoleUtility.GetInput(1, 2);
                if (choice == 1)
                {
                    PlayerTurn();
                }
                else if (choice == 2)
                {
                    SkillUse(player,null);  // 스킬 사용 모드로 PlayerTurn 호출
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

        void PlayerTurn(bool isSkill = false)
        {
            while (true)
            {
                ShowInfos(true);

                Console.WriteLine("0. 취소");

                int choice = ConsoleUtility.GetInput(0, spawn.Length);
                switch (choice)
                {
                    case 0: return;

                    case 2:  // 처음 선택에서 "2. 스킬"을 누르면 SkillUse로 이동
                        if (player.Skills.Count > 0)
                        {
                            
                            SkillUse(player, null);
                            EnemyTurn();
                            return;
                        }
                        else
                        {
                            Attack(player, spawn[choice - 1]);
                            EnemyTurn();
                            return;
                        }
                    
                    default:
                        if (spawn[choice - 1].IsDead)
                        {
                            Console.WriteLine("잘못된 입력입니다.");
                            Thread.Sleep(500);
                            continue;
                        }
                        
                        else
                        {
                            Attack(player, spawn[choice - 1]);
                            EnemyTurn();
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
        void SkillUse(Character actor, Character target)
        {

            while (true)
            {
                ShowInfos(true);
                Console.WriteLine("[스킬 목록]");
                Console.WriteLine("==========================");
                for (int i = 0; i < player.Skills.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {player.Skills[i].Name}");
                }
                Console.WriteLine("==========================");
                Console.WriteLine("0. 취소");

                int skillChoice = ConsoleUtility.GetInput(0, player.Skills.Count);
                switch (skillChoice)
                {
                    case 0: return;
                    default:
                        int choice = ConsoleUtility.GetInput(0, spawn.Length);
                        if (spawn[choice - 1].IsDead)
                        {
                            Console.WriteLine("이미 죽은 대상입니다.");
                            Thread.Sleep(500);
                            continue;
                        }
                        else
                        {
                            if (player.Mp < player.Skills[skillChoice - 1].ManaCost)
                            {
                                Console.WriteLine("마나가 부족합니다.");
                                Thread.Sleep(500);
                                continue;
                            }
                            player.Skills[skillChoice - 1].Use(player, spawn[choice - 1]);
                            EnemyTurn();

                            // 스킬 사용

                            return;
                        }

                }
            }
        }


    }
}