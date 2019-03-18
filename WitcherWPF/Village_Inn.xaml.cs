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
    /// Interakční logika pro Village_Inn.xaml
    /// </summary>
    public partial class Village_Inn : UserControl {
        List<PlayerQuest> pquest = new List<PlayerQuest>();
        FileManager manager = new FileManager();
        public Village_Inn() {
            InitializeComponent();
            pquest = manager.LoadPlayerQuests();
            ChapterQuest();
        }
        public void ChapterQuest() {
            foreach(PlayerQuest item in pquest) {
                if (item.Quest.QuestName == "Záhadná vesnice" && item.Quest.QuestID == 4) {
                    
                    Zoltan.Visibility = Visibility.Hidden;
                    Olaf.Visibility = Visibility.Hidden;
                }
            }
        }
    }
}
