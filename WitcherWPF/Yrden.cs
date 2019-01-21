using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherWPF {
    class Yrden : Sign{
        public int DamageBoost { get; set; }

        public Yrden() {


        }

        public Yrden(int DamageBoost) {
            this.DamageBoost = DamageBoost;
        }
    }
}
