﻿using System;
using System.Collections.Generic;
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

namespace WitcherWPF
{
    /// <summary>
    /// Interakční logika pro Village_House.xaml
    /// </summary>
    public partial class Village_House : UserControl
    {
        
        List<PlayerQuest> qq = new List<PlayerQuest>();
        FileManager manager = new FileManager();
        public Village_House()
        {
            InitializeComponent();
            DoorO1.Visibility = Visibility.Hidden;
            Door();
        }
        public void Door() {
            qq = manager.LoadPlayerQuests();
            foreach (PlayerQuest item in qq) {
                if (item.Quest.QuestName == "Záhadná vesnice" && item.Quest.QuestID == 9) {
                    DoorO1.Visibility = Visibility.Visible;
                    Zoltan.Visibility = Visibility.Hidden;

                }
            }
        }
    }
}
