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
        static string ipath = @"../../saves/GameItems.json";
        static string playerinvpath = @"../../saves/PlayerInventory.json";
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
            LoadInventory();
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
                Button inventoryitem = new Button();
                inventoryitem.Content = inventoryimage;
                inventoryitem.Height = 20;
                inventoryitem.Width = 20;
                inventoryitem.BorderBrush = Brushes.Transparent;
                inventoryitem.ToolTip = item.Item.Name + "\n" + item.Count + "x" + "\n" + item.Item.Description;
                inventoryitem.ContextMenu = cm;
                inventoryitem.Tag = item.Item.Name;
                inventoryitem.Background = Brushes.Transparent;
                InventoryItems.Children.Add(inventoryitem);

            }
        }
        public void DropItem(object sender, RoutedEventArgs e) {
            PlayerInventory invent = new PlayerInventory();
            MenuItem button = (sender as MenuItem);
            invent.DropItem(button);
            InventoryItems.Children.Clear();
            LoadInventory();

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
