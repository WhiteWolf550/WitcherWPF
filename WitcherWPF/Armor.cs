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

        FileManager manager = new FileManager();
        public Armor() {

        }
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
        public void SellArmor(List<Shop> shops, Armor armor) {
            foreach (Shop item in shops) {
                item.Armors.Add(armor);
            }
            manager.SaveShops(shops);
        }
        public void BuyArmor(List<Armor> armors, Armor armor) {
            armors.Add(armor);
            manager.SavePlayerArmor(armors);
        }
        public void CreateArmor() {
            List<Armor> armors = new List<Armor>();
            armors.Add(new Armor("Středně těžká zbroj", "Zbroj wyzimské stráže", "Obnošená zbroj wyzimské stráže", 1, 5, 0, 0, @"img/Armors/Armor_Temeria2.png", 150, null, 10, "Loot"));
            armors.Add(new Armor("Středně těžká zbroj", "Mantikoří zbroj", "Kazajka používaná zaklínači ze školy Mantikory", 1, 10, 0, 0, @"img/Armors/Armor_Manticore.png", 200, "Manticore", 10, "Start"));
            armors.Add(new Armor("Středně těžká zbroj", "Mahakamská zbroj", "Zbroj vyrobená trpaslíky z Mahakamu", 5, 20, 0, 0, @"img/Armors/Armor_Manticore.png", 200, "Mahakam", 10, "Shop"));
            armors.Add(new Armor("Těžká zbroj", "Ocelová zbroj", "Ocelová zbroj s pevnou ocelovou hrudí a vyztuženými nárameníky", 10, 25, 2, 0, @"img/Armors/Armor_Manticore.png", 100, "None", 0, "HumanLoot"));

            manager.SaveArmor(armors);
            //playeramors.Add(armors[0]);
            //manager.SavePlayerArmor(playeramors);
        }
    }
}
