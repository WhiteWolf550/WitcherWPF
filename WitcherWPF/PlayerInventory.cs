using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WitcherWPF
{
    class PlayerInventory
    {
        public Item Item { get; set; }
        public int Count { get; set; }

        FileManager manager = new FileManager();
        public PlayerInventory() {

        }
        public PlayerInventory(Item item, int Count) {
            this.Item = item;
            this.Count = Count;
        }
        public void GetItem() {
            JsonSerializerSettings settings = new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.All
            };
            string ipath = @"../../gamefiles/GameItems.json";
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
        public void DropItem(MenuItem button) {
            JsonSerializerSettings settings = new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.All
            };
            string playerinvpath = @"../../saves/PlayerInventory.json";
            string jsonFromFileinv = File.ReadAllText(playerinvpath);
            List<PlayerInventory> inventory = JsonConvert.DeserializeObject<List<PlayerInventory>>(jsonFromFileinv, settings);
            foreach(var item in inventory) {
                if (item.Item.Name == button.Tag.ToString()) {
                    inventory.Remove(item);
                    break;
                }
            }
            string jsonToFile = JsonConvert.SerializeObject(inventory, settings);
            File.WriteAllText(playerinvpath, jsonToFile);
        }
        public void Read() {
            List<PlayerInventory> inventory = manager.LoadPlayerInventory();

        }
        public string Orens(int sell) {
            List<Sword> inventory = manager.LoadPlayerSwords();           
            if (sell == 1) {
                return "orén";
            } else if (sell > 1 && sell < 5) {
                return "orény";
            } else {
                return "orénů";
            }
        }
    }
}
