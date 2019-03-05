using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherWPF {
    class Bestiary {
        public string Name { get; set; }
        public string Weakness { get; set; }
        public string Strength { get; set; }
        public bool Enabled { get; set; }

        FileManager manager = new FileManager();
        public Bestiary() {

        }
        public Bestiary(string Name, string Weakness, string Strength, bool Enabled) {
            this.Name = Name;
            this.Weakness = Weakness;
            this.Strength = Strength;
            this.Enabled = Enabled;
        }
        public void CreateBestiary() {
            List<Bestiary> bestiary = new List<Bestiary>();

            bestiary.Add(new Bestiary("Ghůl", "něco", "něco", false));
        }
    }
}
