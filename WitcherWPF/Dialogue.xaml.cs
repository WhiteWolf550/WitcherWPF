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
        static string prolog = @"../../dialogues/DialoguePrologue.json";
        JsonSerializerSettings settings = new JsonSerializerSettings {
            TypeNameHandling = TypeNameHandling.All
        };
        //static string questpath = "@../../saves/quests.json";
        //static string jsonFromQuests = File.ReadAllText(questpath);
        //static List<Quest> Questlist = JsonConvert.DeserializeObject<List<Quest>>(jsonFromQuests, settings);

        private Frame parentFrame;
        public string Character;
        DispatcherTimer timer = new DispatcherTimer();
        string jsonFromFileinv = File.ReadAllText(prolog);
        Dialogue df;
        Dialogues dialogues = new Dialogues();

        public Dialogue()
        {
            InitializeComponent();
            
            
        }
        public Dialogue(Frame parentFrame, string Char) : this() {
            this.parentFrame = parentFrame;
            Character = Char;
            dialogues.DialogueGreet(PersonName, PersonText, Character);
            DialogueCharacter.Source = new BitmapImage(new Uri(@"img/Characters/" + Character + ".png", UriKind.Relative));
            LoadOptions();

        }
        public void FoltestDialogue(Button button) {

            dialogues.MainDialogue(PersonName, PersonText, button, QueName, QueGoal, QuestPop, DialogueOptions, Character);
            DialogueOptions.Children.Clear();
            
   
        }
        public void OptionsLoader() {

            LoadOptions();
        }
        void DialogueLoader(Button button) {
            

        }
        
        public void LoadOptions() {
            
            string jsonFromFileinv = File.ReadAllText(prolog);
            List<Dialogues> dialog = JsonConvert.DeserializeObject<List<Dialogues>>(jsonFromFileinv, settings);
            var matches = dialog.Where(s => s.Dialogue == Character);
            var matches2 = matches.Where(s => s.Type == "Talk");
            var matches3 = matches2.Where(s => s.Enabled == true);
            int i = 0;
            string t = "";
            foreach (var item in matches3) {
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
                    option.ToolTip = item.Enabled;
                    t = item.Choice;
                }

                i++;
            }
            
            
        }

        public void Dialogue_Click(object sender, RoutedEventArgs e) {
            DialogueOptions.Visibility = Visibility.Hidden;
            Button button = (Button)sender;
            if (button.Content.ToString() == "Nashle") {
                dialogues.DialogueLeave(PersonName, PersonText, parentFrame, Character);
                
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
            PlayerQuest hider = new PlayerQuest();
            hider.QuestHide(QuestPop);
            timer.Stop();
        }
    }
}
