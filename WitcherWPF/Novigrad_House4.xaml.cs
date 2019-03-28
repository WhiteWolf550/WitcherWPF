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
    /// Interakční logika pro Novigrad_House4.xaml
    /// </summary>
    public partial class Novigrad_House4 : UserControl {
        List<PlayerQuest> pquest = new List<PlayerQuest>();
        FileManager manager = new FileManager();
        public Novigrad_House4() {
            InitializeComponent();
            Lizard.Visibility = Visibility.Hidden;
            Enem();
        }

        public void Enem() {
            pquest = manager.LoadPlayerQuests();
            foreach (PlayerQuest item in pquest) {
                if (item.Quest.QuestName == "Tajná organizace" && item.Quest.QuestID == 6) {
                    Lizard.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
