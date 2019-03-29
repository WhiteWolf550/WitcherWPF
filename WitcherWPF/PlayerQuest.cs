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
        Music sound = new Music();
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
            string prolog = Globals.DialoguePath;
            Player player = new Player();
            List<Quest> Quests = manager.LoadQuests();
            List<Player> playerlist = manager.LoadPlayer();
            List<PlayerQuest> playerQuests = manager.LoadPlayerQuests();
            List<Dialogues> dialogues = manager.LoadDialogue(prolog);
            int id = 0;
            PlayerQuest playerquest = new PlayerQuest();
            int questexp = 0;
            string QuestSound = "QuestUpdate";
            int questreward = 0;
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
                            playerquest = item;
                            QueName.Content = item2.QuestName;
                            QueGoal.Text = item2.QuestGoal;
                            questexp = item2.experience;
                            questreward = item2.reward;

                            if (item2.DialogueActivate != null) {
                                var mat = dialogues.Where(s => s.Choice == item2.DialogueActivate);
                                foreach (var di in mat) {
                                    di.Enabled = true;
                                }
                            }
                        }
                    
                    }
                }
                if (questexp != 0) {
                    player.AddXP(questexp, playerlist);
                    player.LevelUP(playerlist);
                    playerQuests.Remove(playerquest);
                    c = Brushes.Green;
                    QuestSound = "QuestComplete";
                }
                if (questreward != 0) {
                    player.AddMoney(questreward, playerlist);

                }
                manager.SavePlayerQuests(playerQuests);
                manager.SaveDialogues(dialogues, prolog);
                QuestShow(QuestPop);
                sound.PlaySound(QuestSound);
                await Task.Delay(5000);
                QuestHide(QuestPop);
                        

        }
        public void UpdateQuest(string QuestName) {
            Brush c = Brushes.Yellow;
            string prolog = Globals.DialoguePath;
            Player player = new Player();
            List<Quest> Quests = manager.LoadQuests();
            List<Player> playerlist = manager.LoadPlayer();
            List<PlayerQuest> playerQuests = manager.LoadPlayerQuests();
            List<Dialogues> dialogues = manager.LoadDialogue(prolog);
            int id = 0;
            PlayerQuest playerquest = new PlayerQuest();
            int questexp = 0;
            string QuestSound = "QuestUpdate";
            int questreward = 0;
            var matches = playerQuests.Where(s => s.Quest.QuestName == QuestName);
            foreach (PlayerQuest item in matches) {
                id = item.Quest.QuestID;
            }
            var findquest = Quests.Where(s => s.QuestName == QuestName && s.QuestID == id + 1);

            if (matches.Count() == 0) {
                findquest = Quests.Where(s => s.QuestName == QuestName && s.QuestID == 1);
                foreach (Quest item in findquest) {
                    playerQuests.Add(new PlayerQuest(item));
                    
                }
            } else {
                foreach (PlayerQuest item in matches) {
                    foreach (Quest item2 in findquest) {
                        item.Quest.QuestID++;
                        item.Quest.QuestDescription = item2.QuestDescription;
                        item.Quest.QuestGoal = item2.QuestGoal;
                        item.Quest.DialogueActivate = item2.DialogueActivate;
                        playerquest = item;
                        
                        questexp = item2.experience;
                        questreward = item2.reward;

                        if (item2.DialogueActivate != null) {
                            var mat = dialogues.Where(s => s.Choice == item2.DialogueActivate);
                            foreach (var di in mat) {
                                di.Enabled = true;
                            }
                        }
                    }

                }
            }
            if (questexp != 0) {
                player.AddXP(questexp, playerlist);
                player.LevelUP(playerlist);
                playerQuests.Remove(playerquest);
                c = Brushes.Green;
                QuestSound = "QuestComplete";
            }
            if (questreward != 0) {
                player.AddMoney(questreward, playerlist);

            }
            manager.SavePlayerQuests(playerQuests);
            manager.SaveDialogues(dialogues, prolog);
           
            sound.PlaySound(QuestSound);
            


        }
        public void CreatePlayerQuests() {

            List<Quest> qust = manager.LoadQuests();
            List <PlayerQuest> qqq = new List<PlayerQuest>();
            var matches = qust.Where(s => s.QuestID == 1);
            var matches1 = matches.Where(s => s.QuestSeries == "Něco končí, něco začíná");

            foreach (var item in matches1) {
                Quest q = item;
                qqq.Add(new PlayerQuest(q));

            }
            manager.SavePlayerQuests(qqq);
        }
    }
}
