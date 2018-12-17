using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace WitcherWPF
{
    class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Source { get; set; }

        public Item() {

        }

        public Item(string Name, string Description, string Type, string Source) {
            this.Name = Name;
            this.Description = Description;
            this.Type = Type;
            this.Source = Source;
        }
        public void GenerateLoot(WrapPanel LootInventory, Button Hide, Image LootBack, Button TakeLoot) {
            Hide.Visibility = Visibility.Hidden;
            LootInventory.Visibility = Visibility.Visible;
            LootBack.Visibility = Visibility.Visible;
            TakeLoot.Visibility = Visibility.Visible;
            string ipath = @"../../saves/GameItems.json";
            string lootpath = @"../../saves/Loot.json";
            JsonSerializerSettings settings = new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.All
            };
            string jsonFromFileloot = File.ReadAllText(lootpath);
            List<Item> items = new List<Item>();
            List<Item> loot = new List<Item>();
            if (jsonFromFileloot.Length > 0) {
                items = JsonConvert.DeserializeObject<List<Item>>(jsonFromFileloot, settings);
            }else {
                string jsonFromFile = File.ReadAllText(ipath);
                items = JsonConvert.DeserializeObject<List<Item>>(jsonFromFile, settings);
                Dictionary<Item, Button> lootitems = new Dictionary<Item, Button>();
            }
            
            var matches = items.Where(s => s.Type == "Loot").ToList();
            int itc = matches.Count();
            Random rand = new Random();
            int lootcount = rand.Next(1, 4);
            
            for (int i = 0;i <= lootcount;i++) {
                int rn = rand.Next(0, itc);
                Image inventoryimage = new Image();
                inventoryimage.Width = 18;
                inventoryimage.Height = 18;
                inventoryimage.Source = new BitmapImage(new Uri(matches[rn].Source, UriKind.Relative));
                inventoryimage.Margin = new Thickness(-15, -3, -3, -3);
                Button inventoryitem = new Button();
                inventoryitem.Content = inventoryimage;
                inventoryitem.Height = 20;
                inventoryitem.Width = 20;
                inventoryitem.BorderBrush = Brushes.Transparent;
                inventoryitem.Background = Brushes.Transparent;
                LootInventory.Children.Add(inventoryitem);
                Item it = new Item();
                it.Name = matches[rn].Name;
                it.Description = matches[rn].Description;
                it.Type = matches[rn].Type;
                it.Source = matches[rn].Source;
                loot.Add(it);
            }
            string jsonToFile = JsonConvert.SerializeObject(loot, settings);
            File.WriteAllText(lootpath, jsonToFile);

        }
        public void LootToInventory(WrapPanel LootInventory, Button LootButton, Image LootBack) {
            string lootpath = @"../../saves/Loot.json";
            string invpath = @"../../saves/PlayerInventory.json";
            JsonSerializerSettings settings = new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.All
            };
            string jsonFromFile = File.ReadAllText(lootpath);
            List<Item> loot = JsonConvert.DeserializeObject<List<Item>>(jsonFromFile, settings);
            string jsonFromFileinv = File.ReadAllText(invpath);
            List<PlayerInventory> inventory = new List<PlayerInventory>();
            if (jsonFromFileinv.Length > 0) {
                inventory = JsonConvert.DeserializeObject<List<PlayerInventory>>(jsonFromFileinv, settings);
            }
            else {

            }
            foreach (var item in loot) {
                PlayerInventory inv = new PlayerInventory(item, 1);
                inventory.Add(inv);
            }
            LootInventory.Visibility = Visibility.Hidden;
            LootButton.Visibility = Visibility.Hidden;
            LootBack.Visibility = Visibility.Hidden;
            string jsonToFile = JsonConvert.SerializeObject(inventory, settings);
            File.WriteAllText(invpath, jsonToFile);
            loot.Clear();            

        }

    }
}
