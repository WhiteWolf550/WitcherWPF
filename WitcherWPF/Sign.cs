using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherWPF {
    abstract class Sign {
       
        public int EnduranceCost { get; set; }
        public int SignIntensity { get; set; }
        public int Effectivity { get; set; }

        public abstract int EndurCost();
    }
}
