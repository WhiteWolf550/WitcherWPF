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
    /// Interakční logika pro AardSkills.xaml
    /// </summary>
    public partial class AardSkills : UserControl {

        FileManager manager = new FileManager();
        Skills skill = new Skills();
        Dictionary<Skills, Button> skilldict = new Dictionary<Skills, Button>();
        Dictionary<Button, Skills> skilldict2 = new Dictionary<Button, Skills>();
        List<Skills> skills = new List<Skills>();
        List<Player> player = new List<Player>();
        List<Button> buttonlist = new List<Button>();
        
        public AardSkills() {
            InitializeComponent();

        }
        public void Load() {
            
            skills = manager.LoadSkills();
            player = manager.LoadPlayer();
            ButtonList();
            skill.SetSkills(skilldict, skills, buttonlist, "Aard", skilldict2);
            
        }
        public void ButtonList() {
            buttonlist.Add(AardSkill1);
            buttonlist.Add(AardSkill2);
            buttonlist.Add(AardSkill3);
            buttonlist.Add(AardSkill4);
            buttonlist.Add(AardSkill5);
            buttonlist.Add(AardSkill6);
            buttonlist.Add(AardSkill7);
            buttonlist.Add(AardSkill8);
            buttonlist.Add(AardSkill9);
            buttonlist.Add(AardSkill10);
            buttonlist.Add(AardSkill11);
            buttonlist.Add(AardSkill12);
            buttonlist.Add(AardSkill13);
            buttonlist.Add(AardSkill14);
            buttonlist.Add(AardSkill15);
            buttonlist.Add(AardSkill16);
            buttonlist.Add(AardSkill17);
            
        }
        private void SkillClick(object sender, RoutedEventArgs e) {
            Button button = (sender as Button);
            skill.UnlockSkills(skilldict, skills, buttonlist, "Aard", skilldict2, button, player);
            
            
        }
        
        
    }
}
