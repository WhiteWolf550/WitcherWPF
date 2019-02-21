using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace WitcherWPF {
    class PlayerQuest {
        public Quest Quest { get; set; }

        FileManager manager = new FileManager();
        public PlayerQuest(Quest Quest) {
            this.Quest = Quest;
        }
        public PlayerQuest() {

        }


        public void QuestShow(StackPanel Tran) {
            Tran.Visibility = Visibility.Visible;
            var animation = new DoubleAnimation {
                To = 1,
                BeginTime = TimeSpan.FromSeconds(1),
                Duration = TimeSpan.FromSeconds(2),
                FillBehavior = FillBehavior.Stop
            };

            animation.Completed += (s, a) => Tran.Visibility = Visibility.Visible;
            animation.Completed += (s, a) => Tran.Opacity = 1;
            Tran.BeginAnimation(UIElement.OpacityProperty, animation);
        }
        public void QuestHide(StackPanel Tran) {
            var animation = new DoubleAnimation {
                To = 0,
                BeginTime = TimeSpan.FromSeconds(1),
                Duration = TimeSpan.FromSeconds(2),
                FillBehavior = FillBehavior.Stop
            };

            animation.Completed += (s, a) => Tran.Visibility = Visibility.Hidden;

            Tran.BeginAnimation(UIElement.OpacityProperty, animation);
        }
        public void QuestSave(List<PlayerQuest> quests) {
            string questpath = "@../../../../saves/PlayerQuests.json";
            JsonSerializerSettings settings = new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.All
            };
            string jsonToFile = JsonConvert.SerializeObject(quests, settings);
            File.WriteAllText(questpath, jsonToFile);
        }
        public async void UpdateQuest(string QuestName, StackPanel QuestPop, Label QueName, TextBlock QueGoal) {
            Brush c = Brushes.Yellow;
            string prolog = @"../../dialogues/DialoguePrologue.json";
            List<Quest> Quests = manager.LoadQuests();
            List<PlayerQuest> playerQuests = manager.LoadPlayerQuests();
            List<Dialogues> dialogues = manager.LoadDialogue(prolog);
            int id = 0;
            var matches = playerQuests.Where(s => s.Quest.QuestName == QuestName);
            foreach(PlayerQuest item in matches) {
                id = item.Quest.QuestID;
            }
            var findquest = Quests.Where(s => s.QuestName == QuestName && s.QuestID == id + 1);
            
                if (matches.Count() == 0) {
                findquest = Quests.Where(s => s.QuestName == QuestName && s.QuestID == 1);
                    foreach (Quest item in findquest) {
                        playerQuests.Add(new PlayerQuest(item));
                        QueName.Content = item.QuestName;
                        QueGoal.Text = item.QuestGoal;
                    }
                } else {
                    foreach (PlayerQuest item in matches) {
                        foreach (Quest item2 in findquest) {
                            item.Quest.QuestID++;
                            item.Quest.QuestDescription = item2.QuestDescription;
                            item.Quest.QuestGoal = item2.QuestGoal;
                            item.Quest.DialogueActivate = item2.DialogueActivate;

                            QueName.Content = item2.QuestName;
                            QueGoal.Text = item2.QuestGoal;

                            if (item2.DialogueActivate != null) {
                                var mat = dialogues.Where(s => s.Choice == item2.DialogueActivate);
                                foreach (var di in mat) {
                                    di.Enabled = true;
                                }
                            }
                    }
                    }
                
                QuestShow(QuestPop);
                await Task.Delay(5000);
                QuestHide(QuestPop);
                manager.SavePlayerQuests(playerQuests);
                manager.SaveDialogues(dialogues, prolog);
            }
            

        }
    }
}
