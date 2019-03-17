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

namespace WitcherWPF {
    /// <summary>
    /// Interakční logika pro DecisionPage.xaml
    /// </summary>
    public partial class DecisionPage : Page {
        private Frame parentFrame;
        private Time time;

        MediaPlayer media = new MediaPlayer();
        List<Game> game = new List<Game>();
        FileManager manager = new FileManager();
        public DecisionPage() {
            InitializeComponent();

        }
        public DecisionPage(Frame parentFrame, Time time) : this() {
            this.parentFrame = parentFrame;
            this.time = time;
            BlackScreen.Visibility = Visibility.Visible;
            BlackScreen.Opacity = 1;
            Music();

            PlayBackground();
            game = manager.LoadGame();
            Transition();
        }
        private void PlayBackground() {
            mediaElement.Source = new Uri("../../cutscenes/DecisionVideo.mp4", UriKind.Relative);
            mediaElement.Play();
        }
        private void Death_Click(object sender, RoutedEventArgs e) {
            media.Stop();
            ChangeDecision(true);
            parentFrame.Navigate(new Cutscenes(parentFrame, time, "Chapter1Cut3Die"));
        }

        private void Live_Click(object sender, RoutedEventArgs e) {
            media.Stop();
            ChangeDecision(false);
            parentFrame.Navigate(new Cutscenes(parentFrame, time, "Chapter1Cut3Live"));
        }
        public void Transition() {
            var animation = new DoubleAnimation {
                To = 0,
                BeginTime = TimeSpan.FromSeconds(1),
                Duration = TimeSpan.FromSeconds(4),
                FillBehavior = FillBehavior.Stop
            };

            animation.Completed += (s, a) => BlackScreen.Visibility = Visibility.Hidden;
            animation.Completed += (s, a) => BlackScreen.Opacity = 0;

            BlackScreen.BeginAnimation(UIElement.OpacityProperty, animation);
            
        }
        public void ChangeDecision(bool decision) {
            foreach(Game item in game) {
                item.MayorDead = decision;
            }
            manager.SaveGame(game);
        }

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e) {
            mediaElement.Position = new TimeSpan(0, 0, 1);
            mediaElement.Play();
        }
        public void Music() {
            media.Open(new Uri("../../sounds/music/Decide.mp3", UriKind.Relative));
            media.Play();
        }
    }
}
