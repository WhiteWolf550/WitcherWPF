using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherWPF {
    class Quen : Sign {
        public int ShieldDuration { get; set; }
        public int DamageReduction { get; set; }

        public Quen() {

        }

        public Quen(int ShiedlDuration, int DamageReduction) {
            this.ShieldDuration = ShieldDuration;
            this.DamageReduction = DamageReduction;
        }
    }
}
