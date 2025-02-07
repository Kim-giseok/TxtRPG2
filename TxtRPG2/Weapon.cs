using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG2
{
    internal class Weapon : Item
    {
        public Weapon(string name, int atk, string description, int price, int isSoid) : base(name, atk, 0, description, price)
        { }
    }
}
