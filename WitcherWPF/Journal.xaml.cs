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
    /// Interakční logika pro Journal.xaml
    /// </summary>
    public partial class Journal : Page
    {
        private Frame parentFrame;
        public Journal()
        {
            InitializeComponent();
        }
        public Journal(Frame parentFrame) : this() {
            this.parentFrame = parentFrame;
        }
        public void GetInventory(object sender, RoutedEventArgs e) {
            parentFrame.Navigate(new Inventory(parentFrame));
        }
        public void GetQuests(object sender, RoutedEventArgs e) {
            parentFrame.Navigate(new Quests(parentFrame));
        }
        public void GetMap(object sender, RoutedEventArgs e) {
            parentFrame.Navigate(new Map(parentFrame));
        }
        public void GetCharacter(object sender, RoutedEventArgs e) {
            
        }
        public void GetAlchemy(object sender, RoutedEventArgs e) {
            
        }
    }
}
