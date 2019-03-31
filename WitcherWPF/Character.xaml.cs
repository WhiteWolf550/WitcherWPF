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
    /// Interakční logika pro Character.xaml
    /// </summary>
    public partial class Character : Page {
        private Frame parentFrame;
        private Time time;
        Player player = new Player();
        Music sound = new Music();
        public DispatcherTimer skilltimer = new DispatcherTimer();
        public Character() {
            InitializeComponent();
            sound.PlaySound("NewPage");
        }
        public Character(Frame parentFrame, Time time) : this() {
            this.parentFrame = parentFrame;
            this.time = time;
            this.time = time;
            SkillPoints.Content = player.GetSkillPoints();
            SetTimer();
            skilltimer.Start();
        }
        public void SetTimer() {
            skilltimer.Interval = TimeSpan.FromSeconds(1);
            skilltimer.Tick += new EventHandler(Timer_tick);
        }
        public void GetInventory(object sender, RoutedEventArgs e) {
            parentFrame.Navigate(new Inventory(parentFrame, false, time));
            skilltimer.Stop();
        }
        public void GetQuests(object sender, RoutedEventArgs e) {
            parentFrame.Navigate(new Quests(parentFrame, time));
            skilltimer.Stop();
        }
        public void GetMap(object sender, RoutedEventArgs e) {
            parentFrame.Navigate(new Map(parentFrame, time));
            skilltimer.Stop();
        }
        public void GetJournal(object sender, RoutedEventArgs e) {
            parentFrame.Navigate(new Journal(parentFrame, time));
            skilltimer.Stop();
        }
        public void GetAlchemy(object sender, RoutedEventArgs e) {
            MessageBox.Show("Musíš začít meditovat, aby jsi mohl použít alchymii");
        }
        public void GetLocation(object sender, RoutedEventArgs e) {
            parentFrame.Navigate(new Location(parentFrame, time));
            skilltimer.Stop();
        }

        private void AardClick(object sender, RoutedEventArgs e) {
            /*HideAll();
            Aard.Load();
            Aard.Visibility = Visibility.Visible;*/

            grid.Children.Clear();
            AardSkills skll = new AardSkills();
            grid.Children.Add(skll);
            skll.Load();

            sound.PlaySound("ChooseTree");
        }
        private void IgniClick(object sender, RoutedEventArgs e) {

            grid.Children.Clear();
            IgniSkills skll = new IgniSkills();
            grid.Children.Add(skll);
            skll.Load();

            sound.PlaySound("ChooseTree");
        }
        private void QuenClick(object sender, RoutedEventArgs e) {

            grid.Children.Clear();
            QuenSkills skll = new QuenSkills();
            grid.Children.Add(skll);
            skll.Load();

            sound.PlaySound("ChooseTree");
        }
        private void YrdenClick(object sender, RoutedEventArgs e) {

            grid.Children.Clear();
            YrdenSkills skll = new YrdenSkills();
            grid.Children.Add(skll);
            skll.Load();

            sound.PlaySound("ChooseTree");
        }
        private void AxiiClick(object sender, RoutedEventArgs e) {

            grid.Children.Clear();
            AxiiSkills skll = new AxiiSkills();
            grid.Children.Add(skll);
            skll.Load();

            sound.PlaySound("ChooseTree");
        }
        private void StrongClick(object sender, RoutedEventArgs e) {

            grid.Children.Clear();
            StrongSkills skll = new StrongSkills();
            grid.Children.Add(skll);
            skll.Load();

            sound.PlaySound("ChooseTree");
        }
        private void FastClick(object sender, RoutedEventArgs e) {

            grid.Children.Clear();
            EnduranceSkills skll = new EnduranceSkills();
            grid.Children.Add(skll);
            skll.Load();

            sound.PlaySound("ChooseTree");
        }
        
        void Timer_tick(object sender, EventArgs e) {
            SkillPoints.Content = player.GetSkillPoints();
        }
    }
}
