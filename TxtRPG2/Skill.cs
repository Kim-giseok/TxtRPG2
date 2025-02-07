using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG2
{
    public class Skill

    {
        public string Name { get; }
        public int ManaCost { get; }
        public int DamageMultiplier { get; }
        public int Range { get; set; }

        public Skill(string name, int manaCost, int damageMultiplier, int range)
        {
            Name = name;
            ManaCost = manaCost;
            DamageMultiplier = damageMultiplier;
            Range = range;
        }

        public void Use(Player player, Enemy enemy)
        {
            if (player.Mp >= ManaCost)
            {
                player.Mp -= ManaCost;
                int damage = player.Atk * DamageMultiplier;
                enemy.TakeDamage(damage);
                
            }
            
        }
    }
}
