using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG2
{
    public class Archer : Player
    {
        public Archer(string name, int level) : base(name, Job.Archer, level)
        {
            SetJob(); //스탯 설정
        }

        private void SetJob()
        {
            Atk = 20;
            Def = 15;
            Hp = 80;
        }

    }
}
