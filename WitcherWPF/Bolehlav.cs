using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherWPF {
    class Bolehlav : Enemy {

        public Bolehlav() {
            this.AnimationSet = new Dictionary<string, Uri>();
            this.AnimationSet.Add("Strong", new Uri(@"gifs\NPC\npc_fight_strongattack.gif", UriKind.Relative));
            this.AnimationSet.Add("Fast", new Uri("gifs/NPC/npc_fight_fastattack.gif", UriKind.Relative));
            this.AnimationSet.Add("Deffend", new Uri("gifs/NPC/npc_fight_stagger.gif", UriKind.Relative));
            this.AnimationSet.Add("Stagger", new Uri("gifs/NPC/npc_fight_stagger.gif", UriKind.Relative));
            this.AnimationSet.Add("Idle", new Uri("gifs/NPC/npc_fight_idle.gif", UriKind.Relative));
            this.AnimationSet.Add("Hit", new Uri("gifs/NPC/npc_fight_hit.gif", UriKind.Relative));
            this.AnimationSet.Add("Stun", new Uri(@"gifs/NPC/npc_fight_strongattack.gif", UriKind.Relative));
            this.AnimationSet.Add("Death", new Uri("gifs/NPC/npc_fight_stagger.gif", UriKind.Relative));

            this.SoundSet = new Dictionary<string, Uri>();
            this.SoundSet.Add("Strong", new Uri(@"sounds/human/human_Strong.wav", UriKind.Relative));
            this.SoundSet.Add("Fast", new Uri(@"sounds/human/human_Fast.wav", UriKind.Relative));
            this.SoundSet.Add("Hit", new Uri(@"sounds/human/human_hit.wav", UriKind.Relative));
            this.SoundSet.Add("Dodge", new Uri(@"sounds/human/human_dodge.wav", UriKind.Relative));
            this.SoundSet.Add("Death", new Uri(@"sounds/human/human_death.wav", UriKind.Relative));

            this.HurtSteelSword = true;
            this.MaxHP = 200;
            this.HP = this.MaxHP;
            this.Name = "Bolehlav";
            this.Class = "Human";
            this.XP = 300;

            this.StunChance = 10;
            this.DodgeChance = 30;
            this.BleedChance = 10;
            this.StrongSpeed = 800;
            this.FastSpeed = 500;
            this.StrongDamage = 50;
            this.FastChance = 30;
            this.FastDamage = 38;
            this.AttackInterval = 1000;
        }

        public override void EnemyBehavior(double PlayerHP, double PlayerHPMax) {
            if (this.HP < this.MaxHP / 2) {
                this.DodgeChance = 50;
                if (this.HP < this.MaxHP / 4) {
                    this.DodgeChance = 20;
                    this.FastChance = 60;
                    this.FastDamage = 26;
                }
            }
            if (PlayerHP < PlayerHPMax / 2) {
                this.FastChance = 10;
            }
        }
    }
}
