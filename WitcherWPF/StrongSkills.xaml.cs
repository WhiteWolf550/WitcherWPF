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

namespace WitcherWPF
{
    /// <summary>
    /// Interakční logika pro StrongSkills.xaml
    /// </summary>
    public partial class StrongSkills : UserControl
    {

        FileManager manager = new FileManager();
        Skills skill = new Skills();
        List<Skills> skills = new List<Skills>();
        List<Player> player = new List<Player>();
        List<Button> buttonlist = new List<Button>();
        Dictionary<Skills, Button> skilldict = new Dictionary<Skills, Button>();
        Dictionary<Button, Skills> skilldict2 = new Dictionary<Button, Skills>();
        public StrongSkills()
        {
            InitializeComponent();
        }

        public void Load() {
            
            skills = manager.LoadSkills();
            player = manager.LoadPlayer();
            ButtonList();
            skill.SetSkills(skilldict, skills, buttonlist, "Strength", skilldict2);

        }
        public void ButtonList() {
            buttonlist.Add(StrengthSkill1);
            buttonlist.Add(StrengthSkill2);
            buttonlist.Add(StrengthSkill3);
            buttonlist.Add(StrengthSkill4);
            buttonlist.Add(StrengthSkill5);
            buttonlist.Add(StrengthSkill6);
            buttonlist.Add(StrengthSkill7);
            buttonlist.Add(StrengthSkill8);
            buttonlist.Add(StrengthSkill9);
            buttonlist.Add(StrengthSkill10);
            buttonlist.Add(StrengthSkill11);
            buttonlist.Add(StrengthSkill12);
            buttonlist.Add(StrengthSkill13);
            buttonlist.Add(StrengthSkill14);
            buttonlist.Add(StrengthSkill15);
            buttonlist.Add(StrengthSkill16);
            buttonlist.Add(StrengthSkill17);

        }
        private void SkillClick(object sender, RoutedEventArgs e) {
            Button button = (sender as Button);
            skill.UnlockSkills(skilldict, skills, buttonlist, "Strength", skilldict2, button, player);
        }
    }
}
