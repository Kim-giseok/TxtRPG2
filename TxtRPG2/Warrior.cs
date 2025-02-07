using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG2
{

    public class Warrior : Player
    {
        public Warrior (string name, int level) : base(name, Job.Warrior, level)
        {
            SetJob(); //스탯 설정
        }

        private void SetJob()
        {
            Atk = 10;
            Def = 20;
            Hp = 100;
        }  

    }

}
