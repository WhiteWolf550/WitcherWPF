using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherWPF {
    class Enemy {
        public Dictionary<string, Uri> AnimationSet { get; set; }
        public int MaxHP { get; set; }
        public int HP { get; set; }
        public string Name { get; set; }
        public int Damage { get; set; }
        public int StrongSpeed { get; set; }
        public int FastSpeed { get; set; }
        public bool HurtSteelSword { get; set; }
        public int StrongDamage { get; set; }
        public int FastDamage { get; set; }

        


    }
}
