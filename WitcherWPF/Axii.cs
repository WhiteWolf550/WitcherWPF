using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherWPF {
    class Axii : Sign {
        public int Duration { get; set; }
        public int ChannelingTime { get; set; }
        public int StatsDecrease { get; set; }
        public Axii() {

        }

        public Axii(int Duration, int ChannelingTime, int StatDecrease) {
            this.Duration = Duration;
            this.ChannelingTime = ChannelingTime;
            this.StatsDecrease = StatsDecrease;
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
