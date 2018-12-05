using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace WitcherWPF {
    class Quest {
        public string QuestType { get; set; }
        public string QuestName { get; set; }
        public string QuestDescription { get; set; }
        public string QuestGoal { get; set; }
        public int QuestLevel { get; set; }

        public void NewQuest(string QuestType, string QuestName, string QuestDescription, string QuestGoal, int QuestLevel) {
            this.QuestType = QuestType;
            this.QuestName = QuestName;
            this.QuestDescription = QuestDescription;
            this.QuestGoal = QuestGoal;
            this.QuestLevel = QuestLevel;

        }
        public void QuestShow(System.Windows.Controls.StackPanel Tran) {
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
        public void QuestHide(System.Windows.Controls.StackPanel Tran) {
            var animation = new DoubleAnimation {
                To = 0,
                BeginTime = TimeSpan.FromSeconds(1),
                Duration = TimeSpan.FromSeconds(2),
                FillBehavior = FillBehavior.Stop
            };

            animation.Completed += (s, a) => Tran.Visibility = Visibility.Hidden;

            Tran.BeginAnimation(UIElement.OpacityProperty, animation);
        }
        public void QuestSave(List<Quest> quests) {
            string questpath = "@../../saves/quests.json";
            JsonSerializerSettings settings = new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.All
            };
            string jsonToFile = JsonConvert.SerializeObject(quests, settings);
            File.WriteAllText(questpath, jsonToFile);
        }


    }
}
