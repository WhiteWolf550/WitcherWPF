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
    /// Interakční logika pro EnduranceSkills.xaml
    /// </summary>
    public partial class EnduranceSkills : UserControl
    {

        FileManager manager = new FileManager();
        Skills skill = new Skills();
        List<Skills> skills = new List<Skills>();
        List<Player> player = new List<Player>();
        List<Button> buttonlist = new List<Button>();
        Dictionary<Skills, Button> skilldict = new Dictionary<Skills, Button>();
        Dictionary<Button, Skills> skilldict2 = new Dictionary<Button, Skills>();

        public EnduranceSkills()
        {
            InitializeComponent();
        }

        public void Load() {
            
            skills = manager.LoadSkills();
            player = manager.LoadPlayer();
            ButtonList();
            skill.SetSkills(skilldict, skills, buttonlist, "Endurance", skilldict2);

        }
        public void ButtonList() {
            buttonlist.Add(EnduranceSkill1);
            buttonlist.Add(EnduranceSkill2);
            buttonlist.Add(EnduranceSkill3);
            buttonlist.Add(EnduranceSkill4);
            buttonlist.Add(EnduranceSkill5);
            buttonlist.Add(EnduranceSkill6);
            buttonlist.Add(EnduranceSkill7);
            buttonlist.Add(EnduranceSkill8);
            buttonlist.Add(EnduranceSkill9);
            buttonlist.Add(EnduranceSkill10);
            buttonlist.Add(EnduranceSkill11);
            buttonlist.Add(EnduranceSkill12);
            buttonlist.Add(EnduranceSkill13);
            buttonlist.Add(EnduranceSkill14);
            buttonlist.Add(EnduranceSkill15);
            buttonlist.Add(EnduranceSkill16);
            buttonlist.Add(EnduranceSkill17);

        }
        private void SkillClick(object sender, RoutedEventArgs e) {
            Button button = (sender as Button);
            skill.UnlockSkills(skilldict, skills, buttonlist, "Endurance", skilldict2, button, player);
        }
    }
}
