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
    /// Interakční logika pro Village_Crypt.xaml
    /// </summary>
    public partial class Village_Crypt : UserControl
    {
        List<Effect> effects = new List<Effect>();
        List<Crypt> crypts = new List<Crypt>();
        FileManager manager = new FileManager();
        public Village_Crypt()
        {
            InitializeComponent();
            BlackScreen.Visibility = Visibility.Visible;
            effects = manager.LoadEffects();
            crypts = manager.LoadCrypts();
            CheckCat();
            CheckChest();
        }
        public void CheckCat() {
            foreach(Effect item in effects) {
                if (item.Name == "Kočka") {
                    BlackScreen.Visibility = Visibility.Hidden;
                }
            }
        }
        public void CheckChest() {
            foreach(Crypt item in crypts) {
                if (item.Name == "Crypt1" && item.IsEnabled == false) {
                    Chest.Visibility = Visibility.Hidden;
                }
            }
        }

    }
}
