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

namespace WitcherWPF {
    /// <summary>
    /// Interakční logika pro Novigrad_Outside6.xaml
    /// </summary>
    public partial class Novigrad_Outside6 : UserControl {
        public DispatcherTimer time = new DispatcherTimer();
        List<PlayerQuest> pquest = new List<PlayerQuest>();
        FileManager manager = new FileManager();
        public Novigrad_Outside6() {
            DoorO.Visibility = Visibility.Hidden;
            DoorShow();
            InitializeComponent();
            LoadBackground();
        }
        public void LoadBackground() {

            if (Globals.daytime == "night") {
                LocationImage.Source = new BitmapImage(new Uri("img/Locations/Novigrad_Outside6_night.png", UriKind.Relative));
            } else if (Globals.daytime == "day") {
                LocationImage.Source = new BitmapImage(new Uri("img/Locations/Novigrad_Outside6_day.png", UriKind.Relative));
            }

        }
        public void Timer() {
            time.Interval = TimeSpan.FromSeconds(1);
            time.Tick += new EventHandler(Time_tick);

        }
        void Time_tick(object sender, EventArgs e) {
            LoadBackground();

        }
        public void DoorShow() {
            pquest = manager.LoadPlayerQuests();
            foreach (PlayerQuest item in pquest) {
                if (item.Quest.QuestName == "Tajná organizace" && item.Quest.QuestID == 4) {
                    DoorO.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
