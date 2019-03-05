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
            int lootcount = 0;
            Random rand = new Random();
            if (matches.Count < 3) {
                lootcount = rand.Next(1, matches.Count);
            }else {
                lootcount = rand.Next(1, 3);
            }
            
            
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
                    if (matches[rn].Type == "Quest") {
                        i = lootcount + 1;
                    }
                    
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
        public void LootToInventory(WrapPanel LootInventory, Button LootButton, Image LootBack, Button CloseBut, StackPanel QuestPop, Label QueName, TextBlock QueGoal) {
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
            List<PlayerInventory> inventory = manager.LoadPlayerInventory(); 
            foreach (var item1 in loot) {
                var match = loot.Where(s => s.Name == item1.Name).ToList();
                var match2 = new List<PlayerInventory>();
                try {
                    match2 = inventory.Where(s => s.Item.Name == item1.Name).ToList();
                }catch {
                    match2 = new List<PlayerInventory>();
                }
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
                    if (item1.Type == "Quest") {
                        PlayerQuest quest = new PlayerQuest();
                        quest.UpdateQuest(item1.LootType, QuestPop, QueName, QueGoal);
                    }
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
        public void CreateItems() {
            List<Item> items = new List<Item>();
            items.Add(new Item("Kuře", "Jídlo,Po snězení doplní malou část zdraví", "Loot", "Loot", @"img/Items/Food_Chicken.png", "žádné", "Food", "Sníst", 0, 0, null, 20));
            items.Add(new Item("Wyverní maso", "Vzácné maso, které se dá prodat", "Loot", "Loot", @"img/Items/Food_Wyvern_Meat.png", "žádné", "Food", "Sníst", 0, 0, null, 90));
            items.Add(new Item("Jablečný Džus", "Nápoj, lze vypít pro doplňení malé části zdraví", "Loot", "Loot", @"img/Items/Drink_Apple_Juice.png", "žádné", "Drink", "Vypít", 0, 0, null, 15));
            items.Add(new Item("Fisstech", "Silná droga, lze prodat", "Loot", "Loot", @"img/Items/Potion_Fisstech.png", "žádné", "Drug", "Použít", 0, 0, null, 150));
            items.Add(new Item("Víno", "Alkohol, lze prodat kupcům nebo použít", "Alcohol", "Loot", @"img/Items/Alcohol_Winered.png", "žádné", "Alcohol", "Vypít", 0, 0, null, 20));
            items.Add(new Item("Temerská žitná", "Středně silný alkohol, lze prodat kupcům nebo použít jako Alchymistický základ", "MediumAlcohol", "Loot", @"img/Items/Alcohol_Temerian_Rye.png", "žádné", "Alcohol", "Vypít", 0, 0, null, 50));
            items.Add(new Item("Trpasličí vodka", "Velice silný alkohol, lze prodat kupcům za vysokou částku nebo použít jako Alchymistický základ", "StrongAlcohol", "Loot", @"img/Items/Alcohol_Dwarven_Spirit.png", "žádné", "Alcohol", "Vypít", 0, 0, null, 80));

            items.Add(new Item("Barghesti", "Kniha o barghestech", "Loot", "Loot", @"img/Items/Book_Bestiary.png", null, "Barghest", "Číst", 0, 0, "Barghesti jsou fakt svině...", 100));
            //POTIONS
            items.Add(new Item("Puštík", "Elixír, který rychle doplňuje Geraltovu výdrž", "Potion", "Alchemy", @"img/Items/Potion_Tawny_Owl.png", null, "Potion", "Vypít", 20, 2, null, 50));
            items.Add(new Item("Vlaštovka", "Elixír, který rychle doplňuje Geraltovo zdraví", "Potion", "Alchemy", @"img/Items/Potion_Swallow.png", null, "Potion", "Vypít", 20, 2, null, 50));
            items.Add(new Item("Hrom", "Elixír, který značně zvýší sílu útoků", "Potion", "Alchemy", @"img/Items/Potion_Thunderbolt.png", null, "Potion", "Vypít", 25, 2, null, 50));
            items.Add(new Item("Petriho filtr", "Elixír, který značně zvýší intenzitu všech znamení", "Potion", "Alchemy", @"img/Items/Potion_Petris_Philter.png", null, "Potion", "Vypít", 30, 2, null, 80));
            items.Add(new Item("Černá krev", "Elixír, který mění Geraltovu krev na jedovatou pro upíry (upíři dostanou poškození pokud zaútoči na Geralta)", "Potion", "Alchemy", @"img/Items/Potion_Black_Blood.png", null, "Potion", "Vypít", 25, 2, null, 80));
            items.Add(new Item("Úplněk", "Elixír který značně zvýší Geraltovu vitalitu", "Potion", "Alchemy", @"img/Items/Potion_Full_Moon.png", null, "Potion", "Vypít", 25, 2, null, 50));

            //MONSTER LOOT
            items.Add(new Item("Tesáky z příšery", "Tesáky sebrané z příšery", "Alchemy", "Barghest", @"img/Items/Monster_Fang.png", "Rebis", "Alchemy", null, 0, 0, null, 10));
            items.Add(new Item("Prach smrti", "Prach, který se většinou dá získat z přeludů, nebo z jiných příšer", "Alchemy", "Barghest", @"img/Items/Monster_DeathDust.png", "Vitriol", "Alchemy", null, 0, 0, null, 10));

            //HERBS
            items.Add(new Item("Vlaštovičník", "Běžná rostlina s léčivými vlastnostmi", "Alchemy", "Herb", @"img/Items/Herb_Celandine.png", "Rebis", null, null, 0, 0, null, 10));
            items.Add(new Item("Bílá Myrta", "Běžná polní květina s velkými bílými květy", "Alchemy", "Herb", @"img/Items/Herb_Myrtle.png", "Aether", null, null, 0, 0, null, 10));


            items.Add(new Item("Krev z Ghůla", "Krev, která se dá získat z Ghůla", "Alchemy", "Ghůl", @"img/Items/Monster_Ghoul_Blood.png", "Aether", "Alchemy", null, 0, 0, null, 10));
            items.Add(new Item("Bílý Ocet", "Bílý Ocet, který se dá použít v Alchymii", "Alchemy", "Ghůl", @"img/Items/Monster_White_vinegar.png", "Vitriol", "Alchemy", null, 0, 0, null, 10));
            items.Add(new Item("Žluč", "Žluč, která se dá použít v Alchymii", "Alchemy", "Ghůl", @"img/Items/Monster_Abomination_Lymph.png", "Rebis", "Alchemy", null, 0, 0, null, 10));
            //BUILDING
            items.Add(new Item("Dřevo", "Dřevo lze použít jako stavební materiál a nebo ho lze prodat", "Build", "Loot", @"img/Items/Wood.png", "žádné", "Build", null, 0, 0, null, 10));

            //QUEST ITEMS
            items.Add(new Item("Zlatý prsten", "Zlatý prsten, který vypadá hodně staře", "Quest", "Strašidelný dům", @"img/Items/Quest_Ring.png", "žádné", null, null, 0, 0, null, 0));
            manager.SaveItems(items);
        }

    }
}
