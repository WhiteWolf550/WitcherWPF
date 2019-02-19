using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherWPF
{
    class Potion
    {
        public string Name { get; set; }
        public int Toxicity { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public string PotionBase { get; set; }
        public string Ingredient1 { get; set; }
        public string Ingredient2 { get; set; }
        public string Ingredient3 { get; set; }

        public Potion(string Name, int Toxicity, string Ingredient1, string Ingredient2, string Ingredient3, string Icon, string Description, int Duration, string PotionBase) {
            this.Name = Name;
            this.Toxicity = Toxicity;
            this.Ingredient1 = Ingredient1;
            this.Ingredient2 = Ingredient2;
            this.Ingredient3 = Ingredient3;
            this.Icon = Icon;
            this.Description = Description;
            this.Duration = Duration;
            this.PotionBase = PotionBase;
        }
        public Potion() {

        }
        public string PotionDurCheck(int duration) {
            if (duration == 1) {
                return "souboj";
            } else if (duration > 1 && duration < 5) {
                return "souboje";
            } else {
                return "soubojů";
            }
        }
    }
}
