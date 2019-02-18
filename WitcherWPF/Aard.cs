using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherWPF {
    class Aard : Sign {
        public int StunChance { get; set; }
        public int StunDuration { get; set; }
        public int KnockBackChance { get; set; }

        public Aard(int StunChance, int StunDuration) {
            this.StunChance = StunChance;
            this.StunDuration = StunDuration;
        }
        public Aard() {

        }
        public bool Stun() {
            Random rand = new Random();
            int rn = rand.Next(0, 100);
            if (rn < this.StunChance + this.SignIntensity / 10) {
                return true;
            } else {
                return false;
            }
            
        }
        public bool KnockBack() {
            Random rand = new Random();
            int rn = rand.Next(0, 100);
            if (rn < this.KnockBackChance + this.SignIntensity / 10) {
                return true;
            } else {
                return false;
            }

        }

        public override int EndurCost() {
            if (this.Effectivity == 1) {
                return this.EnduranceCost - 5;
            }else {
                return this.EnduranceCost;
            }
        }
    }
}
