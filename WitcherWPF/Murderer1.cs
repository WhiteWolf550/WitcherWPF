using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherWPF {
    class Murderer1 : Human {

        public Murderer1() {
            this.AnimationSet = new Dictionary<string, Uri>();
            this.AnimationSet.Add("Strong", new Uri(@"gifs\NPC\npc_fight_strongattack.gif", UriKind.Relative));
            this.AnimationSet.Add("Fast", new Uri("gifs/NPC/npc_fight_fastattack.gif", UriKind.Relative));
            this.AnimationSet.Add("Deffend", new Uri("gifs/NPC/npc_fight_deffend.gif", UriKind.Relative));
            this.AnimationSet.Add("Stagger", new Uri("gifs/NPC/npc_fight_stagger.gif", UriKind.Relative));
            this.AnimationSet.Add("Idle", new Uri("gifs/NPC/npc_fight_idle.gif", UriKind.Relative));
            this.AnimationSet.Add("Hit", new Uri("gifs/NPC/npc_fight_hit.gif", UriKind.Relative));
            this.AnimationSet.Add("Stun", new Uri("img/anim/bat_hurt.gif", UriKind.Relative));
            this.AnimationSet.Add("Death", new Uri("img/anim/bat_hurt.gif", UriKind.Relative));

            this.HurtSteelSword = true;
            this.MaxHP = 30;
            this.HP = this.MaxHP;
            this.Name = "Vrah";
            this.XP = 30;

            this.StunChance = 5;
            this.Bleedchance = 5;
            this.StrongSpeed = 900;
            this.FastSpeed = 350;
            this.StrongDamage = 12;
            this.FastDamage = 5;


             
        }
    }
}
