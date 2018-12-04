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

namespace WitcherWPF
{
    /// <summary>
    /// Interakční logika pro Dialogue.xaml
    /// </summary>
    public partial class Dialogue : Page
    {
        bool Begin = true;
        bool First = false;
        bool Second = false;
        bool Third = false;
        bool Fourth = false;

        bool FoltestHelp = false;
        string Char = "";
        string DialogPart = "";
        private Frame parentFrame;
        public Dialogue()
        {
            InitializeComponent();
            Char = "Foltest";
            DialogueOptions.Visibility = Visibility.Hidden;
            Option1.Visibility = Visibility.Hidden;
            Option2.Visibility = Visibility.Hidden;
            Option3.Visibility = Visibility.Hidden;
            Option4.Visibility = Visibility.Hidden;
            Option5.Visibility = Visibility.Hidden;
            FoltestDialogue();
        }
        public Dialogue(Frame parentFrame) : this() {
            this.parentFrame = parentFrame;
        }
        public async void FoltestDialogue() {
            if (Begin == true) {
                PersonName.Content = "Foltest";
                PersonText.Content = "Welcome Geralt";
                await Task.Delay(2000);
                DialogueOptions.Visibility = Visibility.Visible;
                Option1.Visibility = Visibility.Visible;
                Option1.Content = "Do you need any help?";
                Option2.Visibility = Visibility.Visible;
                Option2.Content = "What is the situation in the city?";
                First = false;
            } else if (First == true) {
                PersonName.Content = "Foltest";
                PersonText.Content = "Geralt, I need your help";
                await Task.Delay(5000);
                PersonName.Content = "Geralt";
                PersonText.Content = "What do you need sire?";
                await Task.Delay(5000);
                PersonName.Content = "Foltest";
                PersonText.Content = "There is a monster in the city. Will you kill it?";
                DialogueOptions.Visibility = Visibility.Visible;
            }
        }

        private void Dialogue_Click(object sender, RoutedEventArgs e) {
            Button button = (Button)sender;
            if (sender is Button button1) {
                if (button1.Name == "Option1") {
                    if (Char == "Foltest") {
                        First = true;
                        FoltestDialogue();
                        Option1.Visibility = Visibility.Hidden;
                        Option2.Visibility = Visibility.Hidden;
                        Option3.Visibility = Visibility.Hidden;
                        Option4.Visibility = Visibility.Hidden;
                        Option5.Visibility = Visibility.Hidden;
                    }
                }
                else if (button1.Name == "Option2") {

                }else if(button1.Name == "Option3") {

                }else if(button1.Name == "Option4") {

                }else if(button1.Name == "Option5") {

                }
            }
        }
    }
}
