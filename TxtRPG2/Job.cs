using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TxtRPG2;

namespace TxtRPG2
{
    public class Warrior : Player
    {
        public Warrior(string name, int level, int hp, int gold) : base(name, level, "전사", 10, 15, 100, gold)
        {
            Name = name;
            Level = level;
            Job = "전사";
            Atk = 10;  // 전사 공격력
            Def = 15;  // 전사 방어력
            Hp = 100;
            Gold = 1500;
        }

        public string Job { get; set; }

        public void Status()
        {
            Console.WriteLine("\n상태 보기\n캐릭터의 정보가 표시됩니다.");
            Console.WriteLine($"Lv. {Level:D2}");
            Console.WriteLine($"{Name} ({Job})");
            Console.WriteLine($"공격력 : {Atk}");
            Console.WriteLine($"방어력 : {Def}");
            Console.WriteLine($"체력 : {Hp}");
            Console.WriteLine($"Gold : {Gold} G");
            Console.WriteLine("\n0. 나가기");
        }
    }

    public class Archer : Player
    {
        public Archer(string name, int level, int hp, int gold) : base(name, level, "궁수", 20, 10, 80, gold)
        {
            Name = name;
            Level = level;
            Job = "궁수";
            Atk = 20;  // 궁수 공격력
            Def = 10;   // 궁수 방어력
            Hp = 80;
            Gold = 1500;
        }

        public string Job { get; set; }

        public void Status()
        {
            Console.WriteLine("\n상태 보기\n캐릭터의 정보가 표시됩니다.");
            Console.WriteLine($"Lv. {Level:D2}");
            Console.WriteLine($"{Name} ({Job})");
            Console.WriteLine($"공격력 : {Atk}");
            Console.WriteLine($"방어력 : {Def}");
            Console.WriteLine($"체력 : {Hp}");
            Console.WriteLine($"Gold : {Gold} G");
            Console.WriteLine("\n0. 나가기");
        }
    }
}
/*
상태 보기 메서드 호출

// 플레이어 이름 입력
Console.Write("플레이어 이름을 입력하세요: ");
string playerName = Console.ReadLine();

// 직업 선택
Console.WriteLine("직업을 선택하세요: 1. 전사, 2. 궁수");

// 사용자가 입력한 값이 숫자인지 확인
int jobChoice;
bool isValidInput = int.TryParse(Console.ReadLine(), out jobChoice);

// 입력값이 숫자가 아닐 경우 처리
if (!isValidInput)
{
    Console.WriteLine("잘못된 입력입니다. 숫자를 입력해주세요.");
    return;
}

// Player 객체 생성
Character player;

if (jobChoice == 1)
{
    // 전사 선택 시
    Console.WriteLine("전사를 선택하셨습니다.");
    player = new Warrior(playerName, 1, 100, 0);  // 전사 생성
}
else if (jobChoice == 2)
{
    // 궁수 선택 시
    Console.WriteLine("궁수를 선택하셨습니다.");
    player = new Archer(playerName, 1, 80, 0);  // 궁수 생성
}
else
{
    Console.WriteLine("잘못된 선택입니다.");
    return;
}

// 상태 보기 선택
Console.WriteLine("\n1. 상태보기");
Console.WriteLine("0. 나가기");
string input = Console.ReadLine();

if (input == "1")
{
    // 상태 보기 호출
    if (player is Warrior)
    {
        (player as Warrior).Status();
    }
    else if (player is Archer)
    {
        (player as Archer).Status();
    }
}
else if (input == "0")
{
    Console.WriteLine("게임을 종료합니다.");
}
else
{
    Console.WriteLine("잘못된 선택입니다. 프로그램을 종료합니다.");
}
*/