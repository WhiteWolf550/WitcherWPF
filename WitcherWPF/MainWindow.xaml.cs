using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {


        Dialogues dia = new Dialogues();
        Quest q = new Quest();
        Crypt crypt = new Crypt();
        Item item = new Item();
        Potion potion = new Potion();

        public MainWindow() {
            
            InitializeComponent();
            //dia.CreateDialogues();
            //q.CreateQuests();
            item.CreateItems();
            

            
            time.Visibility = Visibility.Hidden;
            Globals.Combat = true;
            
            mainFrame.Navigate(new MainMenu(mainFrame, time));
        }
        
        

    }
}
