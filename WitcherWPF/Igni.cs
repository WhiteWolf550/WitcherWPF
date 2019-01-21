using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherWPF {
    class Igni : Sign {
        public int Damage { get; set; }
        public int BurnChance { get; set; }

        public Igni() {

        }

        public Igni(int Damage, int BurnChance) {
            this.Damage = Damage;
            this.BurnChance = BurnChance;
        }
    }
}
