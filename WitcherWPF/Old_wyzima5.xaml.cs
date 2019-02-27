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
    /// Interakční logika pro Old_wyzima5.xaml
    /// </summary>
    public partial class Old_wyzima5 : UserControl {
        List<PlayerQuest> qq = new List<PlayerQuest>();
        FileManager manager = new FileManager();
        public Old_wyzima5() {
            InitializeComponent();
            Quest();
        }
        public void Quest() {
            qq = manager.LoadPlayerQuests();
            foreach (PlayerQuest item in qq) {
                if (item.Quest.QuestName == "Strašidelný dům" && item.Quest.QuestID == 2) {
                    Shelf.Visibility = Visibility.Hidden;
                    Ghoul.Visibility = Visibility.Visible;
                    Outside.Visibility = Visibility.Hidden;
                }
                if (item.Quest.QuestName == "Strašidelný dům" && item.Quest.QuestID == 3) {
                    Shelf.Visibility = Visibility.Hidden;
                    Ghoul.Visibility = Visibility.Hidden;
                    Outside.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
