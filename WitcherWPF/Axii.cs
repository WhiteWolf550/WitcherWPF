using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherWPF {
    class Axii : Sign {
        public int StunChance { get; set; }

        public Axii() {

        }

        public Axii(int StunChance) {
            this.StunChance = StunChance;
        }
    }
}
