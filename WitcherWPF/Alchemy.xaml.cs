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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WitcherWPF
{
    /// <summary>
    /// Interakční logika pro Alchemy.xaml
    /// </summary>
    public partial class Alchemy : Page
    {
        private Frame parentFrame;
        private Time time;
        private bool meditation;
        FileManager manager = new FileManager();
        List<Potion> potions = new List<Potion>();
        List<Item> items = new List<Item>();
        List<PlayerInventory> playerinventory = new List<PlayerInventory>();
        Dictionary<Button, Potion> potiondict = new Dictionary<Button, Potion>();
        Dictionary<Image, PlayerInventory> itemsdict = new Dictionary<Image, PlayerInventory>();
        Potion potion = new Potion();
        int hour = 0;
        Music sound = new Music();
        Button CurrentPotion = new Button();
        public Alchemy()
        {
            InitializeComponent();
            Hour.Content = Globals.Hour + ":00";
            hour = Globals.Hour;
            ScrollBR.Value = Globals.Hour;
            sound.PlaySound("Alchemy");
        }
        public Alchemy(Frame parentFrame, Time time, bool meditation) : this() {
            this.parentFrame = parentFrame;
            this.time = time;
            this.meditation = meditation;
            potions = manager.LoadPotions();
            playerinventory = manager.LoadPlayerInventory();
            items = manager.LoadItems();

            LoadPotions();
        }
        public void GetInventory(object sender, RoutedEventArgs e) {
            parentFrame.Navigate(new Inventory(parentFrame, false, time, null, null));
            
        }
        public void GetQuests(object sender, RoutedEventArgs e) {
            parentFrame.Navigate(new Quests(parentFrame, time));
            
        }
        public void GetMap(object sender, RoutedEventArgs e) {
            parentFrame.Navigate(new Map(parentFrame, time));
            
        }
        public void GetJournal(object sender, RoutedEventArgs e) {
            parentFrame.Navigate(new Journal(parentFrame, time));
            
        }
        public void GetCharacter(object sender, RoutedEventArgs e) {
            parentFrame.Navigate(new Character(parentFrame, time));
        }
        public void GetLocation(object sender, RoutedEventArgs e) {
            parentFrame.Navigate(new Location(parentFrame, time));
            
        }
        public void LoadPotions() {
            
            foreach(Potion item in potions) {
                string fight = potion.PotionDurCheck(item.Duration);
                StackPanel panel = new StackPanel();
                panel.Orientation = Orientation.Horizontal;
                Image img = new Image();
                img.Width = 30;
                img.Height = 30;
                img.Source = new BitmapImage(new Uri(item.Icon, UriKind.Relative));
                Button button = new Button();
                button.Width = 170;
                button.Height = 50;
                button.Content = item.Name;
                button.Foreground = Brushes.WhiteSmoke;
                button.FontSize = 25;
                button.Background = Brushes.Transparent;
                button.BorderBrush = Brushes.Transparent;
                button.Click += new RoutedEventHandler(ChoosePotion);
                button.ToolTip = item.Description + "\n" + "Doba trvání: " + item.Duration + " " + fight;
                panel.Children.Add(img);
                panel.Children.Add(button);
                PotionPanel.Children.Add(panel);
                potiondict.Add(button, item);
                
            }
        }
        private void ChoosePotion(object sender, RoutedEventArgs e) {
            

            Button button = (sender as Button);
            AddPotionToTable(button);
        }
        public void ShowSubstances(string Substance) {
            Image img1 = new Image();
            img1.Source = new BitmapImage(new Uri(@"img/UI/" + Substance + ".png", UriKind.Relative));
            img1.ToolTip = Substance;
            img1.Margin = new Thickness(0,0,10,0);
            Substances.Children.Add(img1);
            
        }
        public void AddPotionToTable(Button button) {
            Ingredient1.Children.Clear();
            Ingredient2.Children.Clear();
            Ingredient3.Children.Clear();
            AlchemicalBase.Children.Clear();
            Substances.Children.Clear();
            bool it1 = false;
            bool it2 = false;
            bool it3 = false;
            bool it4 = false;
            Potion potionitem = potiondict[button];
            CurrentPotion = button;
            PlayerInventory item1 = new PlayerInventory();
            PlayerInventory item2 = new PlayerInventory();
            PlayerInventory item3 = new PlayerInventory();
            PlayerInventory item4 = new PlayerInventory();
            foreach (PlayerInventory item in playerinventory) {
                if (item.Item.Substance == potionitem.Ingredient1) {
                    item1 = item;
                    it1 = true;
                } else if (item.Item.Substance == potionitem.Ingredient2) {
                    item2 = item;
                    it2 = true;
                } else if (item.Item.Substance == potionitem.Ingredient3) {
                    item3 = item;
                    it3 = true;
                } else if (item.Item.Type == potionitem.PotionBase) {
                    item4 = item;
                    it4 = true;
                }
            }

            

            AddIngredient(item1, Ingredient1, it1, potionitem.Ingredient1);
            AddIngredient(item2, Ingredient2, it2, potionitem.Ingredient2);
            AddIngredient(item3, Ingredient3, it3, potionitem.Ingredient3);
            AddIngredient(item4, AlchemicalBase, it4, null);

            if (it1 == true && it2 == true && it3 == true && it4 == true) {
                PotionBrew.Opacity = 1;
            } else {
                PotionBrew.Opacity = 0.5;
            }


        }
        private void AddIngredient(PlayerInventory item, StackPanel Ingredient, bool isFull, string Substance) {
            if (isFull == true) {
                Image img1 = new Image();
                img1.Source = new BitmapImage(new Uri(item.Item.Source, UriKind.Relative));
                img1.ToolTip = item.Item.Name;
                Ingredient.Children.Add(img1);
                itemsdict.Add(img1, item);
                
            }
            if (Substance != null) {
                ShowSubstances(Substance);
            }
        }
        private void CreatePotionClick(object sender, RoutedEventArgs e) {
            if (PotionBrew.Opacity == 1) {
                if (meditation == true) {
                    CreatePotion();
                }else {
                    MessageBox.Show("Musíš začít meditovat, aby jsi mohl elixír vytvořit!");
                }
            }
        }
        private PlayerInventory GetItem(PlayerInventory Ing, StackPanel Ingredient) {
            foreach (Image item in Ingredient.Children) {
                Ing = itemsdict[item];
            }
            return Ing;
            
        }
        private void RemoveIngredientfromInventory(PlayerInventory pitem) {
            PlayerInventory removeitem = new PlayerInventory();
            bool remove = false;
            foreach(PlayerInventory item in playerinventory) {
                if (pitem == item) {
                    if (item.Item.Count > 1) {
                        item.Item.Count--;
                    }else if(item.Item.Count == 1) {
                        removeitem = item;
                        remove = true;
                    }
                }
            }
            if (remove == true) {
                playerinventory.Remove(removeitem);
            }
        }
        private void PotionToInventory() {
            bool addpotion = false;
            PlayerInventory potionitem = new PlayerInventory();
            foreach (Item item in items) {
                if (item.Name == CurrentPotion.Content.ToString()) {
                    item.Count = 1;
                    potionitem = new PlayerInventory(item);                   
                }
            }
            foreach (PlayerInventory item2 in playerinventory) {
                if (item2.Item.Name == CurrentPotion.Content.ToString()) {
                    item2.Item.Count++;
                    addpotion = false;
                    break;
                } else {
                    addpotion = true;
                }
            }
            if (addpotion == true) {
                playerinventory.Add(potionitem);
            }
        }
        public void CreatePotion() {
            PlayerInventory Ing1 = new PlayerInventory();
            PlayerInventory Ing2 = new PlayerInventory();
            PlayerInventory Ing3 = new PlayerInventory();
            PlayerInventory Base = new PlayerInventory();
            
            Ing1 = GetItem(Ing1, Ingredient1);
            Ing2 = GetItem(Ing2, Ingredient2);
            Ing3 = GetItem(Ing3, Ingredient3);
            Base = GetItem(Base, AlchemicalBase);

            RemoveIngredientfromInventory(Ing1);
            RemoveIngredientfromInventory(Ing2);
            RemoveIngredientfromInventory(Ing3);
            RemoveIngredientfromInventory(Base);

            PotionToInventory();

            manager.SavePlayerInventory(playerinventory);

            AddPotionToTable(CurrentPotion);

        }
        private void ScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            Hour.Content = Math.Round(ScrollBR.Value);
            hour = int.Parse(Hour.Content.ToString());
            Hour.Content += ":00";


        }

        private void StartMeditation_Click(object sender, RoutedEventArgs e) {
            
            MeditationTransitionShow();
        }
        public void MeditationTransitionShow() {
            BlackScreen.Visibility = Visibility.Visible;
            time.Visibility = Visibility.Hidden;
            var animation = new DoubleAnimation {
                To = 1,
                BeginTime = TimeSpan.FromSeconds(0),
                Duration = TimeSpan.FromSeconds(2),
                FillBehavior = FillBehavior.Stop
            };

            animation.Completed += (s, a) => BlackScreen.Visibility = Visibility.Visible;
            animation.Completed += (s, a) => BlackScreen.Opacity = 1;
            animation.Completed += new EventHandler(MeditationTransitionHide);
            animation.Completed += new EventHandler(ChangeTime);
            BlackScreen.BeginAnimation(UIElement.OpacityProperty, animation);
        }
        public void MeditationTransitionHide(object sender, EventArgs e) {
            var animation = new DoubleAnimation {
                To = 0,
                BeginTime = TimeSpan.FromSeconds(1),
                Duration = TimeSpan.FromSeconds(2),
                FillBehavior = FillBehavior.Stop
            };

            animation.Completed += (s, a) => BlackScreen.Visibility = Visibility.Hidden;
            animation.Completed += (s, a) => BlackScreen.Opacity = 0;
            
            BlackScreen.BeginAnimation(UIElement.OpacityProperty, animation);
            
        }
        private void ChangeTime(object sender, EventArgs e) {
            
            Globals.Hour = hour;
            time.Visibility = Visibility.Visible;
        }
    }
}
