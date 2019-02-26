using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherWPF {
    class Game {
        public string CurrentLocation { get; set; }
        public string Chapter { get; set; }
        public bool CombatTutorial { get; set; }

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

        public void NewGame() {
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

            player.CreateDefaultPlayer();
            quest.CreateQuests();
            dialogue.CreateDialogues();
            sword.CreateSwords();
            armor.CreateArmor();
            skills.CreateSkills();
            pquest.CreatePlayerQuests();
            items.CreateItems();
            potions.CreatePotions();
            characters.CreateCharacters();
        }
    }
    
}
