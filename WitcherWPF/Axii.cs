using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherWPF {
    class Axii : Sign {
        public int StunDuration { get; set; }
        public int ChannelingTime { get; set; }
        public Axii() {

        }

        public Axii(int StunDuration, int ChannelingTime) {
            this.StunDuration = StunDuration;
            this.ChannelingTime = ChannelingTime;
        }
    }
}
