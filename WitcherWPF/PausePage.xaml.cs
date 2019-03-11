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
    /// Interakční logika pro PausePage.xaml
    /// </summary>
    public partial class PausePage : Page {
        private Frame parentFrame;
        private Time time;
        FileManager manager = new FileManager();

        List<Game> game = new List<Game>();
        public PausePage() {
            InitializeComponent();
        }
        public PausePage(Frame parentFrame, Time time) : this() {
            this.parentFrame = parentFrame;
            this.time = time;

            time.time.Stop();
            game = manager.LoadGame();
        }

        private void Menu_Click(object sender, RoutedEventArgs e) {
            Globals.Combat = true;
            SaveGlobals();
            parentFrame.Navigate(new MainMenu(parentFrame, time));
        }
        private void ExitnSave_Click(object sender, RoutedEventArgs e) {
            SaveGlobals();
            System.Windows.Application.Current.Shutdown();
        }
        private void Continue_Click(object sender, RoutedEventArgs e) {
            time.time.Start();
            parentFrame.Navigate(new Location(parentFrame, time));
        }
        public void SaveGlobals() {
            foreach (Game item in game) {
                item.Hour = Globals.Hour;
                item.Minute = Globals.Minute;
                item.Chapter = Globals.Chapter;
                item.DialoguePath = Globals.DialoguePath;
                item.CurrentLocation = Globals.location;
            }
            manager.SaveGame(game);
        }
    }
}
