using System;
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
    /// Interakční logika pro Time.xaml
    /// </summary>
    public partial class Time : UserControl {
        public DispatcherTimer time = new DispatcherTimer();
        
        public Location location = new Location();
        Music music = new Music();
        FileManager manager = new FileManager();
        List<Effect> effect = new List<Effect>();
        public int hour = 8;
        public  int minute = 20;
        public int second = 0;
        
        public Time() {
            InitializeComponent();
            Clock.Text = hour + ":" + minute;
            time.Start();
            
        }
        public void Timer() {
            time.Interval = TimeSpan.FromSeconds(1);
            time.Tick += new EventHandler(Time_tick);
        }
        public void ResetLoot() {
            Globals.LootReset = true;
        }
        public string Zero(int num) {
            if(num < 10) {
                return "0" + num;
            }else {
                return num.ToString();
            }
        }
        void Time_tick(object sender, EventArgs e) {
            second += 20;
            if (second > 59) {
                second = 0;
                minute++;
                if (minute > 59) {
                    minute = 0;
                    hour++;
                    ResetLoot();
                    if (hour > 23) {
                        hour = 0;
                    }
                }
            }
            if (hour >= 18 || hour < 9) {
                music.AmbientMusic(false);
                Globals.daytime = "night";
            }else if(hour >= 9 && hour < 18) {
                music.AmbientMusic(true);
                Globals.daytime = "day";
            }
            Clock.Text = hour + ":" + Zero(minute);
            
        }
        
        


    }
}
