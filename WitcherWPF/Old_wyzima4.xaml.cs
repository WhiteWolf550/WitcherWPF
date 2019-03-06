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
    /// Interakční logika pro Old_wyzima4.xaml
    /// </summary>
    public partial class Old_wyzima4 : UserControl
    {
        static public bool Looted = false;
        public Old_wyzima4()
        {
            InitializeComponent();
            CheckLootReset();
        }
        public void HideLoot(bool hide) {
            if (hide == true) {
                Looted = true;
                Shelf.Visibility = Visibility.Hidden;
            } else {
                Looted = false;
                Shelf.Visibility = Visibility.Visible;
            }
        }
        public void CheckLootReset() {
            if (Globals.LootReset == false) {
                CheckLoot();
            }else {
                Looted = false;
                CheckLoot();
            }
        }
        public void CheckLoot() {
            if (Looted == true) {
                Shelf.Visibility = Visibility.Hidden;
            } else if (Looted == false) {
                Shelf.Visibility = Visibility.Visible;
            }
        }
    }
}
