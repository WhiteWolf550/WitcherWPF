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
    /// Interakční logika pro MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page {
        private Frame parentFrame;
        private Time time;
        Music sound = new Music();
        public MainMenu() {
            InitializeComponent();
            MainMenuMusic();
        }

        public MainMenu (Frame parentFrame, Time time) : this() {
            this.parentFrame = parentFrame;
            this.time = time;
            
        }
        private void MainMenuMusic() {
            Globals.Combat = true;
            sound.MainMenuMusic();
        }


        private void NewGameClick(object sender, RoutedEventArgs e) {

        }
        private void LoadGameClick(object sender, RoutedEventArgs e) {
            LoadGame();
        }
        private void ExitGameClick(object sender, RoutedEventArgs e) {

        }
        public void NewGame() {

        }
        public void LoadGame() {
            parentFrame.Navigate(new LoadScreen(parentFrame, time, sound));
        }
    }
}
