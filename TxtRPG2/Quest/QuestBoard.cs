using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG2
{
    class QuestBoard
    {
        public static Quest[] Quests { get; private set; }
        public Player player { get; private set; }

        public QuestBoard(Player player)
        {
            this.player = player;
            Quests =
            [
                new KillQuest("마을을 위협하는 미니언 처치",
                "이봐! 마을 근처에 미니언들이 너무 많아졌다고 생각하지 않나?\n" +
                "마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\n" +
                "모험가인 자네가 좀 처치해주게!", "미니언", 0, 5),
                new EquipQuest("장비를 장착해보자",
                "강해지기 위해서는 전투를 반복하는 것도 좋지만,\n" +
                "그 만큼 장비를 사용하는 것도 중요하지\n" +
                "장비장착, 한번 해보지 않겠나?", 1, 1),
                new GrowQuest("더욱 더 강해지기!",
                "열심히 단련하고 계신가요?\n" +
                "전투를 거듭하다 보면 레벨이 올라 공격력과 방어력이 상승해요\n" +
                "시험삼아 2레벨까지 올려보는건 어때요?", player, 3, 2)
            ];
        }

        public void ShowInfo()
        {
            while (true)
            {
                Console.Clear();
                ConsoleUtility.WriteLine("Quest!!", ConsoleColor.Yellow);
                Console.WriteLine();
                Console.WriteLine("원하시는 퀘스트를 선택해주세요.");
                for (int i = 0; i < Quests.Length; i++)
                {
                    ConsoleUtility.WriteLine($"{i + 1}. {Quests[i].Name}", Quests[i].Stat == Quest.State.End ? ConsoleColor.DarkGray : Console.ForegroundColor);
                }
                Console.WriteLine();
                Console.WriteLine("0. 돌아가기");
                int input = ConsoleUtility.GetInput(0, Quests.Length);
                switch (input)
                {
                    case 0:
                        return;
                    default:
                        if (Quests[input - 1].Stat == Quest.State.End)
                        {
                            Console.WriteLine("선택할 수 없는 퀘스트입니다.");
                            Thread.Sleep(500);
                            break;
                        }
                        Quests[input - 1].ShowInfo();
                        if (Quests[input - 1].Stat == Quest.State.End)
                        {
                            Quests[input - 1].QReward.ApplyReWard(player, new Dictionary<string, int>());
                        }
                        break;
                }
            }
        }
    }
}
