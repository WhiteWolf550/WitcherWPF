﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherWPF
{
    class Barghest : Enemy {

        
        public Barghest() {
            this.AnimationSet = new Dictionary<string, Uri>();
            this.AnimationSet.Add("Strong", new Uri(@"gifs\Barghest/barghest_fight_StrongAttack.gif", UriKind.Relative));
            this.AnimationSet.Add("Fast", new Uri("gifs/Barghest/barghest_fight_FastAttack.gif", UriKind.Relative));
            this.AnimationSet.Add("Deffend", new Uri("gifs/Barghest/barghest_fight_dodge.gif", UriKind.Relative));
            this.AnimationSet.Add("Stagger", new Uri("gifs/Barghest/barghest_fight_hit.gif", UriKind.Relative));
            this.AnimationSet.Add("Idle", new Uri("gifs/Barghest/barghest_fight_idle.gif", UriKind.Relative));
            this.AnimationSet.Add("Hit", new Uri("gifs/Barghest/barghest_fight_hit.gif", UriKind.Relative));
            this.AnimationSet.Add("Stun", new Uri(@"gifs/Barghest/barghest_fight_stunned.gif", UriKind.Relative));
            this.AnimationSet.Add("Death", new Uri("gifs/Barghest/barghest_fight_death.gif", UriKind.Relative));

            this.SoundSet = new Dictionary<string, Uri>();
            this.SoundSet.Add("Strong", new Uri(@"sounds/barghest/bar_Strong.wav", UriKind.Relative));
            this.SoundSet.Add("Fast", new Uri(@"sounds/barghest/bar_Fast.wav", UriKind.Relative));
            this.SoundSet.Add("Hit", new Uri(@"sounds/barghest/bar_hit.wav", UriKind.Relative));
            this.SoundSet.Add("Dodge", new Uri(@"sounds/barghest/bar_dodge.wav", UriKind.Relative));
            this.SoundSet.Add("Death", new Uri(@"sounds/barghest/bar_death.wav", UriKind.Relative));

            this.HurtSteelSword = false;
            this.MaxHP = 100;
            this.HP = this.MaxHP;
            this.Name = "Barghest";
            this.Class = "Dogs";
            this.XP = 30;

            this.StunChance = 10;
            this.DodgeChance = 40;
            this.BleedChance = 30;
            this.StrongSpeed = 700;
            this.FastSpeed = 300;
            this.StrongDamage = 23;
            this.FastChance = 60;
            this.FastDamage = 16;
            this.AttackInterval = 1000;


        }

        public override void EnemyBehavior(double PlayerHP, double PlayerHPMax) {
            if (this.HP < this.MaxHP / 2) {
                this.DodgeChance = 60;
                if (this.HP < this.MaxHP / 4) {
                    this.DodgeChance = 20;
                    this.FastChance = 10;
                    this.StrongDamage = 80;
                }
            }
            if (PlayerHP < PlayerHPMax / 2) {
                this.FastChance = 40;
            }
        }
    }
}
