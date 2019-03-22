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

        FileManager manager = new FileManager();
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
        public void CreatePotions() {
            List<Potion> potions = new List<Potion>();
            potions.Add(new Potion("Vlaštovka", 20, "Vitriol", "Aether", "Rebis", @"img/Items/Potion_Swallow.png", "Elixír, který rychle doplňuje Geraltovo zdraví", 2, "MediumAlcohol"));
            potions.Add(new Potion("Hrom", 25, "Vermilion", "Rebis", "Vitriol", @"img/Items/Potion_Thunderbolt.png", "Elixír, který zvýší sílu útoků o 20%", 2, "StrongAlcohol"));
            potions.Add(new Potion("Puštík", 20, "Rebis", "Aether", "Vermilion", @"img/Items/Potion_Tawny_Owl.png", "Elixír, který rychle doplňuje Geraltovu výdrž", 3, "MediumAlcohol"));
            potions.Add(new Potion("Petriho filtr", 30, "Quebirth", "Vermilion", "Hydragenum", @"img/Items/Potion_Petris_Philter.png", "Elixír, který tbýší intenzitu všech znamení o 20%", 1, "StrongAlcohol"));
            potions.Add(new Potion("Černá krev", 25, "Hydragenum", "Rebis", "Vermilion", @"img/Items/Potion_Black_Blood.png", "Elixír, který mění Geraltovu krev na jedovatou pro upíry (upíři dostanou poškození pokud zaútoči na Geralta)", 3, "StrongAlcohol"));
            potions.Add(new Potion("Úplněk", 25, "Quebirth", "Hydragenum", "Aether", @"img/Items/Potion_Full_Moon.png", "Elixír který značně zvýší Geraltovu vitalitu", 1, "StrongAlcohol"));
            potions.Add(new Potion("Kočka", 25, "Vermilion", "Rebis", "Aether", @"img/Items/Potion_Cat.png", "Elixír který umožní Geraltovi vidět ve tmě", 1, "MediumAlcohol"));

            manager.SavePotions(potions);
        }
    }
}
