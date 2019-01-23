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


        private Frame parentFrame;
        FileManager manager = new FileManager();
        Player player = new Player();
        List<Player> playerlist;
        Aard aard = new Aard();
        Enemy enemy;
        Yrden yrden = new Yrden();
        Dictionary<string, Uri> AnimationSets;
        Dictionary<string, Uri> EnemyAnimationSets = new Dictionary<string, Uri>();
        bool SteelSword;
        bool Strong;
        bool SwordChosen = false;
        bool EnemyStrong;
        int i = 0;
        bool IgniAnim = false;
        bool QuenActive = false;
        bool YrdenActive = false;
        int QuenDurationTime = 0;
        bool PlayerAttacking = false;
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
            foreach(Player item in playerlist) {
                Yrdendur = item.Yrden.Duration;
                Quendur = item.Quen.ShieldDuration;
            }
            PlayerStrongAttackDuration.Interval = new TimeSpan(0, 0, 0, 0, 800);
            PlayerStrongAttackDuration.Tick += new EventHandler(PlayerStrongDuration_Tick);

            PlayerFastAttackDuration.Interval = new TimeSpan(0, 0, 0, 0, 500);
            PlayerFastAttackDuration.Tick += new EventHandler(PlayerFastDuration_Tick);

            EnemyTimeToAttack.Interval = new TimeSpan(0, 0, 0, 0, 1000);
            EnemyTimeToAttack.Tick += new EventHandler(EnemyToAttack_Tick);

            EnemyStrongAttackDuration.Interval = new TimeSpan(0, 0, 0, 0, 800);
            EnemyStrongAttackDuration.Tick += new EventHandler(EnemyStrongDuration_Tick);

            EnemyFastAttackDuration.Interval = new TimeSpan(0, 0, 0, 0, 500);
            EnemyFastAttackDuration.Tick += new EventHandler(EnemyFastDuration_Tick);

            BurnDuration.Interval = TimeSpan.FromSeconds(1);
            BurnDuration.Tick += new EventHandler(Burn_Tick);

            QuenDuration.Interval = new TimeSpan(0, 0, 0, Quendur);
            QuenDuration.Tick += new EventHandler(QuenDuration_Tick);

            YrdenDuration.Interval = new TimeSpan(0, 0, 0, Yrdendur);
            YrdenDuration.Tick += new EventHandler(YrdenDuration_Tick);



        }
        void PlayerStrongDuration_Tick(object sender, EventArgs e) {
            PlayerStrongAttackDuration.Stop();
            EnemyStrongAttackDuration.Stop();
            CanPlayerTouchSword = true;

            if (CanPlayerTouchSword == true && CanEnemyTouchSword == true) {
                StaggerAnimation();
                EnemyStaggerAnimation();
            }else {
                if (enemy.Dodge() == true && YrdenActive == false) {
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
            if (enemy.Dodge() == true && YrdenActive == false) {
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
            PlayerStrongAttackDuration.Stop();
            PlayerFastAttackDuration.Stop();
            EnemyStrongAttack();
        }
        void EnemyFastDuration_Tick(object sender, EventArgs e) {
            CanEnemyTouchSword = true;
            EnemyFastAttackDuration.Stop();
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

                EnemyStrongAttackAnimation();
                
            }
            
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
                EnemyHP.Content = enemy.HP;
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
        public void LoadPlayer() {
            player.LoadAttributes(HealthBar, EnduranceBar, ToxicityBar);
            playerlist = manager.LoadPlayer();
            NoSwordAnimation();

        }
        public void LoadEnemy() {
            enemy = new Murderer1();
            EnemyHP.Content = enemy.MaxHP;
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
                }else if (e.Key == Key.W) {
                    DeffendAnimation();
                }else if (e.Key == Key.A) {
                    DodgeAnimation();
                }else if (e.Key == Key.X) {
                    CastAardAnimation();
                }else if (e.Key == Key.C) {
                    CastIgniAnimation();
                }else if (e.Key == Key.V) {
                    CastQuenAnimation();
                }else if (e.Key == Key.B) {
                    CastYrdenAnimation();
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
                EnemyHP.Content = 0;
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
            ImageBehavior.SetRepeatBehavior(GIFSign, new RepeatBehavior(1));
            
        }
        public void YrdenAnimation() {
            GIFBehind.Visibility = Visibility.Visible;
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = AnimationSets["YrdenFX"];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(GIFSign, image);
            ImageBehavior.SetRepeatBehavior(GIFSign, RepeatBehavior.Forever);
        }
        public void CastAardAnimation() {
            
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = AnimationSets["Aard"];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(Geralt, image);
            ImageBehavior.SetRepeatBehavior(Geralt, new RepeatBehavior(1));
            Aard();
        }
        public void CastIgniAnimation() {
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
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = AnimationSets["Axii"];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(Geralt, image);
            ImageBehavior.SetRepeatBehavior(Geralt, new RepeatBehavior(1));
        }
        public void CastQuenAnimation() {
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = AnimationSets["Quen"];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(Geralt, image);
            ImageBehavior.SetRepeatBehavior(Geralt, new RepeatBehavior(1));
            Quen();
        }
        public void CastYrdenAnimation() {
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
            if (EnemyCanAttack == true) {
                EnemyTimeToAttack.Start();
            }


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
            if (SwordChosen == true && PlayerAttacking == false) {
                StrongAttackAnimation();
                Strong = true;
                PlayerStrongAttackDuration.Start();
            }
            
        }
        private void FastAttack(object sender, RoutedEventArgs e) {
            if (SwordChosen == true && PlayerAttacking == false) {
                FastAttackAnimation();
                Strong = false;
                PlayerFastAttackDuration.Start();
            }
        }
        private void PlayerHit() {
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
            if (EnemyAttacking == true) {
                EnemyStrongAttackDuration.Stop();
                EnemyFastAttackDuration.Stop();
                
            }
        }
        private void Dodge() {
            if (EnemyAttacking == true) {
                EnemyStrongAttackDuration.Stop();
                EnemyFastAttackDuration.Stop();

            }
        }
        private void Igni() {
            IgniAnim = true;
            EnemyHitAnimation();
        }
        private void Axii() {
            
        }
        private void Yrden() {
            YrdenActive = true;
            YrdenAnimation();
            YrdenDuration.Start();
        }
        private void Quen() {
            QuenActive = true;
            QuenAnimation();
            QuenDuration.Start();
        }
        private void Aard() {

            bool isStunned = false;
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
                EnemyHP.Content = enemy.Hit(enemy.HP, damage);
                textb.Text += "Geralt dává poškození za " + damage;
            }else {
                bool Burn = false;
                int damage = 0;
                foreach(Player item in playerlist) {
                    damage = item.Igni.Damage;
                    
                }   
                int hp = enemy.HP - damage;
                enemy.HP -= damage;
                EnemyHP.Content = hp;
                IgniAnim = false;
                textb.Text += "Geralt dává poškození";
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
