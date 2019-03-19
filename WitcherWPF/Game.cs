using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherWPF {
    class Game {
        public string CurrentLocation { get; set; }
        public string DayTime { get; set; }
        public int Chapter { get; set; }
        public string DialoguePath { get; set; }
        public bool CombatTutorial { get; set; }
        public bool AlchemyTutorial { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public bool MayorDead { get; set; }

        FileManager manager = new FileManager();

        public Game(string CurrentLocation, int Chapter, bool CombatTutorial, bool AlchemyTutorial, int Hour, int Minute, string DialoguePath, string DayTime, bool MayorDead) {
            this.CurrentLocation = CurrentLocation;
            this.Chapter = Chapter;
            this.CombatTutorial = CombatTutorial;
            this.AlchemyTutorial = AlchemyTutorial;
            this.Hour = Hour;
            this.Minute = Minute;
            this.DialoguePath = DialoguePath;
            this.DayTime = DayTime;
            this.MayorDead = MayorDead;
        }
        public Game() {

        }
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
        public void CreateGame() {
            List<Game> game = new List<Game>();
            game.Add(new Game("Old_wyzima2", 0, true, true, 8, 0, @"../../dialogues/DialoguePrologue.json", "night", false));

            manager.SaveGame(game);
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
            Bestiary bestiary = new Bestiary();
            Game game = new Game();
            Shop shop = new Shop();
            Crypt crypts = new Crypt();

            items.CreateItems();
            quest.CreateQuests();
            dialogue.CreateDialogues();
            sword.CreateSwords();
            armor.CreateArmor();
            skills.CreateSkills();   
            potions.CreatePotions();
            characters.CreateCharacters();
            bestiary.CreateBestiary();

            player.CreateDefaultPlayer();
            pquest.CreatePlayerQuests();
            shop.CreateShops();
            game.CreateGame();
            crypts.CreateCrypts();

            File.Delete("../../saves/PlayerInventory.json");
            
        }
    }
    
}
