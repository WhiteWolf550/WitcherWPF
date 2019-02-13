using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherWPF {
    class Enemy {
        public Dictionary<string, Uri> AnimationSet { get; set; }
        public Dictionary<string, Uri> SoundSet { get; set; }
        public int MaxHP { get; set; }
        public int HP { get; set; }
        public string Name { get; set; }
        public int StrongSpeed { get; set; }
        public int FastSpeed { get; set; }
        public bool HurtSteelSword { get; set; }
        public int StrongDamage { get; set; }
        public int FastDamage { get; set; }
        public int XP { get; set; }
        public int StunChance { get; set; }
        public string ResistanceTo { get; set; }
        public int DodgeChance { get; set; }
        public int AttackInterval { get; set; }

        public int Hit(int EnemyHealth, int Damage ) {
            this.HP = EnemyHealth - Damage;
            return HP;
        }
        public int Attack(bool StrongAttack) {
            int damage = 0;
            if (StrongAttack == true) {
                damage = this.StrongDamage;
            }else {
                damage = this.FastDamage;
            }
            Random rand = new Random();
            int hitfor = rand.Next(damage, damage + 2);
            return hitfor;
        }
        public bool Dodge() {
            Random rand = new Random();
            int rn = rand.Next(0, 100);
            if (rn < this.DodgeChance) {
                return true;
            }else {
                return false;
            }
        }
    }
    
}
