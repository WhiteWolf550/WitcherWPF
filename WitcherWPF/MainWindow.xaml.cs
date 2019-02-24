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
        List<Potion> potions = new List<Potion>();
        List<Dialogues> dialog = new List<Dialogues>();
        List<Monologue> monologue = new List<Monologue>();
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
            CreateInv();
            CreateDialogue();
            CreateQuests();
            CreatePlayerQuests();
            //CreateSkills();
            //CreateArmors();
            //CreateSwords();
            CreatePlayer();
            //CreatePotions();
            Globals.Combat = false;
            Globals.location = "Old_wyzima1";
            mainFrame.Navigate(new Inventory(mainFrame, false, time));
        }
        public void CreatePotions() {
            potions.Add(new Potion("Vlaštovka", 20, "Vitriol", "Aether", "Rebis", @"img/Items/Potion_Swallow.png", "Elixír, který rychle doplňuje Geraltovo zdraví", 2, "MediumAlcohol"));
            potions.Add(new Potion("Hrom", 25, "Vermilion", "Rebis", "Vitriol", @"img/Items/Potion_Thunderbolt.png", "Elixír, který značně zvýší sílu útoků", 2, "StrongAlcohol"));
            potions.Add(new Potion("Puštík", 20, "Rebis", "Aether", "Vermilion", @"img/Items/Potion_Tawny_Owl.png", "Elixír, který rychle doplňuje Geraltovu výdrž", 3, "MediumAlcohol"));
            potions.Add(new Potion("Petriho filtr", 30, "Quebirth", "Vermilion", "Hydragenum", @"img/Items/Potion_Petris_Philter.png", "Elixír, který značně zvýší intenzitu všech znamení", 1, "StrongAlcohol"));
            potions.Add(new Potion("Černá krev", 25, "Vitriol", "Rebis", "Vermilion", @"img/Items/Potion_Black_Blood.png", "Elixír, který mění Geraltovu krev na jedovatou pro upíry (upíři dostanou poškození pokud zaútoči na Geralta)", 3, "StrongAlcohol"));
            potions.Add(new Potion("Úplněk", 25, "Quebirth", "Hydragenum", "Aether", @"img/Items/Potion_Full_Moon.png", "Elixír který značně zvýší Geraltovu vitalitu", 1, "StrongAlcohol"));

            manager.SavePotions(potions);

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
            swords.Add(new Sword("Ocelový meč", "Temerský ocelový meč", "Meč, který používají temerští vojáci", 1, 10, 0, @"img/Swords/Sword_Temeria.png", 200, null, 10, "Loot"));
            swords.Add(new Sword("Ocelový meč", "Mahakamský sihil", "Meč ukován trpaslíky z té nejlepší oceli až z Mahakamu", 1, 10, 0 , @"img/Swords/Sword_Mahakam_Sihil.png", 200, "Mahakam", 10, "Start"));
            swords.Add(new Sword("Stříbrný meč", "Aerondight", "Ostrý jako břitva, tento meč má svůj vlastní osud, jen čas ukáže jaký", 1, 10, 0, @"img/Swords/Sword_Aerondight.png", 200, null, 0, "Start"));

            playerswords.Add(swords[0]);
            manager.SavePlayerSwords(playerswords);
            string jsonToFile = JsonConvert.SerializeObject(swords, settings);
            File.WriteAllText(swordpath, jsonToFile);
        }
        public void CreateInv() {
            items.Add(new Item("Kuře", "Jídlo,Po snězení doplní malou část zdraví", "Loot", "Loot", @"img/Items/Food_Chicken.png", "žádné", "Food", "Sníst", 0, 0, null, 20));
            items.Add(new Item("Wyverní maso", "Vzácné maso, které se dá prodat", "Loot", "Loot", @"img/Items/Food_Wyvern_Meat.png", "žádné", "Food", "Sníst", 0, 0, null, 90));
            items.Add(new Item("Jablečný Džus", "Nápoj, lze vypít pro doplňení malé části zdraví", "Loot", "Loot", @"img/Items/Drink_Apple_Juice.png", "žádné", "Drink", "Vypít",0, 0, null, 15));
            items.Add(new Item("Fisstech", "Silná droga, lze prodat", "Loot", "Loot", @"img/Items/Potion_Fisstech.png", "žádné", "Drug", "Použít",0, 0, null, 150));
            items.Add(new Item("Víno", "Alkohol, lze prodat kupcům nebo použít", "Alcohol", "Loot", @"img/Items/Alcohol_Winered.png", "žádné", "Alcohol", "Vypít", 0, 0, null, 20));
            items.Add(new Item("Temerská žitná", "Středně silný alkohol, lze prodat kupcům nebo použít jako Alchymistický základ", "MediumAlcohol", "Loot", @"img/Items/Alcohol_Temerian_Rye.png", "žádné", "Alcohol", "Vypít", 0, 0, null, 50));
            items.Add(new Item("Trpasličí vodka", "Velice silný alkohol, lze prodat kupcům za vysokou částku nebo použít jako Alchymistický základ", "StrongAlcohol", "Loot", @"img/Items/Alcohol_Dwarven_Spirit.png", "žádné", "Alcohol", "Vypít", 0, 0, null, 80));
            
            items.Add(new Item("Barghesti", "Kniha o barghestech", "Loot","Loot", @"img/Items/Book_Bestiary.png", null, "Barghest", "Číst", 0, 0, "Barghesti jsou fakt svině...", 100));
            //POTIONS
            items.Add(new Item("Puštík", "Elixír, který rychle doplňuje Geraltovu výdrž", "Potion", "Alchemy", @"img/Items/Potion_Tawny_Owl.png", null, "Potion", "Vypít", 20, 2, null, 50));
            items.Add(new Item("Vlaštovka", "Elixír, který rychle doplňuje Geraltovo zdraví", "Potion", "Alchemy", @"img/Items/Potion_Swallow.png", null, "Potion", "Vypít", 20, 2, null, 50));
            items.Add(new Item("Hrom", "Elixír, který značně zvýší sílu útoků", "Potion", "Alchemy", @"img/Items/Potion_Thunderbolt.png", null, "Potion", "Vypít", 25, 2, null, 50));
            items.Add(new Item("Petriho filtr", "Elixír, který značně zvýší intenzitu všech znamení", "Potion", "Alchemy", @"img/Items/Potion_Petris_Philter.png", null, "Potion", "Vypít", 30, 2, null, 80));
            items.Add(new Item("Černá krev", "Elixír, který mění Geraltovu krev na jedovatou pro upíry (upíři dostanou poškození pokud zaútoči na Geralta)", "Potion", "Alchemy", @"img/Items/Potion_Black_Blood.png", null, "Potion", "Vypít", 25, 2, null, 80));
            items.Add(new Item("Úplněk", "Elixír který značně zvýší Geraltovu vitalitu", "Potion", "Alchemy", @"img/Items/Potion_Full_Moon.png", null, "Potion", "Vypít", 25, 2, null, 50));

            //MONSTER LOOT
            items.Add(new Item("Tesáky z příšery", "Tesáky sebrané z příšery", "Alchemy", "Barghest", @"img/Items/Monster_Fang.png", "Rebis", "Alchemy", null, 0, 0, null, 10));
            items.Add(new Item("Prach smrti", "Prach, který se většinou dá získat z přeludů, nebo z jiných příšer", "Alchemy", "Barghest", @"img/Items/Monster_DeathDust.png", "Vitriol", "Alchemy", null, 0, 0, null, 10));
            

            items.Add(new Item("Krev z Ghůla", "Krev, která se dá získat z Ghůla", "Alchemy", "Ghoul", @"img/Items/Monster_Ghoul_Blood.png", "Vitriol", "Alchemy", null, 0, 0, null, 10));
            items.Add(new Item("Bílý Ocet", "Bílý Ocet, který se dá použít v Alchymii", "Alchemy", "Ghoul", @"img/Items/Monster_Ghoul_Blood.png", "Vitriol", "Alchemy", null, 0, 0, null, 10));
            items.Add(new Item("Bílý Ocet", "Bílý Ocet, který se dá použít v Alchymii", "Alchemy", "Ghoul", @"img/Items/Monster_Ghoul_Blood.png", "Vitriol", "Alchemy", null, 0, 0, null, 10));
            //BUILDING
            items.Add(new Item("Dřevo", "Dřevo lze použít jako stavební materiál a nebo ho lze prodat", "Build", "Loot", @"img/Items/Wood.png", "žádné", "Build", null, 0, 0, null, 10));
            string jsonToFile = JsonConvert.SerializeObject(items, settings);
            File.WriteAllText(ipath, jsonToFile);
        }
        public void CreateDialogue() {
            //----------------------FOLTEST----------------
            //greet
            dialog.Add(new Dialogues("Foltest", "Vítej zpět Zaklínači", 1, "Pozdrav", "Greet", "Foltest", true, null, null, false));

            //1
            dialog.Add(new Dialogues("Foltest", "Geralte, zjistil si už něco o tom vrahovi?",1, "Co potřebujete králi?", "Talk", "Foltest", true, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "Ne králi, zatím ne", 1, "Co potřebujete králi?", "Talk", "Foltest", true, null, null, false));
            dialog.Add(new Dialogues("Foltest", "Tak to by sis měl pospíšit Geralte. Přeci jen jde o tvojí reputaci", 1, "Co potřebujete králi?", "Talk", "Foltest", true, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "To že se jeden Zaklínač pokusí o zabití krále, ihned neznamená, že takový jsou všichni zaklínači", 1, "Co potřebujete králi?", "Talk", "Foltest", true, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Foltest", "Takhle to ale u prostého lidu nefunguje. Lidem stačí, aby se jeden Zaklínač pokusil o vraždu a bude nenávidět všechny", 1, "Co potřebujete králi?", "Talk", "Foltest", true, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Foltest", "Potřebuji mít tuto záležitost co nejrychleji zasebou Zaklínači.", 1, "Co potřebujete králi?", "Talk", "Foltest", true, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "Dobře králi", 1, "Co potřebujete králi?", "Talk", "Foltest", true, "Něco končí, něco začíná", null, false));

            //2
            dialog.Add(new Dialogues("Geralt", "Jak se daří Wyzimě?", 2, "Jak se daří Wyzimě?", "Talk", "Foltest", true, null, null, false));
            dialog.Add(new Dialogues("Foltest", "To tě to opravdu zajímá Zaklínači?", 2, "Jak se daří Wyzimě?", "Talk", "Foltest", true, null, null, false));
            dialog.Add(new Dialogues("Geralt", "Ne, nezajímá", 2, "Jak se daří Wyzimě?", "Talk", "Foltest", true, null, null, false));
            dialog.Add(new Dialogues("Foltest", "Wyzima je pořád v torskách. Budu se ale snažit ji vrátit zpět do své krásy", 2, "Jak se daří Wyzimě?", "Talk", "Foltest", true, null, null, false));
            
            //3
            dialog.Add(new Dialogues("Geralt", "Mám informace o vrahovi?", 1, "Mám informace o vrahovi", "Talk", "Foltest", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Foltest", "Skvělé už víš odkud pocházel a co byl zač?", 1, "Mám informace o vrahovi", "Talk", "Foltest", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "Vím jen to, že se poslední dobou nacházel poblíž Novigradu", 1, "Mám informace o vrahovi", "Talk", "Foltest", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "A přesně tam se vydám", 1, "Mám informace o vrahovi", "Talk", "Foltest", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Foltest", "Proč tak náhlá změna názoru?", 1, "Mám informace o vrahovi", "Talk", "Foltest", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Foltest", "Ještě před chvílí jsi nebyl, tak odhodlán", 1, "Mám informace o vrahovi", "Talk", "Foltest", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "Mám své důvody", 1, "Mám informace o vrahovi", "Talk", "Foltest", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Foltest", "A mohl bych tvé důvody znát?", 1, "Mám informace o vrahovi", "Talk", "Foltest", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "Ne", 1, "Mám informace o vrahovi", "Talk", "Foltest", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "Nemohl", 1, "Mám informace o vrahovi", "Talk", "Foltest", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Foltest", "Dobře. Ať je po tvém", 1, "Mám informace o vrahovi", "Talk", "Foltest", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Foltest", "Pokud se ti o vrahovi podaří zjistit více, tak mi to přijeď sdělit. Budu tady ve Wyzimě", 1, "Mám informace o vrahovi", "Talk", "Foltest", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Foltest", "Samozřejmě tu na tebe bude čekat odměna pokud se ti to podaří vyřešit", 1, "Mám informace o vrahovi", "Talk", "Foltest", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "hm", 1, "Mám informace o vrahovi", "Talk", "Foltest", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "Samozřejmě", 1, "Mám informace o vrahovi", "Talk", "Foltest", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Foltest", "Hodně štěstí Zaklínači", 1, "Mám informace o vrahovi", "Talk", "Foltest", false, "Něco končí, něco začíná", null, false));

            //leave
            dialog.Add(new Dialogues("Geralt", "Nashle králi", 2, "Nashle", "Talk", "Foltest", true, null, null, false));
            dialog.Add(new Dialogues("Foltest", "Nashle Zaklínači", 2, "Nashle", "Talk", "Foltest", true, null, null, false));
            //----------------------TRISS----------------
            //greet
            dialog.Add(new Dialogues("Triss", "Ano Geralte?", 1, "Pozdrav", "Greet", "Triss", true, null, null, false));

            //1
            dialog.Add(new Dialogues("Geralt", "Zjistila jsi něco nového o tom vrahovi?", 1, "Zjistila jsi něco nového o tom vrahovi?", "Talk", "Triss", false, "Kovářova zrůda", null, false));
            dialog.Add(new Dialogues("Triss", "Všechno co vím je že to byl zaklínač, abych zjistila něco více budu potřebovat více času", 1, "Zjistila jsi něco nového o tom vrahovi?", "Talk", "Triss", false, "Kovářova zrůda", null, false));
            dialog.Add(new Dialogues("Geralt", "Já už tu ale déle být nechci Triss. Mám už plné zuby politiky a intriků. Triss pojď semnou, vypadneme odsud", 1, "Zjistila jsi něco nového o tom vrahovi?", "Talk", "Triss", false, "Kovářova zrůda", null, false));
            dialog.Add(new Dialogues("Triss", "Geralte, však víš, že já nemůžu jen tak odejít dokud mě Foltest nepustí.", 1, "Zjistila jsi něco nového o tom vrahovi?", "Talk", "Triss", false, "Kovářova zrůda", null, false));
            dialog.Add(new Dialogues("Geralt", "Ty ani já nic Foltestovi nedlužíme. Jakmile zjistíš něco o tom vrahovi, tak odsud vypadneme a nikdy se nevrátíme", 1, "Zjistila jsi něco nového o tom vrahovi?", "Talk", "Triss", false, "Kovářova zrůda", null, false));
            dialog.Add(new Dialogues("Triss", "Až něco zjistím, tak uvidíme. Mezitím by jsi mohl pomoct místnímu kováři Yavenovi Briggsovi. Toho znáš ne?", 1, "Zjistila jsi něco nového o tom vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "Znám ho potkali jsme se v lese poblíž Brokilonu. To už je tak dlouho, jako kdyby to bylo v minulém životě. Pomůžu mu", 1, "Zjistila jsi něco nového o tom vrahovi?", "Talk", "Triss", false, "Kovářova zrůda", null, false));

            //2
            dialog.Add(new Dialogues("Geralt", "Získala jsi už nějaké informace o vrahovi?", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Triss", "Ano", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Triss", "Ale to co ti teď řeknu se ti nebude líbit", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "Co se děje?", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Triss", "Z jeho vzpomínek jsem zjistila, že pár hodin před útokem s někým komunikoval.", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Triss", "Nevím o čem, protože jeho vzpomínky velice rychle mizí", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "Triss..", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "S kým mluvil?", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Triss", "Mluvil s Lambertem", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "S Lambertem?!", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "To není možné", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "Lambert by se nikdy nezapletl s vrahy a jinou bídou. Natož aby s nimi spolupracoval", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Triss", "Já vím Geralte, také mi to nedává smysl, ale určitě mluvil s Lambertem. To jsem si jistá", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "Celá tahle situace se mi vůbec nelíbí, už jen kvůli tomu, že jsem do toho zapletený já, Lambert a Foltest", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Triss", "Geralte nerada to říkám, ale asi budeš muset pomoct Foltestovi", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "Ano já vím.. Ale rozhodně to nebudu dělat kvůli Foltestovi. Kdyby do toho nebyl zapletený Lambert, tak už tu dávno nejsem", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "Víš alespoň, kde se Lambert nachází?", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Triss", "Vrahovi vzpomínky jsou už hodně slabé, takže si nemůžu být jistá..", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Triss", "Vím jen, že se nacházel poblíž Novigradu. Je možné že i v Novigradu", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "To mi bude muset pro začátek stačit", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "Vydám se tedy na cestu", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Triss", "Bylo by dobré říct Foltestovi, že vyrážíš a alespoň ho seznámit s tím co víme", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "To nech na mě. Řeknu mu jen to důležité", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Triss", "Něž odejdeš přijď se semnou rozloučit", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "Samozřejmě", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));

            //3
            dialog.Add(new Dialogues("Triss", "Opravdu jsi připravený Geralte?", 1, "Vyrazit na cestu", "Talk", "Triss", false, null, null, false));
            dialog.Add(new Dialogues("Triss", "Pokud tu máš něco rozdělaného, tak pochybuji, že to tu zůstane, když se vrátíš", 1, "Vyrazit na cestu", "Talk", "Triss", false, null, null, false));
            dialog.Add(new Dialogues("Geralt", "Ano, jsem připravený", 1, "Vyrazit na cestu", "Talk", "Triss", false, "Něco končí, něco začíná", "Vyrazit", true));
            dialog.Add(new Dialogues("Triss", "Dobře, jdeme", 1, "Vyrazit na cestu", "Talk", "Triss", false, "Něco končí, něco začíná", "Vyrazit", false));
            dialog.Add(new Dialogues("Geralt", "Ne, ještě něco vyřídím", 1, "Vyrazit na cestu", "Talk", "Triss", false, null, "Zůstat", true));
            dialog.Add(new Dialogues("Triss", "Až budeš připravený, tak přijď", 1, "Vyrazit na cestu", "Talk", "Triss", false, null, "Zůstat", false));

            //leave
            dialog.Add(new Dialogues("Geralt", "Měj se Triss", 2, "Nashle", "Talk", "Triss", true, null, null, false));
            dialog.Add(new Dialogues("Triss", "Dávej na sebe pozor Geralte", 2, "Nashle", "Talk", "Triss", true, null, null, false));

            //----------------------YAVEN(BLACKSMITH)----------------
            //greet
            dialog.Add(new Dialogues("Yaven", "Vítaj Vědmáku", 1, "Pozdrav", "Greet", "Yaven", true, null, null, false));

            //1
            dialog.Add(new Dialogues("Geralt", "Slyšel jsem, že máš problém s příšerou", 1, "Problém s příšerou", "Talk", "Yaven", false, "Kovářova zrůda", null, false));
            dialog.Add(new Dialogues("Yaven", "Geralt? Tož si to ty? To jest překvapení teba som tak dlho neviděl! Kde ty sa tu bereš.", 1, "Problém s příšerou", "Talk", "Yaven", false, "Kovářova zrůda", null, false));
            dialog.Add(new Dialogues("Geralt", "To je na delší povídání a na to teď není čas. Mám tu práci", 1, "Problém s příšerou", "Talk", "Yaven", false, "Kovářova zrůda", null, false));
            dialog.Add(new Dialogues("Yaven", "Jasně jasně, Tak co o tej krásce chceš vědět? Velká hnusná hlava, několik ostrejch zubisek a tož pařáty má taky skvostný", 1, "Problém s příšerou", "Talk", "Yaven", false, "Kovářova zrůda", null, false));
            dialog.Add(new Dialogues("Geralt", "Takže by to mohl být Ghůl", 1, "Problém s příšerou", "Talk", "Yaven", false, "Kovářova zrůda", null, false));
            dialog.Add(new Dialogues("Yaven", "Geralt, co já sakra vím. Jsem snad jakejsi vědmák či co? To by si mal vědět ty ne?", 1, "Problém s příšerou", "Talk", "Yaven", false, "Kovářova zrůda", null, false));
            dialog.Add(new Dialogues("Geralt", "Ano já vím, to jsem si říkal jen pro sebe.", 1, "Problém s příšerou", "Talk", "Yaven", false, "Kovářova zrůda", null, false));
            dialog.Add(new Dialogues("Geralt", "80", 1, "Problém s příšerou", "Talk", "Yaven", false, "Kovářova zrůda", null, false));
            dialog.Add(new Dialogues("Yaven", "80? čeho sakra, zas tak starej nejsem", 1, "Problém s příšerou", "Talk", "Yaven", false, "Kovářova zrůda", null, false));
            dialog.Add(new Dialogues("Geralt", "80 orénů", 1, "Problém s příšerou", "Talk", "Yaven", false, "Kovářova zrůda", null, false));
            dialog.Add(new Dialogues("Yaven", "Ach tak, no víš Geralte tož já nejsem zrovna při penězích a po tom všem co se tu ve Wyzimě přihodilo. To taky moc nepomáhá. 50 by šlo?", 1, "Problém s příšerou", "Talk", "Yaven", false, "Kovářova zrůda", null, false));
            dialog.Add(new Dialogues("Geralt", "Dobře, souhlasím. Tak, kde že je ta tvoje 'stvůra'? ", 1, "Problém s příšerou", "Talk", "Yaven", false, "Kovářova zrůda", null, false));
            dialog.Add(new Dialogues("Geralt", "Tož tadydhlenc ve sklepě, když dáš pryč ty bedny a to harampádí", 1, "Problém s příšerou", "Talk", "Yaven", false, "Kovářova zrůda", null, false));
            dialog.Add(new Dialogues("Geralt", "Dobře, ale peníze měj připravené, protože přežiju. Zachvíli jsem zpět. Pokud by jsem se však po 5 minutách nevrátil, tak ty bedny dej radši zpátky", 1, "Problém s příšerou", "Talk", "Yaven", false, "Kovářova zrůda", null, false));

            //2
            dialog.Add(new Dialogues("Geralt", "Ghůl je mrtvý.", 2, "Odměna za Ghůla", "Talk", "Yaven", false, "Kovářova zrůda", null, false));
            dialog.Add(new Dialogues("Geralt", "*Hází hlavu Ghůla na zem*", 2, "Odměna za Ghůla", "Talk", "Yaven", false, "Kovářova zrůda", null, false));
            dialog.Add(new Dialogues("Yaven", "U svaté melitelé tož dej tu zrůdnost pryč, však já ti věřím", 2, "Odměna za Ghůla", "Talk", "Yaven", false, "Kovářova zrůda", null, false));
            dialog.Add(new Dialogues("Yaven", "Díky ti za pomoc Geralt. Tož konečně možu jít zase do sklepa pro posledné věci, Kdybys něco potřeboval, tak přijď ještě mi tu nějaké věci zůstali", 2, "Odměna za Ghůla", "Talk", "Yaven", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Yaven", "Ach a tadyhlenc je těch tvejch 50 orénů", 2, "Odměna za Ghůla", "Talk", "Yaven", false, "Kovářova zrůda", null, false));

            //3
            dialog.Add(new Dialogues("Geralt", "Prodáváš tu stále ještě něco?", 3, "Obchod", "Talk", "Yaven", true, null, null, false));
            dialog.Add(new Dialogues("Yaven", "No tož Geralt... Ono není komu prodávat hehe.", 3, "Obchod", "Talk", "Yaven", true, null, null, false));
            dialog.Add(new Dialogues("Yaven", "Tož občas tedy někdo přivalcuje, ale to spíš chtějí jen mít kde přespat.", 3, "Obchod", "Talk", "Yaven", true, null, null, false));
            dialog.Add(new Dialogues("Yaven", "Já už sa tu chystám na odjezd, jako každý. Tož nemá tu cenu zůstat.", 3, "Obchod", "Talk", "Yaven", true, null, null, false));
            dialog.Add(new Dialogues("Yaven", "Ale Geralt pokud bys měl o jakejsi nástroj zájem, tak mi to pořád něco zbylo. ", 3, "Obchod", "Talk", "Yaven", true, null, null, false));
            dialog.Add(new Dialogues("Geralt", "Dobře, rád se někdy podívám", 3, "Obchod", "Talk", "Yaven", true, null, null, false));

            //leave
            dialog.Add(new Dialogues("Geralt", "Měj se Yavene", 2, "Nashle", "Talk", "Yaven", true, null, null, false));
            dialog.Add(new Dialogues("Yaven", "Tož dávaj na seba pozor Geralt!", 2, "Nashle", "Talk", "Yaven", true, null, null, false));

            string jsonToFile = JsonConvert.SerializeObject(dialog, settings);
            File.WriteAllText(prologue, jsonToFile);
        }
        public void CreateMonologue() {
            //monologue.Add(new Monologue(""));
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
            player.Add(new Player(100, 100, 25, 25, 50, 0, 0, 1000, 1, 1, 50, 0, 5, 2, 0, 0, 0, 0.5, steel, silver, ar, aard, igni, quen, axii, yrden));
            string jsonToFile = JsonConvert.SerializeObject(player, settings);
            File.WriteAllText(playerpath, jsonToFile);
        }
        public void CreateQuests() {
            qq.Add(new Quest (1, "Primary", "Něco končí, něco začíná", "Foltest si předvolal Geralta hned druhý den potom co krála zachránil.", "Zajdi za Foltestem", 1, true, "Něco končí, něco začíná", null, 0 , 0));
            qq.Add(new Quest(2, "Primary", "Něco končí, něco začíná", "Foltest si předvolal Geralta hned druhý den potom co krále zachránil. Foltest Geraltovi oznámil, že by měl něco zjistit o vrahovi s pomocí Triss", "Zajdi za Triss a zjisti něco o vrahovi", 1, true, "Něco končí, něco začíná", "Zjistila jsi něco nového o tom vrahovi?", 0, 0));
            qq.Add(new Quest(3, "Primary", "Něco končí, něco začíná", "Foltest si předvolal Geralta hned druhý den potom co krále zachránil. Foltest Geraltovi oznámil, že by měl něco zjistit o vrahovi s pomocí Triss. Triss Geraltovi sdělila, že na to, aby zjistila, kdo byl vrah, tak potřebuje více času", "Počkej až se Triss dozví více o vrahovi", 1, true, "Něco končí, něco začíná", null, 0, 0));
            qq.Add(new Quest(4, "Primary", "Něco končí, něco začíná", "Foltest si předvolal Geralta hned druhý den potom co krále zachránil. Foltest Geraltovi oznámil, že by měl něco zjistit o vrahovi s pomocí Triss. Triss Geraltovi sdělila, že na to, aby zjistila, kdo byl vrah, tak potřebuje více času", "Vydej se za Triss a zjisti jestli už něco ví", 1, true, "Něco končí, něco začíná", "Získala jsi už nějaké informace o vrahovi?", 0, 0));
            qq.Add(new Quest(5, "Primary", "Něco končí, něco začíná", "Foltest si předvolal Geralta hned druhý den potom co krále zachránil. Foltest Geraltovi oznámil, že by měl něco zjistit o vrahovi s pomocí Triss. Triss Geraltovi sdělila, že na to, aby zjistila, kdo byl vrah, tak potřebuje více času. Triss Geraltovi sdělila, že vrah před smrtí mluvil s Lambertem přes megaskop(telekomunikační zařízení). Tím pádem se Geralt musí vydat na cestu, aby Lamberta našel", "Zajdi za Foltestem a řekni mu, že vyrážíš na cestu", 1, true, "Něco končí, něco začíná", "Mám informace o vrahovi", 0, 0));
            qq.Add(new Quest(6, "Primary", "Něco končí, něco začíná", "Foltest si předvolal Geralta hned druhý den potom co krále zachránil. Foltest Geraltovi oznámil, že by měl něco zjistit o vrahovi s pomocí Triss. Triss Geraltovi sdělila, že na to, aby zjistila, kdo byl vrah, tak potřebuje více času. Triss Geraltovi sdělila, že vrah před smrtí mluvil s Lambertem přes megaskop(telekomunikační zařízení). Tím pádem se Geralt musí vydat na cestu, aby Lamberta našel", "Pokud chceš vyrazit na cestu, tak zajdi za Triss", 1, true, "Něco končí, něco začíná", "Vyrazit na cestu", 0, 0));
            qq.Add(new Quest(7, "Primary", "Něco končí, něco začíná", "Foltest si předvolal Geralta hned druhý den potom co krále zachránil. Foltest Geraltovi oznámil, že by měl něco zjistit o vrahovi s pomocí Triss. Triss Geraltovi sdělila, že na to, aby zjistila, kdo byl vrah, tak potřebuje více času. Triss Geraltovi sdělila, že vrah před smrtí mluvil s Lambertem přes megaskop(telekomunikační zařízení). Tím pádem se Geralt musí vydat na cestu, aby Lamberta našel", "Úkol Dokončen", 1, true, "Něco končí, něco začíná", null, 500, 0));
            qq.Add(new Quest(1, "Primary", "Kovářova zrůda", "Triss řekla Geraltovi, aby pomohl svému starému známému Yavenovi Briggsovi", "Zajdi za kovářem do staré wyzimy a zjisti jaký má problém", 1, true, "Kovářova zrůda", "Problém s příšerou", 0, 0));
            qq.Add(new Quest(2, "Primary", "Kovářova zrůda", "Geralt se za kovářem vydal a kovář mu slibíl, že dostane 50 orénů za to, že nestvůru zabije.", "Jdi do sklepa zabít nestvůru", 1, true, "Kovářova zrůda", null, 0, 0));
            qq.Add(new Quest(3, "Primary", "Kovářova zrůda", "Triss řekla Geraltovi, aby pomohl svému starému známému Yavenovi Briggsovi. Geralt se za kovářem vydal a kovář mu slibíl, že dostane 50 orénů za to, že nestvůru zabije. Geralt po drsném souboji Ghůla zabil a může si dojít pro odměnu", "Vyzvedni si u Yavena odměnu za zabití Ghůla", 1, true, "Kovářova zrůda", "Odměna za Ghůla", 0, 0));
            qq.Add(new Quest(4, "Primary", "Kovářova zrůda", "Triss řekla Geraltovi, aby pomohl svému starému známému Yavenovi Briggsovi. Geralt se za kovářem vydal a kovář mu slibíl, že dostane 50 orénů za to, že nestvůru zabije. Geralt po drsném souboji Ghůla zabil a může si dojít pro odměnu", "Úkol dokončen", 1, true, "Kovářova zrůda", "Odměna za Ghůla", 500, 50));

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
