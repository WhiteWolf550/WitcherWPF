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
        Item it = new Item();
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
            
            it.GenerateLoot(LootInventory, Wyzima_Castle.Flower, LootBack, TakeLoot);
        }
        public void LootToInventory(object sender, RoutedEventArgs e) {
            it.LootToInventory(LootInventory, TakeLoot, LootBack);
        }

    }
}
