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
        List<Armor> armors = new List<Armor>();
        List<Sword> swords = new List<Sword>();
        List<Dialogues> dialog = new List<Dialogues>();
        List<Quest> qq = new List<Quest>();
        List<PlayerQuest> qqq = new List<PlayerQuest>();
        static string ipath = @"../../gamefiles/GameItems.json";
        static string apath = @"../../gamefiles/GameArmors.json";
        static string spath = @"../../gamefiles/GameSwords.json";
        static string qpath = @"../../gamefiles/Quests.json";
        static string armorpath = @"../../gamefiles/Armors.json";
        static string swordpath = @"../../gamefiles/Swords.json";
        static string qqpath = @"../../saves/PlayerQuests.json";
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
            CreateDialogue();
            //CreateQuests();
            CreatePlayerQuests();
            //CreateArmors();
            //CreateSwords();
            mainFrame.Navigate(new Inventory(mainFrame));
        }
        public void CreateArmors() {
            armors.Add(new Armor("Středně těžká zbroj", "Mantikoří zbroj", "Kazajka používaná zaklínači ze školy Mantikory", 1, 50, 0, 0, @"img/Armors/Armor_Manticore.png", 200, "Manticore", 10, "Start"));
            armors.Add(new Armor("Středně těžká zbroj", "Mahakamská zbroj", "Zbroj vyrobená trpaslíky z Mahakamu", 5, 50, 0, 0, @"img/Armors/Armor_Manticore.png", 200, "Mahakam", 10, "Shop"));
            armors.Add(new Armor("Těžká zbroj", "Ocelová zbroj", "Ocelová zbroj s pevnou ocelovou hrudí a vyztuženými nárameníky", 10, 100, 2, 0, @"img/Armors/Armor_Manticore.png", 100, "None", 0, "HumanLoot"));

            string jsonToFile = JsonConvert.SerializeObject(armors, settings);
            File.WriteAllText(armorpath, jsonToFile);
        }
        public void CreateSwords() {
            swords.Add(new Sword("Ocelový meč", "Mahakamský sihil", "Meč ukován trpaslíky z té nejlepší oceli až z Mahakamu", 1, 10, 0 , 0, @"img/Swords/Sword_Mahakam_Sihil.png", 200, "Mahakam", 10, "Start"));
            swords.Add(new Sword("Stříbrný meč", "Aerondight", "Ostrý jako břitva, tento meč má svůj vlastní osud, jen čas ukáže jaký", 1, 10, 0, 0, @"img/Swords/Sword_Aerondight.png", 200, null, 0, "Start"));
            string jsonToFile = JsonConvert.SerializeObject(swords, settings);
            File.WriteAllText(swordpath, jsonToFile);
        }
        public void CreateInv() {
            items.Add(new Item("Kuře", "Jídlo,Po snězení doplní malou část zdraví", "Loot", @"img/Items/Food_Chicken.png", "none", "Food", "Sníst", 20));
            items.Add(new Item("Jablečný Džus", "Nápoj, lze vypít pro doplňení malé části zdraví", "Loot", @"img/Items/Drink_Apple_Juice.png", "none", "Drink", "Vypít", 15));
            items.Add(new Item("Fisstech", "Silná droga, lze prodat", "Loot", @"img/Items/Potion_Fisstech.png", "none", "Drug", "Použít", 150));
            items.Add(new Item("Víno", "Alkohol, lze prodat kupcům nebo použít", "Loot", @"img/Items/Alcohol_Winered.png", "none", "Alcohol", "Vypít", 50));
            string jsonToFile = JsonConvert.SerializeObject(items, settings);
            File.WriteAllText(ipath, jsonToFile);
        }
        public void CreateDialogue() {
            //----------------------FOLTEST----------------
            //greet
            dialog.Add(new Dialogues("Foltest", "Vítej zpět Zaklínači", 1, "Pozdrav", "Greet", "Foltest", true, null));

            //1
            dialog.Add(new Dialogues("Foltest", "Geralte, zjistil si už něco o tom vrahovi?",1, "Co potřebujete králi?", "Talk", "Foltest", true, "Něco končí, něco začíná"));
            dialog.Add(new Dialogues("Geralt", "Ne králi, zatím ne", 1, "Co potřebujete králi?", "Talk", "Foltest", true, null));
            dialog.Add(new Dialogues("Foltest", "Tak to by sis měl pospíšit Geralte. Přeci jen jde o tvojí reputaci", 1, "Co potřebujete králi?", "Talk", "Foltest", true, "Něco končí, něco začíná"));
            dialog.Add(new Dialogues("Geralt", "To že se jeden Zaklínač pokusí o zabití krále, ihned neznamená, že takový jsou všichni zaklínači", 1, "Co potřebujete králi?", "Talk", "Foltest", true, "Něco končí, něco začíná"));
            dialog.Add(new Dialogues("Foltest", "Takhle to ale u prostého lidu nefunguje. Lidem stačí, aby se jeden Zaklínač pokusil o vraždu a bude nenávidět všechny", 1, "Co potřebujete králi?", "Talk", "Foltest", true, "Něco končí, něco začíná"));
            dialog.Add(new Dialogues("Foltest", "Potřebuji mít tuto záležitost co nejrychleji zasebou Zaklínači.", 1, "Co potřebujete králi?", "Talk", "Foltest", true, "Něco končí, něco začíná"));
            dialog.Add(new Dialogues("Geralt", "Dobře králi", 1, "Co potřebujete králi?", "Talk", "Foltest", true, "Něco končí, něco začíná"));

            //2
            dialog.Add(new Dialogues("Geralt", "Jak se daří Wyzimě?", 2, "Jak se daří Wyzimě?", "Talk", "Foltest", true, null));
            dialog.Add(new Dialogues("Foltest", "To tě to opravdu zajímá Zaklínači?", 2, "Jak se daří Wyzimě?", "Talk", "Foltest", true, null));
            dialog.Add(new Dialogues("Geralt", "Ne, nezajímá", 2, "Jak se daří Wyzimě?", "Talk", "Foltest", true, null));
            dialog.Add(new Dialogues("Foltest", "Wyzima je pořád v torskách. Budu se ale snažit ji vrátit zpět do své krásy", 2, "Jak se daří Wyzimě?", "Talk", "Foltest", true, null));

            //leave
            dialog.Add(new Dialogues("Geralt", "Nashle králi", 2, "Nashle", "Talk", "Foltest", true, null));
            dialog.Add(new Dialogues("Foltest", "Nashle Zaklínači", 2, "Nashle", "Talk", "Foltest", true, null));
            //----------------------TRISS----------------
            //greet
            dialog.Add(new Dialogues("Triss", "Ano Geralte?", 1, "Pozdrav", "Greet", "Triss", true, null));

            //1
            dialog.Add(new Dialogues("Geralt", "Zjistila jsi něco nového o tom vrahovi?", 1, "Zjistila jsi něco nového o tom vrahovi?", "Talk", "Triss", false, "Kovářova zrůda"));
            dialog.Add(new Dialogues("Triss", "Všechno co vím je že to byl zaklínač, abych zjistila něco více budu potřebovat více času", 1, "Zjistila jsi něco nového o tom vrahovi?", "Talk", "Triss", false, "Kovářova zrůda"));
            dialog.Add(new Dialogues("Geralt", "Já už tu ale déle být nechci Triss. Mám už plné zuby politiky a intriků. Triss pojď semnou, vypadneme odsud", 1, "Zjistila jsi něco nového o tom vrahovi?", "Talk", "Triss", false, "Kovářova zrůda"));
            dialog.Add(new Dialogues("Triss", "Geralte, však víš, že já nemůžu jen tak odejít dokud mě Foltest nepustí.", 1, "Zjistila jsi něco nového o tom vrahovi?", "Talk", "Triss", false, "Kovářova zrůda"));
            dialog.Add(new Dialogues("Geralt", "Ty ani já nic Foltestovi nedlužíme. Jakmile zjistíš něco o tom vrahovi, tak odsud vypadneme a nikdy se nevrátíme", 1, "Zjistila jsi něco nového o tom vrahovi?", "Talk", "Triss", false, "Kovářova zrůda"));
            dialog.Add(new Dialogues("Triss", "Až něco zjistím, tak uvidíme. Mezitím by jsi mohl pomoct místnímu kováři Yavenovi Briggsovi. Toho znáš ne?", 1, "Zjistila jsi něco nového o tom vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná"));
            dialog.Add(new Dialogues("Geralt", "Znám ho potkali jsme se v lese poblíž Brokilonu. To už je tak dlouho, jako kdyby to bylo v minulém životě. Pomůžu mu", 1, "Zjistila jsi něco nového o tom vrahovi?", "Talk", "Triss", false, "Kovářova zrůda"));



            //leave
            dialog.Add(new Dialogues("Geralt", "Měj se Triss", 2, "Nashle", "Talk", "Triss", true, null));
            dialog.Add(new Dialogues("Triss", "Dávej na sebe pozor Geralte", 2, "Nashle", "Talk", "Triss", true, null));
            string jsonToFile = JsonConvert.SerializeObject(dialog, settings);
            File.WriteAllText(prologue, jsonToFile);
        }
        public void CreatePlayer() {
            Sword silver = null;
            Sword steel = null;
            Armor ar = null;
            string playerpath = @"../../saves/Player.json";
            string qsts = @"../../gamefiles/Swords.json";
            string asts = @"../../gamefiles/Armors.json";
            string jsonFromFileinv = File.ReadAllText(qsts);
            string jsonFromFilearmor = File.ReadAllText(asts);
            List<Sword> sword = JsonConvert.DeserializeObject<List<Sword>>(jsonFromFileinv, settings);
            List<Armor> armor = JsonConvert.DeserializeObject<List<Armor>>(jsonFromFilearmor, settings);
            List<Player> player = new List<Player>();
            var matches = sword.Where(s => s.Type == "Stříbrný meč");
            var matches2 = matches.Where(s => s.LootType == "Start");
            var matches3 = sword.Where(s => s.Type == "Ocelový meč");
            var matches4 = matches3.Where(s => s.LootType == "Start");
            var matches5 = armor.Where(s => s.Name == "Mantikoří zbroj");
            var matches6 = matches5.Where(s => s.LootType == "Start");
            foreach(var manticore in matches6) {
                ar = manticore;
            }
            foreach (var steels in matches4) {
                steel = steels;
            }
            foreach (var aerondight in matches2) {
                silver = aerondight;
            } 
            player.Add(new Player(100, 100, 100, 100, 100, 100, 50, 10, 5, 5, steel, silver, ar));
            string jsonToFile = JsonConvert.SerializeObject(player, settings);
            File.WriteAllText(playerpath, jsonToFile);
        }
        public void CreateQuests() {
            qq.Add(new Quest (1, "Primary", "Něco končí, něco začíná", "Foltest si předvolal Geralta hned druhý den potom co krála zachránil.", "Zajdi za Foltestem", 1, true, "Něco končí, něco začíná", null));
            qq.Add(new Quest(2, "Primary", "Něco končí, něco začíná", "Foltest si předvolal Geralta hned druhý den potom co krále zachránil. Foltest Geraltovi oznámil, že by měl něco zjistit o vrahovi s pomocí Triss", "Zajdi za Triss a zjisti něco o vrahovi", 1, true, "Něco končí, něco začíná", "Zjistila jsi něco nového o tom vrahovi?"));
            qq.Add(new Quest(3, "Primary", "Něco končí, něco začíná", "Foltest si předvolal Geralta hned druhý den potom co krále zachránil. Foltest Geraltovi oznámil, že by měl něco zjistit o vrahovi s pomocí Triss. Triss Geraltovi sdělila, že na to, aby zjistila, kdo byl vrah, tak potřebuje více času", "Počkej až se Triss dozví více o vrahovi", 1, true, "Něco končí, něco začíná", null));
            qq.Add(new Quest(1, "Primary", "Kovářova zrůda", "Triss řekla Geraltovi, aby pomohl svému starému známému Yavenovi Briggsovi", "Zajdi za kovářem do staré wyzimy a zjisti jaký má problém", 1, true, "Kovářova zrůda", null));

            string jsonToFilet = JsonConvert.SerializeObject(qq, settings);
            File.WriteAllText(qpath, jsonToFilet);

        }
        public void CreatePlayerQuests() {
            string qsts = @"../../gamefiles/Quests.json";
            string jsonFromFileinv = File.ReadAllText(qsts);
            List<Quest> qust = JsonConvert.DeserializeObject<List<Quest>>(jsonFromFileinv, settings);
            var matches = qust.Where(s => s.QuestID == 1);
            var matches1 = matches.Where(s => s.QuestSeries == "Něco končí, něco začíná");

            foreach (var item in matches1) {
                Quest q = item;
                qqq.Add(new PlayerQuest(q));

            }
            string jsonToFilet = JsonConvert.SerializeObject(qqq, settings);
            File.WriteAllText(qqpath, jsonToFilet);

        }

    }
}
