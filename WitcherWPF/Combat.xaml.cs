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

        private Frame parentFrame;
        FileManager manager = new FileManager();
        Player player = new Player();
        List<Player> playerlist;
        Aard aard = new Aard();
        Enemy enemy;
        Yrden yrden = new Yrden();
        Dictionary<string, Uri> AnimationSets;
        Dictionary<string, Uri> EnemyAnimationSets = new Dictionary<string, Uri>();
        bool AttackBlock = false;
        int AttackCombo = 0;
        bool SteelSword;
        bool Strong;
        bool SwordChosen = false;
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

        public Combat() {
            InitializeComponent();

            LoadEnemy();
            LoadPlayer();
            SetTimers();

        }
        public void PageLoaded(object sender, RoutedEventArgs e) {
            Application.Current.MainWindow.KeyDown += new KeyEventHandler(Crossway);
        }
        public Combat(Frame parentFrame) : this() {
            this.parentFrame = parentFrame;
        }
        public void SetTimers() {
            int Yrdendur = 0;
            int Quendur = 0;
            int Axiidur = 0;
            int Channeldur = 0;
            foreach(Player item in playerlist) {
                Yrdendur = item.Yrden.Duration;
                Quendur = item.Quen.ShieldDuration;
                Axiidur = item.Axii.StunDuration;
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



        }
        void AfterParry_Tick(object sender, EventArgs e) {
            EnemyTimeToAttack.Start();
            Parry = false;
        }
        void PlayerStrongDuration_Tick(object sender, EventArgs e) {
            PlayerStrongAttackDuration.Stop();
            EnemyStrongAttackDuration.Stop();
            CanPlayerTouchSword = true;

            if (CanPlayerTouchSword == true && CanEnemyTouchSword == true) {
                StaggerAnimation();
                EnemyStaggerAnimation();
            }else {
                if (enemy.Dodge() == true && YrdenActive == false && AxiiActive == false) {
                    EnemyStrongAttackDuration.Stop();
                    EnemyFastAttackDuration.Stop();
                    EnemyTimeToAttack.Stop();
                    EnemyDeffendAnimation();
                }else {
                    EnemyHitAnimation();
                }
                
            }
            
        }
        void PlayerFastDuration_Tick(object sender, EventArgs e) {
            PlayerFastAttackDuration.Stop();
            EnemyStrongAttackDuration.Stop();
            if (enemy.Dodge() == true && YrdenActive == false && AxiiActive == false) {
                EnemyStrongAttackDuration.Stop();
                EnemyFastAttackDuration.Stop();
                EnemyTimeToAttack.Stop();
                EnemyDeffendAnimation();
            } else {
                EnemyHitAnimation();
            }
        }
        void EnemyStrongDuration_Tick(object sender, EventArgs e) {
            CanEnemyTouchSword = true;
            EnemyStrongAttackDuration.Stop();
            AxiiDuration.Stop();
            AxiiChannelingTime.Stop();
            PlayerStrongAttackDuration.Stop();
            PlayerFastAttackDuration.Stop();
            EnemyStrongAttack();
        }
        void EnemyFastDuration_Tick(object sender, EventArgs e) {
            CanEnemyTouchSword = true;
            EnemyFastAttackDuration.Stop();
            AxiiDuration.Stop();
            AxiiChannelingTime.Stop();
            PlayerStrongAttackDuration.Stop();
            PlayerFastAttackDuration.Stop();
            EnemyFastAttack();
        }
        void EnemyToAttack_Tick(object sender, EventArgs e) {
            EnemyTimeToAttack.Stop();
            
            Random rand = new Random();
            int rn = rand.Next(0, 100);
            if (rn < 60) {
                //fast
                
                EnemyFastAttackAnimation();
                
            }else if (rn > 60) {
                EnemyStrong = true;
                EnemyStrongAttackAnimation();
                
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
            foreach (Player item in playerlist) {
                stamina = item.endurance;
                maxstamina = item.maxEndurance;
                toxicity = item.toxicity;
                maxtoxicity = item.maxToxicity;
            }
            player.LoadAttributes(HealthBar, EnduranceBar, ToxicityBar);
            
            NoSwordAnimation();

        }
        public void LoadEnemy() {
            enemy = new Barghest();
            EnemyHP.Value = enemy.MaxHP;
            EnemyHP.ToolTip = enemy.MaxHP;
            EnemyName.Content = enemy.Name;
            EnemyAnimationSets = enemy.AnimationSet;
            EnemyIdleAnimation();
            
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
                        DeffendAnimation();
                    } else if (e.Key == Key.A) {
                        DodgeAnimation();
                    } else if (e.Key == Key.X && stamina >= AardEn) {
                        CastAardAnimation();
                    } else if (e.Key == Key.C && stamina >= IgniEn) {
                        CastIgniAnimation();
                    } else if (e.Key == Key.V && stamina >= QuenEn) {
                        CastQuenAnimation();
                    } else if (e.Key == Key.B && stamina >= YrdenEn) {
                        CastYrdenAnimation();
                    } else if (e.Key == Key.N && stamina >= AxiiEn) {
                        CastAxiiAnimation();
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
            DrawSwordAnimation();
        }
        private void PlayerAnimationEnd(object sender, RoutedEventArgs e) {
            
            PlayerAttacking = false;
            CanPlayerTouchSword = false;
            
            IdleAnimation();
        }
        private void EnemyAnimationEnd(object sender, RoutedEventArgs e) {
            EnemyAttacking = false;
            CanEnemyTouchSword = false;
            if (EnemyCheck() == true) {
                Enemy.Visibility = Visibility.Hidden;
                EnemyCanAttack = false;
                EnemyTimeToAttack.Stop();
                EnemyStrongAttackDuration.Stop();
                EnemyFastAttackDuration.Stop();
                EnemyHP.Value = 0;
                EnemyHP.ToolTip = 0;
            }else {
                EnemyCanAttack = true;
                EnemyIdleAnimation();

            }
            

        }
        public void StrongAttackAnimation() {
            PlayerAttacking = true;
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = AnimationSets["StrongAttack"];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(Geralt, image);
            ImageBehavior.SetRepeatBehavior(Geralt, new RepeatBehavior(1));
        }
        public void FastAttackAnimation() {
            PlayerAttacking = true;
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = AnimationSets["FastAttack"];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(Geralt, image);
            ImageBehavior.SetRepeatBehavior(Geralt, new RepeatBehavior(1));
        }
        public void DeffendAnimation() {
            PlayerAttacking = true;
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = AnimationSets["Deffend"];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(Geralt, image);
            ImageBehavior.SetRepeatBehavior(Geralt, new RepeatBehavior(1));
            Deffend();
        }
        public void DodgeAnimation() {
            PlayerAttacking = true;
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = AnimationSets["Dodge"];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(Geralt, image);
            ImageBehavior.SetRepeatBehavior(Geralt, new RepeatBehavior(1));
            Dodge();
        }
        public void StunnedAnimation() {
            //PlayerAttacking = true;
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = AnimationSets["Stunned"];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(Geralt, image);
            ImageBehavior.SetRepeatBehavior(Geralt, new RepeatBehavior(1));
        }
        public void StaggerAnimation() {
            PlayerAttacking = true;
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = AnimationSets["Stagger"];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(Geralt, image);
            ImageBehavior.SetRepeatBehavior(Geralt, new RepeatBehavior(1));
        }
        public void HitAnimation() {
            PlayerAttacking = true;
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = AnimationSets["Hit"];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(Geralt, image);
            ImageBehavior.SetRepeatBehavior(Geralt, new RepeatBehavior(1));
            EnemyCanAttack = false;
            PlayerHit();
        }
        public void DrawSwordAnimation() {
            PlayerAttacking = true;
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = AnimationSets["Draw"];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(Geralt, image);
            ImageBehavior.SetRepeatBehavior(Geralt, new RepeatBehavior(1));
            SwordChosen = true;
            

        }
        public void IgniAnimation() {
            
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = AnimationSets["IgniFX"];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(GIFSign, image);
            ImageBehavior.SetRepeatBehavior(GIFSign, new RepeatBehavior(1));
        }
        public void QuenAnimation() {
            GIFSelf.Visibility = Visibility.Visible;
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = AnimationSets["QuenFX"];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(GIFSelf, image);
            ImageBehavior.SetRepeatBehavior(GIFSelf, RepeatBehavior.Forever);
        }
        public void AxiiAnimation() {
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = AnimationSets["AxiiFX"];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(GIFSign, image);
            ImageBehavior.SetRepeatBehavior(GIFSign, RepeatBehavior.Forever);
            
        }
        public void YrdenAnimation() {
            GIFBehind.Visibility = Visibility.Visible;

            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = AnimationSets["YrdenFX"];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(GIFBehind, image);
            ImageBehavior.SetRepeatBehavior(GIFBehind, RepeatBehavior.Forever);
        }
        public void CastAardAnimation() {
            PlayerAttacking = true;
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = AnimationSets["Aard"];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(Geralt, image);
            ImageBehavior.SetRepeatBehavior(Geralt, new RepeatBehavior(1));
            Aard();
        }
        public void CastIgniAnimation() {
            PlayerAttacking = true;
            GIFSign.Visibility = Visibility.Visible;
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = AnimationSets["Igni"];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(Geralt, image);
            ImageBehavior.SetRepeatBehavior(Geralt, new RepeatBehavior(1));
            //IgniAnimation();
            
            Igni();
        }
        public void CastAxiiAnimation() {
            PlayerAttacking = true;
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = AnimationSets["Axii"];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(Geralt, image);
            ImageBehavior.SetRepeatBehavior(Geralt, RepeatBehavior.Forever);
            AxiiChannelingTime.Start();
        }
        public void CastQuenAnimation() {
            PlayerAttacking = true;
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = AnimationSets["Quen"];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(Geralt, image);
            ImageBehavior.SetRepeatBehavior(Geralt, new RepeatBehavior(1));
            Quen();
        }
        public void CastYrdenAnimation() {
            PlayerAttacking = true;
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = AnimationSets["Yrden"];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(Geralt, image);
            ImageBehavior.SetRepeatBehavior(Geralt, new RepeatBehavior(1));
            Yrden();
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
                AttackCombo++;
                ComboCheck();
                StrongAttackAnimation();
                Strong = true;
                PlayerStrongAttackDuration.Start();
            }
            
        }
        private void FastAttack(object sender, RoutedEventArgs e) {           
            if (SwordChosen == true && PlayerAttacking == false && AttackBlock == false) {
                AttackCombo++;
                ComboCheck();
                FastAttackAnimation();
                Strong = false;
                PlayerFastAttackDuration.Start();
            }
        }
        private void ComboCheck() {
            if (AttackCombo >= 3) {
                stamina -= 30;
                EnduranceBar.Value = stamina;
                EnduranceBar.ToolTip = EnduranceBar.Value;
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
            EnemyStrongAttackDuration.Stop();
            EnemyFastAttackDuration.Stop();
            EnemyTimeToAttack.Stop();
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
            EnemyCanAttack = true;
        }
        private void Deffend() {
            if (EnemyAttacking == true && EnemyStrong == false) {
                AttackCombo = 0;
                EnemyFastAttackDuration.Stop();
                EnemyStaggerAnimation();
            }
        }
        private void Dodge() {
            if (EnemyAttacking == true) {
                AttackCombo = 0;
                EnemyStrongAttackDuration.Stop();
                EnemyFastAttackDuration.Stop();

            }
        }
        private void Igni() {
            AttackCombo = 0;
            IgniAnim = true;
            stamina -= IgniEn;
            EnduranceBar.Value = stamina;
            EnduranceBar.ToolTip = stamina;
            Stamina.Start();
            EnemyHitAnimation();
        }
        private void Axii() {
            AttackCombo = 0;
            AxiiActive = true;
            stamina -= AxiiEn;
            EnduranceBar.Value = stamina;
            EnduranceBar.ToolTip = stamina;
            GIFSign.Visibility = Visibility.Visible;
            Stamina.Start();
            AxiiDuration.Start();
            AxiiAnimation();
        }
        private void Yrden() {
            AttackCombo = 0;
            YrdenActive = true;
            stamina -= YrdenEn;
            EnduranceBar.Value = stamina;
            EnduranceBar.ToolTip = stamina;
            Stamina.Start();
            YrdenAnimation();
            YrdenDuration.Start();
        }
        private void Quen() {
            AttackCombo = 0;
            QuenActive = true;
            stamina -= QuenEn;
            EnduranceBar.Value = stamina;
            EnduranceBar.ToolTip = stamina;
            Stamina.Start();
            QuenAnimation();
            QuenDuration.Start();
        }
        private void Aard() {
            AttackCombo = 0;
            bool isStunned = false;
            stamina -= AardEn;
            EnduranceBar.Value = stamina;
            EnduranceBar.ToolTip = stamina;
            Stamina.Start();
            int StunDur = 0;
            foreach(Player item in playerlist) {
                isStunned = item.Aard.Stun();
                StunDur = item.Aard.StunDuration;
            }
            
            if (isStunned == true) {
                StunDuration.Interval = new TimeSpan(0, 0, 0, StunDur);
                StunDuration.Tick += new EventHandler(Stun_Tick);

                EnemyStunAnimation();
            }else {
                EnemyStaggerAnimation();
            }
            
            
            
        }
        public void EnemyIdleAnimation() {
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = EnemyAnimationSets["Idle"];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(Enemy, image);
            ImageBehavior.SetRepeatBehavior(Enemy, RepeatBehavior.Forever);
            if (EnemyCanAttack == true && AxiiActive == false && Parry == false) {
                EnemyTimeToAttack.Start();
            }


        }
        public void EnemyHitAnimation() {
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = EnemyAnimationSets["Hit"];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(Enemy, image);
            ImageBehavior.SetRepeatBehavior(Enemy, new RepeatBehavior(1));
            EnemyHit();
        }
        public void EnemyStrongAttackAnimation() {
            EnemyAttacking = true;
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = EnemyAnimationSets["Strong"];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(Enemy, image);
            ImageBehavior.SetRepeatBehavior(Enemy, new RepeatBehavior(1));
            EnemyStrongAttackDuration.Start();
            
        }
        public void EnemyFastAttackAnimation() {
            EnemyAttacking = true;
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = EnemyAnimationSets["Fast"];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(Enemy, image);
            ImageBehavior.SetRepeatBehavior(Enemy, new RepeatBehavior(1));
            EnemyFastAttackDuration.Start();
        }
        public void EnemyDeffendAnimation() {
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = EnemyAnimationSets["Deffend"];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(Enemy, image);
            ImageBehavior.SetRepeatBehavior(Enemy, new RepeatBehavior(1));
        }
        public void EnemyStunAnimation() {
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = EnemyAnimationSets["Stun"];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(Enemy, image);
            ImageBehavior.SetRepeatBehavior(Enemy, RepeatBehavior.Forever);
        }
        public void EnemyStaggerAnimation() {
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = EnemyAnimationSets["Hit"];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(Enemy, image);
            ImageBehavior.SetRepeatBehavior(Enemy, new RepeatBehavior(1));
        }
        public void EnemyDeathAnimation() {
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = EnemyAnimationSets["Death"];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(Enemy, image);
            ImageBehavior.SetRepeatBehavior(Enemy, new RepeatBehavior(1));
        }
        public void EnemyStrongAttack() {
            EnemyStrong = true;
            //EnemyTimeToAttack.Start();
            HitAnimation();
        }
        public void EnemyFastAttack() {
            EnemyStrong = false;
            HitAnimation();
        }

        public bool EnemyCheck() {
            if (enemy.HP <= 0) {
                return true;
            }else {
                return false;
            }
        }
        public void EnemyHit() {
            EnemyStrongAttackDuration.Stop();
            EnemyFastAttackDuration.Stop();
            EnemyTimeToAttack.Stop();
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
                IgniAnimation();

            }
            
            if (EnemyCheck() == true) {
                EnemyDeathAnimation();
            }else {

            }
        }
        public void SignEnd(object sender, RoutedEventArgs e) {

        }
        public void PotBut(object sender, RoutedEventArgs e) {

        }
        public void PotButClose(object sender, RoutedEventArgs e) {

        }
    }
}
