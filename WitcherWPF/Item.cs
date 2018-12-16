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
        public void GenerateLoot(WrapPanel LootInventory, Label Test) {
            string ipath = @"../../saves/GameItems.json";
            JsonSerializerSettings settings = new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.All
            };
            string jsonFromFile = File.ReadAllText(ipath);
            List<Item> items = JsonConvert.DeserializeObject<List<Item>>(jsonFromFile, settings);
            var matches = items.Where(s => s.Type == "Loot").ToList();
            int itc = matches.Count();
            Random rand = new Random();
            int lootcount = rand.Next(1, 4);
            for (int i = 0;i == lootcount;i++) {
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
                Test.Content = rn;

            }
        }
    }
}
