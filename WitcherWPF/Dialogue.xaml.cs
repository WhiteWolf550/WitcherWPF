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
    public partial class Dialogue : Page
    {
        static JsonSerializerSettings settings = new JsonSerializerSettings {
            TypeNameHandling = TypeNameHandling.All
        };
        //static string questpath = "@../../saves/quests.json";
        //static string jsonFromQuests = File.ReadAllText(questpath);
        //static List<Quest> Questlist = JsonConvert.DeserializeObject<List<Quest>>(jsonFromQuests, settings);
        static bool FoltestHelp = true;
        bool Begin = true;
        bool First = false;
        bool Second = false;
        bool Third = false;
        bool Fourth = false;
        string Char = "";
        string DialogPart = "";
        private Frame parentFrame;
        DispatcherTimer timer = new DispatcherTimer();
        public Dialogue()
        {
            InitializeComponent();
            Char = "Foltest";
            DialogueOptions.Visibility = Visibility.Hidden;
            Option1.Visibility = Visibility.Hidden;
            Option2.Visibility = Visibility.Hidden;
            Option3.Visibility = Visibility.Hidden;
            Option4.Visibility = Visibility.Hidden;
            Option5.Visibility = Visibility.Hidden;
            FoltestDialogue();
        }
        public Dialogue(Frame parentFrame) : this() {
            this.parentFrame = parentFrame;
        }
        public async void FoltestDialogue() {
            Option1.Visibility = Visibility.Hidden;
            Option2.Visibility = Visibility.Hidden;
            Option3.Visibility = Visibility.Hidden;
            Option4.Visibility = Visibility.Hidden;
            Option5.Visibility = Visibility.Hidden;
            if (Begin == true) {
                PersonName.Content = "Foltest";
                PersonText.Content = "Welcome Geralt";
                await Task.Delay(2000);
                DialogueOptions.Visibility = Visibility.Visible;
                if (FoltestHelp == true) {
                    Option1.Visibility = Visibility.Visible;
                    Option1.Content = "Do you need any help?";
                }
                Option2.Visibility = Visibility.Visible;
                Option2.Content = "What is the situation in the city?";
                Option5.Visibility = Visibility.Visible;
                Option5.Content = "GoodBye";
                First = false;
            }
            if (DialogPart == "Do you need any help?") {
                PersonName.Content = "Foltest";
                PersonText.Content = "Yes Geralt I have a problem that you can take care off";
                await Task.Delay(5000);
                PersonName.Content = "Geralt";
                PersonText.Content = "What do you need sire?";
                await Task.Delay(5000);
                PersonName.Content = "Foltest";
                PersonText.Content = "There is a monster in the city. Will you kill it?";
                await Task.Delay(5000);
                DialogueOptions.Visibility = Visibility.Visible;
                Option1.Content = "Yes";
                Option2.Content = "No, I cannot help you now";
                Option1.Visibility = Visibility.Visible;
                Option2.Visibility = Visibility.Visible;
            }
            if (DialogPart == "Yes") {
                PersonName.Content = "Geralt";
                PersonText.Content = "Yes, I can do that but i need to know more than that";
                await Task.Delay(5000);
                PersonName.Content = "Foltest";
                PersonText.Content = "Well... I do not know much about the monster but a local smith saw it. He can tell you more about it";
                await Task.Delay(5000);
                PersonName.Content = "Geralt";
                PersonText.Content = "Fine, i will go and ask him";
                Quest MonsterHunt = new Quest();
                MonsterHunt.NewQuest("Main","Silent Killer","Foltest asked Geralt to hunt down a monster that was murdering the last citizens that stayed in Wyzima. Geralt Accepted and was told to go to a local smith and ask him about the details","Ask the Wyzima's smith about the monster", 1);
                QueName.Content = "New Quest: Silent Killer";
                QueGoal.Content = "Ask the Wyzima's smith about the monster";
                MonsterHunt.QuestShow(QuestPop); 
                QuestTimer();
                await Task.Delay(5000);
                if (FoltestHelp == true) {
                    Option1.Visibility = Visibility.Visible;
                    Option1.Content = "Do you need any help?";
                }
                Option2.Visibility = Visibility.Visible;
                Option2.Content = "What is the situation in the city?";
                Option5.Visibility = Visibility.Visible;
                Option5.Content = "GoodBye";
            }
            if (DialogPart == "No, I cannot help you now") {
                PersonName.Content = "Geralt";
                PersonText.Content = DialogPart;
                await Task.Delay(5000);
                PersonName.Content = "Foltest";
                PersonText.Content = "Alright Geralt I cannot force you into anything after you saved my life, but please try to kill it. This city has suffered enough";
                await Task.Delay(5000);
                if (FoltestHelp == true) {
                    Option1.Visibility = Visibility.Visible;
                    Option1.Content = "Do you need any help?";
                }
                Option2.Visibility = Visibility.Visible;
                Option2.Content = "What is the situation in the city?";
                Option5.Visibility = Visibility.Visible;
                Option5.Content = "GoodBye";
            }
            if (DialogPart == "What is the situation in the city?") {
                PersonName.Content = "Geralt";
                PersonText.Content = DialogPart;
                await Task.Delay(5000);
                PersonName.Content = "Foltest";
                PersonText.Content = "People are still scared after what happened here";
                await Task.Delay(5000);
                PersonName.Content = "Foltest";
                PersonText.Content = "And Wyzima is completely destoryed. Almost everything has burned down";
                await Task.Delay(5000);
                PersonName.Content = "Foltest";
                PersonText.Content = "Yes.. the situation is very bad, but i will do my best to bring Wyzima back to life";
                await Task.Delay(4000);
                QuestPop.Visibility = Visibility.Hidden;
                if (FoltestHelp == true) {
                    Option1.Visibility = Visibility.Visible;
                    Option1.Content = "Do you need any help?";
                }
                Option2.Visibility = Visibility.Visible;
                Option2.Content = "What is the situation in the city?";
                Option5.Visibility = Visibility.Visible;
                Option5.Content = "GoodBye";
                
            }
            if (DialogPart == "GoodBye") {
                PersonName.Content = "Geralt";
                PersonText.Content = DialogPart;
                await Task.Delay(5000);
                PersonName.Content = "Foltest";
                PersonText.Content = "Farewell Geralt";
                await Task.Delay(5000);
                parentFrame.Navigate(new Location(parentFrame));
            }
        }

        private void Dialogue_Click(object sender, RoutedEventArgs e) {
            Button button = (Button)sender;
            if (sender is Button button1) {
                if (button1.Name == "Option1") {
                    if (Char == "Foltest") {
                        if (Option1.Content.ToString() == "Do you need any help?") {
                            First = true;
                            Begin = false;
                            DialogPart = "Do you need any help?";
                            FoltestDialogue();
                        }
                        if (Option1.Content.ToString() == "Yes") {
                            DialogPart = "Yes";
                            FoltestHelp = false;
                        }
                        Begin = false;
                        FoltestDialogue();
                    }

                }
                else if (button1.Name == "Option2") {
                    if (Char == "Foltest") {
                        if (Option2.Content.ToString() == "No, I cannot help you now") {
                            DialogPart = "No, I cannot help you now";
                        }
                        if (Option2.Content.ToString() == "What is the situation in the city?") {
                            DialogPart = "What is the situation in the city?";

                        }
                        Begin = false;
                        FoltestDialogue();
                    }
                    
                }
                else if(button1.Name == "Option3") {

                }else if(button1.Name == "Option4") {

                }else if(button1.Name == "Option5") {
                    if (Char == "Foltest") {
                        if (Option5.Content.ToString() == "GoodBye") {
                            DialogPart = "GoodBye";
                        }
                        Begin = false;
                        FoltestDialogue();
                    }

                }
            }
        }
        public void QuestTimer() {
            timer.Interval = new TimeSpan(0, 0, 0, 0, 10000);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }
        void timer_Tick(object sender, EventArgs e) {
            Quest hider = new Quest();
            hider.QuestHide(QuestPop);
            timer.Stop();
        }
    }
}
