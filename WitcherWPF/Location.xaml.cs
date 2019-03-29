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
        UserControl CurrentLocation = new UserControl();
        List<Dialogues> dialogues = new List<Dialogues>();
        FileManager manager = new FileManager();
        static Button Loot = new Button();
        string QuestName = null;
        string MonsterName = null;
        public bool Steps = false;
        public string CutsceneName = null;
        public Location() {
            InitializeComponent();
            
            //Globals.location = loc;
            
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
        public void AddHandlersWyz1(Old_wyzima1 Wyzima_Castle) {

            Wyzima_Castle.Foltest.Click += new RoutedEventHandler(GetDialogue_Click);
            Wyzima_Castle.FirePlace.Click += new RoutedEventHandler(Meditation);
            Wyzima_Castle.Flower.Click += new RoutedEventHandler(GetLoot);
            Wyzima_Castle.Steps.Click += new RoutedEventHandler(Switch_Click);

        }
        public void AddHandlersWyz2(Old_wyzima2 Wyzima_Castle2) {
            Wyzima_Castle2.Triss.Click += new RoutedEventHandler(GetDialogue_Click);
            Wyzima_Castle2.Door.Click += new RoutedEventHandler(Switch_Click);
            Wyzima_Castle2.Steps.Click += new RoutedEventHandler(Switch_Click);
            
        }
        public void AddHandlersWyz3(Old_wyzima3 Wyzima_Outside) {
            Wyzima_Outside.ToCastle.Click += new RoutedEventHandler(Switch_Click);
            Wyzima_Outside.DoorO.Click += new RoutedEventHandler(Switch_Click);
            Wyzima_Outside.Přeživší.Click += new RoutedEventHandler(GetDialogue_Click);
            Wyzima_Outside.ToHouse.Click += new RoutedEventHandler(Switch_Click);
        }
        public void AddHandlersWyz4(Old_wyzima4 Wyzima_Smith) {
            Wyzima_Smith.Outside.Click += new RoutedEventHandler(Switch_Click);
            Wyzima_Smith.Shelf.Click += new RoutedEventHandler(GetLoot);
            Wyzima_Smith.Yaven.Click += new RoutedEventHandler(GetDialogue_Click);
            Wyzima_Smith.FirePlace.Click += new RoutedEventHandler(Meditation);
        }
        public void AddHandlersWyz5(Old_wyzima5 Wyzima_House) {
            Wyzima_House.Shelf.Click += new RoutedEventHandler(GetLoot);
            Wyzima_House.Ghoul.Click += new RoutedEventHandler(EnterCombat1);
            Wyzima_House.Outside.Click += new RoutedEventHandler(Switch_Click);
        }
        public void AddHandlersVillInn(Village_Inn Village_Inn) {
            Village_Inn.Olaf.Click += new RoutedEventHandler(GetDialogue_Click);
            Village_Inn.Zoltan.Click += new RoutedEventHandler(GetDialogue_Click);
            Village_Inn.DoorO.Click += new RoutedEventHandler(Switch_Click);
            Village_Inn.FirePlace.Click += new RoutedEventHandler(Meditation);
        }
        public void AddHandlersVillO1(Village_Outside1 Village_Outside1) {
            Village_Outside1.Turman.Click += new RoutedEventHandler(GetDialogue_Click);
            Village_Outside1.Ghoul.Click += new RoutedEventHandler(EnterCombatChQuest);
            Village_Outside1.MasterHunter.Click += new RoutedEventHandler(GetDialogue_Click);
            Village_Outside1.Barell.Click += new RoutedEventHandler(GetLoot);
            Village_Outside1.DoorO.Click += new RoutedEventHandler(Switch_Click);
            Village_Outside1.DoorO2.Click += new RoutedEventHandler(Switch_Click);
            Village_Outside1.Trader.Click += new RoutedEventHandler(GetDialogue_Click);
            Village_Outside1.Jarek.Click += new RoutedEventHandler(GetDialogue_Click);
        }
        public void AddHandlersVillO2(Village_Outside2 Village_Outside2) {
            Village_Outside2.Loot.Click += new RoutedEventHandler(GetLoot);
            Village_Outside2.Ghoul.Click += new RoutedEventHandler(EnterCombatChQuest);
            Village_Outside2.Steps.Click += new RoutedEventHandler(Switch_Click);
            Village_Outside2.Steps2.Click += new RoutedEventHandler(Switch_Click);
            Village_Outside2.DoorO1.Click += new RoutedEventHandler(Switch_Click);
            Village_Outside2.Zoltan.Click += new RoutedEventHandler(GetDialogue_Click);
        }
        public void AddHandlersVillO3(Village_Outside3 Village_Outside3) {
            Village_Outside3.Madman.Click += new RoutedEventHandler(GetDialogue_Click);
            Village_Outside3.Ghoul.Click += new RoutedEventHandler(EnterCombatChQuest);
            Village_Outside3.Loot.Click += new RoutedEventHandler(GetLoot);
            Village_Outside3.DoorO1.Click += new RoutedEventHandler(Switch_Click);
            Village_Outside3.Steps.Click += new RoutedEventHandler(Switch_Click);
            Village_Outside3.Steps2.Click += new RoutedEventHandler(Switch_Click);
            Village_Outside3.Zoltan.Click += new RoutedEventHandler(GetDialogue_Click);
            Village_Outside3.Crypt1.Click += new RoutedEventHandler(Switch_Click);
            Village_Outside3.BrickMaker.Click += new RoutedEventHandler(GetDialogue_Click);
        }
        public void AddHandlersVillO4(Village_Outside4 Village_Outside4) {
            Village_Outside4.DoorO1.Click += new RoutedEventHandler(Switch_Click);
            Village_Outside4.Steps.Click += new RoutedEventHandler(Switch_Click);
        }
        public void AddHandlersVillO5(Village_Outside5 Village_Outside5) {
            Village_Outside5.DoorO1.Click += new RoutedEventHandler(Switch_Click);
            Village_Outside5.Steps.Click += new RoutedEventHandler(Switch_Click);
        }
        public void AddHandlersVillHo1(Village_House Village_House) {
            Village_House.DoorO1.Click += new RoutedEventHandler(Switch_Click);
            Village_House.Zoltan.Click += new RoutedEventHandler(GetDialogue_Click);
        }
        public void AddHandlersVillHo2(Village_House2 Village_House2) {
            Village_House2.DoorO1.Click += new RoutedEventHandler(Switch_Click);
            Village_House2.Lambert.Click += new RoutedEventHandler(GetDialogue_Click);
        }
        public void AddHandlersVillHo3(Village_House3 Village_House3) {
            Village_House3.DoorO1.Click += new RoutedEventHandler(EnterCutscene);
            Village_House3.Morenn.Click += new RoutedEventHandler(GetDialogue_Click);
        }
        public void AddHandlersVillCrypt(Village_Crypt Village_Crypt) {
            Village_Crypt.Chest.Click += new RoutedEventHandler(GetLoot2);
            Village_Crypt.DoorO1.Click += new RoutedEventHandler(Switch_Click);
            Village_Crypt.Barghest.Click += new RoutedEventHandler(EnterCombatChQuest);
        }
        public void AddHandlersNovO1(Novigrad_Outside1 Novigrad_Outside1) {
            Novigrad_Outside1.Steps.Click += new RoutedEventHandler(Switch_Click);
            Novigrad_Outside1.Steps2.Click += new RoutedEventHandler(Switch_Click);
        }
        public void AddHandlersNovO2(Novigrad_Outside2 Novigrad_Outside2) {
            Novigrad_Outside2.Steps.Click += new RoutedEventHandler(Switch_Click);
            Novigrad_Outside2.DoorO.Click += new RoutedEventHandler(Switch_Click);
            Novigrad_Outside2.Steps2.Click += new RoutedEventHandler(Switch_Click);
            Novigrad_Outside2.Morenn.Click += new RoutedEventHandler(GetDialogue_Click);
            Novigrad_Outside2.Messenger.Click += new RoutedEventHandler(GetDialogue_Click);
        }
        public void AddHandlersNovO3(Novigrad_Outside3 Novigrad_Outside3) {
            Novigrad_Outside3.Steps.Click += new RoutedEventHandler(Switch_Click);
            Novigrad_Outside3.Steps2.Click += new RoutedEventHandler(Switch_Click);
            Novigrad_Outside3.DoorO.Click += new RoutedEventHandler(Switch_Click);
        }
        public void AddHandlersNovO4(Novigrad_Outside4 Novigrad_Outside4) {
            Novigrad_Outside4.Steps.Click += new RoutedEventHandler(Switch_Click);
            Novigrad_Outside4.Steps2.Click += new RoutedEventHandler(Switch_Click);
            Novigrad_Outside4.Abigail.Click += new RoutedEventHandler(GetDialogue_Click);
        }
        public void AddHandlersNovO5(Novigrad_Outside5 Novigrad_Outside5) {
            Novigrad_Outside5.Steps.Click += new RoutedEventHandler(Switch_Click);
            Novigrad_Outside5.DoorO.Click += new RoutedEventHandler(Switch_Click);
            Novigrad_Outside5.DoorO1.Click += new RoutedEventHandler(Switch_Click);
        }
        public void AddHandlersNovO6(Novigrad_Outside6 Novigrad_Outside6) {
            Novigrad_Outside6.Steps.Click += new RoutedEventHandler(Switch_Click);
            Novigrad_Outside6.DoorO.Click += new RoutedEventHandler(EnterCutscene);
            Novigrad_Outside6.DoorO1.Click += new RoutedEventHandler(Switch_Click);
        }
        public void AddHandlersNovO7(Novigrad_Outside7 Novigrad_Outside7) {
            Novigrad_Outside7.DoorO.Click += new RoutedEventHandler(Switch_Click);
            Novigrad_Outside7.DoorO2.Click += new RoutedEventHandler(Switch_Click);
            Novigrad_Outside7.DoorO1.Click += new RoutedEventHandler(Switch_Click);
        }
        public void AddHandlersNovHo1(Novigrad_House1 Novigrad_House1) {
            Novigrad_House1.Triss.Click += new RoutedEventHandler(GetDialogue_Click);
            Novigrad_House1.Loot.Click += new RoutedEventHandler(GetLoot);
            Novigrad_House1.DoorO.Click += new RoutedEventHandler(Switch_Click);
            Novigrad_House1.FirePlace.Click += new RoutedEventHandler(Meditation);
        }
        public void AddHandlersNovHo2(Novigrad_House2 Novigrad_House2) {
            Novigrad_House2.Bolehlav.Click += new RoutedEventHandler(GetDialogue_Click);
            Novigrad_House2.DoorO.Click += new RoutedEventHandler(Switch_Click);
        }
        public void AddHandlersNovCrypt(Novigrad_Crypt Novigrad_Crypt) {

        }
        public void AddHandlersNovInn(Novigrad_Inn Novigrad_Inn) {
            Novigrad_Inn.Griffarin.Click += new RoutedEventHandler(GetDialogue_Click);
            Novigrad_Inn.Trader2.Click += new RoutedEventHandler(GetDialogue_Click);
            Novigrad_Inn.DoorO.Click += new RoutedEventHandler(Switch_Click);
        }
        public void AddHandlersNovHo4(Novigrad_House4 Nov) {
            Nov.Lizard.Click += new RoutedEventHandler(EnterCombatChQuest);
            Nov.Loot.Click += new RoutedEventHandler(GetLoot);
            Nov.DoorO.Click += new RoutedEventHandler(Switch_Click);
        }
        public void AddHandlersNovCave(Novigrad_Cave Nov) {
            Nov.Bolehlav.Click += new RoutedEventHandler(GetDialogue_Click);
            Nov.FirePlace.Click += new RoutedEventHandler(Meditation);
        }
        public void AddHandlersNovPris(Novigrad_Prison Nov) {
            Nov.Morenn.Click += new RoutedEventHandler(GetDialogue_Click);
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
                Old_wyzima2 wyzima2 = new Old_wyzima2();
                grid.Children.Add(wyzima2);
                AddHandlersWyz2(wyzima2);
                CurrentLocation = wyzima2;
            } else if (loc == "Old_wyzima1") {
                Old_wyzima1 wyzima1 = new Old_wyzima1();
                grid.Children.Add(wyzima1);
                AddHandlersWyz1(wyzima1);
                CurrentLocation = wyzima1;
            } else if (loc == "Old_wyzima3") {
                Old_wyzima3 wyzima3 = new Old_wyzima3();
                grid.Children.Add(wyzima3);
                AddHandlersWyz3(wyzima3);
                CurrentLocation = wyzima3;
            } else if (loc == "Old_wyzima4") {
                Old_wyzima4 wyzima4 = new Old_wyzima4();
                grid.Children.Add(wyzima4);
                AddHandlersWyz4(wyzima4);
                CurrentLocation = wyzima4;
            } else if (loc == "Old_wyzima5") {
                Old_wyzima5 wyzima5 = new Old_wyzima5();
                grid.Children.Add(wyzima5);
                AddHandlersWyz5(wyzima5);
                CurrentLocation = wyzima5;
            } else if (loc == "Village_Inn") {
                Village_Inn inn = new Village_Inn();
                grid.Children.Add(inn);
                AddHandlersVillInn(inn);
                CurrentLocation = inn;
            } else if (loc == "Village_Outside1") {
                Village_Outside1 outside1 = new Village_Outside1();
                grid.Children.Add(outside1);
                AddHandlersVillO1(outside1);
                CurrentLocation = outside1;
            } else if (loc == "Village_Outside2") {
                Village_Outside2 outside2 = new Village_Outside2();
                grid.Children.Add(outside2);
                AddHandlersVillO2(outside2);
                CurrentLocation = outside2;
            } else if (loc == "Village_Outside3") {
                Village_Outside3 outside3 = new Village_Outside3();
                grid.Children.Add(outside3);
                AddHandlersVillO3(outside3);
                CurrentLocation = outside3;
            } else if (loc == "Village_Outside4") {
                Village_Outside4 outside4 = new Village_Outside4();
                grid.Children.Add(outside4);
                AddHandlersVillO4(outside4);
                CurrentLocation = outside4;
            } else if (loc == "Village_Outside5") {
                Village_Outside5 outside5 = new Village_Outside5();
                grid.Children.Add(outside5);
                AddHandlersVillO5(outside5);
                CurrentLocation = outside5;
            } else if (loc == "Village_House") {
                Village_House house1 = new Village_House();
                grid.Children.Add(house1);
                AddHandlersVillHo1(house1);
                CurrentLocation = house1;
            } else if (loc == "Village_House2") {
                Village_House2 house2 = new Village_House2();
                grid.Children.Add(house2);
                AddHandlersVillHo2(house2);
                CurrentLocation = house2;
            } else if (loc == "Village_House3") {
                Village_House3 house3 = new Village_House3();
                grid.Children.Add(house3);
                AddHandlersVillHo3(house3);
                CurrentLocation = house3;
            } else if (loc == "Village_Crypt") {
                Village_Crypt crypt = new Village_Crypt();
                grid.Children.Add(crypt);
                AddHandlersVillCrypt(crypt);
                CurrentLocation = crypt;
            } else if (loc == "Novigrad_Outside1") {
                Novigrad_Outside1 location = new Novigrad_Outside1();
                grid.Children.Add(location);
                AddHandlersNovO1(location);
                CurrentLocation = location;
                //PrisonCheck(location);
            } else if (loc == "Novigrad_Outside2") {
                Novigrad_Outside2 location = new Novigrad_Outside2();
                grid.Children.Add(location);
                AddHandlersNovO2(location);
                CurrentLocation = location;
            } else if (loc == "Novigrad_Outside3") {
                Novigrad_Outside3 location = new Novigrad_Outside3();
                grid.Children.Add(location);
                AddHandlersNovO3(location);
                CurrentLocation = location;
            } else if (loc == "Novigrad_Outside4") {
                Novigrad_Outside4 location = new Novigrad_Outside4();
                grid.Children.Add(location);
                AddHandlersNovO4(location);
                CurrentLocation = location;
            } else if (loc == "Novigrad_Outside5") {
                Novigrad_Outside5 location = new Novigrad_Outside5();
                grid.Children.Add(location);
                AddHandlersNovO5(location);
                CurrentLocation = location;
            } else if (loc == "Novigrad_Outside6") {
                Novigrad_Outside6 location = new Novigrad_Outside6();
                grid.Children.Add(location);
                AddHandlersNovO6(location);
                CurrentLocation = location;
            } else if (loc == "Novigrad_Outside7") {
                Novigrad_Outside7 location = new Novigrad_Outside7();
                grid.Children.Add(location);
                AddHandlersNovO7(location);
                CurrentLocation = location;
            } else if (loc == "Novigrad_Crypt") {
                Novigrad_Crypt location = new Novigrad_Crypt();
                grid.Children.Add(location);
                AddHandlersNovCrypt(location);
                CurrentLocation = location;
            } else if (loc == "Novigrad_Inn") {
                Novigrad_Inn location = new Novigrad_Inn();
                grid.Children.Add(location);
                AddHandlersNovInn(location);
                CurrentLocation = location;
            } else if (loc == "Novigrad_House1") {
                Novigrad_House1 location = new Novigrad_House1();
                grid.Children.Add(location);
                AddHandlersNovHo1(location);
                CurrentLocation = location;
            } else if (loc == "Novigrad_House2") {
                Novigrad_House2 location = new Novigrad_House2();
                grid.Children.Add(location);
                AddHandlersNovHo2(location);
                CurrentLocation = location;
            }else if (loc == "Novigrad_House4") {
                Novigrad_House4 location = new Novigrad_House4();
                grid.Children.Add(location);
                AddHandlersNovHo4(location);
                CurrentLocation = location;
            }else if (loc == "Novigrad_Prison") {
                Novigrad_Prison location = new Novigrad_Prison();
                grid.Children.Add(location);
                AddHandlersNovPris(location);
                CurrentLocation = location;
            }else if (loc == "Novigrad_Cave") {
                Novigrad_Cave location = new Novigrad_Cave();
                grid.Children.Add(location);
                AddHandlersNovCave(location);
                CurrentLocation = location;
            }
        }
        public Location(Frame parentFrame, Time time) : this() {
            this.parentFrame = parentFrame;
            this.time = time;
            loc = Globals.location;
            SetLocation();
        }
        public void PrisonCheck(Novigrad_Outside1 location) {
            if (location.Prison() == 1) {
                ActivateDialogue("Co tady děláš?");
                CutsceneTransitionShow("Chapter2Cut2");
            } else if (location.Prison() == 0) {
                ActivateDialogue("Co tu děláš?");
                GetDialogue("Morenn");
            }
        }
        public void ActivateDialogue(string Choice) {
            dialogues = manager.LoadDialogue(Globals.DialoguePath);
            foreach (Dialogues item in dialogues) {
                if (item.Choice == Choice) {
                    item.Enabled = true;
                }
            }
            manager.SaveDialogues(dialogues, Globals.DialoguePath);
        }
        public void GetInventory(object sender, RoutedEventArgs e) {
            Application.Current.MainWindow.KeyDown -= new KeyEventHandler(Keys);
            media.PlaySound("Inventory");
            parentFrame.Navigate(new Inventory(parentFrame, false, time));
        }
        public void GetDialogue_Click(object sender, RoutedEventArgs e) {
            
            Button button = (sender as Button);
            string charworld = button.Name.ToString();
            GetDialogue(charworld);
             
        }
        public void GetDialogue(string charworld) {
            Application.Current.MainWindow.KeyDown -= new KeyEventHandler(Keys);
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
                
                /*Wyzima_House.Ghoul.Visibility = Visibility.Visible;
                Wyzima_House.Outside.Visibility = Visibility.Hidden;
                Wyzima_House.Shelf.Visibility = Visibility.Hidden;*/
            }
        }
        public void DisableLoot() {
            
            /*if (loc == "Old_wyzima1") {
                

            } else if (loc == "Old_wyzima4") {
                Wyzima_Smith.HideLoot(true);
            } else if (loc == "Village_Outside1") {
                Village_Outside1.HideLoot(true);
            } else if (loc == "Village_Outside2") {
                Village_Outside2.HideLoot(true);
            } else if (loc == "Village_Outside3") {
                Village_Outside3.HideLoot(true);
            } else if (loc == "Novigrad_House1") {
                Novigrad_House1.HideLoot(true);
            }*/
            
        }
        public void CloseLoot(object sender, RoutedEventArgs e) {
            TakeLoot.Visibility = Visibility.Hidden;
            LootInventory.Visibility = Visibility.Hidden;
            LootBack.Visibility = Visibility.Hidden;
            CloseBut.Visibility = Visibility.Hidden;
            //Wyzima_Castle.Flower.Visibility = Visibility.Visible;
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
            
            //HideAll();
            LocationSwitch(butclick.Tag.ToString());
            Globals.location = butclick.Tag.ToString();
            loc = butclick.Tag.ToString();
            
        }
        private void Switch_Click(object sender, RoutedEventArgs e) {
            Button button = (sender as Button);
            if (button.Name == "Steps" || button.Name== "Steps2") {
                media.PlaySound("Steps");
                Steps = true;
            }else {
                Steps = false;
            }
            butclick = button;
            TravelShow();
        }
        
        public void LocationSwitch(string loca) {
            grid.Children.Clear();
            if (loca == "Old_wyzima2") {
                Old_wyzima2 wyzima2 = new Old_wyzima2();
                grid.Children.Add(wyzima2);
                AddHandlersWyz2(wyzima2);
                CurrentLocation = wyzima2;
            } else if (loca == "Old_wyzima1") {
                Old_wyzima1 wyzima1 = new Old_wyzima1();
                grid.Children.Add(wyzima1);
                AddHandlersWyz1(wyzima1);
                CurrentLocation = wyzima1;
            } else if (loca == "Old_wyzima3") {
                Old_wyzima3 wyzima3 = new Old_wyzima3();
                grid.Children.Add(wyzima3);
                AddHandlersWyz3(wyzima3);
                CurrentLocation = wyzima3;
            } else if (loca == "Old_wyzima4") {
                Old_wyzima4 wyzima4 = new Old_wyzima4();
                grid.Children.Add(wyzima4);
                AddHandlersWyz4(wyzima4);
                CurrentLocation = wyzima4;
            } else if (loca == "Old_wyzima5") {
                Old_wyzima5 wyzima5 = new Old_wyzima5();
                grid.Children.Add(wyzima5);
                AddHandlersWyz5(wyzima5);
                CurrentLocation = wyzima5;
            } else if (loca == "Village_Inn") {
                Village_Inn inn = new Village_Inn();
                grid.Children.Add(inn);
                AddHandlersVillInn(inn);
                CurrentLocation = inn;
            } else if (loca == "Village_Outside1") {
                Village_Outside1 outside1 = new Village_Outside1();
                grid.Children.Add(outside1);
                AddHandlersVillO1(outside1);
                CurrentLocation = outside1;
            } else if (loca == "Village_Outside2") {
                Village_Outside2 outside2 = new Village_Outside2();
                grid.Children.Add(outside2);
                AddHandlersVillO2(outside2);
                CurrentLocation = outside2;
            } else if (loca == "Village_Outside3") {
                Village_Outside3 outside3 = new Village_Outside3();
                grid.Children.Add(outside3);
                AddHandlersVillO3(outside3);
                CurrentLocation = outside3;
            } else if (loca == "Village_Outside4") {
                Village_Outside4 outside4 = new Village_Outside4();
                grid.Children.Add(outside4);
                AddHandlersVillO4(outside4);
                CurrentLocation = outside4;
            } else if (loca == "Village_Outside5") {
                Village_Outside5 outside5 = new Village_Outside5();
                grid.Children.Add(outside5);
                AddHandlersVillO5(outside5);
                CurrentLocation = outside5;
            } else if (loca == "Village_House") {
                Village_House house1 = new Village_House();
                grid.Children.Add(house1);
                AddHandlersVillHo1(house1);
                CurrentLocation = house1;
            } else if (loca == "Village_House2") {
                Village_House2 house2 = new Village_House2();
                grid.Children.Add(house2);
                AddHandlersVillHo2(house2);
                CurrentLocation = house2;
            } else if (loca == "Village_House3") {
                Village_House3 house3 = new Village_House3();
                grid.Children.Add(house3);
                AddHandlersVillHo3(house3);
                CurrentLocation = house3;
            } else if (loca == "Village_Crypt") {
                Village_Crypt crypt = new Village_Crypt();
                grid.Children.Add(crypt);
                AddHandlersVillCrypt(crypt);
                CurrentLocation = crypt;
            } else if (loca == "Novigrad_Outside1") {
                Novigrad_Outside1 location = new Novigrad_Outside1();
                grid.Children.Add(location);
                AddHandlersNovO1(location);
                CurrentLocation = location;
                PrisonCheck(location);
                EndCheck(location);
            } else if (loca == "Novigrad_Outside2") {
                Novigrad_Outside2 location = new Novigrad_Outside2();
                grid.Children.Add(location);
                AddHandlersNovO2(location);
                CurrentLocation = location;
            } else if (loca == "Novigrad_Outside3") {
                Novigrad_Outside3 location = new Novigrad_Outside3();
                grid.Children.Add(location);
                AddHandlersNovO3(location);
                CurrentLocation = location;
            } else if (loca == "Novigrad_Outside4") {
                Novigrad_Outside4 location = new Novigrad_Outside4();
                grid.Children.Add(location);
                AddHandlersNovO4(location);
                CurrentLocation = location;
            } else if (loca == "Novigrad_Outside5") {
                Novigrad_Outside5 location = new Novigrad_Outside5();
                grid.Children.Add(location);
                AddHandlersNovO5(location);
                CurrentLocation = location;
            } else if (loca == "Novigrad_Outside6") {
                Novigrad_Outside6 location = new Novigrad_Outside6();
                grid.Children.Add(location);
                AddHandlersNovO6(location);
                CurrentLocation = location;
            } else if (loca == "Novigrad_Outside7") {
                Novigrad_Outside7 location = new Novigrad_Outside7();
                grid.Children.Add(location);
                AddHandlersNovO7(location);
                CurrentLocation = location;
            } else if (loca == "Novigrad_Crypt") {
                Novigrad_Crypt location = new Novigrad_Crypt();
                grid.Children.Add(location);
                AddHandlersNovCrypt(location);
                CurrentLocation = location;
            } else if (loca == "Novigrad_Inn") {
                Novigrad_Inn location = new Novigrad_Inn();
                grid.Children.Add(location);
                AddHandlersNovInn(location);
                CurrentLocation = location;
            } else if (loca == "Novigrad_House1") {
                Novigrad_House1 location = new Novigrad_House1();
                grid.Children.Add(location);
                AddHandlersNovHo1(location);
                CurrentLocation = location;
            } else if (loca == "Novigrad_House2") {
                Novigrad_House2 location = new Novigrad_House2();
                grid.Children.Add(location);
                AddHandlersNovHo2(location);
                CurrentLocation = location;
            } else if (loca == "Novigrad_House4") {
                Novigrad_House4 location = new Novigrad_House4();
                grid.Children.Add(location);
                AddHandlersNovHo4(location);
                CurrentLocation = location;
            } else if (loca == "Novigrad_Prison") {
                Novigrad_Prison location = new Novigrad_Prison();
                grid.Children.Add(location);
                AddHandlersNovPris(location);
                CurrentLocation = location;
            } else if (loca == "Novigrad_Cave") {
                Novigrad_Cave location = new Novigrad_Cave();
                grid.Children.Add(location);
                AddHandlersNovCave(location);
                CurrentLocation = location;
            }
        }
        public void EndCheck(Novigrad_Outside1 location) {
            if (location.End() == 1) {
                CutsceneTransitionShow("Chapter2Cut6");
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
            Button button = (sender as Button);
            //MonsterName = button.Tag.ToString();

            CombatTransitionShow();
        }
        private void EnterCutscene(object sender, RoutedEventArgs e) {
            Button button = (sender as Button);
            CutsceneTransitionShow(null);
            CutsceneName = button.Tag.ToString();
        }
        private void EnterCombatChQuest(object sender, RoutedEventArgs e) {
            Button button = (sender as Button);
            QuestName = button.Tag.ToString();
            MonsterName = button.ToolTip.ToString();
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
            animation.Completed += new EventHandler(GoToCombat_Event);
            BlackScreen.BeginAnimation(UIElement.OpacityProperty, animation);
        }
        public void CutsceneTransitionShow(string CutsceneName) {
            if (CutsceneName != null) {
                this.CutsceneName = CutsceneName;
            }
            BlackScreen.Visibility = Visibility.Visible;
            var animation = new DoubleAnimation {
                To = 1,
                BeginTime = TimeSpan.FromSeconds(0),
                Duration = TimeSpan.FromSeconds(2),
                FillBehavior = FillBehavior.Stop
            };

            animation.Completed += (s, a) => BlackScreen.Visibility = Visibility.Visible;
            animation.Completed += (s, a) => BlackScreen.Opacity = 1;
            animation.Completed += new EventHandler(GoToCutscene);
            BlackScreen.BeginAnimation(UIElement.OpacityProperty, animation);
        }
        private void GoToCutscene(object sender, EventArgs e) {
            parentFrame.Navigate(new Cutscenes(parentFrame, time, CutsceneName));
        }
        public void GoToCombat_Event(object sender, EventArgs e) {
            GoToCombat();
        }
        public void GoToCombat() {
            Application.Current.MainWindow.KeyDown -= new KeyEventHandler(Keys);
            if (QuestName == null) {
                parentFrame.Navigate(new Combat(parentFrame, false, time, false, Loot.Tag.ToString(), MonsterName));
            } else {
                parentFrame.Navigate(new Combat(parentFrame, false, time, false, QuestName, MonsterName));
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
