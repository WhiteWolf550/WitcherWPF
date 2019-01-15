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
        static public string loc;
        static public bool LootLock;
        Item it = new Item();
        public Location() {
            InitializeComponent();
            Wyzima_Castle.Foltest.Click += new RoutedEventHandler(GetDialogue);
            Wyzima_Castle.Flower.Click += new RoutedEventHandler(GetLoot);
            Wyzima_Castle2.Triss.Click += new RoutedEventHandler(GetDialogue);
            if (LootLock == true) {
                Wyzima_Castle.Flower.Visibility = Visibility.Hidden;
            }

        }
        public Location(Frame parentFrame, string location) : this() {
            this.parentFrame = parentFrame;
            loc = location;
            SetLocation();
        }
        public void SetLocation() {
            if (loc == "Old_wyzima2") {
                Wyzima_Castle2.Visibility = Visibility.Visible;
            }else if(loc == "Old_wyzima1") {
                Wyzima_Castle.Visibility = Visibility.Visible;
            }
        }
        public Location(Frame parentFrame) : this() {
            this.parentFrame = parentFrame;
            SetLocation();
        }
        public void GetInventory(object sender, RoutedEventArgs e) {
            parentFrame.Navigate(new Inventory(parentFrame));
        }
        public void GetDialogue(object sender, RoutedEventArgs e) {
            Button button = (sender as Button);
            string charworld = button.Name.ToString();
            parentFrame.Navigate(new Dialogue(parentFrame, charworld));
             
        }
        public void GetLoot(object sender, RoutedEventArgs e) {
            
            it.GenerateLoot(LootInventory, Wyzima_Castle.Flower, LootBack, TakeLoot, CloseBut);
        }
        public void LootToInventory(object sender, RoutedEventArgs e) {
            it.LootToInventory(LootInventory, TakeLoot, LootBack, CloseBut);
            LootLock = true;

        }
        public void CloseLoot(object sender, RoutedEventArgs e) {
            TakeLoot.Visibility = Visibility.Hidden;
            LootInventory.Visibility = Visibility.Hidden;
            LootBack.Visibility = Visibility.Hidden;
            CloseBut.Visibility = Visibility.Hidden;
            Wyzima_Castle.Flower.Visibility = Visibility.Visible;
            LootInventory.Children.Clear();
        }
    }
}
