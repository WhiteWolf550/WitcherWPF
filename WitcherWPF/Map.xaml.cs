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
        public Map()
        {
            InitializeComponent();
            WyzimaCastle.WyzimaCastle1.Click += new RoutedEventHandler(Travel);
            WyzimaCastle.WyzimaCastle2.Click += new RoutedEventHandler(Travel);
        }
        public Map(Frame parentFrame, Time time) : this() {
            this.parentFrame = parentFrame;
            this.time = time;
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

        }
        public void GetLocation(object sender, RoutedEventArgs e) {
            parentFrame.Navigate(new Location(parentFrame, time));
        }
        public void Travel(object sender, RoutedEventArgs e) {
            Button button = (sender as Button);
            string tag = button.Tag.ToString();
            parentFrame.Navigate(new Location(parentFrame, tag, time));

        }
    }
}
