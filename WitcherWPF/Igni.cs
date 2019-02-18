using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherWPF {
    class Igni : Sign {
        public int Damage { get; set; }
        public int BurnChance { get; set; }
        public int BurnDamage { get; set; }
        public int BurnDuration { get; set; }
        public Igni() {

        }

        public Igni(int Damage, int BurnChance, int BurnDuration) {
            this.Damage = Damage;
            this.BurnChance = BurnChance;
            this.BurnDuration = BurnDuration;
        }
        
        public bool Burn() {
            Random rand = new Random();
            int rn = rand.Next(0, 100);
            if (rn < this.BurnChance + this.SignIntensity / 10) {
                return true;
            }else {
                return false;
            }
        }

        public override int EndurCost() {
            if (this.Effectivity == 1) {
                return this.EnduranceCost - 5;
            } else {
                return this.EnduranceCost;
            }
        }
    }
}
