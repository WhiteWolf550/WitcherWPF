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
    /// Interakční logika pro QuenSkills.xaml
    /// </summary>
    public partial class QuenSkills : UserControl {

        FileManager manager = new FileManager();
        Skills skill = new Skills();
        List<Skills> skills = new List<Skills>();
        List<Player> player = new List<Player>();
        List<Button> buttonlist = new List<Button>();
        Dictionary<Skills, Button> skilldict = new Dictionary<Skills, Button>();
        Dictionary<Button, Skills> skilldict2 = new Dictionary<Button, Skills>();
        public QuenSkills() {
            InitializeComponent();
        }

        public void Load() {
            
            skills = manager.LoadSkills();
            player = manager.LoadPlayer();
            ButtonList();
            skill.SetSkills(skilldict, skills, buttonlist, "Quen", skilldict2);

        }
        public void ButtonList() {
            buttonlist.Add(QuenSkill1);
            buttonlist.Add(QuenSkill2);
            buttonlist.Add(QuenSkill3);
            buttonlist.Add(QuenSkill4);
            buttonlist.Add(QuenSkill5);
            buttonlist.Add(QuenSkill6);
            buttonlist.Add(QuenSkill7);
            buttonlist.Add(QuenSkill8);
            buttonlist.Add(QuenSkill9);
            buttonlist.Add(QuenSkill10);
            buttonlist.Add(QuenSkill11);
            buttonlist.Add(QuenSkill12);
            buttonlist.Add(QuenSkill13);
            buttonlist.Add(QuenSkill14);
            buttonlist.Add(QuenSkill15);
            buttonlist.Add(QuenSkill16);
            buttonlist.Add(QuenSkill17);

        }
        private void SkillClick(object sender, RoutedEventArgs e) {
            Button button = (sender as Button);
            skill.UnlockSkills(skilldict, skills, buttonlist, "Quen", skilldict2, button, player);
        }
    }
}
