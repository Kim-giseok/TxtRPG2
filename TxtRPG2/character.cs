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
        int Mp { get; set; }
        int Level { get; set; }
        List<Skill> Skills { get; set; }
        void TakeDamage(int Damage);

    }

    public class Character : ICharacter
    {
        public string Name { get; set; }
        public int Hp { get; set; }
        public int Atk { get; set; }
        public int Level { get; set; }
        public int Mp { get; set; }
        public List<Skill> Skills { get; set; } = new List<Skill>();

        public Character(int level, string name, int hp,int mp, int atk)
        {
            Level = level;
            Name = name;
            Hp = hp;
            Mp = mp;
            Atk = atk;
        }

        public void TakeDamage(int Damage)
        {
            Hp -= Damage;
            if (Hp <= 0)
            {
                Hp=0;
            }
        }
        public void UseSkill(int idx, Skill skill)
        {
            if(idx >= 0&& idx < Skills.Count)
            {
                
            }
        }
    }
}
