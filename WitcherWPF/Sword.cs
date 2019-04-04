using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherWPF {
    class Sword : Equipment {
        
        public int Damage { get; set; }
        public int CriticalHit { get; set; }

        FileManager manager = new FileManager();
        public Sword() {

        }
        public Sword(string Type, string Name, string Description, int Level, int Damage, int CriticalHit, string Source, int Price, string SetName, int SetBonus, string LootType) {
            this.Type = Type;
            this.Name = Name;
            this.Description = Description;
            this.Level = Level;
            this.Damage = Damage;
            this.CriticalHit = CriticalHit;
            this.Source = Source;
            this.Price = Price;
            this.SetName = SetName;
            this.SetBonus = SetBonus;
            this.LootType = LootType;
        }
        public void SellSword(List<Shop> shops, Sword sword) {
            foreach(Shop item in shops) {
                item.Swords.Add(sword);
            }
            manager.SaveShops(shops);
        }
        public void BuySword(List<Sword> swords, Sword sword) {
            swords.Add(sword);
            manager.SavePlayerSwords(swords);
        }
        public void CreateSwords() {
            List<Sword> swords = new List<Sword>();
            swords.Add(new Sword("Ocelový meč", "Temerský ocelový meč", "Meč, který používají temerští vojáci", 1, 10, 0, @"img/Swords/Sword_Temeria.png", 200, null, 10, "Loot"));
            swords.Add(new Sword("Ocelový meč", "Mahakamský sihil", "Meč ukován trpaslíky z té nejlepší oceli až z Mahakamu", 1, 10, 0, @"img/Swords/Sword_Mahakam_Sihil.png", 200, "Mahakam", 10, "Start"));
            swords.Add(new Sword("Ocelový meč", "Novigradský sekáč", "Meč ukován v Novigradu", 6, 40, 0, @"img/Swords/Sword_Blade.png", 600, "Mahakam", 10, "Shop"));
            swords.Add(new Sword("Stříbrný meč", "Aerondight", "Ostrý jako břitva, tento meč má svůj vlastní osud, jen čas ukáže jaký", 1, 10, 0, @"img/Swords/Sword_Aerondight.png", 200, null, 0, "Start"));
            swords.Add(new Sword("Stříbrný meč", "Měsiční čepel", "Meč ukovaný z toho nejlepšího meteoritu", 6, 50, 0, @"img/Swords/Sword_Moonblade.png", 600, null, 0, "Shop"));

            manager.SaveSwords(swords);
            //playerswords.Add(swords[0]);
            //manager.SavePlayerSwords(playerswords);
        }
    }
}
