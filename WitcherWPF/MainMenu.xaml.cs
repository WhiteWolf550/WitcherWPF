using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interakční logika pro MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page {
        private Frame parentFrame;
        private Time time;
        Music sound = new Music();
        Game game = new Game();
        List<Game> games = new List<Game>();
        public MainMenu() {
            InitializeComponent();
            
            MainMenuMusic();
            CheckGame();
        }

        public MainMenu (Frame parentFrame, Time time) : this() {
            this.parentFrame = parentFrame;
            this.time = time;
            time.time.Stop();

        }
        private void MainMenuMusic() {
            Globals.Combat = true;
            
            sound.MainMenuMusic();
        }


        private void NewGameClick(object sender, RoutedEventArgs e) {
            TransitionShow();
        }
        private void LoadGameClick(object sender, RoutedEventArgs e) {
            LoadGame();
        }
        private void ExitGameClick(object sender, RoutedEventArgs e) {
            SaveGlobals();
            System.Windows.Application.Current.Shutdown();
        }
        public void NewGame() {
            game.NewGame();

            sound.StopMusic();
            LoadToGlobals();
            parentFrame.Navigate(new Cutscenes(parentFrame, time, "GameIntro"));

        }
        public void LoadToGlobals() {
            List<Game> game = new List<Game>();
            FileManager manager = new FileManager();
            game = manager.LoadGame();
            foreach (Game item in game) {
                Globals.Hour = item.Hour;
                Globals.Minute = item.Minute;
                Globals.location = item.CurrentLocation;
                Globals.DialoguePath = item.DialoguePath;
                Globals.Chapter = item.Chapter;
            }
        }
        public void SaveGlobals() {
            List<Game> game = new List<Game>();
            FileManager manager = new FileManager();
            game = manager.LoadGame();
            foreach (Game item in game) {
                item.Hour = Globals.Hour;
                item.Minute = Globals.Minute;
                item.Chapter = Globals.Chapter;
                item.DialoguePath = Globals.DialoguePath;
                item.CurrentLocation = Globals.location;
            }
            manager.SaveGame(game);
        }
        public void LoadGame() {
            parentFrame.Navigate(new LoadScreen(parentFrame, time, sound));
        }
        public void TransitionShow() {
            BlackScreen.Visibility = Visibility.Visible;
            var animation = new DoubleAnimation {
                To = 1,
                BeginTime = TimeSpan.FromSeconds(0),
                Duration = TimeSpan.FromSeconds(6),
                FillBehavior = FillBehavior.Stop
            };

            animation.Completed += (s, a) => BlackScreen.Visibility = Visibility.Visible;
            animation.Completed += (s, a) => BlackScreen.Opacity = 1;
            animation.Completed += new EventHandler(StartNewGame);
            BlackScreen.BeginAnimation(UIElement.OpacityProperty, animation);
        }
        private void StartNewGame(object sender, EventArgs e) {
            NewGame();
        }
        public void CheckGame() {
            if (!File.Exists("../../saves/Game.json")) {
                GameNew.IsEnabled = false;
            }else {
                GameCompletedCheck();
            }
        }
        public void GameCompletedCheck() {
            FileManager manager = new FileManager();
            games = manager.LoadGame();
            foreach(Game item in games) {
                if (item.Chapter == 4 || Globals.Chapter == 4) {
                    GameNew.IsEnabled = false;
                }
            }
        }
    }
}
