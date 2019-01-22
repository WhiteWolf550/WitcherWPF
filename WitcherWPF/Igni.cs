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

        public Igni() {

        }

        public Igni(int Damage, int BurnChance) {
            this.Damage = Damage;
            this.BurnChance = BurnChance;
        }
        
        public bool Burn() {
            Random rand = new Random();
            int rn = rand.Next(0, 100);
            if (rn < this.BurnChance) {
                return true;
            }else {
                return false;
            }
        } 
    }
}
