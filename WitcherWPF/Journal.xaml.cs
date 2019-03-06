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
    /// Interakční logika pro Journal.xaml
    /// </summary>
    public partial class Journal : Page
    {
        private Frame parentFrame;
        private Time time;
        Music sound = new Music();
        List<Characters> characters = new List<Characters>();
        List<Bestiary> bestiary = new List<Bestiary>();
        Dictionary<Button, Characters> chardict = new Dictionary<Button, Characters>();
        Dictionary<Button, Bestiary> mondict = new Dictionary<Button, Bestiary>();
        FileManager manager = new FileManager();
        public Journal()
        {
            InitializeComponent();
            characters = manager.LoadCharacters();
            bestiary = manager.LoadBestiary();
            sound.PlaySound("NewPage");
        }
        public Journal(Frame parentFrame, Time time) : this() {
            this.parentFrame = parentFrame;
            this.time = time;
        }
        public void GetInventory(object sender, RoutedEventArgs e) {
            parentFrame.Navigate(new Inventory(parentFrame, false, time));
        }
        public void GetQuests(object sender, RoutedEventArgs e) {
            parentFrame.Navigate(new Quests(parentFrame, time));
        }
        public void GetMap(object sender, RoutedEventArgs e) {
            parentFrame.Navigate(new Map(parentFrame, time));
        }
        public void GetCharacter(object sender, RoutedEventArgs e) {
            parentFrame.Navigate(new Character(parentFrame, time));
        }
        public void GetAlchemy(object sender, RoutedEventArgs e) {
            parentFrame.Navigate(new Alchemy(parentFrame, time, false));
        }
        public void GetLocation(object sender, RoutedEventArgs e) {
            parentFrame.Navigate(new Location(parentFrame, time));
        }

        private void GetChar(object sender, RoutedEventArgs e) {
            CharPanel.Children.Clear();
            LoadChar();
        }
        private void GetBestiary(object sender, RoutedEventArgs e) {
            CharPanel.Children.Clear();
            LoadMonsters();
        }
        public void LoadChar() {
            foreach(Characters item in characters) {
                Button button = new Button();
                button.Content = item.Name;
                button.FontSize = 23;
                button.Tag = "Character";
                button.Foreground = Brushes.WhiteSmoke;
                button.Background = Brushes.Transparent;
                button.BorderBrush = Brushes.Transparent;
                button.Click += new RoutedEventHandler(LoadInfoClick);
                CharPanel.Children.Add(button);
                chardict.Add(button, item);
            }
        }
        public void LoadMonsters() {
            foreach (Bestiary item in bestiary) {
                Button button = new Button();
                button.Content = item.Name;
                button.FontSize = 23;
                button.Tag = "Monster";
                button.Foreground = Brushes.WhiteSmoke;
                button.Background = Brushes.Transparent;
                button.BorderBrush = Brushes.Transparent;
                button.Click += new RoutedEventHandler(LoadInfoClick);
                CharPanel.Children.Add(button);
                mondict.Add(button, item);
            }
        }
        private void LoadInfoClick(object sender, RoutedEventArgs e) {
            Button button = (sender as Button);
            if (button.Tag.ToString() == "Character") {
                CharInfo.Visibility = Visibility.Visible;
                MonInfo.Visibility = Visibility.Hidden;

                Characters character = chardict[button];
                CharInfo.LoadInfo(character.Name, character.Description, character.Source);
            }else {
                CharInfo.Visibility = Visibility.Hidden;
                MonInfo.Visibility = Visibility.Visible;
                Bestiary beast = mondict[button];
                MonInfo.LoadInfo(beast.Name, beast.Description, beast.Weakness, beast.Strength, beast.Source);
            }
        }
    }
}
