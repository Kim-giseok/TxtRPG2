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
        public int turnCount; // 전투 턴 수
        public int EnterHp { get; private set; }
        public bool Victory { get => player.Hp != 0; }

        public BattleManager(Player player)
        {
            this.player = player;
            Enemys =
            [
                new Enemy(2, "미니언", 15,5, 5),//레벨, 이름, 체력,마나, 공격력
                new Enemy(3, "공허충", 10,5, 9),
                new Enemy(5, "대포미니언", 25,10, 8),
                new Enemy(100, "챔피언", 300,13, 10)
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
            Console.WriteLine($"HP {player.Hp:D3}/{player.Hp} Mp {player.Mp:D3}/{player.Mp}");
            Console.WriteLine();
        }

        public void Battle()
        {
            // 입장시 플레이어의 Hp저장
            EnterHp = player.Hp;
            // 1~4마리의 랜덤한 수의 적 출현
            spawn = new Enemy[new Random().Next(1, 5)];

            for (int i = 0; i < spawn.Length; i++)
            {
                int idx = new Random().Next(Enemys.Length);
                spawn[i] = new Enemy(Enemys[idx].Level, Enemys[idx].Name, Enemys[idx].Hp, Enemys[idx].Mp, Enemys[idx].Atk);
            }
            turnCount = 0;
            while (true)
            {
                turnCount++;
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

                ShowInfos();//1

                // 선택지 표시/선택
                Console.WriteLine("턴 번호 : " + turnCount);
                Console.WriteLine("1. 공격"); // 스킬
                Console.WriteLine("2. 스킬");
                int choice = ConsoleUtility.GetInput(1, 2);
                if (choice == 1)
                {
                    PlayerTurn();
                }
                else if (choice == 2)
                {
                    SkillUse(player, null);  // 
                }
            }
            // 전투 종료 후 결과화면 출력
            Result();
        }

        void Attack(Character actor, Character target, bool isSkill = false, string skillName = "")
        {
            int damage = (int)(actor.Atk * new Random().Next(90, 110) / 100f + 0.5f);
            int Hp = target.Hp;

            target.TakeDamage(damage);

            while (true)
            {
                Console.Clear();

                Console.WriteLine("Battle!!");
                Console.WriteLine();
                target.ProcessStatusEffects(); // 상태이상 표시
                Console.WriteLine($"{actor.Name}의 공격!");
                
                if (actor is Enemy enemyActor)
                {
                    enemyActor.EnemySkill(enemyActor, (Player)target, spawn);
                }
                Console.WriteLine($"Lv.{target.Level} {target.Name}에게 [데미지 : {damage}]의 추가피해를 입혔습니다. ");
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
            while (true)//enemy 가 죽었을 경우 탈출
            {
                ShowInfos(true);//2

                Console.WriteLine("0. 취소"); // 0번 누르면 탈출

                int choice = ConsoleUtility.GetInput(0, spawn.Length);


                if (choice == 0)
                {
                    break;
                }
                else if (choice > spawn.Length)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(500);

                    continue;
                }
                else
                {
                    if (spawn[choice - 1].IsDead == true)
                    {
                        Console.WriteLine($"이미 죽은 적 입니다.");
                        Thread.Sleep(500);

                        break;
                    }
                    else if (player.IsStun == true)
                    {
                        Console.WriteLine($"기절한 상태 입니다.");
                        Thread.Sleep(500);
                        EnemyTurn();
                        break;
                    }
                    else
                    {
                        Attack(player, spawn[choice - 1]);
                        EnemyTurn();
                        break;
                    }

                }

            }
        }

        void EnemyTurn()
        {

            foreach (var monster in spawn)
            {
                if (!monster.IsDead && !monster.IsStun)
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
                ShowInfos(true); //3
                Console.WriteLine("[스킬 목록]");
                Console.WriteLine("==========================");
                for (int i = 0; i < player.Skills.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {player.Skills[i].Name} (MP {player.Skills[i].ManaCost}) [범위: {player.Skills[i].Range}]");
                }
                Console.WriteLine("==========================");
                Console.WriteLine("0. 취소");

                int skillChoice = ConsoleUtility.GetInput(0, player.Skills.Count);
                if (skillChoice == 0) return;

                Skill chosenSkill = player.Skills[skillChoice - 1]; //선택한 스킬 을 chosenSkill에 저장

                if (player.Mp < chosenSkill.ManaCost)
                {
                    Console.WriteLine("마나가 부족합니다.");
                    Thread.Sleep(500);
                    continue;
                }

                int choice = ConsoleUtility.GetInput(0, spawn.Length);
                if (choice == 0) return;

                Enemy targetEnemy = spawn[choice - 1];

                if (targetEnemy.IsDead)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(500);
                    continue;
                }

                // 스킬 실행 (Range 적용)
                chosenSkill.Use(player, targetEnemy, spawn);

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Battle!!");
                    Console.WriteLine();

                    Console.WriteLine($"{actor.Name}의 공격!");

                    // 범위 공격 대상 리스트 가져오기
                    List<Enemy> finalTargets = new List<Enemy>();
                    finalTargets.Add(targetEnemy);  // 기본 공격 대상
                    Random rand = new Random();
                    List<Enemy> possibleTargets = spawn.Where(e => !e.IsDead && e != targetEnemy).ToList(); // 추가공격 리스트, 죽은적 제외, 최초 공격대상 제외

                    for (int i = 0; i < chosenSkill.Range - 1 && possibleTargets.Count > 0; i++)
                    {
                        int randomIndex = rand.Next(possibleTargets.Count);
                        finalTargets.Add(possibleTargets[randomIndex]);
                        possibleTargets.RemoveAt(randomIndex);
                    }

                    // 모든 대상에 대해 개별적인 체력 변화 출력
                    foreach (var eTarget in finalTargets)
                    {
                        int HpBefore = eTarget.Hp;
                        int damage = (int)(actor.Atk * chosenSkill.DamageMultiplier);
                        eTarget.TakeDamage(damage);

                        Console.WriteLine($"Lv.{eTarget.Level} {eTarget.Name}에게 [{chosenSkill.Name}] [데미지 : {damage}]");
                        eTarget.ProcessStatusEffects();
                        Console.WriteLine();
                        Console.WriteLine($"Lv.{eTarget.Level} {eTarget.Name}");
                        if (eTarget.Hp == 0)
                        {
                            Console.WriteLine($"HP {HpBefore} -> dead");
                        }
                        else
                        {
                            Console.WriteLine($"HP {HpBefore} -> {eTarget.Hp}");
                        }
                    }

                    Console.WriteLine();
                    Console.WriteLine("0. 다음");
                    switch (ConsoleUtility.GetInput(0, 0))
                    {
                        case 0:
                            EnemyTurn();
                            return;
                    }
                    break;
                }
            }
        }
    }
}



