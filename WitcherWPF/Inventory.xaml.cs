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

namespace WitcherWPF
{
    /// <summary>
    /// Interakční logika pro Inventory.xaml
    /// </summary>
    public partial class Inventory : Page
    {
        static JsonSerializerSettings settings = new JsonSerializerSettings {
            TypeNameHandling = TypeNameHandling.All
        };
        static string ipath = @"../../saves/GameItems.json";
        static string playerinvpath = @"../../saves/PlayerInventory.json";
        static string jsonFromFile = File.ReadAllText(ipath);
        List<Item> items = JsonConvert.DeserializeObject<List<Item>>(jsonFromFile, settings);
        private Frame parentFrame;
        
        public Inventory()
        {
            InitializeComponent();
            
        }
        public Inventory(Frame parentFrame) : this() {
            this.parentFrame = parentFrame;
            LoadInventory();
        }

        public void GetMap(object sender, RoutedEventArgs e) {
            parentFrame.Navigate(new Map(parentFrame));
        }
        public void GetQuests(object sender, RoutedEventArgs e) {
            parentFrame.Navigate(new Quests(parentFrame));
        }
        public void GetJournal(object sender, RoutedEventArgs e) {
            parentFrame.Navigate(new Journal(parentFrame));
        }
        public void GetCharacter(object sender, RoutedEventArgs e) {
            parentFrame.Navigate(new Character(parentFrame));
        }
        public void GetAlchemy(object sender, RoutedEventArgs e) {

        }
        public void GetLocation(object sender, RoutedEventArgs e) {
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
                Button inventoryitem = new Button();
                inventoryitem.Content = inventoryimage;
                inventoryitem.Height = 20;
                inventoryitem.Width = 20;
                inventoryitem.BorderBrush = Brushes.Transparent;
                inventoryitem.Background = Brushes.Transparent;
                InventoryItems.Children.Add(inventoryitem);

            }
        }
    }
}
