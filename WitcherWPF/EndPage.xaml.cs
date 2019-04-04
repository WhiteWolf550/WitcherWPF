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

namespace WitcherWPF {
    /// <summary>
    /// Interakční logika pro EndPage.xaml
    /// </summary>
    public partial class EndPage : Page {
        private Frame parentFrame;
        private Time time;
        public EndPage() {
            InitializeComponent();
        }
        public EndPage(Frame parentFrame, Time time) : this() {
            this.parentFrame = parentFrame;
            this.time = time;

            Delay();
            
        }
        public async void Delay() {
            await Task.Delay(5000);
            Globals.Combat = true;
            Globals.Chapter = 4;
            time.time.Stop();
            parentFrame.Navigate(new MainMenu(parentFrame, time));
            
        }
    }
}
