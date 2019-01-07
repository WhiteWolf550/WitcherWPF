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
        List<Item> items = new List<Item>();
        List<Dialogues> dialog = new List<Dialogues>();
        List<Quest> qq = new List<Quest>();
        static string ipath = @"../../saves/GameItems.json";
        static string prologue = @"../../dialogues/DialoguePrologue.json";
        static JsonSerializerSettings settings = new JsonSerializerSettings {
            TypeNameHandling = TypeNameHandling.All
        };
        static string jsonFromFile = File.ReadAllText(ipath);
        //List<Item> items = JsonConvert.DeserializeObject<List<Item>>(jsonFromFile, settings);
        public MainWindow() {
            InitializeComponent();
            //CreateInv();
            //CreatePlayer();
            //CreateDialogue();
            CreateQuests();
            mainFrame.Navigate(new Inventory(mainFrame));
        }
        public void CreateInv() {
            items.Add(new Item("Kuře", "Jídlo,Po snězení doplní malou část zdraví", "Loot", @"img/Items/Food_Chicken.png"));
            items.Add(new Item("Jablečný Džus", "Nápoj, lze vypít pro doplňení malé části zdraví", "Loot", @"img/Items/Drink_Apple_Juice.png"));
            items.Add(new Item("Fisstech", "Silná droga, lze prodat", "Loot", @"img/Items/Potion_Fisstech.png"));
            items.Add(new Item("Víno", "Alkohol, lze prodat kupcům nebo použít", "Loot", @"img/Items/Alcohol_Winered.png"));
            //string jsonToFile = JsonConvert.SerializeObject(items, settings);
            //File.WriteAllText(ipath, jsonToFile);
        }
        public void CreateDialogue() {
            //greet
            dialog.Add(new Dialogues("Foltest", "Vítej zpět Zaklínači", 1, "Pozdrav", "Greet", "Foltest", true));

            dialog.Add(new Dialogues("Foltest", "Geralte, zjistil si už něco o tom vrahovi?",1, "Co potřebujete králi?", "Talk", "Foltest", true));
            dialog.Add(new Dialogues("Geralt", "Ne králi, zatím ne", 1, "Co potřebujete králi?", "Talk", "Foltest", true));
            dialog.Add(new Dialogues("Foltest", "Tak to by sis měl pospíšit Geralte. Přeci jen jde o tvojí reputaci", 1, "Co potřebujete králi?", "Talk", "Foltest", true));
            dialog.Add(new Dialogues("Geralt", "To že se jeden Zaklínač pokusí o zabití krále, ihned neznamená, že takový jsou všichni zaklínači", 1, "Co potřebujete králi?", "Talk", "Foltest", true));
            dialog.Add(new Dialogues("Foltest", "Takhle to ale u prostého lidu nefunguje. Lidem stačí, aby se jeden Zaklínač pokusil o vraždu a bude nenávidět všechny", 1, "Co potřebujete králi?", "Talk", "Foltest", true));
            dialog.Add(new Dialogues("Foltest", "Potřebuji mít tuto záležitost co nejrychleji zasebou Zaklínači.", 1, "Co potřebujete králi?", "Talk", "Foltest", true));
            dialog.Add(new Dialogues("Geralt", "Dobře králi", 1, "Co potřebujete králi?", "Talk", "Foltest", true));

            //2
            dialog.Add(new Dialogues("Geralt", "Jak se daří Wyzimě?", 2, "Jak se daří Wyzimě?", "Talk", "Foltest", true));
            dialog.Add(new Dialogues("Foltest", "To tě to opravdu zajímá Zaklínači?", 2, "Jak se daří Wyzimě?", "Talk", "Foltest", true));
            dialog.Add(new Dialogues("Geralt", "Ne, nezajímá", 2, "Jak se daří Wyzimě?", "Talk", "Foltest", true));
            dialog.Add(new Dialogues("Foltest", "Wyzima je pořád v torskách. Budu se ale snažit ji vrátit zpět do své krásy", 2, "Jak se daří Wyzimě?", "Talk", "Foltest", true));

            //leave
            dialog.Add(new Dialogues("Geralt", "Nashle králi", 2, "Exit", "Talk", "Foltest", true));
            dialog.Add(new Dialogues("Geralt", "Nashle Zaklínači", 2, "Exit", "Talk", "Foltest", true));
            string jsonToFile = JsonConvert.SerializeObject(dialog, settings);
            File.WriteAllText(prologue, jsonToFile);
        }
        public void CreatePlayer() {
            string playerpath = @"../../saves/Player.json";
            List<Player> player = new List<Player>();
            player.Add(new Player(100, 100, 100, 100, 100, 100, 50, 10, 5, 5));
            string jsonToFile = JsonConvert.SerializeObject(player, settings);
            File.WriteAllText(playerpath, jsonToFile);
        }
        public void CreateQuests() {
            string playerpath = @"../../saves/Quests.json";
            qq.Add(new Quest("Primary", "Něco končí, něco začíná", "Foltest si předvolal Geralta hned druhý den potom co krála zaachránil.", "Zajdi za Foltestem", 1, true));
            Quest q = new Quest();
            q.QuestSave(qq);

        }
    }
}
