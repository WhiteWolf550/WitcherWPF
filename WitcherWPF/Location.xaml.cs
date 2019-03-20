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
        static public string loc;
        Item it = new Item();
        Music media = new Music();
        Button butclick = new Button();
        
        static Button Loot = new Button();
        string QuestName = null;
        public bool Steps = false;
        public Location() {
            InitializeComponent();
            
            //Globals.location = loc;
            AddHandlers();
            LoadTransition();

        }
        public void PageLoaded(object sender, RoutedEventArgs e) {
            Application.Current.MainWindow.KeyDown += new KeyEventHandler(Keys);
            
        }
        private void Keys(object sender, KeyEventArgs e) {
            if (e.Key == Key.Escape) {
                OpenMenu();
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
            Wyzima_Outside.Přeživší.Click += new RoutedEventHandler(GetDialogue);
            Wyzima_Outside.ToHouse.Click += new RoutedEventHandler(Switch_Click);
            Wyzima_Smith.Outside.Click += new RoutedEventHandler(Switch_Click);
            Wyzima_Smith.Shelf.Click += new RoutedEventHandler(GetLoot);
            Wyzima_Smith.Yaven.Click += new RoutedEventHandler(GetDialogue);
            Wyzima_Smith.FirePlace.Click += new RoutedEventHandler(Meditation);
            Wyzima_House.Shelf.Click += new RoutedEventHandler(GetLoot);
            Wyzima_House.Ghoul.Click += new RoutedEventHandler(EnterCombat1);
            Wyzima_House.Outside.Click += new RoutedEventHandler(Switch_Click);

            Village_Inn.Olaf.Click += new RoutedEventHandler(GetDialogue);
            Village_Inn.Zoltan.Click += new RoutedEventHandler(GetDialogue);
            Village_Inn.DoorO.Click += new RoutedEventHandler(Switch_Click);
            Village_Inn.FirePlace.Click += new RoutedEventHandler(Meditation);

            Village_Outside1.Turman.Click += new RoutedEventHandler(GetDialogue);
            Village_Outside1.Ghoul.Click += new RoutedEventHandler(EnterCombatChQuest);
            Village_Outside1.MasterHunter.Click += new RoutedEventHandler(GetDialogue);
            Village_Outside1.Barell.Click += new RoutedEventHandler(GetLoot);
            Village_Outside1.DoorO.Click += new RoutedEventHandler(Switch_Click);
            Village_Outside1.DoorO2.Click += new RoutedEventHandler(Switch_Click);

            Village_Outside2.Loot.Click += new RoutedEventHandler(GetLoot);
            Village_Outside2.Ghoul.Click += new RoutedEventHandler(EnterCombatChQuest);
            Village_Outside2.Steps.Click += new RoutedEventHandler(Switch_Click);
            Village_Outside2.Steps2.Click += new RoutedEventHandler(Switch_Click);
            Village_Outside2.DoorO1.Click += new RoutedEventHandler(Switch_Click);
            Village_Outside2.Zoltan.Click += new RoutedEventHandler(GetDialogue);

            Village_Outside3.Madman.Click += new RoutedEventHandler(GetDialogue);
            Village_Outside3.Ghoul.Click += new RoutedEventHandler(EnterCombatChQuest);
            Village_Outside3.Loot.Click += new RoutedEventHandler(GetLoot);
            Village_Outside3.Steps.Click += new RoutedEventHandler(Switch_Click);
            Village_Outside3.Steps2.Click += new RoutedEventHandler(Switch_Click);
            Village_Outside3.Zoltan.Click += new RoutedEventHandler(GetDialogue);
            Village_Outside3.Crypt1.Click += new RoutedEventHandler(Switch_Click);

            Village_Outside4.DoorO1.Click += new RoutedEventHandler(Switch_Click);
            Village_Outside4.Steps.Click += new RoutedEventHandler(Switch_Click);

            Village_Outside5.DoorO1.Click += new RoutedEventHandler(Switch_Click);
            Village_Outside5.Steps.Click += new RoutedEventHandler(Switch_Click);

            Village_House.DoorO1.Click += new RoutedEventHandler(Switch_Click);
            Village_House.Zoltan.Click += new RoutedEventHandler(GetDialogue);

            Village_House2.DoorO1.Click += new RoutedEventHandler(Switch_Click);
            Village_House2.Lambert.Click += new RoutedEventHandler(GetDialogue);

            Village_House3.DoorO1.Click += new RoutedEventHandler(Switch_Click);
            Village_House3.Morenn.Click += new RoutedEventHandler(GetDialogue);

            Village_Crypt.Chest.Click += new RoutedEventHandler(GetLoot2);
            Village_Crypt.DoorO1.Click += new RoutedEventHandler(Switch_Click);
        }
        public Location(Frame parentFrame, string location, Time time) : this() {
            this.parentFrame = parentFrame;
            this.time = time;
            loc = location;
            //time.Visibility = Visibility.Hidden;
            //Music();
            time.Visibility = Visibility.Visible;
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
                
            }else if (loc == "Old_wyzima5") {
                Wyzima_House.Visibility = Visibility.Visible;
                
            }else if (loc == "Village_Inn") {
                Village_Inn.Visibility = Visibility.Visible;
                
            }else if (loc == "Village_Outside1") {
                Village_Outside1.Visibility = Visibility.Visible;
            }else if (loc == "Village_Outside2") {
                Village_Outside2.Visibility = Visibility.Visible;

            }else if (loc == "Village_Outside3") {
                Village_Outside3.Visibility = Visibility.Visible;
            }else if (loc == "Village_Outside4") {
                Village_Outside4.Visibility = Visibility.Visible;
            }else if (loc == "Village_Outside5") {
                Village_Outside5.Visibility = Visibility.Visible;
            }else if (loc == "Village_House") {
                Village_House.Visibility = Visibility.Visible;
            }else if (loc == "Village_House2") {
                Village_House2.Visibility = Visibility.Visible;
            }else if (loc == "Village_House3") {
                Village_House3.Visibility = Visibility.Visible;
            }else if (loc == "Village_Crypt") {
                Village_Crypt.Visibility = Visibility.Visible;
            }
        }
        public Location(Frame parentFrame, Time time) : this() {
            this.parentFrame = parentFrame;
            this.time = time;
            loc = Globals.location;
            SetLocation();
        }
        public void GetInventory(object sender, RoutedEventArgs e) {
            Application.Current.MainWindow.KeyDown -= new KeyEventHandler(Keys);
            media.PlaySound("Inventory");
            parentFrame.Navigate(new Inventory(parentFrame, false, time));
        }
        public void GetDialogue(object sender, RoutedEventArgs e) {
            Application.Current.MainWindow.KeyDown -= new KeyEventHandler(Keys);
            Button button = (sender as Button);
            string charworld = button.Name.ToString();
            parentFrame.Navigate(new Dialogue(parentFrame, charworld, time));
             
        }
        public void GetLoot(object sender, RoutedEventArgs e) {
            LootInventory.Children.Clear();
            Button button = (sender as Button);
            
            string loottype = "Loot";
            if (button.Tag != null) {
                loottype = button.Tag.ToString();
            }
            Loot = button;
            it.GenerateLoot(LootInventory, button, LootBack, TakeLoot, CloseBut, loottype);
        }
        public void GetLoot2(object sender, RoutedEventArgs e) {
            LootInventory.Children.Clear();
            Button button = (sender as Button);
            Loot = button;
            it.GenerateSpecificItems(LootInventory, button, LootBack, TakeLoot, CloseBut, button.Tag.ToString());
        }
        public void LootToInventory(object sender, RoutedEventArgs e) {
            it.LootToInventory(LootInventory, TakeLoot, LootBack, CloseBut, QuestPop, QueName, QueGoal);
            if (Loot.Tag != null) {
                ScriptedEvents(Loot.Tag.ToString());
            }else {

            }
            Globals.LootReset = false;
            DisableLoot();
        }
        public void ScriptedEvents(string Event) {
            if (Event == "Strašidelný dům") {
                Wyzima_House.Ghoul.Visibility = Visibility.Visible;
                Wyzima_House.Outside.Visibility = Visibility.Hidden;
                Wyzima_House.Shelf.Visibility = Visibility.Hidden;
            }
        }
        public void DisableLoot() {
            if (loc == "Old_wyzima1") {
                Wyzima_Castle.HideLoot(true);
            }else if (loc == "Old_wyzima4") {
                Wyzima_Smith.HideLoot(true);
            }else if (loc == "Village_Outside1") {
                Village_Outside1.HideLoot(true);
            }else if (loc == "Village_Outside2") {
                Village_Outside2.HideLoot(true);
            }else if (loc == "Village_Outside3") {
                Village_Outside3.HideLoot(true);
            }
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
            Wyzima_House.Visibility = Visibility.Hidden;
            Village_Inn.Visibility = Visibility.Hidden;
            Village_Outside1.Visibility = Visibility.Hidden;
            Village_Outside2.Visibility = Visibility.Hidden;
            Village_Outside3.Visibility = Visibility.Hidden;
            Village_Outside4.Visibility = Visibility.Hidden;
            Village_Outside5.Visibility = Visibility.Hidden;
            Village_House.Visibility = Visibility.Hidden;
            Village_House2.Visibility = Visibility.Hidden;
            Village_House3.Visibility = Visibility.Hidden;
            Village_Crypt.Visibility = Visibility.Hidden;
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
            }else if (loca == "Old_wyzima5") {
                Wyzima_House.Visibility = Visibility.Visible;
            } else if (loca == "Village_Inn") {
                Village_Inn.Visibility = Visibility.Visible;

            }else if (loca == "Village_Outside1") {
                Village_Outside1.Visibility = Visibility.Visible;
            }else if (loca == "Village_Outside2") {
                Village_Outside2.Visibility = Visibility.Visible;

            }else if (loca == "Village_Outside3") {
                Village_Outside3.Visibility = Visibility.Visible;
            } else if (loca == "Village_Outside4") {
                Village_Outside4.Visibility = Visibility.Visible;
            } else if (loca == "Village_Outside5") {
                Village_Outside5.Visibility = Visibility.Visible;
            } else if (loca == "Village_House") {
                Village_House.Visibility = Visibility.Visible;
            } else if (loca == "Village_House2") {
                Village_House2.Visibility = Visibility.Visible;
            } else if (loca == "Village_House3") {
                Village_House3.Visibility = Visibility.Visible;
            } else if (loca == "Village_Crypt") {
                Village_Crypt.Visibility = Visibility.Visible;
            }
        }
        public void Meditation(object sender, RoutedEventArgs e) {
            Application.Current.MainWindow.KeyDown -= new KeyEventHandler(Keys);
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
        private void EnterCombat1(object sender, RoutedEventArgs e) {
            CombatTransitionShow();
        }
        private void EnterCombatChQuest(object sender, RoutedEventArgs e) {
            Button button = (sender as Button);
            QuestName = button.Tag.ToString();
            CombatTransitionShow();
        }
        public void CombatTransitionShow() {
            BlackScreen.Visibility = Visibility.Visible;
            var animation = new DoubleAnimation {
                To = 1,
                BeginTime = TimeSpan.FromSeconds(0),
                Duration = TimeSpan.FromSeconds(1),
                FillBehavior = FillBehavior.Stop
            };

            animation.Completed += (s, a) => BlackScreen.Visibility = Visibility.Visible;
            animation.Completed += (s, a) => BlackScreen.Opacity = 1;
            animation.Completed += new EventHandler(GoToCombat);
            BlackScreen.BeginAnimation(UIElement.OpacityProperty, animation);
        }
        public void GoToCombat(object sender, EventArgs e) {
            Application.Current.MainWindow.KeyDown -= new KeyEventHandler(Keys);
            if (QuestName == null) {
                parentFrame.Navigate(new Combat(parentFrame, false, time, false, Loot.Tag.ToString(), "Ghůl"));
            }else {
                parentFrame.Navigate(new Combat(parentFrame, false, time, false, QuestName, "Ghůl"));
            }
        }
        private void OpenMenu() {
            Application.Current.MainWindow.KeyDown -= new KeyEventHandler(Keys);
            parentFrame.Navigate(new PausePage(parentFrame, time));
        }
        public void LoadTransition() {
            BlackScreen.Visibility = Visibility.Visible;
            BlackScreen.Opacity = 1;
            var animation = new DoubleAnimation {
                To = 0,
                BeginTime = TimeSpan.FromSeconds(1),
                Duration = TimeSpan.FromSeconds(1),
                FillBehavior = FillBehavior.Stop
            };

            animation.Completed += (s, a) => BlackScreen.Visibility = Visibility.Hidden;
            animation.Completed += (s, a) => BlackScreen.Opacity = 0;

            BlackScreen.BeginAnimation(UIElement.OpacityProperty, animation);
        }
    }
}
