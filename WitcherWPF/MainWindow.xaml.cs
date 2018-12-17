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
        static string ipath = @"../../saves/GameItems.json";
        static JsonSerializerSettings settings = new JsonSerializerSettings {
            TypeNameHandling = TypeNameHandling.All
        };
        static string jsonFromFile = File.ReadAllText(ipath);
        //List<Item> items = JsonConvert.DeserializeObject<List<Item>>(jsonFromFile, settings);
        public MainWindow() {
            InitializeComponent();
            //CreateInv();
            //CreatePlayer();
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
        public void CreatePlayer() {
            string playerpath = @"../../saves/Player.json";
            List<Player> player = new List<Player>();
            player.Add(new Player(100, 100, 100, 100, 100, 100, 50, 10, 5, 5));
            string jsonToFile = JsonConvert.SerializeObject(player, settings);
            File.WriteAllText(playerpath, jsonToFile);
        }
    }
}
