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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace WitcherWPF {
    /// <summary>
    /// Interakční logika pro Location.xaml
    /// </summary>
    public partial class Location : Page {
        private Frame parentFrame;
        private Time time;
        static public string loc = "Old_wyzima1";
        static public bool LootLock;
        Item it = new Item();
        Music media = new Music();
        Button butclick = new Button();
        public bool Steps = false;
        public Location() {
            InitializeComponent();
            Globals.location = loc;
            AddHandlers();
            if (LootLock == true) {
                Wyzima_Castle.Flower.Visibility = Visibility.Hidden;
            }

        }
        public void AddHandlers() {
            Wyzima_Castle.Foltest.Click += new RoutedEventHandler(GetDialogue);
            Wyzima_Castle.FirePlace.Click += new RoutedEventHandler(Meditation);
            Wyzima_Castle.Flower.Click += new RoutedEventHandler(GetLoot);
            Wyzima_Castle.Steps.Click += new RoutedEventHandler(Switch_Click);
            Wyzima_Castle2.Triss.Click += new RoutedEventHandler(GetDialogue);
            Wyzima_Castle2.Door.Click += new RoutedEventHandler(Switch_Click);
            Wyzima_Castle2.Steps.Click += new RoutedEventHandler(Switch_Click);
            Wyzima_Outside.ToCastle.Click += new RoutedEventHandler(Switch_Click);
            Wyzima_Outside.DoorO.Click += new RoutedEventHandler(Switch_Click);
            Wyzima_Smith.Outside.Click += new RoutedEventHandler(Switch_Click);
            Wyzima_Smith.Shelf.Click += new RoutedEventHandler(GetLoot);
            Wyzima_Smith.Yaven.Click += new RoutedEventHandler(GetDialogue);
            Wyzima_Smith.FirePlace.Click += new RoutedEventHandler(Meditation);
        }
        public Location(Frame parentFrame, string location, Time time) : this() {
            this.parentFrame = parentFrame;
            this.time = time;
            loc = location;
            //time.Visibility = Visibility.Hidden;
            //Music();
            SetLocation();
        }
        public void SetLocation() {
            if (loc == "Old_wyzima2") {
                Wyzima_Castle2.Visibility = Visibility.Visible;
            }else if(loc == "Old_wyzima1") {
                Wyzima_Castle.Visibility = Visibility.Visible;
            }else if (loc == "Old_wyzima3") {
                Wyzima_Outside.Visibility = Visibility.Visible;
            } else if (loc == "Old_wyzima4") {
                Wyzima_Smith.Visibility = Visibility.Visible;
            }
        }
        public Location(Frame parentFrame, Time time) : this() {
            this.parentFrame = parentFrame;
            this.time = time;
            loc = Globals.location;
            SetLocation();
        }
        public void GetInventory(object sender, RoutedEventArgs e) {
            media.PlaySound("Inventory");
            parentFrame.Navigate(new Inventory(parentFrame, false, time));
        }
        public void GetDialogue(object sender, RoutedEventArgs e) {
            Button button = (sender as Button);
            string charworld = button.Name.ToString();
            parentFrame.Navigate(new Dialogue(parentFrame, charworld, time));
             
        }
        public void GetLoot(object sender, RoutedEventArgs e) {
            
            it.GenerateLoot(LootInventory, Wyzima_Castle.Flower, LootBack, TakeLoot, CloseBut, "Loot");
        }
        public void LootToInventory(object sender, RoutedEventArgs e) {
            it.LootToInventory(LootInventory, TakeLoot, LootBack, CloseBut);
            LootLock = true;

        }
        public void CloseLoot(object sender, RoutedEventArgs e) {
            TakeLoot.Visibility = Visibility.Hidden;
            LootInventory.Visibility = Visibility.Hidden;
            LootBack.Visibility = Visibility.Hidden;
            CloseBut.Visibility = Visibility.Hidden;
            Wyzima_Castle.Flower.Visibility = Visibility.Visible;
            LootInventory.Children.Clear();
        }
        public void Music(bool day) {
            if (loc != null) {
                //media.AmbientMusic(day, loc);
            }else {
                //media.AmbientMusic(day, "Old_wyzima1");
            }
        }
        public void BattleMusic() {
            media.BattleMusic();
        }
        public void StopBattleMusic() {
            media.StopBattleMusic();
        }
        public string GetLocation() {
            string location = loc;
            return location;
        }
        
        private void Switch() {

            if (Steps == true) {
                
            }else {
                media.PlaySound("OpenDoor");
            }
            
            HideAll();
            LocationSwitch(butclick.Tag.ToString());
            Globals.location = butclick.Tag.ToString();
            loc = butclick.Tag.ToString();
            
        }
        private void Switch_Click(object sender, RoutedEventArgs e) {
            Button button = (sender as Button);
            if (button.Name == "Steps") {
                media.PlaySound("Steps");
                Steps = true;
            }else {
                Steps = false;
            }
            butclick = button;
            TravelShow();
        }
        private void HideAll() {
            Wyzima_Castle2.Visibility = Visibility.Hidden;
            Wyzima_Castle.Visibility = Visibility.Hidden;
            Wyzima_Outside.Visibility = Visibility.Hidden;
            Wyzima_Smith.Visibility = Visibility.Hidden;
        }
        public void LocationSwitch(string loca) {
            if (loca == "Old_wyzima2") {
                Wyzima_Castle2.Visibility = Visibility.Visible;
            } else if (loca == "Old_wyzima1") {
                Wyzima_Castle.Visibility = Visibility.Visible;
            } else if (loca == "Old_wyzima3") {
                Wyzima_Outside.Visibility = Visibility.Visible;
            }else if (loca == "Old_wyzima4") {
                Wyzima_Smith.Visibility = Visibility.Visible;
            }
        }
        public void Meditation(object sender, RoutedEventArgs e) {
            parentFrame.Navigate(new Alchemy(parentFrame, time, true));
        }
        public void TravelHide(object sender, EventArgs e) {
            var animation = new DoubleAnimation {
                To = 0,
                BeginTime = TimeSpan.FromSeconds(1),
                Duration = TimeSpan.FromSeconds(1),
                FillBehavior = FillBehavior.Stop
            };

            animation.Completed += (s, a) => BlackScreen.Visibility = Visibility.Hidden;
            animation.Completed += (s, a) => BlackScreen.Opacity = 0;

            BlackScreen.BeginAnimation(UIElement.OpacityProperty, animation);
            Switch();
        }
        
        public void TravelShow() {
            BlackScreen.Visibility = Visibility.Visible;
            var animation = new DoubleAnimation {
                To = 1,
                BeginTime = TimeSpan.FromSeconds(0),
                Duration = TimeSpan.FromSeconds(1),
                FillBehavior = FillBehavior.Stop
            };

            animation.Completed += (s, a) => BlackScreen.Visibility = Visibility.Visible;
            animation.Completed += (s, a) => BlackScreen.Opacity = 1;
            animation.Completed += new EventHandler(TravelHide);
            BlackScreen.BeginAnimation(UIElement.OpacityProperty, animation);
        }
    }
}
