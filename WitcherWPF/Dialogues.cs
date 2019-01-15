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

        //---------------------DIALOGUE START---------------------------
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
        //---------------------DIALOGUE EXIT---------------------------
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
        //---------------------MAIN DIALOGUE FUNCTION (LOADS DIALOGUE JSON AND WRITES TO USER) ACTIVATES QUESTS BASED ON DIALOGUE---------------------------
        public async void MainDialogue(Label Name, TextBlock Text, Button button, Label QueName, TextBlock QueGoal, StackPanel Pop, StackPanel DialogueOptions, string Character) {
            //---------------------VARIABLES---------------------------
            Brush c;
            string qname = "";
            string qdesc = "";
            string diadis = "";
            string Activate = "";
            string Activate2 = "";
            PlayerQuest que = new PlayerQuest();
            JsonSerializerSettings settings = new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.All
            };
            //---------------------PATHS---------------------------
            string quest = @"../../gamefiles/Quests.json";
            string pquest = @"../../saves/PlayerQuests.json";
            string prolog = @"../../dialogues/DialoguePrologue.json";
            //---------------------JSON READING(QUESTS, PLAYERQUESTS, DIALOGUE)---------------------------
            string jsonFromFileq = File.ReadAllText(quest);
            string jsonFromFileqq = File.ReadAllText(pquest);
            string jsonFromFileinv = File.ReadAllText(prolog);
            //---------------------JSON DESERIALIZE---------------------------
            List<Quest> Quests = JsonConvert.DeserializeObject<List<Quest>>(jsonFromFileq, settings);
            List<PlayerQuest> PlayerQuests = JsonConvert.DeserializeObject<List<PlayerQuest>>(jsonFromFileqq, settings);
            List<Dialogues> dialog = JsonConvert.DeserializeObject<List<Dialogues>>(jsonFromFileinv, settings);
            var matches = dialog.Where(s => s.Dialogue == Character);
            var matches2 = matches.Where(s => s.Type == "Talk");
            var matches3 = matches2.Where(s => s.Enabled == true);
            var matches4 = matches2.Where(s => s.Choice == button.Content.ToString());
            //---------------------WRITING DIALOGUE LINES TO USER---------------------------
            int cd = matches4.Count();
            int ic = 0;
            int id2 = 0;
            foreach (var item in matches4) {

                Name.Content = item.CharName;
                Text.Text = item.CharSay;
                diadis = item.Choice;
                int leng = item.CharSay.Length;
                await Task.Delay(2000);
                if (ic == cd-2) {
                    Activate2 = item.QuestActivate;
                    //DialogueOptions.Visibility = Visibility.Visible;
                    Text.Text = Activate2;
                }
                Activate = item.QuestActivate;
                ic++;
            }
            //---------------------IF QUEST HAS DIALOGUE ACTIVATION---------------------------
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
                        if(pitem.DialogueActivate != null) {
                            var mat = dialog.Where(s => s.Choice == pitem.DialogueActivate);
                            foreach (var di in mat) {
                                di.Enabled = true;
                            }
                        }
                    }
                    
                    
                } else {
                    c = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB8A917"));
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
                            qname = it2.QuestName;
                            qdesc = it2.QuestGoal;
                            if (it2.DialogueActivate != null) {
                                var mat = dialog.Where(s => s.Choice == it2.DialogueActivate);
                                foreach(var di in mat) {
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
                    var matchactiv4 = matchactiv2.Where(s => s.QuestID == id2 + 1);
                    foreach (var it in matchactiv3) {
                        foreach (var it2 in matchactiv4) {
                            it.Quest.QuestID++;
                            it.Quest.QuestDescription = it2.QuestDescription;
                            it.Quest.QuestGoal = it2.QuestGoal;
                            it.Quest.DialogueActivate = it2.DialogueActivate;
                        }
                    }

                }

                var diamatch = dialog.Where(s => s.Choice == diadis);
                foreach(Dialogues dit in diamatch) {
                    dit.Enabled = false;
                }
                QueName.Content = qname;
                QueGoal.Text = qdesc;

                que.QuestShow(Pop);
                await Task.Delay(10000);
                que.QuestHide(Pop);
                que.QuestSave(PlayerQuests);

                //saving
                string jsonToFilet = JsonConvert.SerializeObject(PlayerQuests, settings);
                File.WriteAllText(pquest, jsonToFilet);
                string jsonToFiledia = JsonConvert.SerializeObject(dialog, settings);
                File.WriteAllText(prolog, jsonToFiledia);

            }
            DialogueOptions.Visibility = Visibility.Visible;
            
            
            
            
            


        }
        

    }
    
}
