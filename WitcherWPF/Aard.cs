using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherWPF {
    class Aard : Sign {
        public int StunChance { get; set; }
        public int StunDuration { get; set; }

        public Aard(int StunChance, int StunDuration) {
            this.StunChance = StunChance;
            this.StunDuration = StunDuration;
        }
        public Aard() {

        }
        public bool Stun() {
            Random rand = new Random();
            int rn = rand.Next(0, 100);
            if (rn < this.StunChance) {
                return true;
            } else {
                return false;
            }
            
        }
    }
}
