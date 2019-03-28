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

namespace WitcherWPF {
    /// <summary>
    /// Interakční logika pro Novigrad_Outside1.xaml
    /// </summary>
    public partial class Novigrad_Outside1 : UserControl {

        public DispatcherTimer time = new DispatcherTimer();
        List<PlayerQuest> pquest = new List<PlayerQuest>();
        List<Game> game = new List<Game>();
        List<Dialogues> dialogues = new List<Dialogues>();
        FileManager manager = new FileManager();
        Location location = new Location();
        public Novigrad_Outside1() {
            InitializeComponent();
            LoadBackground();
            Prison();
            Timer();
            time.Start();
        }
        public void LoadBackground() {

            if (Globals.daytime == "night") {
                LocationImage.Source = new BitmapImage(new Uri("img/Locations/Novigrad_Outside1_night.png", UriKind.Relative));
            } else if (Globals.daytime == "day") {
                LocationImage.Source = new BitmapImage(new Uri("img/Locations/Novigrad_Outside1_day.png", UriKind.Relative));
            }

        }
        public void Timer() {
            time.Interval = TimeSpan.FromSeconds(1);
            time.Tick += new EventHandler(Time_tick);

        }
        void Time_tick(object sender, EventArgs e) {
            LoadBackground();

        }
        public void Prison() {
            pquest = manager.LoadPlayerQuests();
            game = manager.LoadGame();
            foreach (PlayerQuest item in pquest) {
                if (item.Quest.QuestName == "Tajná organizace" && item.Quest.QuestID == 1) {
                    foreach(Game item2 in game) {
                        if (item2.MayorDead == true) {
                            ActivateDialogue("Co tady děláš?");
                            location.CutsceneTransitionShow("Chapter2Cut2");
                        }else {
                            ActivateDialogue("Co tu děláš?");
                            location.GetDialogue("Morenn");
                        }
                    }
                }
            }
        }
        public void ActivateDialogue(string Choice) {
            dialogues = manager.LoadDialogue(Globals.DialoguePath);
            foreach(Dialogues item in dialogues) {
                if (item.Choice == Choice) {
                    item.Enabled = true;
                }
            }
        }
    }
}
