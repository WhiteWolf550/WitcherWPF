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
using System.Windows.Threading;

namespace WitcherWPF
{
    /// <summary>
    /// Interakční logika pro Village_Outside3.xaml
    /// </summary>
    public partial class Village_Outside3 : UserControl
    {
        public DispatcherTimer time = new DispatcherTimer();
        List<PlayerQuest> qq = new List<PlayerQuest>();
        List<Crypt> crypts = new List<Crypt>();
        FileManager manager = new FileManager();
        public bool Looted = false;
        public Village_Outside3() {
            InitializeComponent();
            Ghoul.Visibility = Visibility.Hidden;
            Steps2.Visibility = Visibility.Hidden;
            DoorO1.Visibility = Visibility.Hidden;
            Crypt1.Visibility = Visibility.Visible;
            Zoltan.Visibility = Visibility.Hidden;
            Madman.Visibility = Visibility.Hidden;

            GhoulQuest();
            House();
            OldVillage();
            ZoltanCheck();
            //Crypt();
            LoadBackground();
            CheckLootReset();
            Timer();
            time.Start();

        }
        public void LoadBackground() {

            if (Globals.daytime == "night") {
                LocationImage.Source = new BitmapImage(new Uri("img/Locations/Village_Outside3_night.png", UriKind.Relative));
            } else if (Globals.daytime == "day") {
                LocationImage.Source = new BitmapImage(new Uri("img/Locations/Village_Outside3_day.png", UriKind.Relative));
            }

        }
        public void Timer() {
            time.Interval = TimeSpan.FromSeconds(1);
            time.Tick += new EventHandler(Time_tick);

        }
        void Time_tick(object sender, EventArgs e) {
            LoadBackground();
            CheckMadman();

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
        public void CheckMadman() {
            if (Globals.Hour == 4) {
                Madman.Visibility = Visibility.Visible;
            }else {
                Madman.Visibility = Visibility.Hidden;
            }
        }
        public void GhoulQuest() {
            qq = manager.LoadPlayerQuests();
            foreach (PlayerQuest item in qq) {
                if (item.Quest.QuestName == "Problém s ghúly" && item.Quest.QuestID == 1) {
                    Ghoul.Visibility = Visibility.Visible;
                }
            }
        }
        public void OldVillage() {
            qq = manager.LoadPlayerQuests();
            foreach (PlayerQuest item in qq) {
                if (item.Quest.QuestName == "Na stopě Zaklínači" && item.Quest.QuestID == 7) {
                    Steps2.Visibility = Visibility.Visible;
                }
            }
        }
        public void House() {
            qq = manager.LoadPlayerQuests();
            foreach (PlayerQuest item in qq) {
                if (item.Quest.QuestName == "Cesta do Novigradu" && item.Quest.QuestID == 2) {
                    DoorO1.Visibility = Visibility.Visible;
                }
            }
        }
        public void Crypt() {
            crypts = manager.LoadCrypts();
            foreach(Crypt item in crypts) {
                if (item.Name == "Crypt1" &&item.IsEnabled == true) {
                    Crypt1.Visibility = Visibility.Visible;
                }
            }
        }
        public void ZoltanCheck() {
            qq = manager.LoadPlayerQuests();
            foreach (PlayerQuest item in qq) {
                if (item.Quest.QuestName == "Cesta do Novigradu" && item.Quest.QuestID == 1) {
                    Zoltan.Visibility = Visibility.Visible;
                }
            }
        }

    }
}
