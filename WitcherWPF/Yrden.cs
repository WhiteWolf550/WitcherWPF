using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherWPF {
    class Yrden : Sign{
        public int Duration { get; set; }

        public Yrden() {


        }

        public Yrden(int Duration) {
            this.Duration = Duration;
        }
    }
}
