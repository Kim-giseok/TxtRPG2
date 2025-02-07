﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG2
{
    interface ICharacter
    {
        string Name { get; set; }
        int Hp { get; set; }
        int Atk { get; set; }
        int Level { get; set; }
        void TakeDamage(int Damage);

    }

    public class Character : ICharacter
    {
        public string Name { get; set; }
        public int Hp { get; set; }
        public int Atk { get; set; }
        public int Level { get; set; }
        public Character(string name, int hp, int atk, int level)
        {
            Name = name;
            Hp = hp;
            Atk = atk;
            Level = level;
        }

        public void TakeDamage(int Damage)
        {
            Hp -= Damage;
            if (Hp <= 0)
            {
                Hp=0;
            }
        }

    }
}
