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
        
        public int second = 0;
        
        public Time() {
            InitializeComponent();
            Clock.Text = Globals.Hour + ":" + Zero(Globals.Minute);
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
                Globals.Minute++;
                if (Globals.Minute > 59) {
                    Globals.Minute = 0;
                    Globals.Hour++;
                    ResetLoot();
                    if (Globals.Hour > 23) {
                        Globals.Hour = 0;
                    }
                }
            }
            if (Globals.Hour >= 18 || Globals.Hour < 9) {
                music.AmbientMusic(false);
                Globals.daytime = "night";
            }else if(Globals.Hour >= 9 && Globals.Hour < 18) {
                music.AmbientMusic(true);
                Globals.daytime = "day";
            }
            Clock.Text = Globals.Hour + ":" + Zero(Globals.Minute);
            
            
        }
        
        


    }
}
