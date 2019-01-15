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
using WpfAnimatedGif;

namespace WitcherWPF {
    /// <summary>
    /// Interakční logika pro Combat.xaml
    /// </summary>
    /// 
    public partial class Combat : Page {
        
        private Frame parentFrame;
        Player player = new Player(); 
        Enemy enemy;
        Dictionary<string, Uri> AnimationSets;
        Dictionary<string, Uri> EnemyAnimationSets = new Dictionary<string, Uri>();
        bool SteelSword;

        public Combat() {
            InitializeComponent();
            LoadEnemy();
            LoadPlayer();

        }
        public Combat(Frame parentFrame) : this() {
            this.parentFrame = parentFrame;
        }

        public void LoadPlayer() {
            PlayerHP.Content = player.maxHealth;
            PlayerStamina.Content = player.maxEndurance;
            PlayerToxicity.Content = player.toxicity;
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
                SwordAnims();
                SteelSword = true;
            }else if (e.Key == Key.E) {
                SwordAnims();
                SteelSword = false;
            }
        }
        public void SwordAnims() {
            if (SteelSword == true) {
                AnimationSets = player.SteelAnimationSets;
            }else {
                AnimationSets = player.SilverAnimationSets;
            }
            IdleAnimation();
        }
        private void PlayerAnimationEnd(object sender, RoutedEventArgs e) {
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
        }
        public void DrawSwordAnimation() {
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = AnimationSets["Draw"];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(Geralt, image);
            ImageBehavior.SetRepeatBehavior(Geralt, new RepeatBehavior(1));
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
            StrongAttackAnimation();
            int damage = player.Attack(SteelSword, true);
        }
        private void FastAttack(object sender, RoutedEventArgs e) {
            FastAttackAnimation();
            int damage = player.Attack(SteelSword, false);
        }
        public void EnemyIdleAnimation() {
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = EnemyAnimationSets["Idle"];
            image.EndInit();
            ImageBehavior.SetAnimatedSource(Geralt, image);
            ImageBehavior.SetRepeatBehavior(Geralt, RepeatBehavior.Forever);
        }
        public void SignEnd(object sender, RoutedEventArgs e) {

        }
        public void PotBut(object sender, RoutedEventArgs e) {

        }
        public void PotButClose(object sender, RoutedEventArgs e) {

        }
    }
}
