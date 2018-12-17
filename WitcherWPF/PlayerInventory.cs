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
        public Item Item { get; set; }
        public int Count { get; set; }

        public PlayerInventory(Item item, int Count) {
            this.Item = item;
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
            inventory.Add(new PlayerInventory(matches[rand], itemcount));
            string jsonToFile = JsonConvert.SerializeObject(inventory, settings);
            File.WriteAllText(playerinvpath, jsonToFile);
        }
    }
}
