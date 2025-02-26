﻿using System;
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
        public virtual int NowCount { get; private set; }
        public virtual string Goal { get => $"목표 {GoalCount}회 달성"; }
        public enum State { Ready, Accept, Clear, End };
        public State Stat { get; protected set; }

        public Reward QReward { get; private set; }

        public Quest(string name, string descript, int reward, int goalCount, int nowCount = 0, State stat = State.Ready)
        {
            Name = name;
            Descript = descript;
            GoalCount = goalCount;
            NowCount = nowCount;
            Stat = stat;
            QReward = Reward.rewards[reward];
        }
        public Quest(Quest quest, int nowCount, State stat)
        {
            Name = quest.Name;
            Descript = quest.Descript;
            GoalCount = quest.GoalCount;
            NowCount = nowCount;
            Stat = stat;
            QReward = quest.QReward;
        }

        public virtual void ShowInfo()
        {
            while (true)
            {
                Console.Clear();
                ConsoleUtility.WriteLine("Quest!!", ConsoleColor.Yellow);
                Console.WriteLine();
                Console.WriteLine(Name + (Stat == State.Clear ? " - Clear!!" : ""));
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
                    Console.WriteLine($"  {QReward.Gold}G");
                }
                if (QReward.Exp != 0)
                {
                    Console.WriteLine($"  {QReward.Exp}Exp");
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
                                Stat = NowCount >= GoalCount ? State.Clear : State.Accept;
                                break;
                            case State.Accept:
                                Console.WriteLine("아직 보상을 받을 수 없습니다.");
                                Thread.Sleep(500);
                                break;
                            case State.Clear:
                                Stat = State.End;
                                Console.WriteLine("퀘스트를 완료했습니다!!");
                                Thread.Sleep(500);
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
