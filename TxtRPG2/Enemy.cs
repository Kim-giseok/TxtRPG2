using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG2
{
    interface IEnemy
    {
        string Name { get; set; }
        int Hp { get; set; }
        int Atk { get; set; }
        int Level { get; set; }
        bool IsDead { get; set; }

        void GetDamage(int a);
    }
    public class Enemy : IEnemy
    {
        public string Name { get; set; }
        public int Hp { get; set; }
        public int Atk { get; set; }
        public int Level { get; set; }
        public bool IsDead { get; set; }

        public Enemy(string name, int health, int attackPower, int level)
        {
            this.Name = name;
            this.Hp = health;
            this.Atk = attackPower;
            this.Level = level;
            this.IsDead = false;
        }

        public void GetDamage(int a)
        {
            Console.WriteLine("적이 공격을 받았습니다.");
            Hp -= a;
            if (Hp <= 0)
            {
                IsDead = true;
                Console.WriteLine("적이 죽었습니다.");
            }
        }
    }
}
