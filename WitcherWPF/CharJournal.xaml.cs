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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAnimatedGif;

namespace WitcherWPF {
    /// <summary>
    /// Interakční logika pro CharJournal.xaml
    /// </summary>
    public partial class CharJournal : UserControl {
        public CharJournal() {
            InitializeComponent();
        }
        public void LoadInfo(string Name, string Description, string GIF) {
            CharName.Content = Name;
            CharDesc.Text = Description;
            CharIMG.Source = new BitmapImage(new Uri(GIF, UriKind.Relative));
        }
    }
}
