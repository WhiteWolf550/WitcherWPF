using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherWPF {
    class Game {
        public string CurrentLocation { get; set; }
        public string Chapter { get; set; }

        FileManager manager = new FileManager();

        public void SaveGame(List<Player> player, List<PlayerInventory> inventory, List<Armor> armor, List<Sword> sword, List<PlayerQuest> quest) {
            manager.SavePlayer(player);
            manager.SavePlayerArmor(armor);
            manager.SavePlayerQuests(quest);
            manager.SavePlayerSwords(sword);
            manager.SavePlayerInventory(inventory);
        }
        public void SaveGame(List<Player> player, List<PlayerInventory> inventory, List<Armor> armor, List<Sword> sword, List<Effect> effects) {
            manager.SavePlayer(player);
            manager.SavePlayerArmor(armor);
            manager.SavePlayerSwords(sword);
            manager.SavePlayerInventory(inventory);
            manager.SaveEffects(effects);
        }
    }
    
}
