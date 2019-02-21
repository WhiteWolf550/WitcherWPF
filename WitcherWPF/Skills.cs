using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace WitcherWPF
{
    class Skills
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public List<int> Values = new List<int>();
        public int Required { get; set; }
        public bool IsActive { get; set; }
        public bool UnlockNext { get; set; }

        FileManager manager = new FileManager();
        Music sound = new Music();
        public Skills(int ID, string Type, string Name, int Intensity, int Effectivity, int Endurance, int Value4, int Value5, int Value6, int Value7, int Required, bool IsActive, bool UnlockNext) {
            this.ID = ID;
            this.Type = Type;
            this.Name = Name;
            this.Values.Add(Intensity);
            this.Values.Add(Effectivity);
            this.Values.Add(Endurance);
            this.Values.Add(Value4);
            this.Values.Add(Value5);
            this.Values.Add(Value6);
            this.Values.Add(Value7);
            this.Required = Required;
            this.IsActive = IsActive;
            this.UnlockNext = UnlockNext;
        }
        public Skills() {

        }
        public void SetSkills(Dictionary<Skills, Button> skilldict, List<Skills> skills, List<Button> buttonlist, string Type, Dictionary<Button, Skills> skilldict2) {
            List<Skills> matches = skills.Where(s => s.Type == Type).ToList();
            var skillsAndButtons = matches.Zip(buttonlist, (n, w) => new { Skill = n, Button = w });
            foreach (var nw in skillsAndButtons) {
                skilldict.Add(nw.Skill, nw.Button);
                if (skilldict2.Count() < 17) {
                    skilldict2.Add(nw.Button, nw.Skill);
                }
            }
            //return skilldict;
            LoadSkills(skilldict, skills, Type);
        }
        public void LoadSkills(Dictionary<Skills, Button> skilldict, List<Skills> skills, string Type) {
            List<Skills> matches = skills.Where(s => s.Type == Type).ToList();
            foreach (Skills item in matches) {
                if (item.IsActive == false) {
                    skilldict[item].Opacity = 0.5;
                }
            }
        }
        public void UnlockSkills(Dictionary<Skills, Button> skilldict, List<Skills> skills, List<Button> buttonlist, string Type, Dictionary<Button, Skills> skilldict2, Button button, List<Player> player) {
            
            Skills skillitem = skilldict2[button];
            int skillpoints = 0;
            Skills skillitem2 = new Skills();
            Skills skillitemNext = skilldict2[button];
            List<Skills> matches = skills.Where(s => s.Type == Type).ToList();
            List<Skills> matches2 = matches.Where(s => s.ID == skillitem.Required).ToList();
            List<Skills> matches3 = matches.Where(s => s.ID == skillitem.ID + 1).ToList();

            
            foreach (Skills item in matches2) {
                skillitem2 = item;
            }
            foreach (Skills item in matches3) {
                skillitemNext = item;
            }
            foreach (Player item in player) {
                skillpoints = item.skillpoints;
            }


            if (skillitem.IsActive == false) {
                if (skillpoints > 0) {
                    if (skillitem2.IsActive == true) {
                        foreach (Skills item in skills) {
                            if (item.ID == skillitem.ID && item.Type == Type) {
                                item.IsActive = true;
                                skilldict[item].Opacity = 1;
                            }
                            if (skillitem.UnlockNext == true) {
                                //skillitem = skillitemNext;
                                if (item.ID == skillitemNext.ID  && item.Type == Type) {
                                    item.IsActive = true;
                                    skilldict[item].Opacity = 1;
                                }
                            }
                        }
                        TypeCheck(Type, player, skillitem);
                        
                        foreach(Player item in player) {
                            item.skillpoints -= 1;
                        }
                        sound.PlaySound("BuySkill");
                        manager.SavePlayer(player);
                        manager.SaveSkills(skills);
                    }else {
                        MessageBox.Show("Musíš mít předchozí dovednosti, aby jsi mohl dovednost odemknout");
                    }
                } else {
                    MessageBox.Show("Nemáš dostatek dovednostních bodů");
                }
            } else {
                MessageBox.Show("Tuto dovednost už máš");
            }
        }
        public void TypeCheck(string Type, List<Player> player, Skills skillitem) {
            if (Type == "Aard") {
                UnlockAard(player, skillitem);
            }else if (Type == "Igni") {
                UnlockIgni(player, skillitem);
            } else if (Type == "Axii") {
                UnlockAxii(player, skillitem);
            } else if (Type == "Yrden") {
                UnlockYrden(player, skillitem);
            } else if (Type == "Quen") {
                UnlockQuen(player, skillitem);
            } else if (Type == "Strength") {
                UnlockStrength(player, skillitem);
            } else if (Type == "Endurance") {
                UnlockEndurance(player, skillitem);
            }
        }
        public void UnlockAard(List<Player> player, Skills skillitem) {
            foreach (Player item in player) {
                item.Aard.SignIntensity = skillitem.Values[0];
                item.Aard.Effectivity = skillitem.Values[1];
                item.Aard.EnduranceCost = skillitem.Values[2];
                item.Aard.StunChance = skillitem.Values[3];
                item.Aard.StunDuration = skillitem.Values[4];
                item.Aard.KnockBackChance = skillitem.Values[5];
                

            }
        }
        public void UnlockIgni(List<Player> player, Skills skillitem) {
            foreach (Player item in player) {
                item.Igni.SignIntensity = skillitem.Values[0];
                item.Igni.Effectivity = skillitem.Values[1];
                item.Igni.EnduranceCost = skillitem.Values[2];
                item.Igni.BurnChance = skillitem.Values[3];
                item.Igni.BurnDuration = skillitem.Values[4];
                item.Igni.BurnDamage = skillitem.Values[5];
                item.Igni.Damage = skillitem.Values[6];

            }
        }
        public void UnlockQuen(List<Player> player, Skills skillitem) {
            foreach (Player item in player) {
                item.Quen.SignIntensity = skillitem.Values[0];
                item.Quen.Effectivity = skillitem.Values[1];
                item.Quen.EnduranceCost = skillitem.Values[2];
                item.Quen.ShieldDuration = skillitem.Values[3];
                item.Quen.DamageReduction = skillitem.Values[4];
                item.Quen.EffectsResistance = skillitem.Values[5];
                

            }
        }
        public void UnlockAxii(List<Player> player, Skills skillitem) {
            foreach (Player item in player) {
                item.Axii.SignIntensity = skillitem.Values[0];
                item.Axii.Effectivity = skillitem.Values[1];
                item.Axii.EnduranceCost = skillitem.Values[2];
                item.Axii.Duration = skillitem.Values[3];
                item.Axii.ChannelingTime = skillitem.Values[4];
                item.Axii.StatsDecrease = skillitem.Values[5];


            }
        }
        public void UnlockYrden(List<Player> player, Skills skillitem) {
            foreach (Player item in player) {
                item.Yrden.SignIntensity = skillitem.Values[0];
                item.Yrden.Effectivity = skillitem.Values[1];
                item.Yrden.EnduranceCost = skillitem.Values[2];
                item.Yrden.Duration = skillitem.Values[3];
                item.Yrden.Confusion = skillitem.Values[4];
                item.Yrden.AttackBlock = skillitem.Values[5];
                item.Yrden.Pain = skillitem.Values[6];


            }
        }
        public void UnlockStrength(List<Player> player, Skills skillitem) {
            foreach (Player item in player) {
                item.maxHealth += skillitem.Values[0];
                item.strongstyledamage = skillitem.Values[1];
                item.maxToxicity += skillitem.Values[2];
                item.strongStunChance = skillitem.Values[3];
                item.VitalityperLevel = skillitem.Values[4];


            }
        }
        public void UnlockEndurance(List<Player> player, Skills skillitem) {
            foreach (Player item in player) {
                item.maxEndurance += skillitem.Values[0];
                item.faststyledamage = skillitem.Values[1];
                item.EnduranceRegenSpeed = skillitem.Values[2];
                item.StunResistance = skillitem.Values[3];
                item.ParryStunDuration = skillitem.Values[4];
                item.PotionToxicityDebuf = skillitem.Values[5];
                item.maxToxicity += skillitem.Values[6];


            }
        }

    }
}
