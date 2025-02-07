using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG2
{
    internal class Item
    {
        public string Name { get; }
        public int Atk { get; }
        public int Def { get; }
        public string Description { get; }
        public int Price { get; }
        public bool IsSold { get; }

        public virtual void ApearInfo() { }
    }
}
