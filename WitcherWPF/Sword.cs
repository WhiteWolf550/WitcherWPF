using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherWPF {
    class Sword {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public int Damage { get; set; }
        public int Bleedingchance { get; set; }
        public int Poisonchance { get; set; }
        public string Source { get; set; }

        public Sword(string Type, string Name, string Description, int Level, int Damage, int Bleedingchance, int Poisonchance, string Source) {
            this.Type = Type;
            this.Name = Name;
            this.Description = Description;
            this.Level = Level;
            this.Damage = Damage;
            this.Bleedingchance = Bleedingchance;
            this.Poisonchance = Poisonchance;
            this.Source = Source;
        }
    }
}
