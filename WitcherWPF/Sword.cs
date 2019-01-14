﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherWPF {
    class Sword : Equipment {
        
        public int Damage { get; set; }
        public int Bleedingchance { get; set; }
        public int Poisonchance { get; set; }
       

        public Sword(string Type, string Name, string Description, int Level, int Damage, int Bleedingchance, int Poisonchance, string Source, int Price, string SetName, int SetBonus, string LootType) {
            this.Type = Type;
            this.Name = Name;
            this.Description = Description;
            this.Level = Level;
            this.Damage = Damage;
            this.Bleedingchance = Bleedingchance;
            this.Poisonchance = Poisonchance;
            this.Source = Source;
            this.Price = Price;
            this.SetName = SetName;
            this.SetBonus = SetBonus;
            this.LootType = LootType;
        }
    }
}
