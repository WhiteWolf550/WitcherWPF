using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherWPF {
    class Shop {
        public string Name { get; set; }
        public string Type { get; set; }
        List<string> Items { get; set; }
        public string Sword { get; set; }
        public string Armor { get; set; }

        FileManager manager = new FileManager();

        public Shop() {

        }
        public Shop(string Name, string Type, List<string> Items, string Sword, string Armor) {
            this.Name = Name;
            this.Items = Items;
            this.Sword = Sword;
            this.Armor = Armor;
            this.Type = Type;
        }

        public void CreateShops()
        {
            List<Shop> shops = new List<Shop>();
            List<Item> items = manager.LoadItems();
            List<Item> matches = items.Where(s => s.Effect == "Alcohol").ToList();
            List<string> shopitems = new List<string>();
            foreach (Item item in matches)
            {
                shopitems.Add(item.Name);
            }

            shops.Add(new Shop("Yaven", "Blacksmith", shopitems, "Temerský ocelový meč", "Zbroj wyzimské stráže"));
            manager.SaveShops(shops);
        }
    }
}
