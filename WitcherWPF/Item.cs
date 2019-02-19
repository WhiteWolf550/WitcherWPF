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
        public string LootType { get; set; }
        public string Type { get; set; }
        public string Source { get; set; }
        public string Substance { get; set; }
        public string Effect { get; set; }
        public string Action { get; set; }
        public int Toxicity { get; set; }
        public int Duration { get; set; }
        public string Content { get; set; }
        public int Price { get; set; }

        FileManager manager = new FileManager();

        public Item() {

        }

        public Item(string Name, string Description, string Type, string LootType, string Source, string Substance, string Effect, string Action, int Toxicity, int Duration, string Content,  int Price) {
            this.Name = Name;
            this.Description = Description;
            this.LootType = LootType;
            this.Type = Type;
            this.Source = Source;
            this.Substance = Substance;
            this.Effect = Effect;
            this.Action = Action;
            this.Toxicity = Toxicity;
            this.Duration = Duration;
            this.Content = Content;
            this.Price = Price;
        }
        public void EnemyLoot(string Monster, WrapPanel LootInventory) {
            List<Item> items = manager.LoadItems();
            List<Item> matches = items.Where(s => s.Type == Monster).ToList();
            foreach(Item item in matches) {
                
            }
            
        }
        public void GenerateLoot(WrapPanel LootInventory, Button Hide, Image LootBack, Button TakeLoot, Button CloseBut, string LootType) {
            bool pass = true;
            Hide.Visibility = Visibility.Hidden;
            LootInventory.Visibility = Visibility.Visible;
            LootBack.Visibility = Visibility.Visible;
            CloseBut.Visibility = Visibility.Visible;
            TakeLoot.Visibility = Visibility.Visible;
            string ipath = @"../../gamefiles/GameItems.json";
            string lootpath = @"../../saves/Loot.json";
            
            List<Item> loot = new List<Item>();
            List<Item> items = manager.LoadItems();
            //Dictionary<Item, Button> lootitems = new Dictionary<Item, Button>();
            
            
            var matches = items.Where(s => s.LootType == LootType).ToList();
            int itc = matches.Count();
            Random rand = new Random();
            int lootcount = rand.Next(1, 3);
            if (!File.Exists(lootpath)) {

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
                    inventoryitem.ToolTip = matches[rn].Name + "\n" + matches[rn].Description + "\n" + "SUBSTANCE:" + "\n" + matches[rn].Substance;
                    inventoryitem.BorderBrush = Brushes.Transparent;
                    inventoryitem.Background = Brushes.Transparent;
                    LootInventory.Children.Add(inventoryitem);
                    Item it = new Item();
                    it.Name = matches[rn].Name;
                    it.Description = matches[rn].Description;
                    it.Type = matches[rn].Type;
                    it.LootType = matches[rn].LootType;
                    it.Source = matches[rn].Source;
                    it.Substance = matches[rn].Substance;
                    it.Effect = matches[rn].Effect;
                    it.Duration = matches[rn].Duration;
                    it.Action = matches[rn].Action;
                    it.Toxicity = matches[rn].Toxicity;
                    it.Content = matches[rn].Content;
                    it.Price = matches[rn].Price;
                    loot.Add(it);
                    
                }
                manager.SaveItems(loot, lootpath);
            }else if (File.Exists(lootpath)) {

                loot = manager.LoadItems();
                foreach (var item in loot) {
                    Image inventoryimage2 = new Image();
                    inventoryimage2.Width = 18;
                    inventoryimage2.Height = 18;
                    inventoryimage2.Source = new BitmapImage(new Uri(item.Source, UriKind.Relative));
                    inventoryimage2.Margin = new Thickness(-15, -3, -3, -3);
                    Button inventoryitem = new Button();
                    inventoryitem.Content = inventoryimage2;
                    inventoryitem.Height = 20;
                    inventoryitem.Width = 20;
                    inventoryitem.ToolTip = item.Name + "\n" + item.Description + "\n" + "SUBSTANCE:" + "\n" + item.Substance;
                    inventoryitem.BorderBrush = Brushes.Transparent;
                    inventoryitem.Background = Brushes.Transparent;
                    LootInventory.Children.Add(inventoryitem);
                }
            }
            

        }
        public void LootToInventory(WrapPanel LootInventory, Button LootButton, Image LootBack, Button CloseBut) {
            string lootpath = @"../../saves/Loot.json";
            string invpath = @"../../saves/PlayerInventory.json";
            JsonSerializerSettings settings = new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.All
            };
            string jsonFromFile;
            try {
                jsonFromFile = File.ReadAllText(lootpath);
            } catch {
                File.WriteAllText(invpath, "");
                jsonFromFile = File.ReadAllText(lootpath);
            }
            List<Item> loot = JsonConvert.DeserializeObject<List<Item>>(jsonFromFile, settings);
            string jsonFromFileinv = File.ReadAllText(invpath);
            List<PlayerInventory> inventory = new List<PlayerInventory>();
            if (jsonFromFileinv.Length > 0) {
                inventory = JsonConvert.DeserializeObject<List<PlayerInventory>>(jsonFromFileinv, settings);
            }
            else {

            }
            foreach (var item1 in loot) {
                var match = loot.Where(s => s.Name == item1.Name).ToList();
                var match2 = inventory.Where(s => s.Item.Name == item1.Name).ToList();
                var match3 = match2.Where(s => s.Count < 10).ToList();
                if (match2.Count() > 0) {
                    foreach (var item2 in inventory) {
                        if (item2.Item.Name == item1.Name) {
                            if(item2.Count == 10) {
                                if (match3.Count() == 0) {
                                    PlayerInventory inv = new PlayerInventory(item1, 1);
                                    inventory.Add(inv);
                                    break;
                                }else {

                                }
                                
                            }
                            else {

                                item2.Count++;
                                
                            }
                            
                        }
                    }
                }else {
                    PlayerInventory inv = new PlayerInventory(item1, 1);
                    inventory.Add(inv);
                }
                //PlayerInventory inv = new PlayerInventory(item1, cit);
                //inventory.Add(inv);
            }
            LootInventory.Visibility = Visibility.Hidden;
            LootButton.Visibility = Visibility.Hidden;
            LootBack.Visibility = Visibility.Hidden;
            CloseBut.Visibility = Visibility.Hidden;
            string jsonToFile = JsonConvert.SerializeObject(inventory, settings);
            File.WriteAllText(invpath, jsonToFile);
            try {
                File.Delete(lootpath);

            }
            catch (IOException iox) {
                Console.WriteLine(iox.Message);
            }

        }

    }
}
