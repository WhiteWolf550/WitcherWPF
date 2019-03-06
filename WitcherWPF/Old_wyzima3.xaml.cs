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
using System.Windows.Threading;

namespace WitcherWPF
{
    /// <summary>
    /// Interakční logika pro Old_wyzima3.xaml
    /// </summary>
    public partial class Old_wyzima3 : UserControl
    {
        public DispatcherTimer time = new DispatcherTimer();
        List<PlayerQuest> qq = new List<PlayerQuest>();
        FileManager manager = new FileManager();
        public bool Looted = false;
        public Old_wyzima3()
        {
            InitializeComponent();
            LoadBackground();
            QuestDoor();
            Timer();
            time.Start();
           
        }
        public void LoadBackground() {
           
            if (Globals.daytime == "night") {
                LocationImage.Source = new BitmapImage(new Uri("img/Locations/Old_wyzima3_night.png", UriKind.Relative));
            } else if (Globals.daytime == "day") {
                LocationImage.Source = new BitmapImage(new Uri("img/Locations/Old_wyzima3_day.png", UriKind.Relative));
            }
            
        }
        public void Timer() {
            time.Interval = TimeSpan.FromSeconds(1);
            time.Tick += new EventHandler(Time_tick);
            
        }
        void Time_tick(object sender, EventArgs e) {
            LoadBackground();
            
        }
        public void QuestDoor() {
            qq = manager.LoadPlayerQuests();
            foreach(PlayerQuest item in qq) {
                if (item.Quest.QuestName == "Strašidelný dům" && item.Quest.QuestID == 1) {
                    ToHouse.Visibility = Visibility.Visible;
                }
            }
        }

    }
}
