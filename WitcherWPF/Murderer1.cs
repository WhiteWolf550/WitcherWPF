using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherWPF {
    class Murderer1 : Enemy {

        public Murderer1() {
            this.AnimationSet = new Dictionary<string, Uri>();
            this.AnimationSet.Add("Strong", new Uri(@"gifs\NPC\npc_fight_strongattack.gif", UriKind.Relative));
            this.AnimationSet.Add("Fast", new Uri("gifs/NPC/npc_fight_fastattack.gif", UriKind.Relative));
            this.AnimationSet.Add("Deffend", new Uri("gifs/NPC/npc_fight_stagger.gif", UriKind.Relative));
            this.AnimationSet.Add("Stagger", new Uri("gifs/NPC/npc_fight_stagger.gif", UriKind.Relative));
            this.AnimationSet.Add("Idle", new Uri("gifs/NPC/npc_fight_idle.gif", UriKind.Relative));
            this.AnimationSet.Add("Hit", new Uri("gifs/NPC/npc_fight_hit.gif", UriKind.Relative));
            this.AnimationSet.Add("Stun", new Uri(@"gifs/NPC/npc_fight_strongattack.gif", UriKind.Relative));
            this.AnimationSet.Add("Death", new Uri("gifs/NPC/npc_fight_stagger.gif", UriKind.Relative));

            this.HurtSteelSword = true;
            this.MaxHP = 100;
            this.HP = this.MaxHP;
            this.Name = "Vrah";
            this.XP = 30;

            this.StunChance = 5;
            this.DodgeChance = 80;
            this.BleedChance = 5;
            this.StrongSpeed = 800;
            this.FastSpeed = 500;
            this.StrongDamage = 12;
            this.FastDamage = 5;
            this.AttackInterval = 1000;


             
        }

        public override void EnemyBehavior(double PlayerHP, double PlayerHPMax) {
            throw new NotImplementedException();
        }
    }
}
