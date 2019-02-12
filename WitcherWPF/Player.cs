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
        public Dictionary<string, Uri> SoundsSet { get; set; }
        public List<string> Effects = new List<string>();
        public int maxHealth { get; set; }
        public int health { get; set; }
        public int maxEndurance { get; set; }
        public int endurance { get; set; }
        public int maxToxicity { get; set; }
        public int toxicity { get; set; }
        public int experience { get; set; }
        public int experiencetolevelup { get; set; }
        public int skillpoints { get; set; }
        public int level { get; set; }
        public int money { get; set; }
        public int strongStunChance { get; set; }
        public int signIntensity { get; set; }
        public int strongstyledamage { get; set; }
        public int faststyledamage { get; set; }
        public Sword SteelSword { get; set; }
        public Sword SilverSword { get; set; }
        public Armor Armor { get; set; }
        public Aard Aard { get; set; }
        public Igni Igni { get; set; }
        public Quen Quen { get; set; }
        public Yrden Yrden { get; set; }
        public Axii Axii { get; set; }

        public FileManager manager = new FileManager();

        public Player() {
            this.SteelAnimationSets = new Dictionary<string, Uri>();
            this.SteelAnimationSets.Add("NoSword", new Uri("gifs/Geralt/geralt_fight_NoSword.gif", UriKind.Relative));

            //------------------------STEEL SWORD COMBAT ANIMATIONS----------------------------------------------
            this.SteelAnimationSets.Add("Idle", new Uri("gifs/Geralt/geralt_fight_idle.gif", UriKind.Relative));
            this.SteelAnimationSets.Add("StrongAttack", new Uri("gifs/Geralt/geralt_fight_attackSteel1.gif", UriKind.Relative));
            this.SteelAnimationSets.Add("FastAttack", new Uri("gifs/Geralt/geralt_fight_attackSteel2.gif", UriKind.Relative));
            this.SteelAnimationSets.Add("Deffend", new Uri("gifs/Geralt/geralt_fight_defend.gif", UriKind.Relative));
            this.SteelAnimationSets.Add("Dodge", new Uri("gifs/Geralt/geralt_fight_dodge.gif", UriKind.Relative));
            this.SteelAnimationSets.Add("Hit", new Uri("gifs/Geralt/geralt_fight_hit.gif", UriKind.Relative));
            this.SteelAnimationSets.Add("Finisher", new Uri("gifs/Geralt/geralt_fight_finish.gif", UriKind.Relative));
            this.SteelAnimationSets.Add("Stunned", new Uri("gifs/Geralt/geralt_fight_stunned.gif", UriKind.Relative));
            this.SteelAnimationSets.Add("Death", new Uri("gifs/Geralt/geralt_fight_death.gif", UriKind.Relative));
            this.SteelAnimationSets.Add("Draw", new Uri("gifs/Geralt/geralt_fight_DrawSteel.gif", UriKind.Relative));
            this.SteelAnimationSets.Add("Stagger", new Uri("gifs/Geralt/geralt_fight_stagger.gif", UriKind.Relative));
            this.SteelAnimationSets.Add("Drink", new Uri("gifs/Geralt/geralt_fight_drink.gif", UriKind.Relative));
            this.SteelAnimationSets.Add("Aard", new Uri("gifs/Geralt/geralt_fight_castAard.gif", UriKind.Relative));
            this.SteelAnimationSets.Add("Igni", new Uri("gifs/Geralt/geralt_fight_castIgni.gif", UriKind.Relative));
            this.SteelAnimationSets.Add("Axii", new Uri("gifs/Geralt/geralt_fight_castAxii.gif", UriKind.Relative));
            this.SteelAnimationSets.Add("Quen", new Uri("gifs/Geralt/geralt_fight_castQuen.gif", UriKind.Relative));
            this.SteelAnimationSets.Add("Yrden", new Uri("gifs/Geralt/geralt_fight_castYrden.gif", UriKind.Relative));
            this.SteelAnimationSets.Add("AardFX", new Uri("gifs/FX/Aard.gif", UriKind.Relative));
            this.SteelAnimationSets.Add("IgniFX", new Uri("gifs/FX/Igni.gif", UriKind.Relative));
            this.SteelAnimationSets.Add("AxiiFX", new Uri("gifs/FX/Axii.gif", UriKind.Relative));
            this.SteelAnimationSets.Add("QuenFX", new Uri("gifs/FX/Quen.gif", UriKind.Relative));
            this.SteelAnimationSets.Add("YrdenFX", new Uri("gifs/FX/Yrden.gif", UriKind.Relative));
            //------------------------SILVER SWORD COMBAT ANIMATIONS----------------------------------------------
            this.SilverAnimationSets = new Dictionary<string, Uri>();
            this.SilverAnimationSets.Add("NoSword", new Uri("gifs/Geralt/geralt_fight_NoSword.gif", UriKind.Relative));
            this.SilverAnimationSets.Add("Idle", new Uri("gifs/Geralt/geralt_fight_Silveridle.gif", UriKind.Relative));
            this.SilverAnimationSets.Add("StrongAttack", new Uri("gifs/Geralt/geralt_fight_attackSilver1.gif", UriKind.Relative));
            this.SilverAnimationSets.Add("FastAttack", new Uri("gifs/Geralt/geralt_fight_attackSilver2.gif", UriKind.Relative));
            this.SilverAnimationSets.Add("Deffend", new Uri("gifs/Geralt/geralt_fight_Silverdeffend.gif", UriKind.Relative));
            this.SilverAnimationSets.Add("Hit", new Uri("gifs/Geralt/geralt_fight_Silverhit.gif", UriKind.Relative));
            this.SilverAnimationSets.Add("Finisher", new Uri("gifs/Geralt/geralt_fight_Silverfinish.gif", UriKind.Relative));
            this.SilverAnimationSets.Add("Stunned", new Uri("gifs/Geralt/geralt_fight_Silverstunned.gif", UriKind.Relative));
            this.SilverAnimationSets.Add("Death", new Uri("gifs/Geralt/geralt_fight_Silverdeath.gif", UriKind.Relative));
            this.SilverAnimationSets.Add("Draw", new Uri("gifs/Geralt/geralt_fight_DrawSilver.gif", UriKind.Relative));
            this.SilverAnimationSets.Add("Dodge", new Uri("gifs/Geralt/geralt_fight_Silverdodge.gif", UriKind.Relative));
            this.SilverAnimationSets.Add("Drink", new Uri("gifs/Geralt/geralt_fight_Silverdrink.gif", UriKind.Relative));
            this.SilverAnimationSets.Add("Stagger", new Uri("gifs/Geralt/geralt_fight_Silverstagger.gif", UriKind.Relative));
            this.SilverAnimationSets.Add("Aard", new Uri("gifs/Geralt/geralt_fight_SilvercastAard.gif", UriKind.Relative));
            this.SilverAnimationSets.Add("Igni", new Uri("gifs/Geralt/geralt_fight_SilvercastIgni.gif", UriKind.Relative));
            this.SilverAnimationSets.Add("Axii", new Uri("gifs/Geralt/geralt_fight_SilvercastAxii.gif", UriKind.Relative));
            this.SilverAnimationSets.Add("Quen", new Uri("gifs/Geralt/geralt_fight_SilvercastQuen.gif", UriKind.Relative));
            this.SilverAnimationSets.Add("Yrden", new Uri("gifs/Geralt/geralt_fight_SilvercastYrden.gif", UriKind.Relative));
            this.SilverAnimationSets.Add("AardFX", new Uri("gifs/FX/Aard.gif", UriKind.Relative));
            this.SilverAnimationSets.Add("IgniFX", new Uri("gifs/FX/Igni.gif", UriKind.Relative));
            this.SilverAnimationSets.Add("AxiiFX", new Uri("gifs/FX/Axii.gif", UriKind.Relative));
            this.SilverAnimationSets.Add("QuenFX", new Uri("gifs/FX/Quen.gif", UriKind.Relative));
            this.SilverAnimationSets.Add("YrdenFX", new Uri("gifs/FX/Yrden.gif", UriKind.Relative));
            //------------------------COMBAT SOUNDS----------------------------------------------
            this.SoundsSet = new Dictionary<string, Uri>();
            this.SoundsSet.Add("Strong", new Uri("sounds/geralt/ger_Strong.wav", UriKind.Relative));
            this.SoundsSet.Add("Strongm", new Uri("sounds/geralt/ger_Strongm.wav", UriKind.Relative));
            this.SoundsSet.Add("Fast", new Uri("sounds/geralt/ger_Fast.wav", UriKind.Relative));
            this.SoundsSet.Add("Fastm", new Uri("sounds/geralt/ger_Fastm.wav", UriKind.Relative));
            this.SoundsSet.Add("Hit", new Uri("sounds/geralt/ger_hit.wav", UriKind.Relative));
            this.SoundsSet.Add("Parry", new Uri("sounds/geralt/ger_parry.wav", UriKind.Relative));
            this.SoundsSet.Add("Aard", new Uri("sounds/geralt/ger_aard.wav", UriKind.Relative));
            this.SoundsSet.Add("Igni", new Uri("sounds/geralt/ger_igni.wav", UriKind.Relative));
            this.SoundsSet.Add("Axii", new Uri("sounds/geralt/ger_axii.wav", UriKind.Relative));
            this.SoundsSet.Add("QuenA", new Uri("sounds/geralt/ger_quena.wav", UriKind.Relative));
            this.SoundsSet.Add("QuenI", new Uri("sounds/geralt/ger_queni.wav", UriKind.Relative));
            this.SoundsSet.Add("QuenB", new Uri("sounds/geralt/ger_quenb.wav", UriKind.Relative));
            this.SoundsSet.Add("Yrden", new Uri("sounds/geralt/ger_yrden.wav", UriKind.Relative));

        }

        public Player(int MaxHealth, int Health, int MaxEndurance, int Endurance, int MaxToxicity, int Toxicity, int experience, int experiencetolevelup, int skillpoints, int level, int Money, int StrongStunChance, int SignIntensity, int strongstyledamage, int faststyledamage, Sword SteelSword, Sword SilverSword, Armor Armor, Aard Aard, Igni Igni, Quen Quen, Axii Axii, Yrden Yrden ) {
            this.maxHealth = MaxHealth;
            this.health = Health;
            this.maxEndurance = MaxEndurance;
            this.endurance = Endurance;
            this.maxToxicity = MaxToxicity;
            this.toxicity = Toxicity;
            this.experience = experience;
            this.experiencetolevelup = experiencetolevelup;
            this.skillpoints = skillpoints;
            this.level = level;
            this.money = Money;
            this.strongStunChance = StrongStunChance;
            this.signIntensity = SignIntensity;
            this.strongstyledamage = strongstyledamage;
            this.faststyledamage = faststyledamage;
            this.SteelSword = SteelSword;
            this.SilverSword = SilverSword;
            this.Armor = Armor;
            this.Aard = Aard;
            this.Igni = Igni;
            this.Axii = Axii;
            this.Yrden = Yrden;
            this.Quen = Quen;
        }
        public void LoadAttributes(ProgressBar HealthBar, ProgressBar EnduranceBar, ProgressBar ToxicityBar) {
            List<Player> playerinfo = manager.LoadPlayer();
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

                

                
            }
        }
        public void LoadXP(ProgressBar XPBar, Label Level) {
            List<Player> playerinfo = manager.LoadPlayer();
            foreach (var item in playerinfo) {
                XPBar.Maximum = item.experiencetolevelup;
                XPBar.Value = item.experience;
                XPBar.ToolTip = item.experience + "/" + item.experiencetolevelup;

                Level.Content = item.level;
            }
        }
        public void LoadOrens(Label Orens) {
            List<Player> player = manager.LoadPlayer();
            foreach (Player oren in player) {
                Orens.Content = oren.money;
                Orens.ToolTip = "Orény";
            }

        }
        public bool Stun(bool StrongAttack) {
            bool stun = false;
            if (StrongAttack == true) {
                List<Player> player = manager.LoadPlayer();
                int stunchance = 0;
                foreach (Player item in player) {

                    stunchance = item.strongStunChance;

                }
                Random rand = new Random();
                int rn = rand.Next(0, 100);
                if (rn < stunchance) {
                    stun = true;
                } else {
                    stun = false;
                }
            }
            return stun;
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
        public double Hit(double Health, int Damage) {
            Health = Health - Damage;
            return Health;
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
