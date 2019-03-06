using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Threading;

namespace WitcherWPF
{
    /// <summary>
    /// Interakční logika pro Inventory.xaml
    /// </summary>
    public partial class Inventory : Page
    {
        public DispatcherTimer Stamina = new DispatcherTimer();
        static JsonSerializerSettings settings = new JsonSerializerSettings {
            TypeNameHandling = TypeNameHandling.All
        };
        public bool Combat;
        static string ipath = @"../../gamefiles/GameItems.json";
        static string playerinvpath = @"../../saves/PlayerInventory.json";
        static string playergearpath = @"../../saves/Player.json";
        static string playerpath = @"../../saves/Player.json";
        FileManager manager = new FileManager();
        static string jsonFromFile = File.ReadAllText(ipath);
        List<Item> items = JsonConvert.DeserializeObject<List<Item>>(jsonFromFile, settings);
        static string jsonFromFilepl = File.ReadAllText(playerpath);
        List<Player> playerinfo;
        List<Effect> effects;
        List<Sword> sword = new List<Sword>();
        List<Armor> armor = new List<Armor>();
        List<PlayerInventory> pinventory = new List<PlayerInventory>();
        List<Potion> potions = new List<Potion>();
        PlayerInventory inventory = new PlayerInventory();
        Game game = new Game();
        Music sound = new Music();
        Dictionary<MenuItem, PlayerInventory> buttonitems = new Dictionary<MenuItem, PlayerInventory>();
        Dictionary<MenuItem, Sword> swordeq = new Dictionary<MenuItem, Sword>();
        Dictionary<MenuItem, Armor> armoreq = new Dictionary<MenuItem, Armor>();

        private Frame parentFrame;
        private Time time;
        
        public Inventory()
        {
            InitializeComponent();
            sound.PlaySound("NewPage");
            playerinfo = manager.LoadPlayer();
            sword = manager.LoadPlayerSwords();
            armor = manager.LoadPlayerArmors();
            pinventory = manager.LoadPlayerInventory();
            effects = manager.LoadEffects();
            potions = manager.LoadPotions();
            
        }
        public Inventory(Frame parentFrame, bool Combat, Time time) : this() {
            
            this.parentFrame = parentFrame;
            this.time = time;
            this.Combat = Combat;
            Stamina.Interval = TimeSpan.FromSeconds(1);
            Stamina.Tick += new EventHandler(Stamina_tick);
            Player player = new Player();
            player.LoadAttributes(HealthBar, EnduranceBar, ToxicityBar);
            player.LoadXP(XPBar, Level);
            player.LoadOrens(Oren);
            LoadInventory(true);
            LoadInventory(false);
            LoadGear();
            LoadEquipSwords();
            if (EnduranceBar.Value != EnduranceBar.Maximum && Combat == false ) {
                StaminaRegen();
            }
        }
        
        public void GetMap(object sender, RoutedEventArgs e) {
            if (Combat == false) {
                parentFrame.Navigate(new Map(parentFrame, time));
                game.SaveGame(playerinfo, pinventory, armor, sword, effects);
            }
        }
        public void GetQuests(object sender, RoutedEventArgs e) {
            if (Combat == false) {
                parentFrame.Navigate(new Quests(parentFrame, time));
                game.SaveGame(playerinfo, pinventory, armor, sword, effects);
            }
        }
        public void GetJournal(object sender, RoutedEventArgs e) {
            if (Combat == false) {
                parentFrame.Navigate(new Journal(parentFrame, time));
                game.SaveGame(playerinfo, pinventory, armor, sword, effects);
            }
        }
        public void GetCharacter(object sender, RoutedEventArgs e) {
            if (Combat == false) {
                parentFrame.Navigate(new Character(parentFrame, time));
                game.SaveGame(playerinfo, pinventory, armor, sword, effects);
            }
        }
        public void GetAlchemy(object sender, RoutedEventArgs e) {
            if (Combat == false) {
                parentFrame.Navigate(new Combat(parentFrame, false, time, false, null));
                game.SaveGame(playerinfo, pinventory, armor, sword, effects);
            }
        }
        public void GetLocation(object sender, RoutedEventArgs e) {
            if (Combat == false) {
                game.SaveGame(playerinfo, pinventory, armor, sword, effects);
                parentFrame.Navigate(new Location(parentFrame, time));
            }else {
                game.SaveGame(playerinfo, pinventory, armor, sword, effects);
                parentFrame.Navigate(new Combat(parentFrame, true, time, false, null));
            }
        }
        public void LoadInventory(bool InvLoad) {
            
            List<PlayerInventory> matches = new List<PlayerInventory>();
            if (InvLoad == true) {
                InventoryItems.Children.Clear();
                matches = pinventory.Where(s => s.Item.Type != "Alchemy").ToList();
            }else {
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
                    MenuItem drop = new MenuItem();
                    drop.Header = "Zahodit předmět";
                    drop.Click += DropItem;
                    drop.Tag = item.Item.Name;
                    cm.Items.Add(drop);
                    MenuItem use = new MenuItem();
                    use.Header = item.Item.Action;
                    use.Click += UseItem;
                    use.Tag = item.Item.Action;
                    cm.Items.Add(use);
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
                    }else {
                        AlchemyItems.Children.Add(inventoryitem);
                    }
                    buttonitems.Add(use, item);
                }
            }
        }
        public void LoadEquipSwords() {
            
            foreach(Sword item in sword) {
                int sell = item.Price / 2;
                string orens = inventory.Orens(sell);
                StackPanel ItemWrap = new StackPanel();
                ItemWrap.Orientation = Orientation.Horizontal;
                Image ItemImage = new Image();
                ItemImage.Source = new BitmapImage(new Uri(item.Source, UriKind.Relative));
                ContextMenu cmenu = new ContextMenu();
                MenuItem equip = new MenuItem();
                equip.Header = "Vybavit předmět";
                equip.Click += Equip;
                equip.Tag = item.Type;
                cmenu.Items.Add(equip);
                Button ItemButton = new Button();
                ItemButton.ContextMenu = cmenu;
                ItemButton.Content = ItemImage;
                ItemButton.ToolTip = item.Type + "\n" + "\n" + item.Name + "\n" + "\n" + item.Description + "\n" + "\n" + "Útočná síla: " + item.Damage + "\n" + "Šance na kritický zásah: " + item.CriticalHit + "%" +  "\n" + "\n" + "Lze prodat za: " + sell + orens;
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
            
            if (armor != null) {
                foreach (Armor item in armor) {
                    int sell = item.Price / 2;
                    string orens = inventory.Orens(sell);
                    StackPanel ItemWrap = new StackPanel();
                    ItemWrap.Orientation = Orientation.Horizontal;
                    Image ItemImage = new Image();
                    ItemImage.Source = new BitmapImage(new Uri(item.Source, UriKind.Relative));
                    ContextMenu cmenu = new ContextMenu();
                    MenuItem equip = new MenuItem();
                    equip.Header = "Vybavit předmět";
                    equip.Click += Equip;
                    equip.Tag = "Zbroj";
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
        public void LoadGear() {
            
            var matches = playerinfo.Where(s => s.SteelSword.Type == "Ocelový meč");
            foreach (var item in playerinfo) {
                //-------------------STEEL SWORD----------------
                int sell = item.SteelSword.Price / 2;
                string orens = inventory.Orens(sell);
                
                Image inventoryimage = new Image();
                inventoryimage.Source = new BitmapImage(new Uri(item.SteelSword.Source, UriKind.Relative));
                ContextMenu cm = new ContextMenu();
                MenuItem drop = new MenuItem();
                drop.Header = "Zahodit předmět";
                drop.Click += DropGear;
                drop.Tag = item.SteelSword.Name;
                cm.Items.Add(drop);
                Button steel = new Button();
                steel.Content = inventoryimage;
                steel.Height = 200;
                steel.BorderBrush = Brushes.Transparent;
                steel.ToolTip = item.SteelSword.Type + "\n" + "\n" + item.SteelSword.Name +"\n" + "\n" + item.SteelSword.Description + "\n" + "\n" + "Útočná síla: " + item.SteelSword.Damage + "\n" + "Šance na kritický zásah: " + item.SteelSword.CriticalHit + "%" + "\n" + "\n" + "Lze prodat za: " + sell + orens;
                steel.ContextMenu = cm;
                steel.Tag = item.SteelSword.Name;
                steel.Background = Brushes.Transparent;
                SteelSlot.Children.Add(steel);
                
                
            }
            foreach (var item in playerinfo) {
                //-------------------SILVER SWORD----------------
                int sell = item.SilverSword.Price / 2;
                string orens = inventory.Orens(sell);
                Image inventoryimage = new Image();
                inventoryimage.Source = new BitmapImage(new Uri(item.SilverSword.Source, UriKind.Relative));
                ContextMenu cm = new ContextMenu();
                MenuItem drop = new MenuItem();
                drop.Header = "Zahodit předmět";
                drop.Click += DropGear;
                drop.Tag = item.SteelSword.Name;
                cm.Items.Add(drop);
                Button silver = new Button();
                silver.Content = inventoryimage;
                silver.Height = 200;
                silver.BorderBrush = Brushes.Transparent;
                silver.ToolTip = item.SilverSword.Type + "\n" + "\n" + item.SilverSword.Name + "\n" + "\n" + item.SilverSword.Description + "\n" + "\n" + "Útočná síla: " + item.SilverSword.Damage + "\n" + "Šance na kritický zásah: " + item.SilverSword.CriticalHit + "%" + "\n" + "\n" + "Lze prodat za: " + sell + orens;
                silver.ContextMenu = cm;
                silver.Tag = item.SilverSword.Name;
                silver.Background = Brushes.Transparent;
                SilverSlot.Children.Add(silver);
            }
            foreach(var item in playerinfo) {
                //-------------------ARMOR----------------
                int sell = item.Armor.Price / 2;
                string orens = inventory.Orens(sell);
                Image inventoryimage = new Image();
                inventoryimage.Source = new BitmapImage(new Uri(item.Armor.Source, UriKind.Relative));
                ContextMenu cm = new ContextMenu();
                MenuItem drop = new MenuItem();
                drop.Header = "Zahodit předmět";
                drop.Click += DropGear;
                drop.Tag = item.SteelSword.Name;
                cm.Items.Add(drop);
                Button armor = new Button();
                armor.Content = inventoryimage;
                armor.Height = 135;
                armor.BorderBrush = Brushes.Transparent;
                armor.ToolTip = item.Armor.Type + "\n" + "\n" + item.Armor.Name + "\n" + "\n" + item.Armor.Description + "\n" + "\n" + "Zbroj: " +  item.Armor.Armorvalue + "\n" + "Odolnost proti krvácení: " + item.Armor.Bleedingresistance + "%" + "\n" + "Odolnost proti otrávení: " + item.Armor.Poisonresistance + "%" + "\n" + "\n" + "Lze prodat za: " + sell + orens;
                armor.ContextMenu = cm;
                armor.Tag = item.Armor.Name;
                armor.Background = Brushes.Transparent;
                ArmorSlot.Children.Add(armor);
            }
            
        }
        public void DropItem(object sender, RoutedEventArgs e) {
            PlayerInventory invent = new PlayerInventory();
            MenuItem button = (sender as MenuItem);
            pinventory = invent.DropItem(button.Tag.ToString(), pinventory);
            InventoryItems.Children.Clear();
            LoadInventory(true);
            LoadInventory(false);
            manager.SavePlayerInventory(pinventory);

        }
        public void DropGear(object sender, RoutedEventArgs e) {

        }
        private void UseItem(object sender, RoutedEventArgs e) {
            MenuItem button = (sender as MenuItem);
            
            if (button.Tag.ToString() == "Číst") {
                
                Read(button);
            }else if (button.Tag.ToString() == "Vypít") {
                Drink(button);
            }
        }public void Drink(MenuItem drink) {
            Effect e = new Effect();
            e.Name = buttonitems[drink].Item.Name;
            int toxicity = 0;
            foreach (Potion item in potions) {
                if (e.Name == item.Name) {
                    e.Duration = item.Duration;
                    e.Toxicity = item.Toxicity;
                    toxicity = item.Toxicity;
                }
            }

            List<Effect> matches = effects.Where(s => s.Name == buttonitems[drink].Item.Name).ToList();
            if (matches.Count > 0) {
                MessageBox.Show("Tento Elixír jsi už použil!");
            }else {
                effects.Add(e);
                RemoveItem(buttonitems[drink].Item.Name);
                foreach (Player item in playerinfo) {
                    item.toxicity += toxicity;
                }
                game.SaveGame(playerinfo, pinventory, armor, sword, effects);
                parentFrame.Navigate(new Combat(parentFrame, true, time, true, null));


            }
            
        }
        public void RemoveItem(string name) {
            List<PlayerInventory> matches = pinventory.Where(s => s.Item.Name == name).ToList();
            foreach(PlayerInventory item in matches) {
                if (item.Item.Count > 1) {
                    item.Item.Count -= 1;
                }else if(item.Item.Count == 1) {
                    pinventory.Remove(item);
                }
                LoadInventory(true);
                LoadInventory(false);

            }
        }
        public void Read(MenuItem book) {
            PlayerInventory inv = buttonitems[book];
            Book.Visibility = Visibility.Visible;
            BookName.Text = inv.Item.Name;
            BookContent.Text = inv.Item.Content;
        }
        public void CloseBook(object sender, RoutedEventArgs e) {
            Book.Visibility = Visibility.Hidden;
        }
        public void StaminaRegen() {
            Stamina.Start();
        }
        void Stamina_tick(object sender, EventArgs e) {
            
            foreach (var item in playerinfo) {
                item.endurance++;
                EnduranceBar.ToolTip = item.endurance+1 + "/" + item.maxEndurance;
                EnduranceBar.Value = item.endurance;
                if (item.endurance == item.maxEndurance) {
                    item.endurance = item.maxEndurance;
                    EnduranceBar.ToolTip = item.endurance + "/" + item.maxEndurance;
                    EnduranceBar.Value = item.endurance;
                    Stamina.Stop();
                }
            }
            
        }

        private void LoadSwords_Click(object sender, RoutedEventArgs e) {
            EquipWrap.Children.Clear();
            LoadEquipSwords();
        }
        private void LoadArmors_Click(object sender, RoutedEventArgs e) {
            EquipWrap.Children.Clear();
            LoadEquipArmor();
        }
        private void Equip(object sender, RoutedEventArgs e) {
            MenuItem menuitem = (sender as MenuItem);
            if (menuitem.Tag.ToString() == "Stříbrný meč") {
                EquipSilver(menuitem);
            }else if (menuitem.Tag.ToString() == "Ocelový meč") {
                EquipSteel(menuitem);
            } else if (menuitem.Tag.ToString() == "Zbroj") {
                EquipArmor(menuitem);
            }
            
        }
        public void EquipSteel(MenuItem itemq) {
            //List<Player> player = manager.LoadPlayer();
            //List<Sword> sword = manager.LoadPlayerSwords();
            
            foreach(Player item in playerinfo) {
                sword.Add(item.SteelSword);
                item.SteelSword = swordeq[itemq];
            }
            sword.Remove(swordeq[itemq]);
            sound.PlaySound("GrabSword");
            ReloadInventory();
        }
        public void EquipSilver(MenuItem itemq) {
            //List<Player> player = manager.LoadPlayer();
            //List<Sword> sword = manager.LoadPlayerSwords();

            foreach (Player item in playerinfo) {
                sword.Add(item.SilverSword);
                item.SilverSword = swordeq[itemq];
            }
            sword.Remove(swordeq[itemq]);
            sound.PlaySound("GrabSword");
            ReloadInventory();
        }
        public void EquipArmor(MenuItem itemq) {
            //List<Player> player = manager.LoadPlayer();
            //List<Armor> armor = manager.LoadPlayerArmors();

            foreach (Player item in playerinfo) {
                armor.Add(item.Armor);
                item.Armor = armoreq[itemq];
            }
            armor.Remove(armoreq[itemq]);
            sound.PlaySound("GrabArmor");
            ReloadInventory();
        }
        private void ReloadInventory() {
            //manager.SavePlayer(playerinfo);
            //manager.SavePlayerArmor(armor);
            //manager.SavePlayerSwords(sword);
            EquipWrap.Children.Clear();
            SilverSlot.Children.Clear();
            ArmorSlot.Children.Clear();
            SteelSlot.Children.Clear();
            LoadGear();
            
        }

    }
}
