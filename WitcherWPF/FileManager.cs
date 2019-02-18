using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherWPF {
    class FileManager {

        private JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

        public List<Player> LoadPlayer() {
            string path = @"../../saves/Player.json";
            string jsonFromFile = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<List<Player>>(jsonFromFile, settings);
            

        }
        public List<Effect> LoadEffects() {
            string path = @"../../saves/Effects.json";
            string jsonFromFile = "";
            try {
                jsonFromFile = File.ReadAllText(path);
                if (jsonFromFile != null) {
                    return JsonConvert.DeserializeObject<List<Effect>>(jsonFromFile, settings);
                }else {
                    return new List<Effect>();
                }
                
            } catch {

                return new List<Effect>();
            }
            
            


        }

        public List<Dialogues> LoadDialogue(string path) {

            string jsonFromFile = File.ReadAllText(path);
            List<Dialogues> jsonread = JsonConvert.DeserializeObject<List<Dialogues>>(jsonFromFile, settings);
            return jsonread;

        }
        public List<Quest> LoadQuests() {
            string path = @"../../gamefiles/Quests.json";
            string jsonFromFile = File.ReadAllText(path);
            List<Quest> jsonread = JsonConvert.DeserializeObject<List<Quest>>(jsonFromFile, settings);
            return jsonread;

        }
        public List<Skills> LoadSkills() {
            string path = @"../../saves/Skills.json";
            string jsonFromFile = File.ReadAllText(path);
            List<Skills> jsonread = JsonConvert.DeserializeObject<List<Skills>>(jsonFromFile, settings);
            return jsonread;

        }

        public List<PlayerQuest> LoadPlayerQuests() {
            string path = @"../../saves/PlayerQuests.json";
            string jsonFromFile = File.ReadAllText(path);
            List<PlayerQuest> jsonread = JsonConvert.DeserializeObject<List<PlayerQuest>>(jsonFromFile, settings);
            return jsonread;

        }
        public List<Item> LoadItems() {
            string path = @"../../gamefiles/GameItems.json";
            string jsonFromFile = File.ReadAllText(path);
            List<Item> jsonread = JsonConvert.DeserializeObject<List<Item>>(jsonFromFile, settings);
            return jsonread;

        }
        public List<Sword> LoadPlayerSwords() {
            string path = @"../../saves/PlayerSwords.json";
            string jsonFromFile = File.ReadAllText(path);
            List<Sword> jsonread = JsonConvert.DeserializeObject<List<Sword>>(jsonFromFile, settings);
            return jsonread;

        }
        public List<Armor> LoadPlayerArmors() {
            string path = @"../../saves/PlayerArmors.json";
            try {
                string jsonFromFile = File.ReadAllText(path);
                List<Armor> jsonread = JsonConvert.DeserializeObject<List<Armor>>(jsonFromFile, settings);
                return jsonread;
            } catch {
                File.Create(path);
                List<Armor> jsonread = new List<Armor>();
                return jsonread;
            }
            
            

        }
        public List<PlayerInventory> LoadPlayerInventory() {
            string path = @"../../saves/PlayerInventory.json";
            try {
                string jsonFromFile = File.ReadAllText(path);
                List<PlayerInventory> jsonread = JsonConvert.DeserializeObject<List<PlayerInventory>>(jsonFromFile, settings);
                return jsonread;
            }catch {
                List<PlayerInventory> jsonread = new List<PlayerInventory>();
                return jsonread;
            }

        }
        public List<Sign> LoadSigns() {
            string path = @"../../saves/PlayerSigns.json";
            string jsonFromFile = File.ReadAllText(path);
            List<Sign> jsonread = JsonConvert.DeserializeObject<List<Sign>>(jsonFromFile, settings);
            return jsonread;
        }


        public void SavePlayerQuests(List<PlayerQuest> PlayerQuests) {
            string path = @"../../saves/PlayerQuests.json";
            string jsonToFilet = JsonConvert.SerializeObject(PlayerQuests, settings);
            File.WriteAllText(path, jsonToFilet);
        }
        public void SaveDialogues(List<Dialogues> Dialogues, string path) {
            
            string jsonToFilet = JsonConvert.SerializeObject(Dialogues, settings);
            File.WriteAllText(path, jsonToFilet);
        }
        public void SaveSigns(List<Sign> Signs) {
            string path = @"../../saves/PlayerSigns.json";
            string jsonToFilet = JsonConvert.SerializeObject(Signs, settings);
            File.WriteAllText(path, jsonToFilet);
        }
        public void SaveItems(List<Item> Items, string path) {           
            string jsonToFilet = JsonConvert.SerializeObject(Items, settings);
            File.WriteAllText(path, jsonToFilet);
        }
        public void SavePlayerSwords(List<Sword> PlayerSwords) {
            string path = @"../../saves/PlayerSwords.json";
            string jsonToFilet = JsonConvert.SerializeObject(PlayerSwords, settings);
            File.WriteAllText(path, jsonToFilet);
        }
        public void SavePlayerArmor(List<Armor> PlayerArmors) {
            string path = @"../../saves/PlayerArmors.json";
            string jsonToFilet = JsonConvert.SerializeObject(PlayerArmors, settings);
            File.WriteAllText(path, jsonToFilet);
        }
        public void SavePlayer(List<Player> Player) {
            string path = @"../../saves/Player.json";
            string jsonToFilet = JsonConvert.SerializeObject(Player, settings);
            File.WriteAllText(path, jsonToFilet);
        }
        public void SavePlayerInventory(List<PlayerInventory> PlayerInventory) {
            string path = @"../../saves/PlayerInventory.json";
            string jsonToFilet = JsonConvert.SerializeObject(PlayerInventory, settings);
            File.WriteAllText(path, jsonToFilet);
        }
        public void SaveEffects(List<Effect> Effects) {
            string path = @"../../saves/Effects.json";
            string jsonToFilet = JsonConvert.SerializeObject(Effects, settings);
            File.WriteAllText(path, jsonToFilet);
        }
        public void SaveSkills(List<Skills> Skills) {
            string path = @"../../saves/Skills.json";
            string jsonToFilet = JsonConvert.SerializeObject(Skills, settings);
            File.WriteAllText(path, jsonToFilet);
        }
    }
}
