using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherWPF {
    class Axii : Sign {
        public int DamageDecrease { get; set; }

        public Axii() {

        }

        public Axii(int DamagaDecrease) {
            this.DamageDecrease = DamageDecrease;
        }
    }
}
