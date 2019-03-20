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
            this.EffectIco.Add("Kočka", new Uri(@"img/UI/Effect_Cat.png", UriKind.Relative));
            this.EffectIco.Add("Hrom", new Uri(@"img/UI/Effect_Thunderbolt.png", UriKind.Relative));
            this.EffectIco.Add("Puštík", new Uri(@"img/UI/Effect_Tawny_owl.png", UriKind.Relative));
            this.EffectIco.Add("Petriho filtr", new Uri(@"img/UI/Effec_Petris_philter.png", UriKind.Relative));
            this.EffectIco.Add("Úplněk", new Uri(@"img/UI/Effect_Full_moon.png", UriKind.Relative));
        }
        public Effect(string Name) {
            this.Name = Name;
        }

    }
}
