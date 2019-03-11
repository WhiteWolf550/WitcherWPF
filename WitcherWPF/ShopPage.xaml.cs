using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WitcherWPF
{
    /// <summary>
    /// Interakční logika pro ShopPage.xaml
    /// </summary>
    public partial class ShopPage : Page
    {
        private Frame parentFrame;
        private string character;
        private Time time;

        FileManager manager = new FileManager();
        List<PlayerInventory> pinventory = new List<PlayerInventory>();
        List<Shop> shops = new List<Shop>();
        List<Armor> armors = new List<Armor>();
        List<Sword> swords = new List<Sword>();
        Sword swordi = new Sword();
        Armor armori = new Armor();
        Player player = new Player();
        Item item = new Item(); 
        List<Player> playerlist = new List<Player>();
        PlayerInventory CurItem = new PlayerInventory();
        Item CurShopItem = new Item();
        PlayerInventory inventory = new PlayerInventory();
        Dictionary<MenuItem, PlayerInventory> buttonitems = new Dictionary<MenuItem, PlayerInventory>();
        Dictionary<MenuItem, Item> ShopItems = new Dictionary<MenuItem, Item>();
        Dictionary<MenuItem, Sword> swordeq = new Dictionary<MenuItem, Sword>();
        Dictionary<MenuItem, Armor> armoreq = new Dictionary<MenuItem, Armor>();

        Dictionary<MenuItem, Sword> shopswordeq = new Dictionary<MenuItem, Sword>();
        Dictionary<MenuItem, Armor> shoparmoreq = new Dictionary<MenuItem, Armor>();
        public ShopPage()
        {
            InitializeComponent();
            ShopDialog.Visibility = Visibility.Hidden;
            pinventory = manager.LoadPlayerInventory();
            shops = manager.LoadShop();
            swords = manager.LoadPlayerSwords();
            armors = manager.LoadPlayerArmors();
            playerlist = manager.LoadPlayer();

            

        }
        public ShopPage(Frame parentFrame, Time time, string CharacterName) : this() {
            this.parentFrame = parentFrame;
            this.character = CharacterName;
            this.time = time;

            LoadPlayerInventory(true);
            LoadPlayerInventory(false);
            player.LoadOrens(Oren);
            LoadShop();

        }
        public void LoadShop() {
            shops = manager.LoadShop();
            bool sword = false;
            bool armor = false;
            List<Shop> matches = shops.Where(s => s.Name == character).ToList();
            List<Item> items = manager.LoadItems();
            

            foreach(Shop item in matches) {
                if (item.Armors != null && item.Swords != null) {
                    armor = true;
                    sword = true;
                }
            }
            if (armor == true && sword == true) {
                loadShopSwordnArmor();
            }
            loadShopItems(matches);

            

        }
        private void loadShopItems(List<Shop> matches) {
            foreach (Shop item in matches) {

                foreach (Item item2 in item.Items) {
                    string orens = inventory.Orens(item2.Price);
                    Image inventoryimage = new Image();
                    inventoryimage.Width = 18;
                    inventoryimage.Height = 18;
                    inventoryimage.Source = new BitmapImage(new Uri(item2.Source, UriKind.Relative));
                    inventoryimage.Margin = new Thickness(-15, -3, -3, -3);
                    ContextMenu cmenu = new ContextMenu();
                    MenuItem equip = new MenuItem();
                    equip.Header = "Koupit";
                    equip.Click += BuyItem_Click;
                    equip.Tag = item.Type;
                    cmenu.Items.Add(equip);
                    Button inventoryitem = new Button();
                    inventoryitem.ContextMenu = cmenu;
                    inventoryitem.Content = inventoryimage;
                    inventoryitem.Height = 20;
                    inventoryitem.Width = 20;
                    inventoryitem.ToolTip = item2.Name + "\n" + item2.Count +"x" + "\n" + item2.Description + "\n" + "SUBSTANCE:" + "\n" + item2.Substance + "\n" + "CENA: " + item2.Price + orens;
                    inventoryitem.BorderBrush = Brushes.Transparent;
                    inventoryitem.Background = Brushes.Transparent;
                    LootInventory.Children.Add(inventoryitem);
                    ShopItems.Add(equip, item2);
                }
            }
        }
        private void loadShopSwordnArmor() {
            
            foreach (Shop item2 in shops) {
                foreach (Sword item in item2.Swords) {
                    string orens = inventory.Orens(item.Price);
                    Image ItemImage = new Image();
                    ItemImage.Source = new BitmapImage(new Uri(item.Source, UriKind.Relative));
                    ContextMenu cmenu = new ContextMenu();
                    MenuItem equip = new MenuItem();
                    equip.Header = "Koupit";
                    equip.Click += BuyItem_Click;
                    equip.Tag = "Sword";
                    cmenu.Items.Add(equip);
                    Button ItemButton = new Button();
                    ItemButton.ContextMenu = cmenu;
                    ItemButton.Content = ItemImage;
                    ItemButton.ToolTip = item.Type + "\n" + "\n" + item.Name + "\n" + "\n" + item.Description + "\n" + "\n" + "Útočná síla: " + item.Damage + "\n" + "Šance na kritický zásah: " + item.CriticalHit + "%" + "\n" + "\n" + "CENA: " + item.Price + orens;
                    ItemButton.Height = 90;
                    ItemButton.Width = 69;
                    ItemButton.Background = Brushes.Transparent;
                    ItemButton.BorderBrush = Brushes.Transparent;
                    LootInventory.Children.Add(ItemButton);
                    shopswordeq.Add(equip, item);
                }
            }
            foreach (Shop item2 in shops) {
                foreach (Armor item in item2.Armors) {
                    string orens = inventory.Orens(item.Price);
                    Image ItemImage = new Image();
                    ItemImage.Source = new BitmapImage(new Uri(item.Source, UriKind.Relative));
                    ContextMenu cmenu = new ContextMenu();
                    MenuItem equip = new MenuItem();
                    equip.Header = "Koupit";
                    equip.Click += BuyItem_Click;
                    equip.Tag = "Armor";
                    cmenu.Items.Add(equip);
                    Button ItemButton = new Button();
                    ItemButton.ContextMenu = cmenu;
                    ItemButton.Content = ItemImage;
                    ItemButton.ToolTip = item.Type + "\n" + "\n" + item.Name + "\n" + "\n" + item.Description + "\n" + "\n" + "Zbroj: " + item.Armorvalue + "\n" + "Odolnost proti krvácení: " + item.Bleedingresistance + "%" + "\n" + "Odolnost proti otrávení: " + item.Poisonresistance + "%" + "\n" + "\n" + "CENA: " + item.Price +  orens;
                    ItemButton.Height = 90;
                    ItemButton.Width = 69;
                    ItemButton.Background = Brushes.Transparent;
                    ItemButton.BorderBrush = Brushes.Transparent;
                    LootInventory.Children.Add(ItemButton);
                    shoparmoreq.Add(equip, item);

                }
            }
        }
        public void LoadPlayerInventory(bool InvLoad) {
            List<PlayerInventory> matches = new List<PlayerInventory>();
            if (pinventory != null) {
                if (InvLoad == true) {
                    InventoryItems.Children.Clear();
                    matches = pinventory.Where(s => s.Item.Type != "Alchemy").ToList();
                } else {
                    AlchemyItems.Children.Clear();
                    matches = pinventory.Where(s => s.Item.Type == "Alchemy").ToList();
                }

                if (pinventory != null) {
                    foreach (var item in matches) {
                        int p = item.Item.Price;
                        int sell = p / 2;
                        string orens = inventory.Orens(sell);
                        Image inventoryimage = new Image();
                        inventoryimage.Width = 18;
                        inventoryimage.Height = 18;
                        inventoryimage.Source = new BitmapImage(new Uri(item.Item.Source, UriKind.Relative));
                        inventoryimage.Margin = new Thickness(-15, -3, -3, -3);
                        ContextMenu cm = new ContextMenu();
                        MenuItem selli = new MenuItem();
                        selli.Header = "Prodat předmět";
                        selli.Click += SellItem_Click;
                        selli.Tag = item.Item.Name;
                        cm.Items.Add(selli);
                        Button inventoryitem = new Button();
                        inventoryitem.Content = inventoryimage;
                        inventoryitem.Height = 20;
                        inventoryitem.Width = 20;
                        inventoryitem.BorderBrush = Brushes.Transparent;
                        inventoryitem.ToolTip = item.Item.Name + "\n" + item.Item.Count + "x" + "\n" + item.Item.Description + "\n" + "SUBSTANCE:" + "\n" + item.Item.Substance + "\n" + "Lze prodat za: " + sell + " " + orens;
                        inventoryitem.ContextMenu = cm;
                        inventoryitem.Tag = item.Item.Action;
                        inventoryitem.Background = Brushes.Transparent;
                        if (InvLoad == true) {
                            InventoryItems.Children.Add(inventoryitem);
                        } else {
                            AlchemyItems.Children.Add(inventoryitem);
                        }
                        buttonitems.Add(selli, item);
                    }

                }
            }
        }
        public void LoadEquipSwords() {

            foreach (Sword item in swords) {
                int sell = item.Price / 2;
                string orens = inventory.Orens(sell);
                StackPanel ItemWrap = new StackPanel();
                ItemWrap.Orientation = Orientation.Horizontal;
                Image ItemImage = new Image();
                ItemImage.Source = new BitmapImage(new Uri(item.Source, UriKind.Relative));
                ContextMenu cmenu = new ContextMenu();
                MenuItem equip = new MenuItem();
                equip.Header = "Prodat meč";
                equip.Click += SellItem_Click;
                equip.Tag = "Sword";
                cmenu.Items.Add(equip);
                Button ItemButton = new Button();
                ItemButton.ContextMenu = cmenu;
                ItemButton.Content = ItemImage;
                ItemButton.ToolTip = item.Type + "\n" + "\n" + item.Name + "\n" + "\n" + item.Description + "\n" + "\n" + "Útočná síla: " + item.Damage + "\n" + "Šance na kritický zásah: " + item.CriticalHit + "%" + "\n" + "\n" + "Lze prodat za: " + sell + orens;
                ItemButton.Height = 90;
                ItemButton.Width = 69;
                ItemButton.Background = Brushes.Transparent;
                ItemButton.BorderBrush = Brushes.Transparent;
                StackPanel ItemInfo = new StackPanel();
                ItemInfo.Orientation = Orientation.Vertical;
                Label SwordType = new Label();
                SwordType.Content = item.Type;
                SwordType.Foreground = Brushes.WhiteSmoke;
                Label SwordName = new Label();
                SwordName.Content = item.Name;
                SwordName.Foreground = Brushes.WhiteSmoke;
                Label SwordLevel = new Label();
                SwordLevel.Foreground = Brushes.WhiteSmoke;
                SwordLevel.Content = item.Level;
                Label SwordDMG = new Label();
                SwordDMG.Foreground = Brushes.WhiteSmoke;
                SwordDMG.Content = item.Damage;

                ItemInfo.Children.Add(SwordType);
                ItemInfo.Children.Add(SwordName);
                ItemInfo.Children.Add(SwordLevel);
                ItemInfo.Children.Add(SwordDMG);
                ItemWrap.Children.Add(ItemButton);
                ItemWrap.Children.Add(ItemInfo);
                EquipWrap.Children.Add(ItemWrap);
                swordeq.Add(equip, item);
            }
        }
        public void LoadEquipArmor() {

            if (armors != null) {
                foreach (Armor item in armors) {
                    int sell = item.Price / 2;
                    string orens = inventory.Orens(sell);
                    StackPanel ItemWrap = new StackPanel();
                    ItemWrap.Orientation = Orientation.Horizontal;
                    Image ItemImage = new Image();
                    ItemImage.Source = new BitmapImage(new Uri(item.Source, UriKind.Relative));
                    ContextMenu cmenu = new ContextMenu();
                    MenuItem equip = new MenuItem();
                    equip.Header = "Prodat Zbroj";
                    equip.Click += SellItem_Click;
                    equip.Tag = "Armor";
                    cmenu.Items.Add(equip);
                    Button ItemButton = new Button();
                    ItemButton.ContextMenu = cmenu;
                    ItemButton.Content = ItemImage;
                    ItemButton.ToolTip = item.Type + "\n" + "\n" + item.Name + "\n" + "\n" + item.Description + "\n" + "\n" + "Zbroj: " + item.Armorvalue + "\n" + "Odolnost proti krvácení: " + item.Bleedingresistance + "%" + "\n" + "Odolnost proti otrávení: " + item.Poisonresistance + "%" + "\n" + "\n" + "Lze prodat za: " + sell + orens;
                    ItemButton.Height = 90;
                    ItemButton.Width = 69;
                    ItemButton.Background = Brushes.Transparent;
                    ItemButton.BorderBrush = Brushes.Transparent;
                    StackPanel ItemInfo = new StackPanel();
                    ItemInfo.Orientation = Orientation.Vertical;
                    Label ArmorType = new Label();
                    ArmorType.Content = item.Type;
                    ArmorType.Foreground = Brushes.WhiteSmoke;
                    Label ArmorName = new Label();
                    ArmorName.Content = item.Name;
                    ArmorName.Foreground = Brushes.WhiteSmoke;
                    Label ArmorLevel = new Label();
                    ArmorLevel.Foreground = Brushes.WhiteSmoke;
                    ArmorLevel.Content = item.Level;
                    Label ArmorValue = new Label();
                    ArmorValue.Foreground = Brushes.WhiteSmoke;
                    ArmorValue.Content = item.Armorvalue;

                    ItemInfo.Children.Add(ArmorType);
                    ItemInfo.Children.Add(ArmorName);
                    ItemInfo.Children.Add(ArmorLevel);
                    ItemInfo.Children.Add(ArmorValue);
                    ItemWrap.Children.Add(ItemButton);
                    ItemWrap.Children.Add(ItemInfo);
                    EquipWrap.Children.Add(ItemWrap);
                    armoreq.Add(equip, item);
                }
            }
        }
        private void SellItem_Click(object sender, RoutedEventArgs e) {
            MenuItem menu = (sender as MenuItem);
            
             
            if (menu.Tag.ToString() == "Sword") {
                SellSword(menu);
            }else if (menu.Tag.ToString() == "Armor") {
                SellArmor(menu);
            }else {
                SellItem_Box(menu);
            }

        }
        public void SellItem_Box(MenuItem menu) {
            PlayerInventory item = buttonitems[menu];
            MaxNum.Content = "/" + item.Item.Count;
            ScrollBR.Maximum = item.Item.Count;
            CurItem = item;
            ShopDialog.Visibility = Visibility.Visible;
            SellnBuyBut.Tag = "Sell";
            SellnBuyBut.Content = "Prodat";
        }
        public void BuyItem_Box(MenuItem menu) {
            Item item = ShopItems[menu];
            MaxNum.Content = "/" + item.Count;
            ScrollBR.Maximum = item.Count;
            CurShopItem = item;
            ShopDialog.Visibility = Visibility.Visible;
            SellnBuyBut.Tag = "Buy";
            SellnBuyBut.Content = "Koupit";
        }
        private void SellorBuy_Click(object sender, RoutedEventArgs e) {
            Button button = (sender as Button);
            if (button.Tag.ToString() == "Buy") {
                Buy();
            }else if (button.Tag.ToString() == "Sell") {
                Sell();
            }
        }
        public void Buy() {
            ShopDialog.Visibility = Visibility.Hidden;
            int price = CurShopItem.Price;
            bool hasmoney = player.Pay(price * Int32.Parse(Num.Content.ToString()), playerlist);
            if (hasmoney == true) {
                foreach (Shop item in shops) {
                    if (Int32.Parse(Num.Content.ToString()) == CurShopItem.Count) {
                        item.Items.Remove(CurShopItem);
                    } else {
                        foreach (Item item2 in item.Items) {
                            if (item2 == CurShopItem) {
                                item2.Count -= Int32.Parse(Num.Content.ToString());
                            }
                        }
                    }
                }

                manager.SavePlayer(playerlist);
                manager.SavePlayerInventory(pinventory);
                manager.SaveShops(shops);
                inventory.BuyItem(CurShopItem, pinventory, Int32.Parse(Num.Content.ToString()));
                LoadShopInventory();
                LoadInventory();
            }else {
                MessageBox.Show("Nemáš dostatek orénů na zakoupení předmětu!");
            }
        }
        private void BuyItem_Click(object sender, RoutedEventArgs e) {
            MenuItem menu = (sender as MenuItem);


            if (menu.Tag.ToString() == "Sword") {
                BuySword(menu);
            } else if (menu.Tag.ToString() == "Armor") {
                BuyArmor(menu);
            } else {
                BuyItem_Box(menu);
            }
        }
        private void ExitShop_Click(object sender, RoutedEventArgs e) {
            parentFrame.Navigate(new Dialogue(parentFrame, character, time));
        }

        private void ScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            Num.Content = Math.Round(ScrollBR.Value);
        }
        private void Sell() {
            int def = CurItem.Item.Count;
            ShopDialog.Visibility = Visibility.Hidden;
            if (Int32.Parse(Num.Content.ToString()) == CurItem.Item.Count) {
                pinventory.Remove(CurItem);
            }else {
                foreach(PlayerInventory item in pinventory) {
                    if (item == CurItem) {
                        item.Item.Count -= Int32.Parse(Num.Content.ToString());
                    }
                }
            }
            int sellprice = CurItem.Item.Price / 2;
            player.AddMoney(sellprice * Int32.Parse(Num.Content.ToString()), playerlist);
            manager.SavePlayer(playerlist);
            manager.SavePlayerInventory(pinventory);

            item.GiveItem(CurItem, shops, Int32.Parse(Num.Content.ToString()));
            
            LoadShopInventory();
            LoadInventory();
        }
        public void BuySword(MenuItem menu) {
            Sword sw = shopswordeq[menu];
            if (swords.Count < 2) {
                int price = CurShopItem.Price / 2;
                bool hasmoney = player.Pay(price * Int32.Parse(Num.Content.ToString()), playerlist);
                if (hasmoney == true) {
                    foreach (Shop item in shops) {
                        item.Swords.Remove(sw);
                    }

                    manager.SavePlayerSwords(swords);
                    manager.SaveShops(shops);
                    manager.SavePlayer(playerlist);
                    sw.BuySword(swords, sw);
                    EquipWrap.Children.Clear();
                    LoadShopInventory();
                    LoadInventory();
                } else {
                    MessageBox.Show("Nemáš dostatek orénů na zakoupení předmětu!");
                }
            }else if(swords.Count == 2) {
                MessageBox.Show("Nemůžeš u sebe mít více jak 2 meče!");
            }
        }
        public void BuyArmor(MenuItem menu) {
            Armor ar = shoparmoreq[menu];
            if (armors.Count < 2) {
                int price = CurShopItem.Price / 2;
                bool hasmoney = player.Pay(price * Int32.Parse(Num.Content.ToString()), playerlist);
                if (hasmoney == true) {
                    foreach (Shop item in shops) {
                        item.Armors.Remove(ar);
                    }

                    manager.SavePlayerSwords(swords);
                    manager.SaveShops(shops);
                    manager.SavePlayer(playerlist);
                    ar.BuyArmor(armors, ar);
                    EquipWrap.Children.Clear();
                    LoadShopInventory();
                    LoadInventory();
                } else {
                    MessageBox.Show("Nemáš dostatek orénů na zakoupení předmětu!");
                }
            }else if (armors.Count == 2) {
                MessageBox.Show("Nemůžeš u sebe mít více jak 2 zbroje!");
            }
        }
        public void SellSword(MenuItem menu) {
            Sword sw = swordeq[menu];
            swords.Remove(sw);
            int sellprice = sw.Price / 2;
            player.AddMoney(sellprice, playerlist);
            manager.SavePlayerSwords(swords);
            manager.SavePlayer(playerlist);
            swordi.SellSword(shops, sw);
            EquipWrap.Children.Clear();
            LoadShopInventory();
            LoadInventory();
        }
        public void SellArmor(MenuItem menu) {
            Armor ar = armoreq[menu];
            armors.Remove(ar);
            int sellprice = ar.Price / 2;
            player.AddMoney(sellprice, playerlist);
            manager.SavePlayerArmor(armors);
            manager.SavePlayer(playerlist);
            armori.SellArmor(shops, ar);
            EquipWrap.Children.Clear();
            LoadShopInventory();
            LoadInventory();
            
        }
        
        private void Close_Click(object sender, RoutedEventArgs e) {
            ShopDialog.Visibility = Visibility.Hidden;
        }
        private void LoadInventory() {
            pinventory = manager.LoadPlayerInventory();
            AlchemyItems.Children.Clear();
            InventoryItems.Children.Clear();
            LoadPlayerInventory(true);
            LoadPlayerInventory(false);
            player.LoadOrens(Oren);
        }
        private void LoadShopInventory() {
            LootInventory.Children.Clear();
            LoadShop();
        }
        private void LoadSwords_Click(object sender, RoutedEventArgs e) {
            EquipWrap.Children.Clear();
            LoadEquipSwords();
        }
        private void LoadArmors_Click(object sender, RoutedEventArgs e) {
            EquipWrap.Children.Clear();
            LoadEquipArmor();
        }
    }
}

