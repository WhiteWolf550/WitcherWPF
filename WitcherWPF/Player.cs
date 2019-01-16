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

        public Dictionary<string, Uri> SteelAnimationSets { get; set; }
        public Dictionary<string, Uri> SilverAnimationSets { get; set; }
        public int maxHealth { get; set; }
        public int health { get; set; }
        public int maxEndurance { get; set; }
        public int endurance { get; set; }
        public int maxToxicity { get; set; }
        public int toxicity { get; set; }
        public int experience { get; set; }
        public int experiencetolevelup { get; set; }
        public int money { get; set; }
        public int strongStunChance { get; set; }
        public int fastStunChance { get; set; }
        public int signIntensity { get; set; }
        public int strongstyledamage { get; set; }
        public int faststyledamage { get; set; }
        public Sword SteelSword { get; set; }
        public Sword SilverSword { get; set; }
        public Armor Armor { get; set; }

        public FileManager manager;

        public Player() {
            this.SteelAnimationSets = new Dictionary<string, Uri>();
            this.SteelAnimationSets.Add("NoSword", new Uri("gifs/Geralt/geralt_fight_NoSword.gif", UriKind.Relative));
            //------------------------STEEL SWORD COMBAT ANIMATIONS----------------------------------------------
            this.SteelAnimationSets.Add("Idle", new Uri("gifs/Geralt/geralt_fight_idle.gif", UriKind.Relative));
            this.SteelAnimationSets.Add("StrongAttack", new Uri("gifs/Geralt/geralt_fight_attackSteel1.gif", UriKind.Relative));
            this.SteelAnimationSets.Add("FastAttack", new Uri("gifs/Geralt/geralt_fight_attackSteel2.gif", UriKind.Relative));
            this.SteelAnimationSets.Add("Deffend", new Uri("gifs/Geralt/geralt_fight_deffend.gif", UriKind.Relative));
            this.SteelAnimationSets.Add("Hit", new Uri("gifs/Geralt/geralt_fight_hit.gif", UriKind.Relative));
            this.SteelAnimationSets.Add("Draw", new Uri("gifs/Geralt/geralt_fight_DrawSteel.gif", UriKind.Relative));
            this.SteelAnimationSets.Add("Stagger", new Uri("gifs/Geralt/geralt_fight_stagger.gif", UriKind.Relative));
            this.SteelAnimationSets.Add("Drink", new Uri("gifs/Geralt/geralt_fight_drink.gif", UriKind.Relative));
            this.SteelAnimationSets.Add("Aard", new Uri("gifs/Geralt/geralt_fight_castAard.gif", UriKind.Relative));
            this.SteelAnimationSets.Add("Igni", new Uri("gifs/Geralt/geralt_fight_castIgni.gif", UriKind.Relative));
            this.SteelAnimationSets.Add("Axii", new Uri("gifs/Geralt/geralt_fight_castAxii.gif", UriKind.Relative));
            this.SteelAnimationSets.Add("Quen", new Uri("gifs/Geralt/geralt_fight_castQuen.gif", UriKind.Relative));
            this.SteelAnimationSets.Add("Yrden", new Uri("gifs/Geralt/geralt_fight_castYrden.gif", UriKind.Relative));
            //------------------------SILVER SWORD COMBAT ANIMATIONS----------------------------------------------
            this.SilverAnimationSets = new Dictionary<string, Uri>();
            this.SilverAnimationSets.Add("NoSword", new Uri("gifs/Geralt/geralt_fight_NoSword.gif", UriKind.Relative));
            this.SilverAnimationSets.Add("Idle", new Uri("gifs/Geralt/geralt_fight_Silveridle.gif", UriKind.Relative));
            this.SilverAnimationSets.Add("StrongAttack", new Uri("gifs/Geralt/geralt_fight_attackSilver1.gif", UriKind.Relative));
            this.SilverAnimationSets.Add("FastAttack", new Uri("gifs/Geralt/geralt_fight_attackSilver2.gif", UriKind.Relative));
            this.SilverAnimationSets.Add("Deffend", new Uri("gifs/Geralt/geralt_fight_Silverdeffend.gif", UriKind.Relative));
            this.SilverAnimationSets.Add("Hit", new Uri("gifs/Geralt/geralt_fight_Silverhit.gif", UriKind.Relative));
            this.SilverAnimationSets.Add("Draw", new Uri("gifs/Geralt/geralt_fight_DrawSilver.gif", UriKind.Relative));
            this.SilverAnimationSets.Add("Stagger", new Uri("gifs/Geralt/geralt_fight_Silverstagger.gif", UriKind.Relative));
            this.SilverAnimationSets.Add("Drink", new Uri("gifs/Geralt/geralt_fight_Silverdrink.gif", UriKind.Relative));
            this.SilverAnimationSets.Add("Aard", new Uri("gifs/Geralt/geralt_fight_SilvercastAard.gif", UriKind.Relative));
            this.SilverAnimationSets.Add("Igni", new Uri("gifs/Geralt/geralt_fight_SilvercastIgni.gif", UriKind.Relative));
            this.SilverAnimationSets.Add("Axii", new Uri("gifs/Geralt/geralt_fight_SilvercastAxii.gif", UriKind.Relative));
            this.SilverAnimationSets.Add("Quen", new Uri("gifs/Geralt/geralt_fight_SilvercastQuen.gif", UriKind.Relative));
            this.SilverAnimationSets.Add("Yrden", new Uri("gifs/Geralt/geralt_fight_SilvercastYrden.gif", UriKind.Relative));


        }

        public Player(int MaxHealth, int Health, int MaxEndurance, int Endurance, int MaxToxicity, int Toxicity, int experience, int experiencetolevelup, int Money, int StrongStunChance, int FastStunChance, int SignIntensity, int strongstyledamage, int faststyledamage, Sword SteelSword, Sword SilverSword, Armor Armor ) {
            this.maxHealth = MaxHealth;
            this.health = Health;
            this.maxEndurance = MaxEndurance;
            this.endurance = Endurance;
            this.maxToxicity = MaxToxicity;
            this.toxicity = Toxicity;
            this.experience = experience;
            this.experiencetolevelup = experiencetolevelup;
            this.money = Money;
            this.strongStunChance = StrongStunChance;
            this.fastStunChance = FastStunChance;
            this.signIntensity = SignIntensity;
            this.strongstyledamage = strongstyledamage;
            this.faststyledamage = faststyledamage;
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
        public bool Stun(bool StrongAttack) {
            List<Player> player = manager.LoadPlayer();
            int stunchance = 0;
            foreach(Player item in player) {
                if (StrongAttack == true) {
                    stunchance = item.strongStunChance;
                }else {
                    stunchance = item.fastStunChance;
                }
            }
            Random rand = new Random();
            int rn = rand.Next(0, 100);
            if (rn < stunchance) {
                return true;
            }else {
                return false;
            }
        }
        public bool Bleed(bool SteelSword) {
            List<Player> player = manager.LoadPlayer();
            int bleedchance = 0;
            foreach (Player item in player) {
                if (SteelSword == true) {
                    bleedchance = item.SteelSword.Bleedingchance;
                } else {
                    bleedchance = item.SilverSword.Bleedingchance;
                }
            }
            Random rand = new Random();
            int rn = rand.Next(0, 100);
            if (rn < bleedchance) {
                return true;
            } else {
                return false;
            }
        }
        public bool Poison(bool SteelSword) {
            List<Player> player = manager.LoadPlayer();
            int poisonchance = 0;
            foreach(Player item in player) {
                if (SteelSword == true) {
                    poisonchance = item.SteelSword.Poisonchance;
                }else {
                    poisonchance = item.SilverSword.Poisonchance;
                }
            }
            Random rand = new Random();
            int rn = rand.Next(0, 100);
            if (rn < poisonchance) {
                return true;
            } else {
                return false;
            }
        }
        public int Attack(bool SteelSword, bool StrongAttack) {
            List<Player> player = manager.LoadPlayer();
            int sworddamage = 0;
            int style = 0;
            foreach (Player item in player) {
                if (SteelSword == true) {
                    sworddamage = item.SteelSword.Damage;
                }else {
                    sworddamage = item.SilverSword.Damage;
                }
                if (StrongAttack == true) {
                    style = item.strongstyledamage;
                }else {
                    style = item.faststyledamage;
                }
            }
            Random rand = new Random();
            int damage = rand.Next(sworddamage + style - 5, sworddamage + style);
            return damage;
            
        }
        
        public void Deffend() {

        }
        public int Hit(int Health, int Damage) {
            int HP = Health - Damage;
            return HP;
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
