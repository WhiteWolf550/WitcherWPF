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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WitcherWPF {
    /// <summary>
    /// Interakční logika pro YrdenSkills.xaml
    /// </summary>
    public partial class YrdenSkills : UserControl {

        FileManager manager = new FileManager();
        Skills skill = new Skills();
        List<Skills> skills = new List<Skills>();
        List<Player> player = new List<Player>();
        List<Button> buttonlist = new List<Button>();
        Dictionary<Skills, Button> skilldict = new Dictionary<Skills, Button>();
        Dictionary<Button, Skills> skilldict2 = new Dictionary<Button, Skills>();
        public YrdenSkills() {
            InitializeComponent();
        }

        public void Load() {
            
            skills = manager.LoadSkills();
            player = manager.LoadPlayer();
            ButtonList();
            skill.SetSkills(skilldict, skills, buttonlist, "Yrden", skilldict2);

        }
        public void ButtonList() {
            buttonlist.Add(YrdenSkill1);
            buttonlist.Add(YrdenSkill2);
            buttonlist.Add(YrdenSkill3);
            buttonlist.Add(YrdenSkill4);
            buttonlist.Add(YrdenSkill5);
            buttonlist.Add(YrdenSkill6);
            buttonlist.Add(YrdenSkill7);
            buttonlist.Add(YrdenSkill8);
            buttonlist.Add(YrdenSkill9);
            buttonlist.Add(YrdenSkill10);
            buttonlist.Add(YrdenSkill11);
            buttonlist.Add(YrdenSkill12);
            buttonlist.Add(YrdenSkill13);
            buttonlist.Add(YrdenSkill14);
            buttonlist.Add(YrdenSkill15);
            buttonlist.Add(YrdenSkill16);
            buttonlist.Add(YrdenSkill17);

        }
        private void SkillClick(object sender, RoutedEventArgs e) {
            Button button = (sender as Button);
            skill.UnlockSkills(skilldict, skills, buttonlist, "Yrden", skilldict2, button, player);
        }
    }
}
