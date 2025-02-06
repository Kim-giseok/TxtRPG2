using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG2
{
    interface IEnemy // 인터페이스 선언 
    {
        string Name { get; set; }
        int Hp { get; set; }
        int Atk { get; set; }
        int Level { get; set; }
        bool IsDead { get; set; }

        void GetDamage(int Damage);// 받는 피해
    }
    public class Enemy : IEnemy // 인터페이스 상속
    {
        public string Name { get; set; }//이름
        public int Hp { get; set; }// 체력
        public int Atk { get; set; }// 공격력
        public int Level { get; set; }// 레벨
        public bool IsDead { get; set; }// 사망여부

        public Enemy(int level, string name, int hp, int atk ) // 레벨, 이름, 체력, 공격력을 받는 생성자
        {
            this.Level = level;
            this.Name = name;
            this.Hp = hp;
            this.Atk = atk;
            this.IsDead = false;
        }

        public void GetDamage(int Damage) // 피해를 받는 메소드
        {
            Console.WriteLine($"적이 {Damage}만큼의 피해를 받았습니다.");
            Hp -= Damage;
            if (Hp <= 0)
            {
                IsDead = true;
                Console.WriteLine("적이 죽었습니다.");
            }
        }
    }
}
