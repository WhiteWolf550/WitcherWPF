using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherWPF {
    class Shop {
        public string Name { get; set; }
        public string Type { get; set; }
        List<Item> Items { get; set; }
        public string Sword { get; set; }
        public string Armor { get; set; }

        FileManager manager = new FileManager();

        public Shop() {

        }
        public Shop(string Name, string Type, List<Item> Items, string Sword, string Armor) {
            this.Name = Name;
            this.Items = Items;
            this.Sword = Sword;
            this.Armor = Armor;
            this.Type = Type;
        }

        public void CreateShops() {
            List<Shop> shops = new List<Shop>();
            List<Item> items = manager.LoadItems();
            List<Sword> sword = manager.LoadSwords();
            List<Armor> armor = manager.LoadArmors();
            List<Item> matches = items.Where(s => s.Type == "Blacksmith").ToList();
            
            shops.Add(new Shop("Yaven", "Blacksmith", matches, "Temerský ocelový meč", "Zbroj wyzimské stráže"));
            manager.SaveShops(shops);
        }
    }
}
