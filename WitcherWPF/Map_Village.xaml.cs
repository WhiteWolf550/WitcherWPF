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

namespace WitcherWPF {
    /// <summary>
    /// Interakční logika pro Map_Village.xaml
    /// </summary>
    public partial class Map_Village : UserControl {
        public Map_Village() {
            InitializeComponent();
            Barghest.Visibility = Visibility.Hidden;
            ShowEnemy();
        }
        public void ShowEnemy() {
            Random rand = new Random();
            int rn = rand.Next(0, 23);
            if (Globals.Hour == rn) {
                Barghest.Visibility = Visibility.Visible;
            }
        }
    }
}
