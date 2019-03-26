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
    /// Interakční logika pro Novigrad_House1.xaml
    /// </summary>
    public partial class Novigrad_House1 : UserControl {
        static public bool Looted = false;
        public Novigrad_House1() {
            InitializeComponent();
            CheckLootReset();
        }
        public void HideLoot(bool hide) {
            if (hide == true) {
                Looted = true;
                Loot.Visibility = Visibility.Hidden;
            } else {
                Looted = false;
                Loot.Visibility = Visibility.Visible;
            }
        }
        public void CheckLootReset() {
            if (Globals.LootReset == false) {
                CheckLoot();
            } else {
                Looted = false;
                CheckLoot();
            }
        }
        public void CheckLoot() {
            if (Looted == true) {
                Loot.Visibility = Visibility.Hidden;
            } else if (Looted == false) {
                Loot.Visibility = Visibility.Visible;
            }
        }
    }
}
