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


        private Frame parentFrame;
        Player player = new Player(); 
        Enemy enemy;
        Dictionary<string, Uri> AnimationSets;
        Dictionary<string, Uri> EnemyAnimationSets = new Dictionary<string, Uri>();
        bool SteelSword;
        bool Strong;
        bool SwordChosen = false;
        bool EnemyStrong;
        bool PlayerMoving = false;

        public Combat() {
            InitializeComponent();
            LoadEnemy();
            LoadPlayer();
            SetTimers();

        }
        public Combat(Frame parentFrame) : this() {
            this.parentFrame = parentFrame;
        }
        public void SetTimers() {
            PlayerStrongAttackDuration.Interval = new TimeSpan(0, 0, 0, 0, 800);
            PlayerStrongAttackDuration.Tick += new EventHandler(PlayerStrongDuration_Tick);

            PlayerFastAttackDuration.Interval = new TimeSpan(0, 0, 0, 0, 500);
            PlayerFastAttackDuration.Tick += new EventHandler(PlayerFastDuration_Tick);

            EnemyTimeToAttack.Interval = new TimeSpan(0, 0, 0, 0, 700);
            EnemyTimeToAttack.Tick += new EventHandler(EnemyToAttack_Tick);

            EnemyStrongAttackDuration.Interval = new TimeSpan(0, 0, 0, 0, 800);
            EnemyStrongAttackDuration.Tick += new EventHandler(EnemyStrongDuration_Tick);
        }
        void PlayerStrongDuration_Tick(object sender, EventArgs e) {
            PlayerStrongAttackDuration.Stop();
            EnemyTimeToAttack.Stop();
            EnemyHitAnimation();
        }
        void PlayerFastDuration_Tick(object sender, EventArgs e) {
            PlayerFastAttackDuration.Stop();
            EnemyTimeToAttack.Stop();
            EnemyHitAnimation();
        }
        void EnemyStrongDuration_Tick(object sender, EventArgs e) {

        }
        void EnemyToAttack_Tick(object sender, EventArgs e) {
            EnemyTimeToAttack.Stop();
            PlayerFastAttackDuration.Stop();
            PlayerStrongAttackDuration.Stop();
            Random rand = new Random();
            int rn = rand.Next(0, 100);
            if (rn < 60) {
                //fast
                EnemyStrongAttackAnimation();
            }else if (rn > 60) {

                EnemyStrongAttackDuration.Start();
            }
            
        }
        public void LoadPlayer() {
            player.LoadAttributes(HealthBar, EnduranceBar, ToxicityBar);
            NoSwordAnimation();

        }
        public void LoadEnemy() {
            enemy = new Murderer1();
            EnemyHP.Content = enemy.MaxHP;
            EnemyAnimationSets = enemy.AnimationSet;
            EnemyIdleAnimation();
            
        }
        public void Crossway(object sender, KeyEventArgs e) {
            if (e.Key == Key.Q) {
                SteelSword = true;
                SwordAnims();               
            }else if (e.Key == Key.E) {
                SteelSword = false;
                SwordAnims();
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
            EnemyTimeToAttack.Start();
            IdleAnimation();
        }
        private void EnemyAnimationEnd(object sender, RoutedEventArgs e) {
            EnemyIdleAnimation();
        }
        public void StrongAttackAnimation() {
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = AnimationSets["StrongAttack"];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(Geralt, image);
            ImageBehavior.SetRepeatBehavior(Geralt, new RepeatBehavior(1));
        }
        public void FastAttackAnimation() {
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = AnimationSets["FastAttack"];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(Geralt, image);
            ImageBehavior.SetRepeatBehavior(Geralt, new RepeatBehavior(1));
        }
        public void DeffendAnimation() {
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = AnimationSets["Deffend"];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(Geralt, image);
            ImageBehavior.SetRepeatBehavior(Geralt, new RepeatBehavior(1));
        }
        public void StaggerAnimation() {
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = AnimationSets["Stagger"];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(Geralt, image);
            ImageBehavior.SetRepeatBehavior(Geralt, new RepeatBehavior(1));
        }
        public void HitAnimation() {
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = AnimationSets["Hit"];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(Geralt, image);
            ImageBehavior.SetRepeatBehavior(Geralt, new RepeatBehavior(1));
            PlayerHit();
        }
        public void DrawSwordAnimation() {
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = AnimationSets["Draw"];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(Geralt, image);
            ImageBehavior.SetRepeatBehavior(Geralt, new RepeatBehavior(1));
            SwordChosen = true;
            

        }
        public void CastAardAnimation() {
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = AnimationSets["Aard"];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(Geralt, image);
            ImageBehavior.SetRepeatBehavior(Geralt, new RepeatBehavior(1));
        }
        public void CastIgniAnimation() {
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = AnimationSets["Igni"];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(Geralt, image);
            ImageBehavior.SetRepeatBehavior(Geralt, new RepeatBehavior(1));
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
        }
        public void CastYrdenAnimation() {
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = AnimationSets["Yrden"];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(Geralt, image);
            ImageBehavior.SetRepeatBehavior(Geralt, new RepeatBehavior(1));
        }
        public void IdleAnimation() {
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
            if (SwordChosen == true) {
                StrongAttackAnimation();
                Strong = true;
                PlayerStrongAttackDuration.Start();
            }
            
        }
        private void FastAttack(object sender, RoutedEventArgs e) {
            if (SwordChosen == true) {
                FastAttackAnimation();
                Strong = false;
                PlayerFastAttackDuration.Start();
            }
        }
        public void PlayerHit() {
            int damage = enemy.Attack(EnemyStrong);
            double PlayerHP = player.Hit(HealthBar.Value, damage);
            HealthBar.Value = PlayerHP;
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
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = EnemyAnimationSets["Strong"];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(Enemy, image);
            ImageBehavior.SetRepeatBehavior(Enemy, new RepeatBehavior(1));
            EnemyStrongAttack();
            
        }
        public void EnemyFastAttackAnimation() {
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = EnemyAnimationSets["Fast"];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(Enemy, image);
            ImageBehavior.SetRepeatBehavior(Enemy, new RepeatBehavior(1));
            EnemyFastAttack();
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
            HitAnimation();
        }
        public void EnemyFastAttack() {
            EnemyStrong = false;
            HitAnimation();
        }


        public void EnemyHit() {
            int damage = player.Attack(SteelSword, Strong);
            EnemyHP.Content = enemy.Hit(enemy.HP, damage);
            textb.Text += "Geralt dává poškození za " + damage;
        }
        public void SignEnd(object sender, RoutedEventArgs e) {

        }
        public void PotBut(object sender, RoutedEventArgs e) {

        }
        public void PotButClose(object sender, RoutedEventArgs e) {

        }
    }
}
