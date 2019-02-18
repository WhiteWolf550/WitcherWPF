using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherWPF {
    class Yrden : Sign{
        public int Duration { get; set; }
        public int Pain { get; set; }
        public int Confusion { get; set; }
        public int AttackBlock { get; set; }
        public Yrden() {


        }

        public Yrden(int Duration, int Pain, int Confusion, int AttackBlock) {
            this.Duration = Duration;
            this.Pain = Pain;
            this.Confusion = Confusion;
            this.AttackBlock = AttackBlock;

            
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
