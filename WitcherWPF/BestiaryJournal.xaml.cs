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
    /// Interakční logika pro BestiaryJournal.xaml
    /// </summary>
    public partial class BestiaryJournal : UserControl {
        public BestiaryJournal() {
            InitializeComponent();
        }
        public void LoadInfo(string Name, string Description, string Weak, string Strength,  string GIF) {
            MonsterName.Content = Name;
            CharDesc.Text = Description;
            MonWeak.Text = Weak;
            MonStrength.Text = Strength;
            MonsterIMG.Source = new BitmapImage(new Uri(GIF, UriKind.Relative));
        }
    }
}
