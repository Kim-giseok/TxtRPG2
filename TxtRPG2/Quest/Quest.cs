using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG2
{
    class Quest
    {
        public string Name { get; }
        public string Descript { get; }
        public int GoalCount { get; }
        public int NowCount { get; private set; }
        public virtual string Goal { get => $"목표 {GoalCount}회 달성"; }
        public enum State { Ready, Accept, Clear, End };
        public State Stat { get; private set; }

        public Reward QReward { get; private set; }

        public Quest(string name, string descript, int goalCount, int nowCount = 0, State stat = State.Ready, int reward = 0)
        {
            Name = name;
            Descript = descript;
            GoalCount = goalCount;
            NowCount = nowCount;
            Stat = stat;
            QReward = Reward.rewards[reward];
        }

        public void ShowInfo()
        {
            while (true)
            {
                Console.Clear();
                ConsoleUtility.WriteLine("Quest!!", ConsoleColor.Yellow);
                Console.WriteLine();
                Console.WriteLine(Name);
                Console.WriteLine();
                Console.WriteLine(Descript);

                Console.WriteLine();
                Console.WriteLine($"- {Goal} ({NowCount}/{GoalCount})");
                Console.WriteLine();

                Console.WriteLine("- 보상 -");
                foreach (var item in QReward.Items)
                {
                    Console.WriteLine($"  {item.Name} x 1");
                }
                if (QReward.Gold != 0)
                {
                    Console.WriteLine($"{QReward.Gold}G");
                }
                if (QReward.Exp != 0)
                {
                    Console.WriteLine($"{QReward.Exp}Exp");
                }

                Console.WriteLine();
                switch (Stat)
                {
                    case State.Ready:
                        Console.WriteLine("1. 수락");
                        break;
                    case State.Accept:
                        ConsoleUtility.WriteLine("1. 보상 받기", ConsoleColor.DarkGray);
                        break;
                    case State.Clear:
                        Console.WriteLine("1. 보상 받기");
                        break;
                }
                Console.WriteLine("0. 돌아가기");
                int input = ConsoleUtility.GetInput(0, 1);
                switch (input)
                {
                    case 0:
                        return;
                    case 1:
                        switch (Stat)
                        {
                            case State.Ready:
                                Stat = State.Accept;
                                break;
                            case State.Accept:
                                Console.WriteLine("아직 보상을 받을 수 없습니다.");
                                Thread.Sleep(500);
                                break;
                            case State.Clear:
                                Stat = State.End;
                                return;
                        }
                        break;
                }
            }
        }

        public void Triger()
        {
            if (Stat == State.Accept)
            {
                if (++NowCount == GoalCount)
                {
                    Stat = State.Clear;
                }
            }
        }
    }
}
