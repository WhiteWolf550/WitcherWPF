using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WitcherWPF
{
    class PlayerInventory
    {
        public Item Item { get; set; }

        FileManager manager = new FileManager();
        public PlayerInventory() {

        }
        public PlayerInventory(Item item) {
            this.Item = item;
            
        }
        public void GetItem() {

            List<Item> items = manager.LoadItems();
            List<PlayerInventory> inventory = manager.LoadPlayerInventory();
            Random rn = new Random();
            var matches = items.Where(s => s.Type == "Loot").ToList();
            int matchcount = matches.Count();
            int itemcount = rn.Next(0, 6);
            int rand = rn.Next(0, matchcount);
            matches[rand].Count = 1;
            inventory.Add(new PlayerInventory(matches[rand]));
            manager.SavePlayerInventory(inventory);
        }
        public void BuyItem(Item item, List<PlayerInventory> pinventory, int num) {
            List<PlayerInventory> items = pinventory.Where(s => s.Item.Name == item.Name).ToList();
            var match3 = items.Where(s => s.Item.Count < 10).ToList();
            if (items.Count > 0) {
                foreach (PlayerInventory item2 in items) {
                    if (item2.Item.Name == item.Name) {
                        if (item2.Item.Count == 10) {
                            if (match3.Count() == 0) {
                                PlayerInventory it = new PlayerInventory();
                                it.Item = item;
                                it.Item.Count = num;
                                pinventory.Add(it);
                            }else {

                            }
                        } else {
                            if (item2.Item.Count + num > 10) {
                                int rest = item2.Item.Count + num - 10;
                                item2.Item.Count = 10;
                                PlayerInventory it = new PlayerInventory();
                                it.Item = item;
                                it.Item.Count = rest;

                                pinventory.Add(it);
                                break;
                            } else {
                                item2.Item.Count += num;
                            }
                        }
                    }
                }
            } else {
                PlayerInventory item3 = new PlayerInventory();
                item3.Item = item;
                item3.Item.Count = num;
                pinventory.Add(item3);
            }
            manager.SavePlayerInventory(pinventory);
        }
        public List<PlayerInventory> DropItem(string buttonTag, List<PlayerInventory> inventory) {
            
            foreach(var item in inventory) {
                if (item.Item.Name == buttonTag) {
                    inventory.Remove(item);
                    break;
                }
            }
            return inventory;
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
