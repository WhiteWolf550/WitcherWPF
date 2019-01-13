using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherWPF {
    class Armor {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public int Armorvalue { get; set; }
        public int Bleedingresistance { get; set; }
        public int Poisonresistance { get; set; }
        public string Source { get; set; }
        public int Price { get; set; }
        public string SetName { get; set; }
        public int SetBonus { get; set; }
        public string LootType { get; set; }

        public Armor(string Type, string Name, string Description, int Level, int Armorvalue, int Bleedingresistance, int Poisonresistance, string Source, int Price, string SetName, int SetBonus, string LootType) {
            this.Type = Type;
            this.Name = Name;
            this.Description = Description;
            this.Level = Level;
            this.Armorvalue = Armorvalue;
            this.Bleedingresistance = Bleedingresistance;
            this.Poisonresistance = Poisonresistance;
            this.Source = Source;
            this.Price = Price;
            this.SetName = SetName;
            this.SetBonus = SetBonus;
            this.LootType = LootType;
        }
    }
}
