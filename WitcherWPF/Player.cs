using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using WpfAnimatedGif;

namespace WitcherWPF {
    class Player {
        public int maxHealth { get; set; }
        public int health { get; set; }
        public int maxEndurance { get; set; }
        public int endurance { get; set; }
        public int maxToxicity { get; set; }
        public int toxicity { get; set; }
        public int money { get; set; }
        public int strongStunChance { get; set; }
        public int fastStunChance { get; set; }
        public int signIntensity { get; set; }
        public Sword SteelSword { get; set; }
        public Sword SilverSword { get; set; }
        public Armor Armor { get; set; }
        

        public Player() {
            
            
        }

        public Player(int MaxHealth, int Health, int MaxEndurance, int Endurance, int MaxToxicity, int Toxicity, int Money, int StrongStunChance, int FastStunChance, int SignIntensity, Sword SteelSword, Sword SilverSword, Armor Armor ) {
            this.maxHealth = MaxHealth;
            this.health = Health;
            this.maxEndurance = MaxEndurance;
            this.endurance = Endurance;
            this.maxToxicity = MaxToxicity;
            this.toxicity = Toxicity;
            this.money = Money;
            this.strongStunChance = StrongStunChance;
            this.fastStunChance = FastStunChance;
            this.signIntensity = SignIntensity;
            this.SteelSword = SteelSword;
            this.SilverSword = SilverSword;
            this.Armor = Armor;
        }
        public void LoadAttributes(ProgressBar HealthBar, ProgressBar EnduranceBar, ProgressBar ToxicityBar, Label Orens) {
            string playerpath = @"../../saves/Player.json";
            JsonSerializerSettings settings = new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.All
            };
            string jsonFromFile = File.ReadAllText(playerpath);
            List<Player> playerinfo = JsonConvert.DeserializeObject<List<Player>>(jsonFromFile, settings);
            foreach (var item in playerinfo) {
                HealthBar.Maximum = item.maxHealth;
                HealthBar.Value = item.health;
                HealthBar.ToolTip = item.health + "/" + item.maxHealth; 

                EnduranceBar.Maximum = item.maxEndurance;
                EnduranceBar.Value = item.endurance;
                EnduranceBar.ToolTip = item.endurance + "/" + item.maxEndurance; 

                ToxicityBar.Maximum = item.maxToxicity;
                ToxicityBar.Value = item.toxicity;
                ToxicityBar.ToolTip = item.toxicity + "/" + item.maxToxicity;

                Orens.Content = item.money;
                Orens.ToolTip = "Orény";
            }
        }
        public void StrongAttack() {
            
        }
        public void FastAttack() {

        }
        public void Deffend() {

        }
        public void Hit() {

        }
        public void CastAard() {

        }
        public void CastIgni() {

        }
        public void CastQuen() {

        }
        public void CastAxii() {

        }
        public void CastYrden() {

        }
        
    }
}
