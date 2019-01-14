using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace WitcherWPF
{
    class Dialogues
    {
        public string CharName { get; set; }
        public string CharSay { get; set; }
        public int DialogueID { get; set; }
        public string Choice { get; set; }
        public string Type { get; set; }
        public string Dialogue { get; set; }
        public bool Enabled { get; set; }
        public string QuestActivate { get; set; }

        public Dialogues() {

        }
        public Dialogues(string charName, string charSay, int dialogueID, string choice, string type, string dialogue, bool enabled, string QuestActivate) {
            this.CharName = charName;
            this.CharSay = charSay;
            this.DialogueID = dialogueID;
            this.Choice = choice;
            this.Type = type;
            this.Dialogue = dialogue;
            this.Enabled = enabled;
            this.QuestActivate = QuestActivate;
        }
        public async void DialogueGreet(Label Name, TextBlock Text, string Character) {
            JsonSerializerSettings settings = new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.All
            };
            string prolog = @"../../dialogues/DialoguePrologue.json";
            string jsonFromFileinv = File.ReadAllText(prolog);
            List<Dialogues> dialog = JsonConvert.DeserializeObject<List<Dialogues>>(jsonFromFileinv, settings);
            var matches = dialog.Where(s => s.Dialogue == Character);
            var matches2 = matches.Where(s => s.Type == "Greet");
            
            foreach (var item in matches2) {
                Name.Content = item.CharName;
                Text.Text = item.CharSay;
                int leng = item.CharSay.Length;
                await Task.Delay(20000);
                
   
            }
        }
        public async void DialogueLeave(Label Name, TextBlock Text, Frame parentFrame, string Character) {
            JsonSerializerSettings settings = new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.All
            };
            string prolog = @"../../dialogues/DialoguePrologue.json";
            string jsonFromFileinv = File.ReadAllText(prolog);
            List<Dialogues> dialog = JsonConvert.DeserializeObject<List<Dialogues>>(jsonFromFileinv, settings);
            var matches = dialog.Where(s => s.Dialogue == Character);
            var matches2 = matches.Where(s => s.Choice == "Nashle");

            foreach (var item in matches2) {
                Name.Content = item.CharName;
                Text.Text = item.CharSay;
                int leng = item.CharSay.Length;
                await Task.Delay(2000);


            }
            parentFrame.Navigate(new Location(parentFrame));
        }
        public async void Foltest(Label Name, TextBlock Text, Button button, Label QueName, Label QueGoal, StackPanel Pop, StackPanel DialogueOptions, string Character) {
            Brush c;
            string qname = "";
            string qdesc = "";
            PlayerQuest que = new PlayerQuest();
            JsonSerializerSettings settings = new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.All
            };
            string quest = @"../../gamefiles/Quests.json";
            string pquest = @"../../saves/PlayerQuests.json";
            string jsonFromFileq = File.ReadAllText(quest);
            List<Quest> Quests = JsonConvert.DeserializeObject<List<Quest>>(jsonFromFileq, settings);
            string jsonFromFileqq = File.ReadAllText(pquest);
            List<PlayerQuest> PlayerQuests = JsonConvert.DeserializeObject<List<PlayerQuest>>(jsonFromFileqq, settings);

            string prolog = @"../../dialogues/DialoguePrologue.json";
            string jsonFromFileinv = File.ReadAllText(prolog);
            List<Dialogues> dialog = JsonConvert.DeserializeObject<List<Dialogues>>(jsonFromFileinv, settings);
            var matches = dialog.Where(s => s.Dialogue == Character);
            var matches2 = matches.Where(s => s.Type == "Talk");
            var matches3 = matches2.Where(s => s.Enabled == true);
            var matches4 = matches2.Where(s => s.Choice == button.Content.ToString());
            string Activate = "";
            foreach (var item in matches4) {
                Name.Content = item.CharName;
                Text.Text = item.CharSay;
                
                int leng = item.CharSay.Length;
                await Task.Delay(2000);
                Activate = item.QuestActivate;
            }
            if (Activate != null) {
                var match = PlayerQuests.Where(s => s.Quest.QuestSeries == Activate);
                if (match.Count() == 0) {
                    c = Brushes.Yellow;
                    var match2 = Quests.Where(s => s.QuestSeries == Activate);
                    var match3 = match2.Where(s => s.QuestID == 1);
                    
                    foreach(var pitem in match3) {
                        PlayerQuests.Add(new PlayerQuest(pitem));
                        qname = pitem.QuestName;
                        qdesc = pitem.QuestGoal;
                    }
                } else {
                    c = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#DDE82323")); ;
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
                            qname = it2.QuestName;
                            qdesc = it2.QuestGoal;
                        }
                    }
                }

                QueName.Content = qname;
                QueGoal.Content = qdesc;

                que.QuestShow(Pop);
                await Task.Delay(10000);
                que.QuestHide(Pop);
                que.QuestSave(PlayerQuests);
            }
            DialogueOptions.Visibility = Visibility.Visible;


        }
        
    }
    
}
