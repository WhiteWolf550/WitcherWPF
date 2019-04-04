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
using System.Windows.Threading;

namespace WitcherWPF
{
    /// <summary>
    /// Interakční logika pro Dialogue.xaml
    /// LAMBERT
    /// FOLTEST
    /// TRISS
    /// 
    /// </summary>
    public partial class Dialogue : Page {
        
        //static string questpath = "@../../saves/quests.json";
        //static string jsonFromQuests = File.ReadAllText(questpath);
        //static List<Quest> Questlist = JsonConvert.DeserializeObject<List<Quest>>(jsonFromQuests, settings);

        private Frame parentFrame;
        private Time time;
        public string Character;
        private bool Forced;
        DispatcherTimer timer = new DispatcherTimer();
        Dialogue df;
        FileManager manager = new FileManager();
        Dialogues dialogues = new Dialogues();
        PlayerInventory inventory = new PlayerInventory();
        List<PlayerInventory> inventoryitems = new List<PlayerInventory>();
        List<Quest> Quests = new List<Quest>();
        List<PlayerQuest> PlayerQuests = new List<PlayerQuest>();
        List<Player> playerlist = new List<Player>();
        bool skip = false;
        int delay = 1000;
        Player player = new Player();
        Music sound = new Music();
        string prolog = Globals.DialoguePath;
        string QuestName = null;
        string Enemy = "";
        string CutsceneName = null;

        public Dialogue() {
            InitializeComponent();
            BlackScreen.Visibility = Visibility.Visible;
            playerlist = manager.LoadPlayer();
            Quests = manager.LoadQuests();
            PlayerQuests = manager.LoadPlayerQuests();
            TravelHide();

        }
        public Dialogue(Frame parentFrame, string Char, Time time, bool Forced) : this() {
            this.parentFrame = parentFrame;
            this.time = time;
            this.Forced = Forced;
            inventoryitems = manager.LoadPlayerInventory();
            Character = Char;
            dialogues.DialogueGreet(PersonName, PersonText, Character);
            DialogueCharacter.Source = new BitmapImage(new Uri(@"img/Characters/" + Character + ".png", UriKind.Relative));
            LoadOptions(false, null);
            CheckShop();

        }
        public void DialogueS(Button button, bool Decision) {

            //dialogues.MainDialogue(PersonName, PersonText, button, QueName, QueGoal, QuestPop, DialogueOptions, Character, parentFrame, time);
            MainDialogue(button, Decision);
            DialogueOptions.Children.Clear();
            //LoadOptions();


        }
        public void PageLoaded(object sender, RoutedEventArgs e) {
            Application.Current.MainWindow.KeyDown += new KeyEventHandler(Keys);
        }
        public void Keys(object sender, KeyEventArgs e) {
            if (e.Key == Key.D) {
                skip = true;
                delay = 0;
            } 
        }

        public void LoadOptions(bool Decision, string choice) {

            List<Dialogues> dialog = manager.LoadDialogue(prolog);
            var matches = dialog.Where(s => s.Dialogue == Character);
            var matches2 = matches.Where(s => s.Type == "Talk");
            var matches3 = matches2.Where(s => s.Enabled == true);
            if (Decision == true) {
                matches3 = matches2.Where(s => s.Enabled == true &&  s.IsChoice == true && s.Choice == choice);
            }
            int i = 0;
            string t = "";
            foreach (var item in matches3) {
                if (t != item.Choice) {
                    Button option = new Button();
                    if (Decision == false) {
                        option.Content = item.Choice;
                        option.Tag = item.Choice;
                    } else {
                        option.Content = item.Decision;
                        option.Tag = item.Choice;
                    }
                    
                    option.FontSize = 20;
                    option.MinHeight = 20;
                    option.HorizontalAlignment = HorizontalAlignment.Center;
                    option.BorderBrush = Brushes.Transparent;
                    option.Background = Brushes.Transparent;
                    option.Foreground = Brushes.WhiteSmoke;
                    option.Click += new RoutedEventHandler(Dialogue_Click);
                    DialogueOptions.Children.Add(option);
                    t = item.Choice;
                }

                i++;
            }
        }
        public void LoadDecisionOptions(string choice) {

            List<Dialogues> dialog = manager.LoadDialogue(prolog);
            var matches = dialog.Where(s => s.Dialogue == Character);
            var matches2 = matches.Where(s => s.Type == "Talk");
            var matches3 = matches2.Where(s => s.Enabled == true && s.IsChoice == true && s.Choice == choice);
            int i = 0;
            string t = "";
            foreach (var item in matches3) {
                
                Button option = new Button();
                option.Content = item.Decision;
                option.Tag = item.Choice;
                option.FontSize = 20;
                option.HorizontalAlignment = HorizontalAlignment.Center;
                option.BorderBrush = Brushes.Transparent;
                option.Background = Brushes.Transparent;
                option.Foreground = Brushes.WhiteSmoke;
                option.Click += new RoutedEventHandler(Dialogue_Click);
                DialogueOptions.Children.Add(option);
                t = item.Choice;
                i++;
            }
        }

        public void Dialogue_Click(object sender, RoutedEventArgs e) {
            DialogueOptions.Visibility = Visibility.Hidden;
            Button button = (Button)sender;
            if (button.Content.ToString() == "Nashle") {
                if (Forced == false) {
                    dialogues.DialogueLeave(PersonName, PersonText, parentFrame, Character, time);
                }else {
                    DialogueOptions.Visibility = Visibility.Visible;
                }

            } else if (button.Content.ToString() == button.Tag.ToString()) {
                DialogueS(button, false);
            }else if (button.Content.ToString() != button.Tag.ToString()) {
                DialogueS(button, true);
            }
            //FoltestDialogue(button);


        }
        public void QuestTimer() {
            timer.Interval = new TimeSpan(0, 0, 0, 0, 10000);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }
        void timer_Tick(object sender, EventArgs e) {
            PlayerQuest hider = new PlayerQuest();
            hider.QuestHide(QuestPop);
            timer.Stop();
        }
        public void TravelHide() {
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
            animation.Completed += new EventHandler(GoToCombat);
            BlackScreen.BeginAnimation(UIElement.OpacityProperty, animation);
        }
        public void TravelCutsceneShow() {
            BlackScreen.Visibility = Visibility.Visible;
            var animation = new DoubleAnimation {
                To = 1,
                BeginTime = TimeSpan.FromSeconds(0),
                Duration = TimeSpan.FromSeconds(1),
                FillBehavior = FillBehavior.Stop
            };

            animation.Completed += (s, a) => BlackScreen.Visibility = Visibility.Visible;
            animation.Completed += (s, a) => BlackScreen.Opacity = 1;
            animation.Completed += new EventHandler(GoToCutscene);
            BlackScreen.BeginAnimation(UIElement.OpacityProperty, animation);
        }
        public void ChangeFrame() {

        }
        public async void MainDialogue(Button button, bool FromDecision) {
            PersonText.Text = "";
            //---------------------VARIABLES---------------------------
            Brush c;
            Quest qst = new Quest();
            Dialogues dlgs = new Dialogues();
            PlayerQuest playerquest = new PlayerQuest();
            string qname = "";
            string qdesc = "";
            
            string diadis = "";
            string Activate = "";
            string Activate2 = "";
            string QuestSound = "QuestUpdate";
            string Decision = null;
            string Choice = null;
            PlayerQuest que = new PlayerQuest();
            int questexp = 0;
            int questreward = 0;

            //---------------------PATHS---------------------------



            //---------------------FILE LOADING---------------------------


            List<Dialogues> dialog = manager.LoadDialogue(Globals.DialoguePath);
            //---------------------FILTERS---------------------------
            var matches = dialog.Where(s => s.Dialogue == Character);
            var matches2 = matches.Where(s => s.Type == "Talk");
            var matches3 = matches2.Where(s => s.Enabled == true);
            var matches4 = matches2.Where(s => s.Choice == button.Content.ToString());
            if (FromDecision == true) {
                matches4 = matches2.Where(s => s.Decision == button.Content.ToString() && s.Choice == button.Tag.ToString());
            }
            //---------------------WRITING DIALOGUE LINES TO USER---------------------------
            int cd = matches4.Count();
            int ic = 0;
            int id2 = 0;
            foreach (var item in matches4) {
                if (FromDecision == false) {
                    if (item.Decision != null) {
                        Decision = item.Decision;
                        Choice = item.Choice;
                        break;
                    }
                }
                delay = 1000;
                PersonText.Text = "";
                string StringToDisplay = item.CharSay;
                char[] CharactersofString = StringToDisplay.ToCharArray();
                PersonName.Content = item.CharName;
                foreach (char f in CharactersofString) {
                    PersonText.Text += f;
                    await Task.Delay(40);
                    if (skip == true) {
                        skip = false;
                        break;                        
                    }
                }
                await Task.Delay(delay);


                diadis = item.Choice;
                dlgs = item;

                if (ic == cd - 2) {
                    Activate2 = item.QuestActivate;


                }
                Activate = item.QuestActivate;
                ic++;
            }
            if (Decision == null) {
                //---------------------IF DIALOGUE HAS QUEST ACTIVATION---------------------------
                if (Activate != null) {

                    var match = PlayerQuests.Where(s => s.Quest.QuestSeries == Activate);
                    if (match.Count() == 0) {

                        c = Brushes.Yellow;
                        var match2 = Quests.Where(s => s.QuestSeries == Activate);

                        var match3 = match2.Where(s => s.QuestID == 1);

                        foreach (var pitem in match3) {
                            qst = pitem;
                            PlayerQuests.Add(new PlayerQuest(pitem));
                            qname = pitem.QuestName;
                            qdesc = pitem.QuestGoal;
                            if (pitem.DialogueActivate != null) {
                                var mat = dialog.Where(s => s.Choice == pitem.DialogueActivate);
                                foreach (var di in mat) {
                                    di.Enabled = true;
                                }
                            }
                        }


                    } else {

                        c = Brushes.Orange;
                        //c = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB8A917"));
                        int id = 0;

                        foreach (var item in match) {
                            id = item.Quest.QuestID;
                        }
                        var matches5 = Quests.Where(s => s.QuestSeries == Activate);
                        var matches6 = matches5.Where(s => s.QuestID == id + 1);
                        foreach (var it in match) {
                            foreach (var it2 in matches6) {
                                it.Quest.QuestID++;
                                it.Quest.QuestDescription = it2.QuestDescription;
                                it.Quest.QuestGoal = it2.QuestGoal;
                                it.Quest.DialogueActivate = it2.DialogueActivate;
                                it.Quest.experience = it2.experience;
                                it.Quest.reward = it2.reward;
                                qname = it2.QuestName;
                                qdesc = it2.QuestGoal;
                                questexp = it.Quest.experience;
                                questreward = it.Quest.reward;
                                playerquest = it;
                                qst = it2;
                                if (it2.DialogueActivate != null) {
                                    var mat = dialog.Where(s => s.Choice == it2.DialogueActivate);
                                    foreach (var di in mat) {
                                        di.Enabled = true;
                                    }
                                }
                            }
                        }

                    }
                    if (Activate2 != Activate) {
                        var matchactiv2 = Quests.Where(s => s.QuestSeries == Activate2);
                        var matchactiv3 = PlayerQuests.Where(s => s.Quest.QuestSeries == Activate2);
                        foreach (var item in matchactiv3) {
                            id2 = item.Quest.QuestID;
                        }
                        if (matchactiv3.Count() > 0) {
                            var matchactiv4 = matchactiv2.Where(s => s.QuestID == id2 + 1);
                            foreach (var it in matchactiv3) {
                                foreach (var it2 in matchactiv4) {
                                    it.Quest.QuestID++;
                                    it.Quest.QuestDescription = it2.QuestDescription;
                                    it.Quest.QuestGoal = it2.QuestGoal;
                                    it.Quest.DialogueActivate = it2.DialogueActivate;

                                    if (it2.DialogueActivate != null) {
                                        var mat = dialog.Where(s => s.Choice == it2.DialogueActivate);
                                        foreach (var di in mat) {
                                            di.Enabled = true;
                                        }
                                    }
                                }
                            }
                        }else {
                            var matchactiv5 = matchactiv2.Where(s => s.QuestSeries == Activate2 && s.QuestID == 1);
                            foreach(Quest item in matchactiv5) {
                                PlayerQuests.Add(new PlayerQuest(item));
                                if (item.DialogueActivate != null) {
                                    var mat = dialog.Where(s => s.Choice == item.DialogueActivate);
                                    foreach (var di in mat) {
                                        di.Enabled = true;
                                    }
                                }
                            }
                        }

                    }

                    var diamatch = dialog.Where(s => s.Choice == diadis);
                    foreach (Dialogues dit in diamatch) {
                        dit.Enabled = false;
                    }

                    
                    if (questexp != 0) {
                        player.AddXP(questexp, playerlist);
                        player.LevelUP(playerlist);
                        PlayerQuests.Remove(playerquest);
                        c = Brushes.Green;
                        QuestSound = "QuestComplete";
                    }
                    if (questreward != 0) {
                        player.AddMoney(questreward, playerlist);

                    }
                    bool isEvent = ScriptedEvents2(dlgs, qst);
                    if (isEvent == false) {
                        QueName.Content = qname;
                        QueGoal.Text = qdesc;
                        
                    }else {
                        QueName.Content = qst.QuestName;
                        QueGoal.Text = qst.QuestGoal;
                        
                    }
                    QueName.Foreground = c;
                    QueGoal.Foreground = c;

                    QuestName = qname;

                    que.QuestShow(QuestPop);
                    sound.PlaySound(QuestSound);
                    await Task.Delay(6000);
                    que.QuestHide(QuestPop);
                    que.QuestSave(PlayerQuests);

                    //saving

                    manager.SavePlayerQuests(PlayerQuests);
                    manager.SavePlayer(playerlist);
                    manager.SaveDialogues(dialog, prolog);
                    Forced = false;
                    ScriptedEvents(dlgs, qst);
                    

                }
                
                LoadOptions(false, null);
                DialogueOptions.Visibility = Visibility.Visible;
                
            } else {
                DialogueOptions.Children.Clear();
                DialogueOptions.Visibility = Visibility.Visible;
                LoadDecisionOptions(Choice);
            } 
            
        }
        private void ScriptedEvents(Dialogues Dialogue, Quest quest) {
            if (Dialogue.Choice == "Problém s příšerou") {
                Enemy = "Ghůl";
                TravelShow();
            }
            if (Dialogue.Choice == "Našel jsem prsten") {
                inventory.DropItem("Zlatý prsten", inventoryitems);
                manager.SavePlayerInventory(inventoryitems);
            }
            if (Dialogue.Choice == "Vyrazit na cestu") {
                TravelCutsceneShow();
            }
            if (Dialogue.Choice == "Co si myslíš, že děláš") {
                Enemy = "Human1";
                CutsceneName = "Chapter1Cut3Begin";
                TravelShow();
            }
            if (Dialogue.Choice == "Zlatý prsten") {
                inventory.AddItem("Rodiný prsten", 1);
            }
            if (Dialogue.Choice == "Mám ten prsten") {
                foreach(PlayerInventory item in inventoryitems) {
                    if (item.Item.Name == "Rodiný prsten") {
                        inventoryitems.Remove(item);
                        break;
                    }
                }
                manager.SavePlayerInventory(inventoryitems);
            }
            if (Dialogue.Choice == "Co tady děláš?") {
                Globals.location = "Novigrad_Outside3";
            }
            if (Dialogue.Choice == "Dejte mi Triss a přežijete") {
                Enemy = "Bolehlav";
                CutsceneName = "Chapter2Cut4";
                TravelShow();
            }
            if (Dialogue.Choice == "Co se stalo?") {
                inventory.DropItem("Magický krystal", inventoryitems);
                manager.SavePlayerInventory(inventoryitems);
            }
            if (Dialogue.Choice == "Jsi v pořádku?") {
                dialogues.DialogueLeave(PersonName, PersonText, parentFrame, Character, time);
            }


        }
        private bool ScriptedEvents2(Dialogues Dialogue, Quest quest) {
            bool go = false;
            if (Dialogue.Choice == "Informace o Lambertovi" && Dialogue.Decision == "Nemám čas na tvoje kraviny") {
                foreach (PlayerQuest item in PlayerQuests) {
                    if (item.Quest.QuestName == quest.QuestName) {
                        item.Quest.QuestDescription = "Starosta byl Geraltovým projevem lehce zastrašen a jsem si skoro jist, že to byl i Geraltův plán, ale jelikož mu starosta odmítl pomoci, tak Geralt musí najít jiný způsob jak Lamberta najít. Zoltan Geraltovi řekl o šílenci ve vesnici, který by mohl mít nějaké informace. Šílenec Geraltovi neporadil.";
                        item.Quest.QuestGoal = "Najdi jinou stopu";
                        quest.QuestGoal = "Najdi jinou stopu";
                        quest.QuestDescription = "Starosta byl Geraltovým projevem lehce zastrašen a jsem si skoro jist, že to byl i Geraltův plán, ale jelikož mu starosta odmítl pomoci, tak Geralt musí najít jiný způsob jak Lamberta najít. Zoltan Geraltovi řekl o šílenci ve vesnici, který by mohl mít nějaké informace. Šílenec Geraltovi neporadil.";
                        go = true;
                    }
                }
            }else {
                go = false;
            }
            if (go == true) {
                return true;
            }else {
                return false;
            }
        }
        private void GoToCombat(object sender, EventArgs e) {
            parentFrame.Navigate(new Combat(parentFrame, false, time, false, QuestName, Enemy, CutsceneName));
        }
        private void GoToCutscene(object sender, EventArgs e) {
            parentFrame.Navigate(new Cutscenes(parentFrame, time, "PrologueCut2"));
        }
        private void CloseLoot(object sender, RoutedEventArgs e) {

        }
        private void OpenShop_Click(object sender, RoutedEventArgs e) {
            OpenShop();
        }
        public void OpenShop() {
            parentFrame.Navigate(new ShopPage(parentFrame, time, Character));
        }
        public void CheckShop() {
            List<Shop> shops = manager.LoadShop();
            foreach(Shop item in shops) {
                if (item.Name == Character) {
                    Shop.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
