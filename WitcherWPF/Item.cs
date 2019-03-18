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
        public string Content { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
        FileManager manager = new FileManager();

        public Item() {

        }

        public Item(string Name, string Description, string Type, string LootType, string Source, string Substance, string Effect, string Action, string Content,  int Price) {
            this.Name = Name;
            this.Description = Description;
            this.LootType = LootType;
            this.Type = Type;
            this.Source = Source;
            this.Substance = Substance;
            this.Effect = Effect;
            this.Action = Action;
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
            string lootpath = @"../../saves/Loot.json";
            
            List<Item> loot = new List<Item>();
            List<int> useditems = new List<int>();
            List<Item> items = manager.LoadItems();
            //Dictionary<Item, Button> lootitems = new Dictionary<Item, Button>();
            
            
            List<Item> matches = items.Where(s => s.LootType == LootType).ToList();
            int itc = matches.Count();
            int lootcount = 0;
            Random rand = new Random();
            if (matches.Count < 3) {
                if (matches.Count() == 1) {
                    lootcount = 0;
                }
            }else {
                lootcount = rand.Next(1, 3);
            }
            
            
            if (!File.Exists(lootpath)) {

                for (int i = 0;i <= lootcount;i++) {
                    int rn = rand.Next(0, itc);
                    int randomcount = rand.Next(1, 5);
                    
                    if (!useditems.Contains(rn)) {
                        Image inventoryimage = new Image();
                        inventoryimage.Width = 18;
                        inventoryimage.Height = 18;
                        inventoryimage.Source = new BitmapImage(new Uri(matches[rn].Source, UriKind.Relative));
                        inventoryimage.Margin = new Thickness(-15, -3, -3, -3);
                        Button inventoryitem = new Button();
                        inventoryitem.Content = inventoryimage;
                        inventoryitem.Height = 20;
                        inventoryitem.Width = 20;
                        inventoryitem.ToolTip = matches[rn].Name + "\n" + randomcount + "x" + "\n" + matches[rn].Description + "\n" + "SUBSTANCE:" + "\n" + matches[rn].Substance;
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
                        it.Action = matches[rn].Action;
                        it.Content = matches[rn].Content;
                        it.Price = matches[rn].Price;
                        it.Count = randomcount;
                        loot.Add(it);
                        if (matches[rn].Type == "Quest")
                        {
                            i = lootcount + 1;
                        }
                        useditems.Add(rn);
                    }else {
                        i--;
                    }
                    
                }
                manager.SaveItems(loot, lootpath);
            }else if (File.Exists(lootpath)) {

                loot = manager.LoadLoot();
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
                    inventoryitem.ToolTip = item.Name + "\n" + item.Count + "x" + "\n" + item.Description + "\n" + "SUBSTANCE:" + "\n" + item.Substance;
                    inventoryitem.BorderBrush = Brushes.Transparent;
                    inventoryitem.Background = Brushes.Transparent;
                    LootInventory.Children.Add(inventoryitem);
                }
            }
            

        }
        public void LootToInventory(WrapPanel LootInventory, Button LootButton, Image LootBack, Button CloseBut, StackPanel QuestPop, Label QueName, TextBlock QueGoal) {
            string lootpath = @"../../saves/Loot.json";
            List<Item> loot = manager.LoadLoot();
            List<PlayerInventory> inventory = manager.LoadPlayerInventory();
            if (inventory == null) {
                inventory = new List<PlayerInventory>();
            }
            foreach (var item1 in loot) {
                var match = loot.Where(s => s.Name == item1.Name).ToList();
                var match2 = new List<PlayerInventory>();
                try {
                    match2 = inventory.Where(s => s.Item.Name == item1.Name).ToList();
                }catch {
                    match2 = new List<PlayerInventory>();
                }
                var match3 = match2.Where(s => s.Item.Count < 10).ToList();
                if (match2.Count() > 0) {
                    foreach (var item2 in inventory) {
                        if (item2.Item.Name == item1.Name) {
                            if(item2.Item.Count == 10) {
                                if (match3.Count() == 0) {
                                    PlayerInventory inv = new PlayerInventory(item1);
                                    inventory.Add(inv);
                                    break;
                                }else {

                                }
                                
                            }
                            else {
                                if (item2.Item.Count + item1.Count > 10)
                                {
                                    int rest = item2.Item.Count + item1.Count - 10;
                                    item2.Item.Count = 10; 
                                    item1.Count = rest;
                                    PlayerInventory inv = new PlayerInventory(item1);
                                    inventory.Add(inv);
                                    break;
                                }else {
                                    item2.Item.Count += item1.Count;
                                }
                                
                            }
                            
                        }
                    }
                }else {
                    if (item1.Type == "Quest") {
                        PlayerQuest quest = new PlayerQuest();
                        quest.UpdateQuest(item1.LootType, QuestPop, QueName, QueGoal);
                    }
                    PlayerInventory inv = new PlayerInventory(item1);
                    inventory.Add(inv);
                }
                //PlayerInventory inv = new PlayerInventory(item1, cit);
                //inventory.Add(inv);
            }
            LootInventory.Visibility = Visibility.Hidden;
            LootButton.Visibility = Visibility.Hidden;
            LootBack.Visibility = Visibility.Hidden;
            CloseBut.Visibility = Visibility.Hidden;
            manager.SavePlayerInventory(inventory);
            try {
                File.Delete(lootpath);

            }
            catch (IOException iox) {
                Console.WriteLine(iox.Message);
            }

        }
        public void GiveItem(PlayerInventory pitem, List<Shop> shops, int num) {
            int def = pitem.Item.Count;
            foreach(Shop item in shops) {
                List<Item> items = item.Items.Where(s => s.Name == pitem.Item.Name).ToList();
                var match3 = items.Where(s => s.Count < 10).ToList();
                if (items.Count > 0) {
                    foreach (Item item2 in item.Items) {
                        if (item2.Name == pitem.Item.Name) {
                            if (item2.Count == 10) {
                                if (match3.Count() == 0) {
                                    item.Items.Add(pitem.Item);
                                    break;
                                }else {

                                }
                            } else {
                                if (item2.Count + num > 10) {
                                    int rest = item2.Count + num - 10;
                                    item2.Count = 10;
                                    Item it = pitem.Item;
                                    it.Count = rest;

                                    item.Items.Add(it);
                                    break;
                                } else {
                                    item2.Count += num;
                                }
                            }
                        }
                    }
                }else {
                    Item item3 = pitem.Item;
                    item3.Count = num;
                    item.Items.Add(item3);
                }
            }
            
            manager.SaveShops(shops);
            
        }
        public void CreateItems() {
            List<Item> items = new List<Item>();
            items.Add(new Item("Kuře", "Jídlo,Po snězení doplní malou část zdraví", "Food", "Loot", @"img/Items/Food_Chicken.png", "žádné", "Food", "Sníst", null, 20));
            items.Add(new Item("Wyverní maso", "Vzácné maso, které se dá prodat", "Food", "Loot", @"img/Items/Food_Wyvern_Meat.png", "žádné", "Food", "Sníst", null, 90));
            items.Add(new Item("Jablečný Džus", "Nápoj, lze vypít pro doplňení malé části zdraví", "Drink", "Loot", @"img/Items/Drink_Apple_Juice.png", "žádné", "Drink", "Vypít", null, 15));
            items.Add(new Item("Fisstech", "Silná droga, lze prodat", "Loot", "Loot", @"img/Items/Potion_Fisstech.png", "žádné", "Drug", "Použít", null, 150));
            items.Add(new Item("Víno", "Alkohol, lze prodat kupcům nebo použít", "Alcohol", "Loot", @"img/Items/Alcohol_Winered.png", "žádné", "Alcohol", "Vypít", null, 20));
            items.Add(new Item("Temerská žitná", "Středně silný alkohol, lze prodat kupcům nebo použít jako Alchymistický základ", "MediumAlcohol", "Loot", @"img/Items/Alcohol_Temerian_Rye.png", "žádné", "Alcohol", "Vypít", null, 50));
            items.Add(new Item("Trpasličí vodka", "Velice silný alkohol, lze prodat kupcům za vysokou částku nebo použít jako Alchymistický základ", "StrongAlcohol", "Loot", @"img/Items/Alcohol_Dwarven_Spirit.png", "žádné", "Alcohol", "Vypít", null, 80));

            items.Add(new Item("Barghesti", "Kniha o barghestech", "Loot", "Loot", @"img/Items/Book_Bestiary.png", null, "Barghest", "Číst", "Barghesti jsou fakt svině...", 100));
            //POTIONS
            items.Add(new Item("Puštík", "Elixír, který rychle doplňuje Geraltovu výdrž", "Potion", "Alchemy", @"img/Items/Potion_Tawny_Owl.png", null, "Potion", "Vypít", null, 50));
            items.Add(new Item("Vlaštovka", "Elixír, který rychle doplňuje Geraltovo zdraví", "Potion", "Alchemy", @"img/Items/Potion_Swallow.png", null, "Potion", "Vypít", null, 50));
            items.Add(new Item("Hrom", "Elixír, který značně zvýší sílu útoků", "Potion", "Alchemy", @"img/Items/Potion_Thunderbolt.png", null, "Potion", "Vypít", null, 50));
            items.Add(new Item("Petriho filtr", "Elixír, který značně zvýší intenzitu všech znamení", "Potion", "Alchemy", @"img/Items/Potion_Petris_Philter.png", null, "Potion", "Vypít", null, 80));
            items.Add(new Item("Černá krev", "Elixír, který mění Geraltovu krev na jedovatou pro upíry (upíři dostanou poškození pokud zaútoči na Geralta)", "Potion", "Alchemy", @"img/Items/Potion_Black_Blood.png", null, "Potion", "Vypít", null, 80));
            items.Add(new Item("Úplněk", "Elixír který značně zvýší Geraltovu vitalitu", "Potion", "Alchemy", @"img/Items/Potion_Full_Moon.png", null, "Potion", "Vypít", null, 50));
            items.Add(new Item("Kočka", "Elixír který umožní Geraltovi vidět ve tmě", "Potion", "Alchemy", @"img/Items/Potion_Cat.png", null, "Potion", "Vypít", null, 20));

            //MONSTER LOOT
            items.Add(new Item("Tesáky z příšery", "Tesáky sebrané z příšery", "Alchemy", "Barghest", @"img/Items/Monster_Fang.png", "Rebis", "Alchemy", null, null, 10));
            items.Add(new Item("Prach smrti", "Prach, který se většinou dá získat z přeludů, nebo z jiných příšer", "Alchemy", "Barghest", @"img/Items/Monster_DeathDust.png", "Hydragenum", "Alchemy", null, null, 10));

            //HERBS
            items.Add(new Item("Vlaštovičník", "Běžná rostlina s léčivými vlastnostmi", "Alchemy", "Celandine", @"img/Items/Herb_Celandine.png", "Rebis", null, null, null, 10));
            items.Add(new Item("Bílá Myrta", "Běžná polní květina s velkými bílými květy", "Alchemy", "White_Myrtle", @"img/Items/Herb_Myrtle.png", "Vitriol", null, null, null, 10));
            items.Add(new Item("Ginatia", "Okvětní plátky běžného křoví", "Alchemy", "Ginatia", @"img/Items/Herb_Ginatia.png", "Vermilion", null, null, null, 20));


            items.Add(new Item("Krev z Ghůla", "Krev, která se dá získat z Ghůla", "Alchemy", "Ghůl", @"img/Items/Monster_Ghoul_Blood.png", "Aether", "Alchemy", null, null, 10));
            items.Add(new Item("Bílý Ocet", "Bílý Ocet, který se dá použít v Alchymii", "Alchemy", "Ghůl", @"img/Items/Monster_White_vinegar.png", "Vitriol", "Alchemy", null, null, 10));
            items.Add(new Item("Žluč", "Žluč, která se dá použít v Alchymii", "Alchemy", "Ghůl", @"img/Items/Monster_Abomination_Lymph.png", "Rebis", "Alchemy", null, null, 10));
            //BUILDING
            items.Add(new Item("Dřevo", "Dřevo lze použít jako stavební materiál a nebo ho lze prodat", "Build", "Loot", @"img/Items/Wood.png", "žádné", "Build", null, null, 10));

            //QUEST ITEMS
            items.Add(new Item("Zlatý prsten", "Zlatý prsten, který vypadá hodně staře", "Quest", "Strašidelný dům", @"img/Items/Quest_Ring.png", "žádné", null, null, null, 0));

            //SPECIAL
            items.Add(new Item("Část zbroje: Hruď", "Stará část zbroje, která vypadá hodně staře", "Quest", "Crypt1", @"img/Items/Armor_Part1.png",  "žádné", null, null, null, 0));
            items.Add(new Item("Část zbroje: Manuskript", "Stará část zbroje, která vypadá hodně staře", "Quest", "Crypt2", @"img/Items/Armor_Part2.png", "žádné", null, null, null, 0));
            items.Add(new Item("Část zbroje: Nátepník", "Stará část zbroje, která vypadá hodně staře", "Quest", "Crypt3", @"img/Items/Armor_Part3.png", "žádné", null, null, null, 0));




            manager.SaveItems(items);
        }

    }
}
