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
    /// Interakční logika pro AxiiSkills.xaml
    /// </summary>
    public partial class AxiiSkills : UserControl {

        FileManager manager = new FileManager();
        Skills skill = new Skills();
        List<Skills> skills = new List<Skills>();
        List<Player> player = new List<Player>();
        List<Button> buttonlist = new List<Button>();
        Dictionary<Skills, Button> skilldict = new Dictionary<Skills, Button>();
        Dictionary<Button, Skills> skilldict2 = new Dictionary<Button, Skills>();
        public AxiiSkills() {
            InitializeComponent();
        }

        public void Load() {
            
            skills = manager.LoadSkills();
            player = manager.LoadPlayer();
            ButtonList();
            skill.SetSkills(skilldict, skills, buttonlist, "Axii", skilldict2);

        }
        public void ButtonList() {
            buttonlist.Add(AxiiSkill1);
            buttonlist.Add(AxiiSkill2);
            buttonlist.Add(AxiiSkill3);
            buttonlist.Add(AxiiSkill4);
            buttonlist.Add(AxiiSkill5);
            buttonlist.Add(AxiiSkill6);
            buttonlist.Add(AxiiSkill7);
            buttonlist.Add(AxiiSkill8);
            buttonlist.Add(AxiiSkill9);
            buttonlist.Add(AxiiSkill10);
            buttonlist.Add(AxiiSkill11);
            buttonlist.Add(AxiiSkill12);
            buttonlist.Add(AxiiSkill13);
            buttonlist.Add(AxiiSkill14);
            buttonlist.Add(AxiiSkill15);
            buttonlist.Add(AxiiSkill16);
            buttonlist.Add(AxiiSkill17);

        }
        private void SkillClick(object sender, RoutedEventArgs e) {
            Button button = (sender as Button);
            skill.UnlockSkills(skilldict, skills, buttonlist, "Axii", skilldict2, button, player);
        }
    }
}
