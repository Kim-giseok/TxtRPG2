namespace TxtRPG2
{
    public interface ICharacter //ICharacter 인터페이스 정의
    {
        string Name { get; set; }
        int Hp { get; set; }
        int Atk { get; set; }
        int Level { get; set; }
        void TakeDamage(int damage);
    }
    public class Character : ICharacter //캐릭터 클래스 . ICharacter 인터페이스 상속 
    {
        public string Name { get; set; }
        public int Hp { get; set; }
        public int Atk { get; set; }
        public int Level { get; set; }
        public void TakeDamage(int Damage)
        {
            // 공격력의 ±10% 변동을 적용한 최종 공격력 계산
            Damage = (int)Math.Ceiling(Damage * (1 + (new Random().NextDouble() * 0.2 - 0.1)));
            //finalAtk = (int)Damage; // 데미지를 int로 변환
            Console.WriteLine($"{Name}이 {Damage}만큼의 피해를 받았습니다.(기준 공격력: {Atk})");

            Hp -= Damage;
            if (Hp <= 0)
            {
                Hp = 0;
                Console.WriteLine($"{Name}이 죽었습니다.");
            }
            else
            {
                Console.WriteLine($"남은 체력: {Hp}");
            }
        }
    }
    public class Player : Character //플레이어 클래스 . 캐릭터 인터페이스 상속
    {
        public string Job { get; }
        public int Def { get; set; }
        public int Gold { get; set; }

        public Player(string name, int level, string job, int atk, int def, int hp, int gold)
        {
            Name = name;
            Level = level;
            Job = job;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
        }



        public void Status() //플레이어의 상태를 보여주는 메서드
        {
            Console.WriteLine("상태 보기\n캐릭터의 정보가 표시됩니다.");
            Console.WriteLine($"\nlv. {Level:D2}");
            Console.WriteLine($"{Name} ({Job})");
            Console.WriteLine($"공격력 : {Atk}");
            Console.WriteLine($"방어력 : {Def}");
            Console.WriteLine($"체력 : {Hp}");
            Console.WriteLine($"Gold : {Gold} G");
            Console.Write("\n0.나가기\n>> ");

            string input = Console.ReadLine();
            if (input == "0")
            {
                Console.WriteLine("상태보기 창을 닫습니다.");
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }
}
