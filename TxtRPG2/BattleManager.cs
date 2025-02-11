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
            Console.WriteLine($"HP {player.Hp}/100 Mp {player.Mp}/50");
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
            turnCount = 1;
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

                ShowInfos();//1

                // 선택지 표시/선택
                Console.WriteLine("턴 번호 : " + turnCount);
                Console.WriteLine("1. 공격");
                Console.WriteLine("2. 스킬");
                int choice = ConsoleUtility.GetInput(1, 2);
                switch (choice)
                {
                    case 1:
                        PlayerAttack();
                        break;
                    case 2:
                        PlayerSkill();
                        break;
                }
            }
            // 전투 종료 후 결과화면 출력
            Result();
        }

        void PlayerAttack()
        {
            while (true)
            {
                ShowInfos(true);//2

                Console.WriteLine("0. 취소");
                int choice = ConsoleUtility.GetInput(0, spawn.Length);
                switch (choice)
                {
                    case 0:
                        return;
                    default:
                        if (spawn[choice - 1].IsDead)
                        {
                            Console.WriteLine($"이미 죽은 적 입니다.");
                            Thread.Sleep(500);
                            break;
                        }
                        player.Attack(spawn[choice - 1]);
                        EnemyTurn();
                        return;
                }
            }
        }

        void PlayerSkill()
        {
            //스킬 선택창 출력
            while (true)
            {
                ShowInfos();

                player.ShowSkills();
                Console.WriteLine("0. 취소");
                Console.WriteLine();
                int input = ConsoleUtility.GetInput(0, player.Skills.Count);
                switch (input)
                {
                    case 0:
                        return;
                    default:
                        SelectSkillTarget(player.Skills[input - 1]);
                        return;
                }
            }
        }

        void SelectSkillTarget(Skill skill)
        {
            if (skill.Range == 1)
            {
                while (true)
                {
                    ShowInfos(true);

                    Console.WriteLine("0. 취소");
                    int choice = ConsoleUtility.GetInput(0, spawn.Length);
                    switch (choice)
                    {
                        case 0:
                            return;
                        default:
                            if (spawn[choice - 1].IsDead)
                            {
                                Console.WriteLine($"이미 죽은 적 입니다.");
                                Thread.Sleep(500);
                                break;
                            }
                            player.UseSkill(skill, new Character[] { spawn[choice - 1] });
                            EnemyTurn();
                            return;
                    }
                }
            }
            else
            {
                List<Character> lives = new List<Character>();
                foreach (Character c in spawn)
                {
                    if (!c.IsDead)
                    {
                        lives.Add(c);
                    }
                }

                lives = lives.OrderBy(x => new Random().Next()).ToList();
                int targetnum = lives.Count > skill.Range ? skill.Range : lives.Count;
                Character[] targets = new Character[targetnum];
                for (int i = 0; i < targets.Length; i++)
                {
                    targets[i] = lives[i];
                }

                player.UseSkill(skill, targets);
                EnemyTurn();
            }
        }

        void EnemyTurn()
        {

            foreach (var monster in spawn)
            {
                if (!monster.IsDead)
                {
                    monster.Attack(player);
                }
            }
            turnCount++;
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
