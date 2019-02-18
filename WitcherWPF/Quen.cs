using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherWPF {
    class Quen : Sign {
        public int ShieldDuration { get; set; }
        public int DamageReduction { get; set; }
        public int EffectsResistance { get; set; }

        public Quen() {

        }

        public Quen(int ShiedlDuration, int DamageReduction, int DamageBack, int EffectsResistance) {
            this.ShieldDuration = ShieldDuration;
            this.DamageReduction = DamageReduction;
            this.EffectsResistance = EffectsResistance;
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
