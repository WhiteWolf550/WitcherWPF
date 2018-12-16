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

namespace WitcherWPF {
    /// <summary>
    /// Interakční logika pro Location.xaml
    /// </summary>
    public partial class Location : Page {
        private Frame parentFrame;
        public Location() {
            InitializeComponent();
            Wyzima_Castle.Foltest.Click += new RoutedEventHandler(GetDialogueFoltest);
            Wyzima_Castle.Flower.Click += new RoutedEventHandler(GetLoot);

        }
        public Location(Frame parentFrame) : this() {
            this.parentFrame = parentFrame;
        }
        public void GetInventory(object sender, RoutedEventArgs e) {
            parentFrame.Navigate(new Inventory(parentFrame));
        }
        public void GetDialogueFoltest(object sender, RoutedEventArgs e) {
            parentFrame.Navigate(new Dialogue(parentFrame));
             
        }
        public void GetLoot(object sender, RoutedEventArgs e) {
            Item it = new Item();
            //it.GenerateLoot(LootInventory, Test);
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
            foreach (Item item in matches) {
                int rn = rand.Next(0, itc);
                Image inventoryimage = new Image();
                inventoryimage.Width = 18;
                inventoryimage.Height = 18;
                inventoryimage.Source = new BitmapImage(new Uri(item.Source, UriKind.Relative));
                inventoryimage.Margin = new Thickness(-15, -3, -3, -3);
                Button inventoryitem = new Button();
                inventoryitem.Content = inventoryimage;
                inventoryitem.Height = 20;
                inventoryitem.Width = 20;
                inventoryitem.ToolTip = item.Source;
                inventoryitem.BorderBrush = Brushes.Transparent;
                inventoryitem.Background = Brushes.Transparent;
                LootInventory.Children.Add(inventoryitem);

            }
            LootInventory.Visibility = Visibility.Visible;
            LootBack.Visibility = Visibility.Visible;
        }

    }
}
