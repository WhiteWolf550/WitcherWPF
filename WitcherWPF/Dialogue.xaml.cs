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
        static string questpath = @"../../saves/Quests.json";
        private Frame parentFrame;
        DispatcherTimer timer = new DispatcherTimer();
        static string prolog = @"../../dialogues/DialoguePrologue.json";
        static string jsonFromFileinv = File.ReadAllText(prolog);
        List<Dialogues> dialog = JsonConvert.DeserializeObject<List<Dialogues>>(jsonFromFileinv, settings);
        Dialogues dialogues = new Dialogues();

        public Dialogue()
        {
            InitializeComponent();
            
            
        }
        public Dialogue(Frame parentFrame) : this() {
            this.parentFrame = parentFrame;
            dialogues.DialogueGreet(PersonName, PersonText);
            LoadOptions();

        }
        public void FoltestDialogue(Button button) {
            dialogues.Foltest(PersonName, PersonText, button, QueName, QueGoal, QuestPop, DialogueOptions);
            
        }
        public void LoadOptions() {
            var matches = dialog.Where(s => s.Dialogue == "Foltest");
            var matches2 = matches.Where(s => s.Type == "Talk");
            int i = 0;
            string t = "";
            foreach (var item in matches2) {
                if (t != item.Choice) {
                    Button option = new Button();
                    option.Content = item.Choice;
                    option.Tag = item.Dialogue;
                    option.FontSize = 20;
                    option.HorizontalAlignment = HorizontalAlignment.Center;
                    option.BorderBrush = Brushes.Transparent;
                    option.Background = Brushes.Transparent;
                    option.Foreground = Brushes.Black;
                    option.Click += new RoutedEventHandler(Dialogue_Click);
                    DialogueOptions.Children.Add(option);
                    t = item.Choice;
                }

                i++;
            }
            
            //FoltestDialogue();
        }

        private void Dialogue_Click(object sender, RoutedEventArgs e) {
            DialogueOptions.Visibility = Visibility.Hidden;
            Button button = (Button)sender;
            if (button.Content.ToString() == "Nashle") {
                dialogues.DialogueLeave(PersonName, PersonText, parentFrame);
                
            }
            else {
                FoltestDialogue(button);
            }
            //FoltestDialogue(button);
            

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
