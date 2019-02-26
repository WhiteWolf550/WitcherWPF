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
        public void CreateSkills() {
            List<Skills> skills = new List<Skills>();
            //----------------------AARD-------------------------------------------------------
            skills.Add(new Skills(1, "Aard", "AardSkill1", 10, 0, 15, 10, 3, 10, 0, 0, true, true));
            skills.Add(new Skills(2, "Aard", "AardSkill2", 10, 0, 15, 10, 3, 10, 0, 1, true, false));
            skills.Add(new Skills(3, "Aard", "AardSkill3", 10, 0, 15, 15, 3, 10, 0, 2, false, false));
            skills.Add(new Skills(4, "Aard", "AardSkill4", 30, 0, 15, 15, 3, 10, 0, 3, false, true));
            skills.Add(new Skills(5, "Aard", "AardSkill5", 30, 0, 15, 15, 3, 30, 0, 4, false, false));
            skills.Add(new Skills(6, "Aard", "AardSkill6", 30, 0, 15, 25, 3, 30, 0, 5, false, false));
            skills.Add(new Skills(7, "Aard", "AardSkill7", 30, 0, 15, 25, 3, 30, 0, 6, false, false));
            skills.Add(new Skills(8, "Aard", "AardSkill8", 50, 0, 20, 25, 3, 30, 0, 7, false, true));
            skills.Add(new Skills(9, "Aard", "AardSkill9", 50, 0, 20, 25, 3, 30, 0, 8, false, false));
            skills.Add(new Skills(10, "Aard", "AardSkill10", 50, 0, 20, 25, 5, 30, 0, 9, false, false));
            skills.Add(new Skills(11, "Aard", "AardSkill11", 50, 0, 20, 30, 5, 30, 0, 10, false, false));
            skills.Add(new Skills(12, "Aard", "AardSkill12", 80, 0, 20, 30, 5, 30, 0, 11, false, true));
            skills.Add(new Skills(13, "Aard", "AardSkill13", 80, 0, 20, 30, 5, 30, 0, 12, false, false));
            skills.Add(new Skills(14, "Aard", "AardSkill14", 80, 1, 20, 30, 5, 30, 0, 13, false, false));
            skills.Add(new Skills(15, "Aard", "AardSkill15", 80, 1, 20, 30, 8, 30, 0, 14, false, false));
            skills.Add(new Skills(16, "Aard", "AardSkill16", 100, 1, 25, 30, 8, 30, 0, 15, false, true));
            skills.Add(new Skills(17, "Aard", "AardSkill17", 100, 1, 25, 35, 8, 40, 0, 16, false, false));
            //----------------------IGNI-------------------------------------------------------
            skills.Add(new Skills(1, "Igni", "IgniSkill1", 10, 0, 15, 5, 3, 1, 10, 0, true, true));
            skills.Add(new Skills(2, "Igni", "IgniSkill2", 10, 0, 15, 5, 3, 1, 10, 1, true, false));
            skills.Add(new Skills(3, "Igni", "IgniSkill3", 10, 0, 15, 5, 3, 1, 15, 2, false, false));
            skills.Add(new Skills(4, "Igni", "IgniSkill4", 30, 0, 15, 5, 3, 1, 15, 3, false, true));
            skills.Add(new Skills(5, "Igni", "IgniSkill5", 30, 0, 15, 5, 3, 1, 15, 4, false, false));
            skills.Add(new Skills(6, "Igni", "IgniSkill6", 30, 0, 15, 10, 4, 1, 15, 5, false, false));
            skills.Add(new Skills(7, "Igni", "IgniSkill7", 30, 0, 15, 10, 4, 1, 25, 6, false, false));
            skills.Add(new Skills(8, "Igni", "IgniSkill8", 50, 0, 20, 10, 4, 1, 25, 7, false, true));
            skills.Add(new Skills(9, "Igni", "IgniSkill9", 50, 0, 20, 10, 4, 1, 25, 8, false, false));
            skills.Add(new Skills(10, "Igni", "IgniSkill10", 50, 0, 20, 10, 4, 3, 25, 9, false, false));
            skills.Add(new Skills(11, "Igni", "IgniSkill11", 50, 0, 20, 20, 4, 3, 25, 10, false, false));
            skills.Add(new Skills(12, "Igni", "IgniSkill12", 80, 0, 20, 20, 4, 3, 25, 11, false, true));
            skills.Add(new Skills(13, "Igni", "IgniSkill13", 80, 0, 20, 20, 4, 3, 25, 12, false, false));
            skills.Add(new Skills(14, "Igni", "IgniSkill14", 80, 1, 20, 20, 4, 3, 25, 13, false, false));
            skills.Add(new Skills(15, "Igni", "IgniSkill15", 80, 1, 20, 20, 8, 3, 25, 14, false, false));
            skills.Add(new Skills(16, "Igni", "IgniSkill16", 100, 1, 25, 20, 8, 3, 25, 15, false, true));
            skills.Add(new Skills(17, "Igni", "IgniSkill17", 100, 1, 25, 30, 8, 3, 30, 16, false, false));
            //----------------------QUEN-------------------------------------------------------
            skills.Add(new Skills(1, "Quen", "QuenSkill1", 5, 0, 15, 5, 5, 0, 0, 0, true, true));
            skills.Add(new Skills(2, "Quen", "QuenSkill2", 5, 0, 15, 5, 5, 0, 0, 1, true, false));
            skills.Add(new Skills(3, "Quen", "QuenSkill3", 5, 0, 15, 5, 10, 0, 0, 2, false, false));
            skills.Add(new Skills(4, "Quen", "QuenSkill4", 10, 0, 15, 5, 10, 0, 0, 3, false, true));
            skills.Add(new Skills(5, "Quen", "QuenSkill5", 10, 0, 15, 5, 10, 0, 0, 4, false, false));
            skills.Add(new Skills(6, "Quen", "QuenSkill6", 10, 0, 15, 5, 10, 1, 0, 5, false, false));
            skills.Add(new Skills(7, "Quen", "QuenSkill7", 10, 0, 15, 5, 30, 1, 0, 6, false, false));
            skills.Add(new Skills(8, "Quen", "QuenSkill8", 15, 0, 20, 5, 30, 1, 0, 7, false, true));
            skills.Add(new Skills(9, "Quen", "QuenSkill9", 15, 0, 20, 5, 30, 1, 0, 8, false, false));
            skills.Add(new Skills(10, "Quen", "QuenSkill10", 15, 0, 20, 10, 30, 1, 0, 9, false, false));
            skills.Add(new Skills(11, "Quen", "QuenSkill11", 15, 0, 20, 10, 50, 1, 0, 10, false, false));
            skills.Add(new Skills(12, "Quen", "QuenSkill12", 20, 0, 20, 10, 50, 1, 0, 11, false, true));
            skills.Add(new Skills(13, "Quen", "QuenSkill13", 20, 0, 20, 10, 50, 1, 0, 12, false, false));
            skills.Add(new Skills(14, "Quen", "QuenSkill14", 20, 1, 20, 10, 50, 1, 0, 13, false, false));
            skills.Add(new Skills(15, "Quen", "QuenSkill15", 20, 1, 20, 15, 50, 1, 0, 14, false, false));
            skills.Add(new Skills(16, "Quen", "QuenSkill16", 25, 1, 25, 15, 50, 1, 0, 15, false, true));
            skills.Add(new Skills(17, "Quen", "QuenSkill17", 25, 1, 25, 20, 100, 1, 0, 16, false, false));
            //----------------------AXII-------------------------------------------------------
            skills.Add(new Skills(1, "Axii", "AxiiSkill1", 10, 0, 15, 4, 3, 5, 0, 0, true, true));
            skills.Add(new Skills(2, "Axii", "AxiiSkill2", 10, 0, 15, 4, 3, 5, 0, 1, true, false));
            skills.Add(new Skills(3, "Axii", "AxiiSkill3", 10, 0, 15, 4, 3, 10, 0, 2, false, false));
            skills.Add(new Skills(4, "Axii", "AxiiSkill4", 30, 0, 15, 4, 3, 10, 0, 3, false, true));
            skills.Add(new Skills(5, "Axii", "AxiiSkill5", 30, 0, 15, 4, 3, 10, 0, 4, false, false));
            skills.Add(new Skills(6, "Axii", "AxiiSkill6", 30, 0, 15, 4, 3, 15, 0, 5, false, false));
            skills.Add(new Skills(7, "Axii", "AxiiSkill7", 30, 0, 15, 4, 2, 15, 0, 6, false, false));
            skills.Add(new Skills(8, "Axii", "AxiiSkill8", 50, 0, 20, 4, 2, 15, 0, 7, false, true));
            skills.Add(new Skills(9, "Axii", "AxiiSkill9", 50, 0, 20, 4, 2, 15, 0, 8, false, false));
            skills.Add(new Skills(10, "Axii", "AxiiSkill10", 50, 0, 20, 7, 2, 15, 0, 9, false, false));
            skills.Add(new Skills(11, "Axii", "AxiiSkill11", 50, 0, 20, 7, 2, 20, 0, 10, false, false));
            skills.Add(new Skills(12, "Axii", "AxiiSkill12", 80, 0, 20, 7, 2, 20, 0, 11, false, true));
            skills.Add(new Skills(13, "Axii", "AxiiSkill13", 80, 0, 20, 7, 2, 20, 0, 12, false, false));
            skills.Add(new Skills(14, "Axii", "AxiiSkill14", 80, 1, 20, 7, 2, 20, 0, 13, false, false));
            skills.Add(new Skills(15, "Axii", "AxiiSkill15", 80, 1, 20, 7, 1, 20, 0, 14, false, false));
            skills.Add(new Skills(16, "Axii", "AxiiSkill16", 100, 1, 25, 7, 1, 20, 0, 15, false, true));
            skills.Add(new Skills(17, "Axii", "AxiiSkill17", 100, 1, 25, 10, 1, 30, 0, 16, false, false));
            //----------------------YRDEN-------------------------------------------------------
            skills.Add(new Skills(1, "Yrden", "YrdenSkill1", 10, 0, 15, 5, 0, 0, 0, 0, true, true));
            skills.Add(new Skills(2, "Yrden", "YrdenSkill2", 10, 0, 15, 5, 0, 0, 0, 0, true, false));
            skills.Add(new Skills(3, "Yrden", "YrdenSkill3", 10, 0, 15, 5, 0, 0, 1, 0, false, false));
            skills.Add(new Skills(4, "Yrden", "YrdenSkill4", 30, 0, 15, 5, 0, 0, 1, 0, false, true));
            skills.Add(new Skills(5, "Yrden", "YrdenSkill5", 30, 0, 15, 5, 0, 0, 1, 0, false, false));
            skills.Add(new Skills(6, "Yrden", "YrdenSkill6", 30, 0, 15, 5, 1, 0, 1, 0, false, false));
            skills.Add(new Skills(7, "Yrden", "YrdenSkill7", 30, 0, 15, 7, 1, 0, 1, 0, false, false));
            skills.Add(new Skills(8, "Yrden", "YrdenSkill8", 50, 0, 20, 7, 1, 0, 1, 0, false, true));
            skills.Add(new Skills(9, "Yrden", "YrdenSkill9", 50, 0, 20, 7, 1, 0, 1, 0, false, false));
            skills.Add(new Skills(10, "Yrden", "YrdenSkill10", 50, 0, 20, 8, 1, 0, 1, 0, false, false));
            skills.Add(new Skills(11, "Yrden", "YrdenSkill11", 50, 0, 20, 8, 1, 1, 1, 0, false, false));
            skills.Add(new Skills(12, "Yrden", "YrdenSkill12", 80, 0, 20, 8, 1, 1, 1, 0, false, true));
            skills.Add(new Skills(13, "Yrden", "YrdenSkill13", 80, 0, 20, 8, 1, 1, 1, 0, false, false));
            skills.Add(new Skills(14, "Yrden", "YrdenSkill14", 80, 1, 20, 8, 1, 1, 1, 0, false, false));
            skills.Add(new Skills(15, "Yrden", "YrdenSkill15", 80, 1, 20, 9, 1, 1, 1, 0, false, false));
            skills.Add(new Skills(16, "Yrden", "YrdenSkill16", 100, 1, 25, 9, 1, 1, 1, 0, false, true));
            skills.Add(new Skills(17, "Yrden", "YrdenSkill17", 100, 1, 25, 10, 1, 1, 1, 0, false, false));
            //----------------------STRENGTH-------------------------------------------------------
            skills.Add(new Skills(1, "Strength", "StrengthSkill1", 0, 2, 0, 0, 0, 0, 0, 0, true, false));
            skills.Add(new Skills(2, "Strength", "StrengthSkill2", 0, 5, 0, 0, 0, 0, 0, 1, false, false));
            skills.Add(new Skills(3, "Strength", "StrengthSkill3", 0, 5, 2, 0, 0, 0, 0, 2, false, false));
            skills.Add(new Skills(4, "Strength", "StrengthSkill4", 50, 5, 0, 0, 0, 0, 0, 3, false, false));
            skills.Add(new Skills(5, "Strength", "StrengthSkill5", 0, 5, 0, 10, 0, 0, 0, 4, false, false));
            skills.Add(new Skills(6, "Strength", "StrengthSkill6", 0, 10, 0, 10, 0, 0, 0, 5, false, false));
            skills.Add(new Skills(7, "Strength", "StrengthSkill7", 0, 10, 2, 10, 0, 0, 0, 6, false, false));
            skills.Add(new Skills(8, "Strength", "StrengthSkill8", 25, 10, 0, 10, 0, 0, 0, 7, false, false));
            skills.Add(new Skills(9, "Strength", "StrengthSkill9", 0, 10, 0, 10, 1, 0, 0, 8, false, false));
            skills.Add(new Skills(10, "Strength", "StrengthSkill10", 0, 25, 0, 10, 1, 0, 0, 9, false, false));
            skills.Add(new Skills(11, "Strength", "StrengthSkill11", 0, 25, 2, 10, 1, 0, 0, 10, false, false));
            skills.Add(new Skills(12, "Strength", "StrengthSkill12", 25, 25, 0, 10, 1, 0, 0, 11, false, false));
            skills.Add(new Skills(13, "Strength", "StrengthSkill13", 0, 25, 0, 20, 1, 0, 0, 12, false, false));
            skills.Add(new Skills(14, "Strength", "StrengthSkill14", 0, 25, 2, 20, 1, 0, 0, 13, false, false));
            skills.Add(new Skills(15, "Strength", "StrengthSkill15", 0, 35, 0, 20, 1, 0, 0, 14, false, false));
            skills.Add(new Skills(16, "Strength", "StrengthSkill16", 50, 35, 0, 20, 1, 0, 0, 15, false, false));
            skills.Add(new Skills(17, "Strength", "StrengthSkill17", 0, 50, 12, 20, 1, 0, 0, 16, false, false));
            //----------------------Endurance-------------------------------------------------------
            skills.Add(new Skills(1, "Endurance", "EnduranceSkill1", 0, 0, 0, 0, 0, 0, 0, 0, true, false));
            skills.Add(new Skills(2, "Endurance", "EnduranceSkill2", 0, 2, 0, 0, 0, 0, 0, 1, false, false));
            skills.Add(new Skills(3, "Endurance", "EnduranceSkill3", 0, 2, 0, 0, 2, 0, 0, 2, false, false));
            skills.Add(new Skills(4, "Endurance", "EnduranceSkill4", 4, 2, 0, 0, 2, 0, 0, 3, false, false));
            skills.Add(new Skills(5, "Endurance", "EnduranceSkill5", 0, 2, 0, 10, 2, 0, 0, 4, false, false));
            skills.Add(new Skills(6, "Endurance", "EnduranceSkill6", 0, 6, 0, 10, 2, 0, 0, 5, false, false));
            skills.Add(new Skills(7, "Endurance", "EnduranceSkill7", 5, 6, 0, 10, 2, 0, 0, 6, false, false));
            skills.Add(new Skills(8, "Endurance", "EnduranceSkill8", 2, 6, 0, 10, 2, 0, 0, 7, false, false));
            skills.Add(new Skills(9, "Endurance", "EnduranceSkill9", 0, 6, 0, 10, 5, 0, 0, 8, false, false));
            skills.Add(new Skills(10, "Endurance", "EnduranceSkill10", 0, 6, 0, 20, 5, 0, 0, 9, false, false));
            skills.Add(new Skills(11, "Endurance", "EnduranceSkill11", 0, 10, 0, 20, 5, 0, 0, 10, false, false));
            skills.Add(new Skills(12, "Endurance", "EnduranceSkill12", 2, 10, 0, 20, 5, 0, 0, 11, false, false));
            skills.Add(new Skills(13, "Endurance", "EnduranceSkill13", 0, 15, 0, 20, 5, 0, 0, 12, false, false));
            skills.Add(new Skills(14, "Endurance", "EnduranceSkill14", 0, 15, 0, 20, 5, 5, 0, 13, false, false));
            skills.Add(new Skills(15, "Endurance", "EnduranceSkill15", 0, 15, 1, 20, 5, 5, 0, 14, false, false));
            skills.Add(new Skills(16, "Endurance", "EnduranceSkill16", 2, 15, 1, 20, 5, 5, 0, 15, false, false));
            skills.Add(new Skills(17, "Endurance", "EnduranceSkill17", 0, 25, 1, 20, 5, 5, 50, 16, false, false));
            manager.SaveSkills(skills);
        }

    }
}
