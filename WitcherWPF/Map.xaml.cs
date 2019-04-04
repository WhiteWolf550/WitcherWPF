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
    /// Interakční logika pro Map.xaml
    /// </summary>
    public partial class Map : Page
    {
        private Frame parentFrame;
        private Time time;
        Music sound = new Music();
        static public string map;
        public Map()
        {
            InitializeComponent();
            sound.PlaySound("NewPage");
            Clicks();
            SetMap();
        }
        public void Clicks() {
            Village.Barghest.Click += new RoutedEventHandler(StartCombat_Click);
            
            
        }
        public Map(Frame parentFrame, Time time) : this() {
            this.parentFrame = parentFrame;
            this.time = time;
        }
        public void SetMap() {
            map = Globals.location;
            if (map == "Old_wyzima2" || map == "Old_wyzima1") {
                WyzimaCastle.Visibility = Visibility.Visible;
            } else if(map == "Old_wyzima3") {
                Old_Wyzima.Visibility = Visibility.Visible;
            }else if (map == "Village_Outside1") {
                Village.Visibility = Visibility.Visible;
            }else if (map == "Village_Outside2") {
                Village.Visibility = Visibility.Visible;
            }else if (map == "Village_Outside3") {
                Village.Visibility = Visibility.Visible;
            }else if (map == "Novigrad_Outside1") {
                Novigrad.Visibility = Visibility.Visible;
            } else if (map == "Novigrad_Outside2") {
                Novigrad.Visibility = Visibility.Visible;
            } else if (map == "Novigrad_Outside3") {
                Novigrad.Visibility = Visibility.Visible;
            } else if (map == "Novigrad_Outside4") {
                Novigrad.Visibility = Visibility.Visible;
            } else if (map == "Novigrad_Outside5") {
                Novigrad.Visibility = Visibility.Visible;
            } else if (map == "Novigrad_Outside6") {
                Novigrad.Visibility = Visibility.Visible;
            } else if (map == "Novigrad_Outside7") {
                Novigrad.Visibility = Visibility.Visible;
            }
        }
        public void GetInventory(object sender, RoutedEventArgs e) {
            parentFrame.Navigate(new Inventory(parentFrame, false, time));
        }
        public void GetQuests(object sender, RoutedEventArgs e) {
            parentFrame.Navigate(new Quests(parentFrame, time));
        }
        public void GetJournal(object sender, RoutedEventArgs e) {
            parentFrame.Navigate(new Journal(parentFrame, time));
        }
        public void GetCharacter(object sender, RoutedEventArgs e) {
            parentFrame.Navigate(new Character(parentFrame, time));
        }
        public void GetAlchemy(object sender, RoutedEventArgs e) {
            MessageBox.Show("Musíš začít meditovat, aby jsi mohl použít alchymii");
        }
        public void GetLocation(object sender, RoutedEventArgs e) {
            parentFrame.Navigate(new Location(parentFrame, time));
        }
        public void Travel(object sender, RoutedEventArgs e) {
            Button button = (sender as Button);
            string tag = button.Tag.ToString();
            parentFrame.Navigate(new Location(parentFrame, tag, time));

        }
        public void StartCombat_Click(object sender, RoutedEventArgs e) {
            Button button = (sender as Button);
            parentFrame.Navigate(new Combat(parentFrame, false, time, false, null, button.Tag.ToString()));
        }
        
        
    }
}
