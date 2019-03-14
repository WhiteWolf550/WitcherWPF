using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherWPF {
    class Shop {
        public string Name { get; set; }
        public string Type { get; set; }
        public List<Item> Items { get; set; }
        public List<Sword> Swords { get; set; }
        public List<Armor> Armors { get; set; }

        FileManager manager = new FileManager();

        public Shop() {

        }
        public Shop(string Name, string Type, List<Item> Items, List<Sword> Sword, List<Armor> Armor) {
            this.Name = Name;
            this.Items = Items;
            this.Swords = Sword;
            this.Armors = Armor;
            this.Type = Type;
        }

        public void CreateShops()
        {
            List<Shop> shops = new List<Shop>();
            List<Item> items = manager.LoadItems();
            List<Sword> swords = manager.LoadSwords();
            List<Armor> armors = manager.LoadArmors();

            List<Item> matches = items.Where(s => s.Effect == "Alcohol").ToList();
            List<Item> matches2 = items.Where(s => s.Effect == "Alcohol" || s.Type == "Drink" || s.Type == "Food").ToList();
            List<Sword> mats = swords.Where(s => s.Level == 1).ToList();
            List<Armor> mata = armors.Where(s => s.Level == 1).ToList();
            foreach(Item item in matches) {
                item.Count = 10;
            }   
            foreach(Item item in matches2) {
                item.Count = 10;
            }
            shops.Add(new Shop("Yaven", "Blacksmith", matches, mats, mata));
            shops.Add(new Shop("Olaf", "Innkeeper", matches2, null, null));


            manager.SaveShops(shops);
        }
    }
}
