using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherWPF {
    class Base {
        Dictionary<string, bool> upgrades { get; set; }
        Dictionary<string, bool> characters { get; set; }
        public int Income { get; set; }
    }
}
