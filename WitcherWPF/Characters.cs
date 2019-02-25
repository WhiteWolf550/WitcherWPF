using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherWPF {
    class Characters {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }

        public Characters(string Name, string Description, string Source) {
            this.Name = Name;
            this.Description = Description;
            this.Source = Source;
        }
    }
}
