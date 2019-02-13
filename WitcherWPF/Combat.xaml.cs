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
using System.Windows.Threading;
using WpfAnimatedGif;

namespace WitcherWPF {
    /// <summary>
    /// Interakční logika pro Combat.xaml
    /// </summary>
    /// 
    public partial class Combat : Page {
        
        DispatcherTimer PlayerStrongAttackDuration = new DispatcherTimer();
        DispatcherTimer PlayerFastAttackDuration = new DispatcherTimer();
        DispatcherTimer EnemyTimeToAttack = new DispatcherTimer();
        DispatcherTimer EnemyStrongAttackDuration = new DispatcherTimer();
        DispatcherTimer EnemyFastAttackDuration = new DispatcherTimer();
        DispatcherTimer StunDuration = new DispatcherTimer();
        DispatcherTimer BurnDuration = new DispatcherTimer();
        DispatcherTimer QuenDuration = new DispatcherTimer();
        DispatcherTimer YrdenDuration = new DispatcherTimer();
        DispatcherTimer AxiiDuration = new DispatcherTimer();
        DispatcherTimer AxiiChannelingTime = new DispatcherTimer();
        DispatcherTimer Stamina = new DispatcherTimer();
        DispatcherTimer PlayerTired = new DispatcherTimer();
        DispatcherTimer ParryDuration = new DispatcherTimer();
        DispatcherTimer SignChecker = new DispatcherTimer();

        MediaPlayer backgroundmedia = new MediaPlayer();
        MediaPlayer Enemymedia = new MediaPlayer();
        MediaPlayer Playermedia = new MediaPlayer();

        private Frame parentFrame;
        private Time time;
        
        private bool frominventory;
        FileManager manager = new FileManager();
        static Player player = new Player();
        static int CombatCount;

        List<Player> playerlist;
        List<Effect> Effects;
        Aard aard = new Aard();
        Item item = new Item();
        static Enemy enemy;
        Yrden yrden = new Yrden();
        static Dictionary<string, Uri> AnimationSets;
        static Dictionary<string, Uri> SoundsSet;
        static Dictionary<string, Uri> EnemyAnimationSets = new Dictionary<string, Uri>();
        static Dictionary<string, Uri> EnemySoundsSet = new Dictionary<string, Uri>();
        static string EnemName;
        static double EnemyHealthPoints;
        bool AttackBlock = false;
        int AttackCombo = 0;
        bool SteelSword;
        bool Strong;
        static bool SwordChosen = false;
        bool EnemyStrong;
        int i = 0;
        bool IgniAnim = false;
        bool QuenActive = false;
        bool YrdenActive = false;
        bool AxiiActive = false;
        int QuenDurationTime = 0;
        int AardEn = 0;
        int IgniEn = 0;
        int QuenEn = 0;
        int YrdenEn = 0;
        int AxiiEn = 0;
        int stamina = 0;
        int toxicity = 0;
        int maxtoxicity = 0;
        int maxstamina = 0;
        bool PlayerAttacking = false;
        bool Parry = false;
        bool EnemyAttacking = false;
        bool EnemyCanAttack = false;
        bool CanPlayerTouchSword = false;
        bool CanEnemyTouchSword = false;
        bool EnemyDodge = false;
        bool Stunned = false;

        public Combat() {
            InitializeComponent();

            

        }
        public void PageLoaded(object sender, RoutedEventArgs e) {
            Application.Current.MainWindow.KeyDown += new KeyEventHandler(Crossway);
        }
        public Combat(Frame parentFrame, bool frominventory, Time time) : this() {
            this.parentFrame = parentFrame;
            this.frominventory = frominventory;
            this.time = time;
            Deathmenu.Load.Click += new RoutedEventHandler(LoadGame);
            Deathmenu.Exit.Click += new RoutedEventHandler(ExitGame);
            StaminaCheck();
            LoadEnemy();
            LoadPlayer();
            LoadEffects();
            SetTimers();
            SignChecker.Start();
            if (frominventory == false) {
                SetMusic();
            }


        }
        public void SetMusic() {
            time.location.BattleMusic();
            backgroundmedia.Open(new Uri(@"../../sounds/music/Mighty.mp3", UriKind.Relative));
            backgroundmedia.Volume = 0.1;
            backgroundmedia.MediaEnded += new EventHandler(Music_Ended);
            backgroundmedia.Play();
        }
        public void GameOver() {
            backgroundmedia.Open(new Uri(@"../../sounds/misc/gameover.wav", UriKind.Relative));
            backgroundmedia.Volume = 1;
            backgroundmedia.MediaEnded -= new EventHandler(Music_Ended);
            backgroundmedia.Play();
        }
        private void Music_Ended(object sender, EventArgs e) {
            backgroundmedia.Position = TimeSpan.Zero;
            backgroundmedia.Play();
        }
        public void SetTimers() {
            int Yrdendur = 0;
            int Quendur = 0;
            int Axiidur = 0;
            int Aarddur = 0;
            int Channeldur = 0;
            foreach(Player item in playerlist) {
                Yrdendur = item.Yrden.Duration;
                Quendur = item.Quen.ShieldDuration;
                Axiidur = item.Axii.StunDuration;
                Aarddur = item.Aard.StunDuration;
                Channeldur = item.Axii.ChannelingTime;
                YrdenEn = item.Yrden.EnduranceCost;
                AardEn = item.Aard.EnduranceCost;
                QuenEn = item.Quen.EnduranceCost;
                IgniEn = item.Igni.EnduranceCost;
                AxiiEn = item.Axii.EnduranceCost;
            }
            PlayerStrongAttackDuration.Interval = new TimeSpan(0, 0, 0, 0, 800);
            PlayerStrongAttackDuration.Tick += new EventHandler(PlayerStrongDuration_Tick);

            PlayerFastAttackDuration.Interval = new TimeSpan(0, 0, 0, 0, 500);
            PlayerFastAttackDuration.Tick += new EventHandler(PlayerFastDuration_Tick);

            EnemyTimeToAttack.Interval = new TimeSpan(0, 0, 0, 0, enemy.AttackInterval);
            EnemyTimeToAttack.Tick += new EventHandler(EnemyToAttack_Tick);     

            EnemyStrongAttackDuration.Interval = new TimeSpan(0, 0, 0, 0, enemy.StrongSpeed);
            EnemyStrongAttackDuration.Tick += new EventHandler(EnemyStrongDuration_Tick);

            EnemyFastAttackDuration.Interval = new TimeSpan(0, 0, 0, 0, enemy.FastSpeed);
            EnemyFastAttackDuration.Tick += new EventHandler(EnemyFastDuration_Tick);

            BurnDuration.Interval = TimeSpan.FromSeconds(1);
            BurnDuration.Tick += new EventHandler(Burn_Tick);

            QuenDuration.Interval = new TimeSpan(0, 0, 0, Quendur);
            QuenDuration.Tick += new EventHandler(QuenDuration_Tick);

            YrdenDuration.Interval = new TimeSpan(0, 0, 0, Yrdendur);
            YrdenDuration.Tick += new EventHandler(YrdenDuration_Tick);

            AxiiDuration.Interval = new TimeSpan(0, 0, 0, Axiidur);
            AxiiDuration.Tick += new EventHandler(AxiiDuration_Tick);

            AxiiChannelingTime.Interval = new TimeSpan(0, 0, 0, Channeldur);
            AxiiChannelingTime.Tick += new EventHandler(AxiiChannel_Tick);

            Stamina.Interval = TimeSpan.FromSeconds(1);
            Stamina.Tick += new EventHandler(Stamina_tick);

            PlayerTired.Interval = new TimeSpan(0, 0, 0, 10);
            PlayerTired.Tick += new EventHandler(Tired_Tick);

            ParryDuration.Interval = new TimeSpan(0, 0, 0, 2);
            ParryDuration.Tick += new EventHandler(Parry_Tick);

            StunDuration.Interval = new TimeSpan(0, 0, 0, Aarddur);
            StunDuration.Tick += new EventHandler(Stun_tick);

            SignChecker.Interval = TimeSpan.FromSeconds(1);
            SignChecker.Tick += new EventHandler(Sign_tick);

        }
        void Stun_tick(object sender, EventArgs e) {
            StunDuration.Stop();
            EnemyIdleAnimation();

        }
        void Parry_Tick(object sender, EventArgs e) {
            ParryDuration.Stop();
            Parry = false;
            EnemyCanAttack = true;
            EnemyMain();
        }
        void Sign_tick(object sender, EventArgs e) {
            foreach(Player item in playerlist) {
                if (stamina >= item.Aard.EnduranceCost) {
                    Aard_ico.Visibility = Visibility.Visible;
                }else {
                    Aard_ico.Visibility = Visibility.Hidden;
                }

                if(stamina >= item.Igni.EnduranceCost) {
                    Igni_ico.Visibility = Visibility.Visible;
                }else {
                    Igni_ico.Visibility = Visibility.Hidden;
                }

                if (stamina >= item.Axii.EnduranceCost) {
                    Axii_ico.Visibility = Visibility.Visible;
                } else {
                    Axii_ico.Visibility = Visibility.Hidden;
                }

                if (stamina >= item.Quen.EnduranceCost) {
                    Quen_ico.Visibility = Visibility.Visible;
                } else {
                    Quen_ico.Visibility = Visibility.Hidden;
                }

                if (stamina >= item.Yrden.EnduranceCost) {
                    Yrden_ico.Visibility = Visibility.Visible;
                } else {
                    Yrden_ico.Visibility = Visibility.Hidden;
                }


            }
        }
        void PlayerStrongDuration_Tick(object sender, EventArgs e) {
            PlayerStrongAttackDuration.Stop();
            EnemyStrongAttackDuration.Stop();
            CanPlayerTouchSword = true;

            if (CanPlayerTouchSword == true && CanEnemyTouchSword == true) {
                PlayerAnimations("Stagger", Geralt);
                EnemyAnimations("Stagger");
            }else {
                if (EnemyDodge == true && YrdenActive == false && AxiiActive == false && Stunned == false) {
                    EnemySound("Dodge");
                    EnemyStrongAttackDuration.Stop();
                    EnemyFastAttackDuration.Stop();
                    EnemyTimeToAttack.Stop();
                    EnemyAnimations("Deffend");
                }else {

                    EnemyHit();
                }
                
            }
            
        }
        void PlayerFastDuration_Tick(object sender, EventArgs e) {
            PlayerFastAttackDuration.Stop();
            EnemyStrongAttackDuration.Stop();
            if (EnemyDodge == true && YrdenActive == false && AxiiActive == false && Stunned == false) {
                EnemySound("Dodge");
                EnemyStrongAttackDuration.Stop();
                EnemyFastAttackDuration.Stop();
                EnemyTimeToAttack.Stop();
                EnemyAnimations("Deffend");
            } else {
                Stunned = false;
                EnemyHit();
            }
        }
        void EnemyStrongDuration_Tick(object sender, EventArgs e) {
            CanEnemyTouchSword = true;
            EnemyStrongAttackDuration.Stop();
            AxiiDuration.Stop();
            AxiiChannelingTime.Stop();
            PlayerStrongAttackDuration.Stop();
            PlayerFastAttackDuration.Stop();
            EnemyAttack();
        }
        void EnemyFastDuration_Tick(object sender, EventArgs e) {
            CanEnemyTouchSword = true;
            EnemyFastAttackDuration.Stop();
            AxiiDuration.Stop();
            AxiiChannelingTime.Stop();
            PlayerStrongAttackDuration.Stop();
            PlayerFastAttackDuration.Stop();
            EnemyAttack();
        }
        void EnemyToAttack_Tick(object sender, EventArgs e) {
            EnemyTimeToAttack.Stop();
            
            Random rand = new Random();
            int rn = rand.Next(0, 100);
            if (rn < 60) {
                //fast
                EnemySound("Strong");
                EnemyFastAttack();
                
            }else if (rn > 60) {
                EnemySound("Strong");
                EnemyStrong = true;
                EnemyStrongAttack();
                
            }
            
        }
        void Stamina_tick(object sender, EventArgs e) {
            
            
            stamina += 1;
            
            if (stamina >= maxstamina) {
                stamina = maxstamina;
                Stamina.Stop();
            }
            EnduranceBar.Value = stamina;
            EnduranceBar.ToolTip = stamina;
        }
        void Stun_Tick(object sender, EventArgs e ) {
            StunDuration.Stop();
            Stunned = false;
            EnemyIdleAnimation();
        }
        void Burn_Tick(object sender, EventArgs e) {
            int dmg = 0;
            if (i == 5) {
                BurnDuration.Stop();
                i = 0;
            } else {
                i++;
                foreach (Player item in playerlist) {
                    dmg = item.Igni.BurnDamage;
                }
                enemy.HP = enemy.HP - dmg;
                EnemyHP.Value = enemy.HP;
                EnemyHP.ToolTip = enemy.HP;
            }
        }
        void QuenDuration_Tick(object sender, EventArgs e) {
            QuenDuration.Stop();
            PlayerSound("QuenB");
            GIFSelf.Visibility = Visibility.Hidden;
            QuenActive = false;
        }
        void YrdenDuration_Tick(object sender, EventArgs e) {
            YrdenDuration.Stop();
            GIFBehind.Visibility = Visibility.Hidden;
            YrdenActive = false;

        }
        void AxiiDuration_Tick(object sender, EventArgs e) {
            AxiiDuration.Stop();
            AxiiActive = false;
            GIFSign.Visibility = Visibility.Hidden;
            
        }
        void AxiiChannel_Tick(object sender, EventArgs e) {
            GIFSign.Visibility = Visibility.Visible;
            AxiiActive = true;
            EnemyFastAttackDuration.Stop();
            EnemyTimeToAttack.Stop();
            EnemyStrongAttackDuration.Stop();
            AxiiChannelingTime.Stop();
            IdleAnimation();
            Axii();
        }
        void Tired_Tick(object sender, EventArgs e) {
            PlayerTired.Stop();
            AttackBlock = false;
            AttackCombo = 0;
        }
        public void LoadPlayer() {
            playerlist = manager.LoadPlayer();
            Effects = manager.LoadEffects();
            SoundsSet = player.SoundsSet;
            foreach (Player item in playerlist) {
                stamina = item.endurance;
                maxstamina = item.maxEndurance;
                toxicity = item.toxicity;
                maxtoxicity = item.maxToxicity;
            }
            
            player.LoadAttributes(HealthBar, EnduranceBar, ToxicityBar);
            if (frominventory == false) {
                NoSwordAnimation();
            }else {
                if (SwordChosen == true && EnemyCheck() == false) {
                    EnemyTimeToAttack.Start();
                    
                    IdleAnimation();
                }else {
                    NoSwordAnimation();
                }
            }
        }
        public void LoadEffects() {
            foreach(Effect item in Effects) {
                Image img = new Image();
                img.Source = new BitmapImage(item.EffectIco[item.Name]);
                img.ToolTip = item.Name;
                EffectBar.Children.Add(img);
                UsePotion(item);

            }
            
        }
        public void LoadEnemy() {
            if (frominventory == false) {
                enemy = new Barghest();
                EnemyHP.Value = enemy.MaxHP;
                EnemyHP.ToolTip = enemy.MaxHP;
                EnemyName.Content = enemy.Name;            
            }else {
                EnemyHP.Value = EnemyHealthPoints;
                EnemyHP.ToolTip = EnemyHealthPoints;
                EnemyName.Content = EnemName;
                enemy.HP = Convert.ToInt32(EnemyHealthPoints);
            }
            EnemyAnimationSets = enemy.AnimationSet;
            EnemySoundsSet = enemy.SoundSet;
            if (EnemyCheck() != true) {
                EnemyIdleAnimation();
            }else {
                Enemy.Visibility = Visibility.Hidden;
                EnemyTimeToAttack.Stop();
                EnemyFastAttackDuration.Stop();
                EnemyStrongAttackDuration.Stop();
            }

        }
        public void Crossway(object sender, KeyEventArgs e) {
            if (PlayerAttacking == false) {
                if (e.Key == Key.Q) {
                    SteelSword = true;
                    SwordAnims();
                } else if (e.Key == Key.E) {
                    SteelSword = false;
                    SwordAnims();

                }
                if (SwordChosen == true && AttackBlock == false) {
                    if (e.Key == Key.W) {
                        Deffend();
                    } else if (e.Key == Key.A) {
                        Dodge();
                    } else if (e.Key == Key.X && stamina >= AardEn) {
                        Aard();
                    } else if (e.Key == Key.C && stamina >= IgniEn) {
                        Igni();
                    } else if (e.Key == Key.V && stamina >= QuenEn) {
                        PlayerSound("QuenA");
                        Quen();
                    } else if (e.Key == Key.B && stamina >= YrdenEn) {
                        Yrden();
                    } else if (e.Key == Key.N && stamina >= AxiiEn) {
                        PlayerAttacking = true;
                        AxiiChannelingTime.Start();
                        PlayerAnimationsRepeat("Axii", Geralt);
                    }
                }
            }
        }
        public void SwordAnims() {
            if (SteelSword == true) {
                AnimationSets = player.SteelAnimationSets;
            }else {
                AnimationSets = player.SilverAnimationSets;
            }
            PlayerAnimations("Draw", Geralt);
            PlayerAttacking = true;
            SwordChosen = true;
        }
        private void PlayerAnimationEnd(object sender, RoutedEventArgs e) {
            
            PlayerAttacking = false;
            CanPlayerTouchSword = false;
            PlayerDeath(true);

            
        }
        private void EnemyAnimationEnd(object sender, RoutedEventArgs e) {
            EnemyMain();
            

        }
        private void EnemyMain() {
            EnemyAttacking = false;
            CanEnemyTouchSword = false;
            EnemyDeath();
            PlayerDeath(false);
        }
        public void EnemyDeath() {
            if (EnemyCheck() == true) {
                Enemy.Visibility = Visibility.Hidden;
                EnemyCanAttack = false;
                EnemyTimeToAttack.Stop();
                EnemyStrongAttackDuration.Stop();
                EnemyFastAttackDuration.Stop();
                EnemyHP.Value = 0;
                EnemyHP.ToolTip = 0;
                AttackBlock = true;
                SkullLoot.Visibility = Visibility.Visible;
                CombatExit.Visibility = Visibility.Visible;
                player.AddXP(enemy.XP, playerlist);
            } else {
                if (Parry == false) {
                    EnemyCanAttack = true;
                }else {
                    EnemyCanAttack = false;
                }
                EnemyIdleAnimation();

            }
        }
        private void EnemyLoot(object sender, RoutedEventArgs e) {
            item.GenerateLoot(LootInventory, SkullLoot, LootBack, TakeLoot, CloseBut, enemy.Name);
        }
        private void CloseLoot(object sender, RoutedEventArgs e) {
            TakeLoot.Visibility = Visibility.Hidden;
            LootInventory.Visibility = Visibility.Hidden;
            LootBack.Visibility = Visibility.Hidden;
            CloseBut.Visibility = Visibility.Hidden;
            SkullLoot.Visibility = Visibility.Visible;
            LootInventory.Children.Clear();
        }
        private void LootToInventory(object sender, RoutedEventArgs e) {
            item.LootToInventory(LootInventory, TakeLoot, LootBack, CloseBut);
        }
        public void PlayerDeath(bool hide) {
            if (PlayerCheck() == true) {
                EnemyCanAttack = false;
                if (hide == true) {
                    Geralt.Visibility = Visibility.Hidden;
                    
                }
                EnemyTimeToAttack.Stop();
                EnemyStrongAttackDuration.Stop();
                EnemyFastAttackDuration.Stop();
                HealthBar.Value = 0;
                HealthBar.ToolTip = 0;
                Stamina.Stop();
            } else {
                if (hide == true) {
                    IdleAnimation();
                }
            }
        }
        public void EnemySound(string Key) {
            Enemymedia.Open(EnemySoundsSet[Key]);
            Enemymedia.Play();
        }
        public void PlayerSound(string Key) {
            Playermedia.Open(SoundsSet[Key]);
            Playermedia.Play();
        }

        public void PlayerAnimations(string Key, Image GIF) {
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = AnimationSets[Key];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(GIF, image);
            ImageBehavior.SetRepeatBehavior(GIF, new RepeatBehavior(1));
        }
        public void PlayerAnimationsRepeat(string Key, Image GIF) {
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = AnimationSets[Key];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(GIF, image);
            ImageBehavior.SetRepeatBehavior(GIF, RepeatBehavior.Forever);
        }
        public void IdleAnimation() {
            PlayerAttacking = false; 
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = AnimationSets["Idle"];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(Geralt, image);
            ImageBehavior.SetRepeatBehavior(Geralt, RepeatBehavior.Forever);
        }
        public void NoSwordAnimation() {
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = player.SteelAnimationSets["NoSword"];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(Geralt, image);
            ImageBehavior.SetRepeatBehavior(Geralt, RepeatBehavior.Forever);
        }
       


        private void StrongAttack(object sender, RoutedEventArgs e) {
            
            if (SwordChosen == true && PlayerAttacking == false && AttackBlock == false) {
                if (enemy.Dodge() == true && YrdenActive == false && AxiiActive == false) {
                    EnemyDodge = true;
                    PlayerSound("Strongm");
                }else {
                    EnemyDodge = false;
                    PlayerSound("Strong");
                }
                AttackCombo++;
                ComboCheck();
                PlayerAttacking = true;
                PlayerAnimations("StrongAttack", Geralt);
                Strong = true;
                PlayerStrongAttackDuration.Start();
            }
            
        }
        private void FastAttack(object sender, RoutedEventArgs e) {
            
            if (SwordChosen == true && PlayerAttacking == false && AttackBlock == false) {
                if (enemy.Dodge() == true && YrdenActive == false && AxiiActive == false) {
                    EnemyDodge = true;
                    PlayerSound("Fastm");
                } else {
                    EnemyDodge = false;
                    PlayerSound("Fast");
                }
                AttackCombo++;
                ComboCheck();
                PlayerAttacking = true;
                PlayerAnimations("FastAttack", Geralt);
                Strong = false;
                PlayerFastAttackDuration.Start();
            }
        }
        private void ComboCheck() {
            if (AttackCombo >= 3) {
                stamina -= 30;
                EnduranceBar.Value = stamina;
                EnduranceBar.ToolTip = EnduranceBar.Value;
                Stamina.Start();
                if (stamina <= 0) {
                    stamina = 0;
                    AttackBlock = true;
                    Stamina.Start();
                    PlayerTired.Start();
                }
            }
        }

        private void PlayerHit() {
            AttackCombo = 0;
            int damage = 0;
            int num = 0;
            double PlayerHP = 0;
            PlayerAttacking = true;
            EnemyStrongAttackDuration.Stop();
            EnemyFastAttackDuration.Stop();
            EnemyTimeToAttack.Stop();
            PlayerSound("Hit");
            PlayerAnimations("Hit", Geralt);
            if (QuenActive == true) {
                damage = enemy.Attack(EnemyStrong);
                foreach(Player item in playerlist) {
                    num = item.Quen.DamageReduction;
                }
                int decreaseddamage = damage * 1 - num;
                if (decreaseddamage < 0) {
                    decreaseddamage = 0;
                }
                PlayerHP = player.Hit(HealthBar.Value, decreaseddamage);
                QuenActive = false;
                QuenDuration.Stop();
                GIFSelf.Visibility = Visibility.Hidden;
                textb.Text += "Geralt dostává damage za " + decreaseddamage + "(" + damage + ")" + "\n";
                
            } else {
                damage = enemy.Attack(EnemyStrong);
                PlayerHP = player.Hit(HealthBar.Value, damage);             
            }
            HealthBar.Value = PlayerHP;
            HealthBar.ToolTip = PlayerHP;
            if (PlayerCheck() == true) {
                EnemyCanAttack = false;
                DeathScreenShow();
                GameOver();
                PlayerAnimations("Death", Geralt);

            }else {
                EnemyCanAttack = true;
            }
            
            

        }
        private void Deffend() {
            PlayerAttacking = true;
            PlayerSound("Parry");
            PlayerAnimations("Deffend", Geralt);
            if (EnemyAttacking == true && EnemyStrong == false) {
                
                AttackCombo = 0;
                EnemyCanAttack = false;
                Parry = true;
                ParryDuration.Start();
                EnemyFastAttackDuration.Stop();
                EnemyAnimations("Stagger");
            }
        }
        private void Dodge() {
            PlayerAttacking = true;
            PlayerAnimations("Dodge", Geralt);
            if (EnemyAttacking == true) {
                AttackCombo = 0;
                EnemyStrongAttackDuration.Stop();
                EnemyFastAttackDuration.Stop();

            }
        }
        private void Igni() {
            GIFSign.Visibility = Visibility.Visible;
            PlayerAttacking = true;
            PlayerSound("Igni");
            PlayerAnimations("Igni", Geralt);
            PlayerAnimations("IgniFX", GIFSign);
            AttackCombo = 0;
            IgniAnim = true;
            stamina -= IgniEn;
            EnduranceBar.Value = stamina;
            EnduranceBar.ToolTip = stamina;
            Stamina.Start();
            EnemyHit();
        }
        private void Axii() {
            PlayerAttacking = false;
            PlayerSound("Axii");
            AttackCombo = 0;
            AxiiActive = true;
            stamina -= AxiiEn;
            EnduranceBar.Value = stamina;
            EnduranceBar.ToolTip = stamina;
            GIFSign.Visibility = Visibility.Visible;
            Stamina.Start();
            PlayerAnimationsRepeat("AxiiFX", GIFSign);
            AxiiDuration.Start();
        }
        private void Yrden() {
            GIFBehind.Visibility = Visibility.Visible;
            PlayerAttacking = true;
            PlayerSound("Yrden");
            PlayerAnimations("Yrden", Geralt);
            AttackCombo = 0;
            YrdenActive = true;
            stamina -= YrdenEn;
            EnduranceBar.Value = stamina;
            EnduranceBar.ToolTip = stamina;
            Stamina.Start();
            PlayerAnimationsRepeat("YrdenFX", GIFBehind);
            YrdenDuration.Start();
        }
        private void Quen() {
            GIFSelf.Visibility = Visibility.Visible;
            PlayerAttacking = true;
            PlayerSound("QuenI");
            PlayerAnimations("Quen", Geralt);
            AttackCombo = 0;
            QuenActive = true;
            stamina -= QuenEn;
            EnduranceBar.Value = stamina;
            EnduranceBar.ToolTip = stamina;
            Stamina.Start();
            PlayerAnimationsRepeat("QuenFX", GIFSelf);
            QuenDuration.Start();
        }
        private void Aard() {
            PlayerAttacking = true;
            PlayerSound("Aard");
            PlayerAnimations("Aard", Geralt);
            EnemyTimeToAttack.Stop();
            EnemyStrongAttackDuration.Stop();
            EnemyFastAttackDuration.Stop();
            AttackCombo = 0;
            bool isStunned = false;
            stamina -= AardEn;
            EnduranceBar.Value = stamina;
            EnduranceBar.ToolTip = stamina;
            Stamina.Start();
            foreach(Player item in playerlist) {
                isStunned = item.Aard.Stun();
            }

            if (isStunned == true) {
                Stunned = true;
                StunDuration.Start();
                EnemyAnimationsRepeat("Stun");
            }else {
                PlayerAttacking = true;
                EnemyAnimations("Stagger");
            }
            
            
            
        }
        public void EnemyIdleAnimation() {
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = EnemyAnimationSets["Idle"];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(Enemy, image);
            ImageBehavior.SetRepeatBehavior(Enemy, RepeatBehavior.Forever);
            if (EnemyCanAttack == true && AxiiActive == false && Parry == false && EnemyCheck() == false) {
                EnemyTimeToAttack.Start();
            }
        }
        public void EnemyAnimations(string Key) {
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = EnemyAnimationSets[Key];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(Enemy, image);
            ImageBehavior.SetRepeatBehavior(Enemy, new RepeatBehavior(1));
        }
        public void EnemyAnimationsRepeat(string Key) {
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = EnemyAnimationSets[Key];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(Enemy, image);
            ImageBehavior.SetRepeatBehavior(Enemy, RepeatBehavior.Forever);
        }
        public void EnemyStrongAttack() {
            EnemyAttacking = true;
            EnemyStrong = true;
            EnemyAnimations("Strong");
            EnemyStrongAttackDuration.Start();
        }
        public void EnemyAttack() {
            PlayerHit();
        }
        public void EnemyFastAttack() {
            EnemyAttacking = true;
            EnemyStrong = false;
            EnemyAnimations("Fast");
            EnemyFastAttackDuration.Start();
        }

        public bool EnemyCheck() {
            if (enemy.HP <= 0) {
                return true;
            }else {
                return false;
            }
        }
        public bool PlayerCheck() {
            if (HealthBar.Value <= 0) {
                return true;
            }else {
                return false;
            }
        }
        public void EnemyHit() {
            StunDuration.Stop();
            EnemyStrongAttackDuration.Stop();
            EnemyFastAttackDuration.Stop();
            EnemyTimeToAttack.Stop();
            EnemySound("Hit");
            EnemyAnimations("Hit");
            if (IgniAnim != true) {
                int damage = player.Attack(SteelSword, Strong);
                if (SteelSword != enemy.HurtSteelSword) {
                    damage = damage / 3;
                    textb.Text = "reduce!";
                }
                EnemyHP.Value = enemy.Hit(enemy.HP, damage);
                EnemyHP.ToolTip = EnemyHP.Value;
                textb.Text = "Geralt dává poškození za " + damage;
            }else {
                bool Burn = false;
                int damage = 0;
                foreach(Player item in playerlist) {
                    damage = item.Igni.Damage;
                    

                }
                
                int hp = enemy.HP - damage;
                enemy.HP -= damage;
                EnemyHP.Value = hp;
                EnemyHP.ToolTip = EnemyHP.Value;
                IgniAnim = false;
                textb.Text = "Geralt dává poškození";
                foreach(Player item in playerlist) {
                    Burn = item.Igni.Burn();
                }
                if (Burn == true) {
                    
                    BurnDuration.Start();
                } else {

                }
                PlayerAnimations("IgniFX", GIFSign);

            }
            
            if (EnemyCheck() == true) {
                EnemySound("Death");
                EnemyAnimations("Death");
            }else {

            }
        }
        public void SignEnd(object sender, RoutedEventArgs e) {

        }
        private void PlayerLoad() {
            foreach(Player item in playerlist) {
                
                item.health = (int) HealthBar.Value;
                item.endurance = (int) EnduranceBar.Value;
                item.toxicity = (int)ToxicityBar.Value;
            }
        }
        public void StaminaCheck() {
            if(stamina < 100) {
                Stamina.Start();
            }
        }
        private void Inventory(object sender, RoutedEventArgs e) {
            PlayerLoad();
            manager.SavePlayer(playerlist);
            EnemyHealthPoints = EnemyHP.Value;
            EnemName = enemy.Name;
            Application.Current.MainWindow.KeyDown -= new KeyEventHandler(Crossway);
            parentFrame.Navigate(new Inventory(parentFrame, true, time));
            
        }
        public void UsePotion(Effect effect) {
            if (effect.Name == "Vlaštovka") {
                Heal();
            }
        }
        public void Heal() {

        }
        public void DeathScreenShow() {
            Deathmenu.Visibility = Visibility.Visible;
            var animation = new DoubleAnimation {
                To = 1,
                BeginTime = TimeSpan.FromSeconds(1),
                Duration = TimeSpan.FromSeconds(5),
                FillBehavior = FillBehavior.Stop
            };

            animation.Completed += (s, a) => Deathmenu.Visibility = Visibility.Visible;
            animation.Completed += (s, a) => Deathmenu.Opacity = 1;
            Deathmenu.BeginAnimation(UIElement.OpacityProperty, animation);
        }
        private void LoadGame(object sender, RoutedEventArgs e) {
            parentFrame.Navigate(new Combat(parentFrame, false, time));
        }
        private void ExitGame(object sender, RoutedEventArgs e) {
            System.Windows.Application.Current.Shutdown();
        }
        private void ExitCombat(object sender, RoutedEventArgs e) {
            Save();
            Application.Current.MainWindow.KeyDown -= new KeyEventHandler(Crossway);
            parentFrame.Navigate(new Location(parentFrame, time));
        }
        private void Save() {
            manager.SavePlayer(playerlist);
            manager.SaveEffects(Effects);
        }

    }
}
