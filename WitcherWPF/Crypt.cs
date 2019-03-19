using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherWPF {
    class Crypt {
        public string Name { get; set; }
        public bool IsEnabled { get; set; }

        
        public Crypt(string Name, bool IsEnabled) {
            this.Name = Name;
            this.IsEnabled = IsEnabled;
        }

        public void CreateCrypts() {
            FileManager manager = new FileManager();
            List<Crypt> crypts = new List<Crypt>();

            crypts.Add(new Crypt("Crypt1", true));
            crypts.Add(new Crypt("Crypt2", true));
            crypts.Add(new Crypt("Crypt3", true));
        }
        public Crypt() {

        }
    }
}
