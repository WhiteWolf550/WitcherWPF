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

        public Dialogues() {

        }
        public Dialogues(string charName, string charSay, int dialogueID, string choice, string type, string dialogue, bool enabled) {
            this.CharName = charName;
            this.CharSay = charSay;
            this.DialogueID = dialogueID;
            this.Choice = choice;
            this.Type = type;
            this.Dialogue = dialogue;
            this.Enabled = enabled;
        }
        public async void DialogueGreet(Label Name, Label Text) {
            JsonSerializerSettings settings = new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.All
            };
            string prolog = @"../../dialogues/DialoguePrologue.json";
            string jsonFromFileinv = File.ReadAllText(prolog);
            List<Dialogues> dialog = JsonConvert.DeserializeObject<List<Dialogues>>(jsonFromFileinv, settings);
            var matches = dialog.Where(s => s.Dialogue == "Foltest");
            var matches2 = matches.Where(s => s.Type == "Greet");
            
            foreach (var item in matches2) {
                Name.Content = item.CharName;
                Text.Content = item.CharSay;
                int leng = item.CharSay.Length;
                await Task.Delay(20000);
                
   
            }
        }
        public async void DialogueLeave(Label Name, Label Text, Frame parentFrame) {
            JsonSerializerSettings settings = new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.All
            };
            string prolog = @"../../dialogues/DialoguePrologue.json";
            string jsonFromFileinv = File.ReadAllText(prolog);
            List<Dialogues> dialog = JsonConvert.DeserializeObject<List<Dialogues>>(jsonFromFileinv, settings);
            var matches = dialog.Where(s => s.Dialogue == "Foltest");
            var matches2 = matches.Where(s => s.Choice == "Nashle");

            foreach (var item in matches2) {
                Name.Content = item.CharName;
                Text.Content = item.CharSay;
                int leng = item.CharSay.Length;
                await Task.Delay(2000);


            }
            parentFrame.Navigate(new Location(parentFrame));
        }
        public async void Foltest(Label Name, Label Text, Button button, Label QueName, Label QueGoal, StackPanel Pop, StackPanel DialogueOptions) {
            Quest que = new Quest();
            JsonSerializerSettings settings = new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.All
            };
            string quest = @"../../saves/Quests.json";
            string jsonFromFileq = File.ReadAllText(quest);
            List<Quest> Quests = JsonConvert.DeserializeObject<List<Quest>>(jsonFromFileq, settings);

            string prolog = @"../../dialogues/DialoguePrologue.json";
            string jsonFromFileinv = File.ReadAllText(prolog);
            List<Dialogues> dialog = JsonConvert.DeserializeObject<List<Dialogues>>(jsonFromFileinv, settings);
            var matches = dialog.Where(s => s.Dialogue == "Foltest");
            var matches2 = matches.Where(s => s.Type == "Talk");
            var matches3 = matches2.Where(s => s.Enabled == true);
            var matches4 = matches2.Where(s => s.Choice == button.Content.ToString());
            string t = "";
            foreach (var item in matches4) {
                Name.Content = item.CharName;
                Text.Content = item.CharSay;
                
                int leng = item.CharSay.Length;
                await Task.Delay(2000);
                t = item.Choice;
            }
            if (t == "Co potřebujete králi?") {
                var matches5 = Quests.Where(s => s.QuestName == "Něco končí, něco začíná");
                foreach (var item1 in matches5) {
                    item1.QuestGoal = "Zajdi za Triss a zjisti něco o vrahovi";
                    item1.QuestDescription += "\n" + "Foltest Geraltovi oznámil, že by měl něco zjistit o vrahovi s pomocí Triss";
                    QueName.Content = item1.QuestName;
                    QueGoal.Content = item1.QuestGoal;
                    
                }
                que.QuestShow(Pop);
                await Task.Delay(10000);
                que.QuestHide(Pop);
                que.QuestSave(Quests);
            }
            DialogueOptions.Visibility = Visibility.Visible;

        }
        
    }
    
}
