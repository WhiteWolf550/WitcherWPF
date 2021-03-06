﻿using Newtonsoft.Json;
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
        private Time time;
        Music sound = new Music();
        FileManager manager = new FileManager();
        public Quests()
        {
            InitializeComponent();
            sound.PlaySound("NewPage");
            QuestBackground.Visibility = Visibility.Hidden;
            NameQ.Visibility = Visibility.Hidden;
            DescQ.Visibility = Visibility.Hidden;
            GoalQ.Visibility = Visibility.Hidden;
        }
        public Quests(Frame parentFrame, Time time) : this() {
            this.parentFrame = parentFrame;
            this.time = time;
            LoadQuests();
        }
        public void GetInventory(object sender, RoutedEventArgs e) {
            parentFrame.Navigate(new Inventory(parentFrame, false, time));
        }
        public void GetMap(object sender, RoutedEventArgs e) {
            parentFrame.Navigate(new Map(parentFrame, time));
        }
        public void GetJournal(object sender, RoutedEventArgs e) {
            parentFrame.Navigate(new Journal(parentFrame, time));
        }
        public void GetCharacter(object sender, RoutedEventArgs e) {
            parentFrame.Navigate(new Character(parentFrame, time));
        }
        public void GetAlchemy(object sender, RoutedEventArgs e) {
            MessageBox.Show("Musíš začít meditovat, aby jsi mohl použít alchymii");
        }
        public void GetLocation(object sender, RoutedEventArgs e) {
            parentFrame.Navigate(new Location(parentFrame, time));
        }
        public void LoadQuests() {
            List<PlayerQuest> quests = manager.LoadPlayerQuests();
            var matches = quests.Where(s => s.Quest.QuestActive == true);
            foreach (var item in matches) {
                Border border = new Border();
                
                StackPanel stack = new StackPanel();
                stack.Orientation = Orientation.Horizontal;
                

                Image img = new Image();
                if (item.Quest.QuestType == "Primary") {
                    img.Source = new BitmapImage(new Uri(@"img/UI/Primary_quests.png", UriKind.Relative));
                }else {
                    img.Source = new BitmapImage(new Uri(@"img/UI/Secondary_quests.png", UriKind.Relative));
                }
                img.Width = 32;
                img.Height = 32;

                Button but = new Button();
                but.Content = item.Quest.QuestName;
                but.FontSize = 20;
                but.Foreground = Brushes.White;
                but.Width = 300;
                but.Height = 70;
                but.Background = Brushes.Transparent;
                but.BorderBrush = Brushes.Transparent;
                but.Click += new RoutedEventHandler(OpenQuest);
                but.Tag = item.Quest.QuestName;
                stack.Children.Add(img);
                stack.Children.Add(but);
                QuestStack.Children.Add(stack);
            }
        }
        public void OpenQuest(object sender, RoutedEventArgs e) {
            QuestBackground.Visibility = Visibility.Visible;
            NameQ.Visibility = Visibility.Visible;
            DescQ.Visibility = Visibility.Visible;
            GoalQ.Visibility = Visibility.Visible;
            List<PlayerQuest> quests = manager.LoadPlayerQuests();
            Button button = (Button)sender;
            var matches = quests.Where(s => s.Quest.QuestName == button.Tag.ToString());
            foreach (var item in matches ) {
                NameQ.Content = item.Quest.QuestName;
                GoalQ.Text = item.Quest.QuestGoal;
                DescQ.Text = item.Quest.QuestDescription;
            }
        }
    }
}
