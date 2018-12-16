using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherWPF
{
    class PlayerInventory
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Source { get; set; }
        public int Count { get; set; }

        public PlayerInventory(string Name, string Description, string Type, string Source, int Count) {
            this.Name = Name;
            this.Description = Description;
            this.Type = Type;
            this.Source = Source;
            this.Count = Count;
        }
        public void GetItem() {
            JsonSerializerSettings settings = new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.All
            };
            string ipath = @"../../saves/GameItems.json";
            string playerinvpath = @"../../saves/PlayerInventory.json";
            string jsonFromFile = File.ReadAllText(ipath);
            string jsonFromFileinv = File.ReadAllText(playerinvpath);
            List<Item> items = JsonConvert.DeserializeObject<List<Item>>(jsonFromFile, settings);
            List<PlayerInventory> inventory = JsonConvert.DeserializeObject<List<PlayerInventory>>(jsonFromFileinv, settings);
            Random rn = new Random();
            var matches = items.Where(s => s.Type == "Loot").ToList();
            int matchcount = matches.Count();
            int itemcount = rn.Next(0, 6);
            int rand = rn.Next(0, matchcount);
            inventory.Add(new PlayerInventory(matches[rand].Name, matches[rand].Description, matches[rand].Type, matches[rand].Source, itemcount));
            string jsonToFile = JsonConvert.SerializeObject(inventory, settings);
            File.WriteAllText(playerinvpath, jsonToFile);
        }
    }
}
