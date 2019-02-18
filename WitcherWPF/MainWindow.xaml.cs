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
        List<Sword> playerswords = new List<Sword>();
        List<Armor> playeramors = new List<Armor>();
        List<Sign> signs = new List<Sign>();
        List<Dialogues> dialog = new List<Dialogues>();
        List<Quest> qq = new List<Quest>();
        List<Skills> skills = new List<Skills>();
        private MediaPlayer mediaPlayer = new MediaPlayer();
        Uri uri = new Uri((@"../../sounds/music/The_Order.mp3"), UriKind.Relative);
        List<PlayerQuest> qqq = new List<PlayerQuest>();
        FileManager manager = new FileManager();
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
            mediaPlayer.Open(uri);
            time.Timer();
            //mediaPlayer.Play();
            //CreateInv();
            CreateDialogue();
            //CreateQuests();
            CreatePlayerQuests();
            CreateSkills();
            CreateArmors();
            CreateSwords();
            CreatePlayer();

            mainFrame.Navigate(new Inventory(mainFrame, false, time));
        }
        public void CreateSkills() {
            //----------------------AARD-------------------------------------------------------
            skills.Add(new Skills(1, "Aard","AardSkill1", 10, 0, 15, 10, 3, 10, 0, 0, true, true));
            skills.Add(new Skills(2, "Aard", "AardSkill2", 10, 0, 15, 10, 3, 10, 0, 1, true, false));
            skills.Add(new Skills(3, "Aard", "AardSkill3", 10, 0, 15, 15, 3, 10, 0, 2, false, false));
            skills.Add(new Skills(4, "Aard", "AardSkill4", 30, 0, 15, 15, 3, 10, 0, 3, false, true));
            skills.Add(new Skills(5, "Aard", "AardSkill5", 30, 0, 15, 15, 3, 30, 0, 4, false, false));
            skills.Add(new Skills(6, "Aard", "AardSkill6", 30, 0, 15, 25, 3, 30, 0, 5, false, false));
            skills.Add(new Skills(7, "Aard", "AardSkill7", 30, 0, 15, 25, 3, 30, 0, 6, false, false));
            skills.Add(new Skills(8, "Aard", "AardSkill8", 50, 0, 20, 25, 3, 30, 0, 7, false, true));
            skills.Add(new Skills(9, "Aard", "AardSkill9", 50, 0, 20, 25, 3, 30, 0, 8, false, false));
            skills.Add(new Skills(10, "Aard", "AardSkill10", 50, 0, 20, 25, 5, 30, 0, 9, false, false));
            skills.Add(new Skills(11, "Aard", "AardSkill11", 50, 0, 20, 30, 5, 30, 0, 10, false, false));
            skills.Add(new Skills(12, "Aard", "AardSkill12", 80, 0, 20, 30, 5, 30, 0, 11, false, true));
            skills.Add(new Skills(13, "Aard", "AardSkill13", 80, 0, 20, 30, 5, 30, 0, 12, false, false));
            skills.Add(new Skills(14, "Aard", "AardSkill14", 80, 1, 20, 30, 5, 30, 0, 13, false, false));
            skills.Add(new Skills(15, "Aard", "AardSkill15", 80, 1, 20, 30, 8, 30, 0, 14, false, false));
            skills.Add(new Skills(16, "Aard", "AardSkill16", 100, 1, 25, 30, 8, 30, 0, 15, false, true));
            skills.Add(new Skills(17, "Aard", "AardSkill17", 100, 1, 25, 35, 8, 40, 0, 16, false, false));
            //----------------------IGNI-------------------------------------------------------
            skills.Add(new Skills(1, "Igni", "IgniSkill1", 10, 0, 15, 5, 3, 1, 10, 0, true, true));
            skills.Add(new Skills(2, "Igni", "IgniSkill2", 10, 0, 15, 5, 3, 1, 10, 1, true, false));
            skills.Add(new Skills(3, "Igni", "IgniSkill3", 10, 0, 15, 5, 3, 1, 15, 2, false, false));
            skills.Add(new Skills(4, "Igni", "IgniSkill4", 30, 0, 15, 5, 3, 1, 15, 3, false, true));
            skills.Add(new Skills(5, "Igni", "IgniSkill5", 30, 0, 15, 5, 3, 1, 15, 4, false, false));
            skills.Add(new Skills(6, "Igni", "IgniSkill6", 30, 0, 15, 10, 4, 1, 15, 5, false, false));
            skills.Add(new Skills(7, "Igni", "IgniSkill7", 30, 0, 15, 10, 4, 1, 25, 6, false, false));
            skills.Add(new Skills(8, "Igni", "IgniSkill8", 50, 0, 20, 10, 4, 1, 25, 7, false, true));
            skills.Add(new Skills(9, "Igni", "IgniSkill9", 50, 0, 20, 10, 4, 1, 25, 8, false, false));
            skills.Add(new Skills(10, "Igni", "IgniSkill10", 50, 0, 20, 10, 4, 3, 25, 9, false, false));
            skills.Add(new Skills(11, "Igni", "IgniSkill11", 50, 0, 20, 20, 4, 3, 25, 10, false, false));
            skills.Add(new Skills(12, "Igni", "IgniSkill12", 80, 0, 20, 20, 4, 3, 25, 11, false, true));
            skills.Add(new Skills(13, "Igni", "IgniSkill13", 80, 0, 20, 20, 4, 3, 25, 12, false, false));
            skills.Add(new Skills(14, "Igni", "IgniSkill14", 80, 1, 20, 20, 4, 3, 25, 13, false, false));
            skills.Add(new Skills(15, "Igni", "IgniSkill15", 80, 1, 20, 20, 8, 3, 25, 14, false, false));
            skills.Add(new Skills(16, "Igni", "IgniSkill16", 100, 1, 25, 20, 8, 3, 25, 15, false, true));
            skills.Add(new Skills(17, "Igni", "IgniSkill17", 100, 1, 25, 30, 8, 3, 30, 16, false, false));
            //----------------------QUEN-------------------------------------------------------
            skills.Add(new Skills(1, "Quen", "QuenSkill1", 5, 0, 15, 5, 5, 0, 0, 0, true, true));
            skills.Add(new Skills(2, "Quen", "QuenSkill2", 5, 0, 15, 5, 5, 0, 0, 1, true, false));
            skills.Add(new Skills(3, "Quen", "QuenSkill3", 5, 0, 15, 5, 10, 0, 0, 2, false, false));
            skills.Add(new Skills(4, "Quen", "QuenSkill4", 10, 0, 15, 5, 10, 0, 0, 3, false, true));
            skills.Add(new Skills(5, "Quen", "QuenSkill5", 10, 0, 15, 5, 10, 0, 0, 4, false, false));
            skills.Add(new Skills(6, "Quen", "QuenSkill6", 10, 0, 15, 5, 10, 1, 0, 5, false, false));
            skills.Add(new Skills(7, "Quen", "QuenSkill7", 10, 0, 15, 5, 30, 1, 0, 6, false, false));
            skills.Add(new Skills(8, "Quen", "QuenSkill8", 15, 0, 20, 5, 30, 1, 0, 7, false, true));
            skills.Add(new Skills(9, "Quen", "QuenSkill9", 15, 0, 20, 5, 30, 1, 0, 8, false, false));
            skills.Add(new Skills(10, "Quen", "QuenSkill10", 15, 0, 20, 10, 30, 1, 0, 9, false, false));
            skills.Add(new Skills(11, "Quen", "QuenSkill11", 15, 0, 20, 10, 50, 1, 0, 10, false, false));
            skills.Add(new Skills(12, "Quen", "QuenSkill12", 20, 0, 20, 10, 50, 1, 0, 11, false, true));
            skills.Add(new Skills(13, "Quen", "QuenSkill13", 20, 0, 20, 10, 50, 1, 0, 12, false, false));
            skills.Add(new Skills(14, "Quen", "QuenSkill14", 20, 1, 20, 10, 50, 1, 0, 13, false, false));
            skills.Add(new Skills(15, "Quen", "QuenSkill15", 20, 1, 20, 15, 50, 1, 0, 14, false, false));
            skills.Add(new Skills(16, "Quen", "QuenSkill16", 25, 1, 25, 15, 50, 1, 0, 15, false, true));
            skills.Add(new Skills(17, "Quen", "QuenSkill17", 25, 1, 25, 20, 100, 1, 0, 16, false, false));
            //----------------------AXII-------------------------------------------------------
            skills.Add(new Skills(1, "Axii", "AxiiSkill1", 10, 0, 15, 4, 3, 5, 0, 0, true, true));
            skills.Add(new Skills(2, "Axii", "AxiiSkill2", 10, 0, 15, 4, 3, 5, 0, 1, true, false));
            skills.Add(new Skills(3, "Axii", "AxiiSkill3", 10, 0, 15, 4, 3, 10, 0, 2, false, false));
            skills.Add(new Skills(4, "Axii", "AxiiSkill4", 30, 0, 15, 4, 3, 10, 0, 3, false, true));
            skills.Add(new Skills(5, "Axii", "AxiiSkill5", 30, 0, 15, 4, 3, 10, 0, 4, false, false));
            skills.Add(new Skills(6, "Axii", "AxiiSkill6", 30, 0, 15, 4, 3, 15, 0, 5, false, false));
            skills.Add(new Skills(7, "Axii", "AxiiSkill7", 30, 0, 15, 4, 2, 15, 0, 6, false, false));
            skills.Add(new Skills(8, "Axii", "AxiiSkill8", 50, 0, 20, 4, 2, 15, 0, 7, false, true));
            skills.Add(new Skills(9, "Axii", "AxiiSkill9", 50, 0, 20, 4, 2, 15, 0, 8, false, false));
            skills.Add(new Skills(10, "Axii", "AxiiSkill10", 50, 0, 20, 7, 2, 15, 0, 9, false, false));
            skills.Add(new Skills(11, "Axii", "AxiiSkill11", 50, 0, 20, 7, 2, 20, 0, 10, false, false));
            skills.Add(new Skills(12, "Axii", "AxiiSkill12", 80, 0, 20, 7, 2, 20, 0, 11, false, true));
            skills.Add(new Skills(13, "Axii", "AxiiSkill13", 80, 0, 20, 7, 2, 20, 0, 12, false, false));
            skills.Add(new Skills(14, "Axii", "AxiiSkill14", 80, 1, 20, 7, 2, 20, 0, 13, false, false));
            skills.Add(new Skills(15, "Axii", "AxiiSkill15", 80, 1, 20, 7, 1, 20, 0, 14, false, false));
            skills.Add(new Skills(16, "Axii", "AxiiSkill16", 100, 1, 25, 7, 1, 20, 0, 15, false, true));
            skills.Add(new Skills(17, "Axii", "AxiiSkill17", 100, 1, 25, 10, 1, 30, 0, 16, false, false));
            //----------------------YRDEN-------------------------------------------------------
            skills.Add(new Skills(1, "Yrden", "YrdenSkill1", 10, 0, 15, 5, 0, 0, 0, 0, true, true));
            skills.Add(new Skills(2, "Yrden", "YrdenSkill2", 10, 0, 15, 5, 0, 0, 0, 0, true, false));
            skills.Add(new Skills(3, "Yrden", "YrdenSkill3", 10, 0, 15, 5, 0, 0, 1, 0, false, false));
            skills.Add(new Skills(4, "Yrden", "YrdenSkill4", 30, 0, 15, 5, 0, 0, 1, 0, false, true));
            skills.Add(new Skills(5, "Yrden", "YrdenSkill5", 30, 0, 15, 5, 0, 0, 1, 0, false, false));
            skills.Add(new Skills(6, "Yrden", "YrdenSkill6", 30, 0, 15, 5, 1, 0, 1, 0, false, false));
            skills.Add(new Skills(7, "Yrden", "YrdenSkill7", 30, 0, 15, 7, 1, 0, 1, 0, false, false));
            skills.Add(new Skills(8, "Yrden", "YrdenSkill8", 50, 0, 20, 7, 1, 0, 1, 0, false, true));
            skills.Add(new Skills(9, "Yrden", "YrdenSkill9", 50, 0, 20, 7, 1, 0, 1, 0, false, false));
            skills.Add(new Skills(10, "Yrden", "YrdenSkill10", 50, 0, 20, 8, 1, 0, 1, 0, false, false));
            skills.Add(new Skills(11, "Yrden", "YrdenSkill11", 50, 0, 20, 8, 1, 1, 1, 0, false, false));
            skills.Add(new Skills(12, "Yrden", "YrdenSkill12", 80, 0, 20, 8, 1, 1, 1, 0, false, true));
            skills.Add(new Skills(13, "Yrden", "YrdenSkill13", 80, 0, 20, 8, 1, 1, 1, 0, false, false));
            skills.Add(new Skills(14, "Yrden", "YrdenSkill14", 80, 1, 20, 8, 1, 1, 1, 0, false, false));
            skills.Add(new Skills(15, "Yrden", "YrdenSkill15", 80, 1, 20, 9, 1, 1, 1, 0, false, false));
            skills.Add(new Skills(16, "Yrden", "YrdenSkill16", 100, 1, 25, 9, 1, 1, 1, 0, false, true));
            skills.Add(new Skills(17, "Yrden", "YrdenSkill17", 100, 1, 25, 10, 1, 1, 1, 0, false, false));
            //----------------------STRENGTH-------------------------------------------------------
            skills.Add(new Skills(1, "Strength", "StrengthSkill1", 0, 2, 0, 0, 0, 0, 0, 0, true, false));
            skills.Add(new Skills(2, "Strength", "StrengthSkill2", 0, 5, 0, 0, 0, 0, 0, 1, false, false));
            skills.Add(new Skills(3, "Strength", "StrengthSkill3", 0, 5, 2, 0, 0, 0, 0, 2, false, false));
            skills.Add(new Skills(4, "Strength", "StrengthSkill4", 50, 5, 0, 0, 0, 0, 0, 3, false, false));
            skills.Add(new Skills(5, "Strength", "StrengthSkill5", 0, 5, 0, 10, 0, 0, 0, 4, false, false));
            skills.Add(new Skills(6, "Strength", "StrengthSkill6", 0, 10, 0, 10, 0, 0, 0, 5, false, false));
            skills.Add(new Skills(7, "Strength", "StrengthSkill7", 0, 10, 2, 10, 0, 0, 0, 6, false, false));
            skills.Add(new Skills(8, "Strength", "StrengthSkill8", 25, 10, 0, 10, 0, 0, 0, 7, false, false));
            skills.Add(new Skills(9, "Strength", "StrengthSkill9", 0, 10, 0, 10, 1, 0, 0, 8, false, false));
            skills.Add(new Skills(10, "Strength", "StrengthSkill10", 0, 25, 0, 10, 1, 0, 0, 9, false, false));
            skills.Add(new Skills(11, "Strength", "StrengthSkill11", 0, 25, 2, 10, 1, 0, 0, 10, false, false));
            skills.Add(new Skills(12, "Strength", "StrengthSkill12", 25, 25, 0, 10, 1, 0, 0, 11, false, false));
            skills.Add(new Skills(13, "Strength", "StrengthSkill13", 0, 25, 0, 20, 1, 0, 0, 12, false, false));
            skills.Add(new Skills(14, "Strength", "StrengthSkill14", 0, 25, 2, 20, 1, 0, 0, 13, false, false));
            skills.Add(new Skills(15, "Strength", "StrengthSkill15", 0, 35, 0, 20, 1, 0, 0, 14, false, false));
            skills.Add(new Skills(16, "Strength", "StrengthSkill16", 50, 35, 0, 20, 1, 0, 0, 15, false, false));
            skills.Add(new Skills(17, "Strength", "StrengthSkill17", 0, 50, 12, 20, 1, 0, 0, 16, false, false));
            //----------------------Endurance-------------------------------------------------------
            skills.Add(new Skills(1, "Endurance", "EnduranceSkill1", 0, 0, 0, 0, 0, 0, 0, 0, true, false));
            skills.Add(new Skills(2, "Endurance", "EnduranceSkill2", 0, 2, 0, 0, 0, 0, 0, 1, false, false));
            skills.Add(new Skills(3, "Endurance", "EnduranceSkill3", 0, 2, 0, 0, 2, 0, 0, 2, false, false));
            skills.Add(new Skills(4, "Endurance", "EnduranceSkill4", 4, 2, 0, 0, 2, 0, 0, 3, false, false));
            skills.Add(new Skills(5, "Endurance", "EnduranceSkill5", 0, 2, 0, 10, 2, 0, 0, 4, false, false));
            skills.Add(new Skills(6, "Endurance", "EnduranceSkill6", 0, 6, 0, 10, 2, 0, 0, 5, false, false));
            skills.Add(new Skills(7, "Endurance", "EnduranceSkill7", 5, 6, 0, 10, 2, 0, 0, 6, false, false));
            skills.Add(new Skills(8, "Endurance", "EnduranceSkill8", 2, 6, 0, 10, 2, 0, 0, 7, false, false));
            skills.Add(new Skills(9, "Endurance", "EnduranceSkill9", 0, 6, 0, 10, 5, 0, 0, 8, false, false));
            skills.Add(new Skills(10, "Endurance", "EnduranceSkill10", 0, 6, 0, 20, 5, 0, 0, 9, false, false));
            skills.Add(new Skills(11, "Endurance", "EnduranceSkill11", 0, 10, 0, 20, 5, 0, 0, 10, false, false));
            skills.Add(new Skills(12, "Endurance", "EnduranceSkill12", 2, 10, 0, 20, 5, 0, 0, 11, false, false));
            skills.Add(new Skills(13, "Endurance", "EnduranceSkill13", 0, 15, 0, 20, 5, 0, 0, 12, false, false));
            skills.Add(new Skills(14, "Endurance", "EnduranceSkill14", 0, 15, 0, 20, 5, 5, 0, 13, false, false));
            skills.Add(new Skills(15, "Endurance", "EnduranceSkill15", 0, 15, 1, 20, 5, 5, 0, 14, false, false));
            skills.Add(new Skills(16, "Endurance", "EnduranceSkill16", 2, 15, 1, 20, 5, 5, 0, 15, false, false));
            skills.Add(new Skills(17, "Endurance", "EnduranceSkill17", 0, 25, 1, 20, 5, 5, 50, 16, false, false));
            manager.SaveSkills(skills);
        }
        public void CreateArmors() {
            armors.Add(new Armor("Středně těžká zbroj", "Zbroj wyzimské stráže", "Obnošená zbroj wyzimské stráže", 1, 5, 0, 0, @"img/Armors/Armor_Temeria2.png", 150, null, 10, "Loot"));
            armors.Add(new Armor("Středně těžká zbroj", "Mantikoří zbroj", "Kazajka používaná zaklínači ze školy Mantikory", 1, 10, 0, 0, @"img/Armors/Armor_Manticore.png", 200, "Manticore", 10, "Start"));
            armors.Add(new Armor("Středně těžká zbroj", "Mahakamská zbroj", "Zbroj vyrobená trpaslíky z Mahakamu", 5, 20, 0, 0, @"img/Armors/Armor_Manticore.png", 200, "Mahakam", 10, "Shop"));
            armors.Add(new Armor("Těžká zbroj", "Ocelová zbroj", "Ocelová zbroj s pevnou ocelovou hrudí a vyztuženými nárameníky", 10, 25, 2, 0, @"img/Armors/Armor_Manticore.png", 100, "None", 0, "HumanLoot"));

            playeramors.Add(armors[0]);
            manager.SavePlayerArmor(playeramors);
            string jsonToFile = JsonConvert.SerializeObject(armors, settings);
            File.WriteAllText(armorpath, jsonToFile);
        }
        public void CreateSwords() {
            swords.Add(new Sword("Ocelový meč", "Temerský ocelový meč", "Meč, který používají temerští vojáci", 1, 40, 0, @"img/Swords/Sword_Temeria.png", 200, null, 10, "Loot"));
            swords.Add(new Sword("Ocelový meč", "Mahakamský sihil", "Meč ukován trpaslíky z té nejlepší oceli až z Mahakamu", 1, 10, 0 , @"img/Swords/Sword_Mahakam_Sihil.png", 200, "Mahakam", 10, "Start"));
            swords.Add(new Sword("Stříbrný meč", "Aerondight", "Ostrý jako břitva, tento meč má svůj vlastní osud, jen čas ukáže jaký", 1, 10, 0, @"img/Swords/Sword_Aerondight.png", 200, null, 0, "Start"));

            playerswords.Add(swords[0]);
            manager.SavePlayerSwords(playerswords);
            string jsonToFile = JsonConvert.SerializeObject(swords, settings);
            File.WriteAllText(swordpath, jsonToFile);
        }
        public void CreateInv() {
            items.Add(new Item("Kuře", "Jídlo,Po snězení doplní malou část zdraví", "Loot", "Loot", @"img/Items/Food_Chicken.png", "žádné", "Food", "Sníst", 0, 0, null, 20));
            items.Add(new Item("Jablečný Džus", "Nápoj, lze vypít pro doplňení malé části zdraví", "Loot", "Loot", @"img/Items/Drink_Apple_Juice.png", "žádné", "Drink", "Vypít",0, 0, null, 15));
            items.Add(new Item("Fisstech", "Silná droga, lze prodat", "Loot", "Loot", @"img/Items/Potion_Fisstech.png", "žádné", "Drug", "Použít",0, 0, null, 150));
            items.Add(new Item("Víno", "Alkohol, lze prodat kupcům nebo použít", "Loot", "Loot", @"img/Items/Alcohol_Winered.png", "žádné", "Alcohol", "Vypít", 0, 0, null, 50));
            items.Add(new Item("Vlaštovka", "Elixír, Pomalu doplňuje zdraví po určitou dobu", "Loot", "Loot", @"img/Items/Potion_Full_Moon.png", null, "Potion", "Vypít", 30, 2, null, 80));
            items.Add(new Item("Barghesti", "Kniha o barghestech", "Loot","Loot", @"img/Items/Book_Bestiary.png", null, "Barghest", "Číst", 0, 0, "Barghesti jsou fakt svině...", 100));
            items.Add(new Item("Tesáky z příšery", "Tesáky sebrané z příšery", "Alchemy", "Barghest", @"img/Items/Monster_Fang.png", "Vitriol", "Alchemy", null, 0, 0, null, 10));
            items.Add(new Item("Prach smrti", "Prach, který se většinou dá získat z přeludů, nebo z jiných příšer", "Alchemy", "Barghest", @"img/Items/Monster_DeathDust.png", "Rebis", "Alchemy", null, 0, 0, null, 10));
            items.Add(new Item("Ektoplasma", "Ektoplasma z přeludů", "Alchemy", "Barghest", @"img/Items/Monster_Ectoplasm.png", "Nigredo", "Alchemy", null, 0, 0, null, 10));
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

            Aard aard = new Aard();
            aard.SignIntensity = 10;
            aard.Effectivity = 0;
            aard.EnduranceCost = 15;
            aard.StunChance = 10;
            aard.StunDuration = 3;
            aard.KnockBackChance = 15;
            Igni igni = new Igni();
            igni.SignIntensity = 10;
            igni.Effectivity = 0;
            igni.EnduranceCost = 15;
            igni.Damage = 10;
            igni.BurnChance = 5;
            igni.BurnDamage = 1;
            igni.BurnDuration = 3;
            Quen quen = new Quen();
            quen.SignIntensity = 5;
            quen.Effectivity = 0;
            quen.EnduranceCost = 15;
            quen.ShieldDuration = 5;
            quen.DamageReduction = 5;
            quen.EffectsResistance = 0;
            Axii axii = new Axii();
            axii.SignIntensity = 10;
            axii.Effectivity = 0;
            axii.EnduranceCost = 15;
            axii.Duration = 4;
            axii.ChannelingTime = 3;
            axii.StatsDecrease = 5;
            Yrden yrden = new Yrden();
            yrden.SignIntensity = 10;
            yrden.Effectivity = 0;
            yrden.EnduranceCost = 15;
            yrden.Duration = 5;
            yrden.AttackBlock = 0;
            yrden.Confusion = 0;
            yrden.Pain = 0;


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
            player.Add(new Player(100, 100, 25, 25, 50, 0, 0, 1000, 30, 1, 50, 5, 5, 2, 0, 0, 0, 0.5, steel, silver, ar, aard, igni, quen, axii, yrden));
            string jsonToFile = JsonConvert.SerializeObject(player, settings);
            File.WriteAllText(playerpath, jsonToFile);
        }
        public void CreateQuests() {
            qq.Add(new Quest (1, "Primary", "Něco končí, něco začíná", "Foltest si předvolal Geralta hned druhý den potom co krála zachránil.", "Zajdi za Foltestem", 1, true, "Něco končí, něco začíná", null, 0));
            qq.Add(new Quest(2, "Primary", "Něco končí, něco začíná", "Foltest si předvolal Geralta hned druhý den potom co krále zachránil. Foltest Geraltovi oznámil, že by měl něco zjistit o vrahovi s pomocí Triss", "Zajdi za Triss a zjisti něco o vrahovi", 1, true, "Něco končí, něco začíná", "Zjistila jsi něco nového o tom vrahovi?", 0));
            qq.Add(new Quest(3, "Primary", "Něco končí, něco začíná", "Foltest si předvolal Geralta hned druhý den potom co krále zachránil. Foltest Geraltovi oznámil, že by měl něco zjistit o vrahovi s pomocí Triss. Triss Geraltovi sdělila, že na to, aby zjistila, kdo byl vrah, tak potřebuje více času", "Počkej až se Triss dozví více o vrahovi", 1, true, "Něco končí, něco začíná", null, 0));
            qq.Add(new Quest(1, "Primary", "Kovářova zrůda", "Triss řekla Geraltovi, aby pomohl svému starému známému Yavenovi Briggsovi", "Zajdi za kovářem do staré wyzimy a zjisti jaký má problém", 1, true, "Kovářova zrůda", null, 0));

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
