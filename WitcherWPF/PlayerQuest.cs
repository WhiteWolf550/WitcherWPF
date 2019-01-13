using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
    }
}
