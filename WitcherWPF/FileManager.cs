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

        public List<PlayerQuest> LoadPlayerQuests() {
            string path = @"../../saves/PlayerQuests.json";
            string jsonFromFile = File.ReadAllText(path);
            List<PlayerQuest> jsonread = JsonConvert.DeserializeObject<List<PlayerQuest>>(jsonFromFile, settings);
            return jsonread;

        }
        public List<PlayerInventory> LoadPlayerInventory(string path) {

            string jsonFromFile = File.ReadAllText(path);
            List<PlayerInventory> jsonread = JsonConvert.DeserializeObject<List<PlayerInventory>>(jsonFromFile, settings);
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
    }
}
