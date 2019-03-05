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
        
        
        Music sound = new Music();
        List<PlayerQuest> qqq = new List<PlayerQuest>();
        FileManager manager = new FileManager();
        Player player = new Player();
        Quest quest = new Quest();
        Dialogues dialogue = new Dialogues();
        Sword sword = new Sword();
        Armor armor = new Armor();
        Skills skills = new Skills();
        PlayerQuest pquest = new PlayerQuest();
        Item items = new Item();
        Potion potions = new Potion();
        Characters characters = new Characters();
        Shop shop = new Shop();
        
        public MainWindow() {
            
            InitializeComponent();
            

            //player.CreateDefaultPlayer();
            quest.CreateQuests();
            dialogue.CreateDialogues();
            //sword.CreateSwords();
            //armor.CreateArmor();
            //skills.CreateSkills();
            pquest.CreatePlayerQuests();
            items.CreateItems();
            //shop.CreateShops();

            //shop.CreateShops();
            //potions.CreatePotions();
            //characters.CreateCharacters();

            
            time.Visibility = Visibility.Hidden;
            Globals.Combat = true;
            Globals.location = "Old_wyzima1";
            mainFrame.Navigate(new MainMenu(mainFrame, time));
        }
        
        

    }
}
