using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WitcherWPF {
    /// <summary>
    /// Interakční logika pro App.xaml
    /// </summary>
    public partial class App : Application {
        
        private void Application_Exit(object sender, ExitEventArgs e) {
            List<Game> game = new List<Game>();
            FileManager manager = new FileManager();
            game = manager.LoadGame();
            foreach (Game item in game) {
                item.Hour = Globals.Hour;
                item.Minute = Globals.Minute;
                item.Chapter = Globals.Chapter;
                item.DialoguePath = Globals.DialoguePath;
                item.CurrentLocation = Globals.location;
            }
            manager.SaveGame(game);
        }
    }
}
