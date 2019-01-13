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
    class Quest {
        public int QuestID { get; set; }
        public string QuestType { get; set; }
        public string QuestName { get; set; }
        public string QuestDescription { get; set; }
        public string QuestGoal { get; set; }
        public int QuestLevel { get; set; }
        public bool QuestActive { get; set; }
        public string QuestSeries { get; set; }

        public Quest() {

        }
        public Quest(int QuestID, string QuestType, string QuestName, string QuestDescription, string QuestGoal, int QuestLevel, bool QuestActive, string QuestSeries) {
            this.QuestID = QuestID;
            this.QuestType = QuestType;
            this.QuestName = QuestName;
            this.QuestDescription = QuestDescription;
            this.QuestGoal = QuestGoal;
            this.QuestLevel = QuestLevel;
            this.QuestActive = QuestActive;
            this.QuestSeries = QuestSeries;

        }
        


    }
}
