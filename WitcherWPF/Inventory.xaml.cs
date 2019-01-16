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
        static string ipath = @"../../gamefiles/GameItems.json";
        static string playerinvpath = @"../../saves/PlayerInventory.json";
        static string playergearpath = @"../../saves/Player.json";
        static string playerpath = @"../../saves/Player.json";
        static string jsonFromFile = File.ReadAllText(ipath);
        List<Item> items = JsonConvert.DeserializeObject<List<Item>>(jsonFromFile, settings);
        static string jsonFromFilepl = File.ReadAllText(playerpath);
        List<Player> playerinfo = JsonConvert.DeserializeObject<List<Player>>(jsonFromFilepl, settings);

        private Frame parentFrame;
        
        public Inventory()
        {
            InitializeComponent();
            
        }
        public Inventory(Frame parentFrame) : this() {
            this.parentFrame = parentFrame;
            Stamina.Interval = TimeSpan.FromSeconds(1);
            Stamina.Tick += new EventHandler(Stamina_tick);
            Player player = new Player();
            player.LoadAttributes(HealthBar, EnduranceBar, ToxicityBar);
            player.LoadOrens(Oren);
            LoadInventory();
            LoadGear();
            if (EnduranceBar.Value != EnduranceBar.Maximum ) {
                StaminaRegen();
            }
        }

        public void GetMap(object sender, RoutedEventArgs e) {
            string jsonToFile = JsonConvert.SerializeObject(playerinfo, settings);
            File.WriteAllText(playerpath, jsonToFile);
            parentFrame.Navigate(new Map(parentFrame));
        }
        public void GetQuests(object sender, RoutedEventArgs e) {
            string jsonToFile = JsonConvert.SerializeObject(playerinfo, settings);
            File.WriteAllText(playerpath, jsonToFile);
            parentFrame.Navigate(new Quests(parentFrame));
        }
        public void GetJournal(object sender, RoutedEventArgs e) {
            string jsonToFile = JsonConvert.SerializeObject(playerinfo, settings);
            File.WriteAllText(playerpath, jsonToFile);
            parentFrame.Navigate(new Journal(parentFrame));
        }
        public void GetCharacter(object sender, RoutedEventArgs e) {
            string jsonToFile = JsonConvert.SerializeObject(playerinfo, settings);
            File.WriteAllText(playerpath, jsonToFile);
            parentFrame.Navigate(new Character(parentFrame));
        }
        public void GetAlchemy(object sender, RoutedEventArgs e) {

        }
        public void GetLocation(object sender, RoutedEventArgs e) {
            string jsonToFile = JsonConvert.SerializeObject(playerinfo, settings);
            File.WriteAllText(playerpath, jsonToFile);
            parentFrame.Navigate(new Location(parentFrame));
        }
        public void LoadInventory() {
            string jsonFromFilein = File.ReadAllText(playerinvpath);
            List<PlayerInventory> inventory = new List<PlayerInventory>();
            if (jsonFromFilein.Length > 0) {
                inventory = JsonConvert.DeserializeObject<List<PlayerInventory>>(jsonFromFilein, settings);
            } else {
                
            }
            foreach (var item in inventory) {
                string orens = "";
                int p = item.Item.Price;
                int sell = p / 2;
                if (sell == 1) {
                    orens = "orén";
                }else if (sell > 1 && sell < 5) {
                    orens = "orény";
                }else {
                    orens = "orénů";
                }
                Image inventoryimage = new Image();
                inventoryimage.Width = 18;
                inventoryimage.Height = 18;
                inventoryimage.Source = new BitmapImage(new Uri(item.Item.Source, UriKind.Relative));
                inventoryimage.Margin = new Thickness(-15,-3,-3,-3);
                ContextMenu cm = new ContextMenu();
                MenuItem drop = new MenuItem();
                drop.Header = "Zahodit předmět";
                drop.Click += DropItem;
                drop.Tag = item.Item.Name;
                cm.Items.Add(drop);
                MenuItem use = new MenuItem();
                use.Header = item.Item.Action;
                use.Click += UseItem;
                use.Tag = item.Item.Name;
                cm.Items.Add(use);
                Button inventoryitem = new Button();
                inventoryitem.Content = inventoryimage;
                inventoryitem.Height = 20;
                inventoryitem.Width = 20;
                inventoryitem.BorderBrush = Brushes.Transparent;
                inventoryitem.ToolTip = item.Item.Name + "\n" + item.Count + "x" + "\n" + item.Item.Description + "\n" + "SUBSTANCE:" + "\n" + item.Item.Substance + "\n" + "Lze prodat za: " + sell + " " + orens;
                inventoryitem.ContextMenu = cm;
                inventoryitem.Tag = item.Item.Name;
                inventoryitem.Background = Brushes.Transparent;
                InventoryItems.Children.Add(inventoryitem);

            }
        }
        public void LoadGear() {
            string jsonFromFilein = File.ReadAllText(playergearpath);
            List<Player> gear = new List<Player>();
            if (jsonFromFilein.Length > 0) {
                gear = JsonConvert.DeserializeObject<List<Player>>(jsonFromFilein, settings);
            } else {

            }
            var matches = gear.Where(s => s.SteelSword.Type == "Ocelový meč");
            foreach (var item in gear) {
                //-------------------STEEL SWORD----------------
                string orens = "";
                int p = item.SteelSword.Price;
                int sell = p / 2;
                if (sell == 1) {
                    orens = "orén";
                } else if (sell > 1 && sell < 5) {
                    orens = "orény";
                } else {
                    orens = "orénů";
                }
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
                steel.ToolTip = item.SteelSword.Type + "\n" + "\n" + item.SteelSword.Name +"\n" + "\n" + item.SteelSword.Description + "\n" + "\n" + "Útočná síla: " + item.SteelSword.Damage + "\n" + "Šance na krvácení: " + item.SteelSword.Bleedingchance + "%" + "\n" + "Šance na otrávení: " + item.SteelSword.Poisonchance + "%" + "\n" + "\n" + "Lze prodat za: " + sell + orens;
                steel.ContextMenu = cm;
                steel.Tag = item.SteelSword.Name;
                steel.Background = Brushes.Transparent;
                SteelSlot.Children.Add(steel);
                
                
            }
            foreach (var item in gear) {
                //-------------------SILVER SWORD----------------
                string orens = "";
                int p = item.SilverSword.Price;
                int sell = p / 2;
                if (sell == 1) {
                    orens = "orén";
                } else if (sell > 1 && sell < 5) {
                    orens = "orény";
                } else {
                    orens = "orénů";
                }
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
                silver.ToolTip = item.SilverSword.Type + "\n" + "\n" + item.SilverSword.Name + "\n" + "\n" + item.SilverSword.Description + "\n" + "\n" + "Útočná síla: " + item.SilverSword.Damage + "\n" + "Šance na krvácení: " + item.SilverSword.Bleedingchance + "%" + "\n" + "Šance na otrávení: " + item.SilverSword.Poisonchance + "%" + "\n" + "\n" + "Lze prodat za: " + sell + orens;
                silver.ContextMenu = cm;
                silver.Tag = item.SilverSword.Name;
                silver.Background = Brushes.Transparent;
                SilverSlot.Children.Add(silver);
            }
            foreach(var item in gear) {
                //-------------------ARMOR----------------
                string orens = "";
                int p = item.Armor.Price;
                int sell = p / 2;
                if (sell == 1) {
                    orens = "orén";
                } else if (sell > 1 && sell < 5) {
                    orens = "orény";
                } else {
                    orens = "orénů";
                }
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
            invent.DropItem(button);
            InventoryItems.Children.Clear();
            LoadInventory();

        }
        public void DropGear(object sender, RoutedEventArgs e) {

        }
        public void UseItem(object sender, RoutedEventArgs e) {

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
    }
}
