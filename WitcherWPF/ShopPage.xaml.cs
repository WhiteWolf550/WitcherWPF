using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WitcherWPF
{
    /// <summary>
    /// Interakční logika pro ShopPage.xaml
    /// </summary>
    public partial class ShopPage : Page
    {
        private Frame parentFrame;
        private string character;
        private Time time;

        FileManager manager = new FileManager();
        List<PlayerInventory> pinventory = new List<PlayerInventory>();
        List<Shop> shops = new List<Shop>();
        List<Armor> armors = new List<Armor>();
        List<Sword> swords = new List<Sword>();
        PlayerInventory inventory = new PlayerInventory();
        Dictionary<MenuItem, PlayerInventory> buttonitems = new Dictionary<MenuItem, PlayerInventory>();
        public ShopPage()
        {
            InitializeComponent();
            pinventory = manager.LoadPlayerInventory();
            shops = manager.LoadShop();
            swords = manager.LoadSwords();
            armors = manager.LoadArmors();

            

        }
        public ShopPage(Frame parentFrame, Time time, string CharacterName) : this() {
            this.parentFrame = parentFrame;
            this.character = CharacterName;
            this.time = time;

            LoadPlayerInventory(true);
            LoadPlayerInventory(false);
            LoadShop();

        }
        public void LoadShop() {
            string sword = null;
            string armor = null;
            List<Shop> matches = shops.Where(s => s.Name == character).ToList();
            List<Item> items = manager.LoadItems();
            

            foreach(Shop item in matches) {
                if (item.Sword != null && item.Armor != null) {
                    armor = item.Armor;
                    sword = item.Sword;
                }
            }
            if (armor != null && sword != null) {
                loadShopSwordnArmor(sword, armor);
            }
            loadShopItems(matches);

            

        }
        private void loadShopItems(List<Shop> matches) {
            foreach (Shop item in matches) {

                foreach (Item item2 in item.Items) {
                    Image inventoryimage = new Image();
                    inventoryimage.Width = 18;
                    inventoryimage.Height = 18;
                    inventoryimage.Source = new BitmapImage(new Uri(item2.Source, UriKind.Relative));
                    inventoryimage.Margin = new Thickness(-15, -3, -3, -3);
                    Button inventoryitem = new Button();
                    inventoryitem.Content = inventoryimage;
                    inventoryitem.Height = 20;
                    inventoryitem.Width = 20;
                    inventoryitem.ToolTip = item2.Name + "\n" + "1x" + "\n" + item2.Description + "\n" + "SUBSTANCE:" + "\n" + item2.Substance;
                    inventoryitem.BorderBrush = Brushes.Transparent;
                    inventoryitem.Background = Brushes.Transparent;
                    LootInventory.Children.Add(inventoryitem);
                }
            }
        }
        private void loadShopSwordnArmor(string sword, string armor) {
            List<Sword> matches = swords.Where(s => s.Name == sword).ToList();
            List<Armor> matches2 = armors.Where(s => s.Name == armor).ToList();
            foreach (Sword item in matches) {
                string orens = inventory.Orens(item.Price);
                Image ItemImage = new Image();
                ItemImage.Source = new BitmapImage(new Uri(item.Source, UriKind.Relative));
                ContextMenu cmenu = new ContextMenu();
                MenuItem equip = new MenuItem();
                equip.Header = "Koupit";
                equip.Click += BuyItem_Click;
                equip.Tag = item.Type;
                cmenu.Items.Add(equip);
                Button ItemButton = new Button();
                ItemButton.ContextMenu = cmenu;
                ItemButton.Content = ItemImage;
                ItemButton.ToolTip = item.Type + "\n" + "\n" + item.Name + "\n" + "\n" + item.Description + "\n" + "\n" + "Útočná síla: " + item.Damage + "\n" + "Šance na kritický zásah: " + item.CriticalHit + "%" + "\n" + "\n" + "Lze prodat za: " + orens;
                ItemButton.Height = 90;
                ItemButton.Width = 69;
                ItemButton.Background = Brushes.Transparent;
                ItemButton.BorderBrush = Brushes.Transparent;
                LootInventory.Children.Add(ItemButton);
            }
            foreach (Armor item in matches2) {
                string orens = inventory.Orens(item.Price);
                Image ItemImage = new Image();
                ItemImage.Source = new BitmapImage(new Uri(item.Source, UriKind.Relative));
                ContextMenu cmenu = new ContextMenu();
                MenuItem equip = new MenuItem();
                equip.Header = "Koupit";
                equip.Click += BuyItem_Click;
                equip.Tag = item.Type;
                cmenu.Items.Add(equip);
                Button ItemButton = new Button();
                ItemButton.ContextMenu = cmenu;
                ItemButton.Content = ItemImage;
                ItemButton.ToolTip = item.Type + "\n" + "\n" + item.Name + "\n" + "\n" + item.Description + "\n" + "\n" + "Zbroj: " + item.Armorvalue + "\n" + "Odolnost proti krvácení: " + item.Bleedingresistance + "%" + "\n" + "Odolnost proti otrávení: " + item.Poisonresistance + "%" + "\n" + "\n" + "Lze prodat za: " + orens;
                ItemButton.Height = 90;
                ItemButton.Width = 69;
                ItemButton.Background = Brushes.Transparent;
                ItemButton.BorderBrush = Brushes.Transparent;
                LootInventory.Children.Add(ItemButton);
            }
        }
        public void LoadPlayerInventory(bool InvLoad) {
            List<PlayerInventory> matches = new List<PlayerInventory>();
            if (pinventory != null) {
                if (InvLoad == true) {
                    InventoryItems.Children.Clear();
                    matches = pinventory.Where(s => s.Item.Type != "Alchemy").ToList();
                } else {
                    AlchemyItems.Children.Clear();
                    matches = pinventory.Where(s => s.Item.Type == "Alchemy").ToList();
                }

                if (pinventory != null) {
                    foreach (var item in matches) {
                        int p = item.Item.Price;
                        int sell = p / 2;
                        string orens = inventory.Orens(sell);
                        Image inventoryimage = new Image();
                        inventoryimage.Width = 18;
                        inventoryimage.Height = 18;
                        inventoryimage.Source = new BitmapImage(new Uri(item.Item.Source, UriKind.Relative));
                        inventoryimage.Margin = new Thickness(-15, -3, -3, -3);
                        ContextMenu cm = new ContextMenu();
                        MenuItem selli = new MenuItem();
                        selli.Header = "Prodat předmět";
                        selli.Click += SellItem_Click;
                        selli.Tag = item.Item.Name;
                        cm.Items.Add(selli);
                        Button inventoryitem = new Button();
                        inventoryitem.Content = inventoryimage;
                        inventoryitem.Height = 20;
                        inventoryitem.Width = 20;
                        inventoryitem.BorderBrush = Brushes.Transparent;
                        inventoryitem.ToolTip = item.Item.Name + "\n" + item.Item.Count + "x" + "\n" + item.Item.Description + "\n" + "SUBSTANCE:" + "\n" + item.Item.Substance + "\n" + "Lze prodat za: " + sell + " " + orens;
                        inventoryitem.ContextMenu = cm;
                        inventoryitem.Tag = item.Item.Action;
                        inventoryitem.Background = Brushes.Transparent;
                        if (InvLoad == true) {
                            InventoryItems.Children.Add(inventoryitem);
                        } else {
                            AlchemyItems.Children.Add(inventoryitem);
                        }
                        buttonitems.Add(selli, item);
                    }

                }
            }
        }
        private void SellItem_Click(object sender, RoutedEventArgs e) {

        }
        private void BuyItem_Click(object sender, RoutedEventArgs e) {

        }
        private void ExitShop_Click(object sender, RoutedEventArgs e) {
            parentFrame.Navigate(new Dialogue(parentFrame, character, time));
        }
    }
}

