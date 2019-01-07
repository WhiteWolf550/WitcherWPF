using Newtonsoft.Json;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WitcherWPF
{
    /// <summary>
    /// Interakční logika pro Quests.xaml
    /// </summary>
    public partial class Quests : Page
    {
        
        private Frame parentFrame;
        public Quests()
        {
            InitializeComponent();
        }
        public Quests(Frame parentFrame) : this() {
            this.parentFrame = parentFrame;
            LoadQuests();
        }
        public void GetInventory(object sender, RoutedEventArgs e) {
            parentFrame.Navigate(new Inventory(parentFrame));
        }
        public void GetMap(object sender, RoutedEventArgs e) {
            parentFrame.Navigate(new Map(parentFrame));
        }
        public void GetJournal(object sender, RoutedEventArgs e) {
            parentFrame.Navigate(new Journal(parentFrame));
        }
        public void GetCharacter(object sender, RoutedEventArgs e) {
            parentFrame.Navigate(new Character(parentFrame));
        }
        public void GetAlchemy(object sender, RoutedEventArgs e) {

        }
        public void GetLocation(object sender, RoutedEventArgs e) {
            parentFrame.Navigate(new Location(parentFrame));
        }
        public void LoadQuests() {
            JsonSerializerSettings settings = new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.All
            };
            string questpath = @"../../saves/Quests.json";
            string jsonFromFile = File.ReadAllText(questpath);
            List<Quest> quests = JsonConvert.DeserializeObject<List<Quest>>(jsonFromFile, settings);
            var matches = quests.Where(s => s.QuestActive == true);
            foreach (var item in matches) {
                StackPanel stack = new StackPanel();
                stack.Orientation = Orientation.Horizontal;

                Image img = new Image();
                img.Source = new BitmapImage(new Uri(@"img/UI/Primary_quests.png", UriKind.Relative));
                img.Width = 32;
                img.Height = 32;

                Button but = new Button();
                but.Content = item.QuestName;
                but.FontSize = 20;
                but.Foreground = Brushes.White;
                but.Background = Brushes.Transparent;
                but.BorderBrush = Brushes.Transparent;
                but.Click += new RoutedEventHandler(OpenQuest);
                but.Tag = item.QuestName;
                stack.Children.Add(img);
                stack.Children.Add(but);
                QuestStack.Children.Add(stack);
            }
        }
        public void OpenQuest(object sender, RoutedEventArgs e) {
            JsonSerializerSettings settings = new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.All
            };
            string questpath = @"../../saves/Quests.json";
            string jsonFromFile = File.ReadAllText(questpath);
            List<Quest> quests = JsonConvert.DeserializeObject<List<Quest>>(jsonFromFile, settings);
            Button button = (Button)sender;
            var matches = quests.Where(s => s.QuestName == button.Tag.ToString());
            foreach (var item in matches ) {
                NameQ.Content = item.QuestName;
                GoalQ.Content = item.QuestGoal;
                DescQ.Text = item.QuestDescription;
            }
        }
    }
}
