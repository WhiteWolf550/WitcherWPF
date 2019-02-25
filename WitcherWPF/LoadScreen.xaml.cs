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
using System.Windows.Threading;

namespace WitcherWPF
{
    /// <summary>
    /// Interakční logika pro LoadScreen.xaml
    /// </summary>
    public partial class LoadScreen : Page
    {
        private Frame parentFrame;
        private Time time;
        DispatcherTimer loadtimer = new DispatcherTimer();

        private Music sound;
        public LoadScreen() {
            InitializeComponent();
            LoadTimer();
            loadtimer.Start();
        }
        public LoadScreen(Frame parentFrame, Time time, Music sound) : this() {
            this.parentFrame = parentFrame;
            this.time = time;
            this.sound = sound;

        }
        public void LoadTimer() {
            loadtimer.Interval = TimeSpan.FromSeconds(1);
            loadtimer.Tick += new EventHandler(Load_Tick);
        }
        void Load_Tick(object sender, EventArgs e) {
            LoadBar.Value += 10;
            if (LoadBar.Value == 100) {
                loadtimer.Stop();
                TransitionShow();
            }
        }
        public void Load(object sender, EventArgs E) {
            sound.StopMusic();
            Globals.Combat = false;
            time.Visibility = Visibility.Visible;
            time.Timer();
            parentFrame.Navigate(new Location(parentFrame, time));
        }
        public void TransitionShow() {
            BlackScreen.Visibility = Visibility.Visible;
            var animation = new DoubleAnimation {
                To = 1,
                BeginTime = TimeSpan.FromSeconds(0),
                Duration = TimeSpan.FromSeconds(1),
                FillBehavior = FillBehavior.Stop
            };

            animation.Completed += (s, a) => BlackScreen.Visibility = Visibility.Visible;
            animation.Completed += (s, a) => BlackScreen.Opacity = 1;
            animation.Completed += new EventHandler(Load);
            BlackScreen.BeginAnimation(UIElement.OpacityProperty, animation);
        }
    }
}
