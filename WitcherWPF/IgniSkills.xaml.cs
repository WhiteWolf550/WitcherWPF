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
    /// Interakční logika pro IgniSkills.xaml
    /// </summary>
    public partial class IgniSkills : UserControl
    {
        FileManager manager = new FileManager();
        Skills skill = new Skills();
        List<Skills> skills = new List<Skills>();
        List<Player> player = new List<Player>();
        List<Button> buttonlist = new List<Button>();
        Dictionary<Skills, Button> skilldict = new Dictionary<Skills, Button>();
        Dictionary<Button, Skills> skilldict2 = new Dictionary<Button, Skills>();
        public IgniSkills()
        {
            InitializeComponent();
        }

        public void Load() {
            
            skills = manager.LoadSkills();
            player = manager.LoadPlayer();
            ButtonList();
            skill.SetSkills(skilldict, skills, buttonlist, "Igni", skilldict2);

        }
        public void ButtonList() {
            buttonlist.Add(IgniSkill1);
            buttonlist.Add(IgniSkill2);
            buttonlist.Add(IgniSkill3);
            buttonlist.Add(IgniSkill4);
            buttonlist.Add(IgniSkill5);
            buttonlist.Add(IgniSkill6);
            buttonlist.Add(IgniSkill7);
            buttonlist.Add(IgniSkill8);
            buttonlist.Add(IgniSkill9);
            buttonlist.Add(IgniSkill10);
            buttonlist.Add(IgniSkill11);
            buttonlist.Add(IgniSkill12);
            buttonlist.Add(IgniSkill13);
            buttonlist.Add(IgniSkill14);
            buttonlist.Add(IgniSkill15);
            buttonlist.Add(IgniSkill16);
            buttonlist.Add(IgniSkill17);

        }
        private void SkillClick(object sender, RoutedEventArgs e) {
            Button button = (sender as Button);
            skill.UnlockSkills(skilldict, skills, buttonlist, "Igni", skilldict2, button, player);
        }

    }
}

