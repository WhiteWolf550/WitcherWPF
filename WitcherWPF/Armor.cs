using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherWPF {
    class Armor : Equipment {
        
        public int Armorvalue { get; set; }
        public int Bleedingresistance { get; set; }
        public int Poisonresistance { get; set; }
        

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
