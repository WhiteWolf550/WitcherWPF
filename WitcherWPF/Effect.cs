using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherWPF {
    public class Effect {
        public string Name { get; set; }
        public int Duration { get; set; }
        public int Toxicity { get; set; }
        public Dictionary<string, Uri> EffectIco = new Dictionary<string, Uri>();
        
        public Effect() {
            this.EffectIco.Add("Vlaštovka", new Uri(@"img/UI/Effect_Swallow.png", UriKind.Relative));
        }
        public Effect(string Name) {
            this.Name = Name;
        }

    }
}
