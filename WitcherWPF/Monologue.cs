using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherWPF {
    class Monologue {

        public string MonologueLine { get; set; }
        public bool isActive { get; set; }
        public string ItemName { get; set; }
        public bool Deactivate { get; set; }

        public Monologue(string MonologueLine, bool isActive, string ItemName, bool Deactivate) {
            this.MonologueLine = MonologueLine;
            this.isActive = isActive;
            this.ItemName = ItemName;
            this.Deactivate = Deactivate;
        }


    }
    
}
