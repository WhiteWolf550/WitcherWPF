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
    /// Interakční logika pro Cutscenes.xaml
    /// </summary>
    public partial class Cutscenes : Page
    {
        private Frame parentFrame;
        private Time time;
        private string CutsceneName;
        

        public Cutscenes()
        {
            InitializeComponent();
            Application.Current.MainWindow.KeyDown += new KeyEventHandler(Keys);
        }

        public Cutscenes(Frame parentFrame, Time time, string CutsceneName) : this() {
            this.parentFrame = parentFrame;
            this.time = time;
            this.CutsceneName = CutsceneName;

            time.Visibility = Visibility.Hidden;
            Globals.Combat = true;
            PlayCutscene();
        }
        public void Keys(object sender, KeyEventArgs e) {
            if (e.Key == Key.Space) {
                Next();
            }
        }
        public void PlayCutscene() {
            CutScene.Source = new Uri("../../cutscenes/" + CutsceneName + ".mp4", UriKind.Relative);
            CutScene.Play();
        }
        public void CutPlay() {
            CutScene.Source = new Uri("../../cutscenes/" + CutsceneName + ".mp4", UriKind.Relative);
            CutScene.Play();
        }
        private void CutScene_MediaEnded(object sender, RoutedEventArgs e) {
            Next();
        }
        public void Next() {
            if (CutsceneName == "GameIntro") {
                CutsceneName = "PrologueCut1";
                CutPlay();
            }else if (CutsceneName == "PrologueCut1") {
                RemoveHandler();
                Globals.Combat = false;
                time.Timer();
                parentFrame.Navigate(new Location(parentFrame, "Old_wyzima2", time));
            }else if (CutsceneName == "PrologueCut2") {
                CutsceneName = "Chapter1Cut1";
                CutPlay();
            }else if(CutsceneName == "Chapter1Cut1") {
                parentFrame.Navigate(new Combat(parentFrame, false, time, false, null, "Barghest", "Chapter1Cut2"));
            }else if (CutsceneName == "Chapter1Cut2") {
                Globals.Combat = false;
                parentFrame.Navigate(new Location(parentFrame, "Old_wyzima2", time));
            }
        }
        private void RemoveHandler() {
            Application.Current.MainWindow.KeyDown -= new KeyEventHandler(Keys);
        }
    }
}
