﻿using System;
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
    /// Interakční logika pro Cutscenes.xaml
    /// </summary>
    public partial class Cutscenes : Page
    {
        private Frame parentFrame;
        private Time time;
        private string CutsceneName;

        PlayerQuest quest = new PlayerQuest();
        public Cutscenes()
        {
            InitializeComponent();
            Application.Current.MainWindow.KeyDown += new KeyEventHandler(Keys);
        }

        public Cutscenes(Frame parentFrame, Time time, string CutsceneName) : this() {
            this.parentFrame = parentFrame;
            this.time = time;
            this.CutsceneName = CutsceneName;

            time.Visibility = Visibility.Hidden;
            Globals.Combat = true;
            PlayCutscene();
        }
        public void Keys(object sender, KeyEventArgs e) {
            if (e.Key == Key.Space) {
                Next();
            }
        }
        public void PlayCutscene() {
            CutScene.Source = new Uri("../../cutscenes/" + CutsceneName + ".mp4", UriKind.Relative);
            CutScene.Play();
        }
        public void CutPlay() {
            CutScene.Source = new Uri("../../cutscenes/" + CutsceneName + ".mp4", UriKind.Relative);
            CutScene.Play();
        }
        private void CutScene_MediaEnded(object sender, RoutedEventArgs e) {
            Next();
        }
        public void Next() {
            if (CutsceneName == "GameIntro") {
                CutsceneName = "PrologueCut1";
                CutPlay();
            }else if (CutsceneName == "PrologueCut1") {
                RemoveHandler();
                Globals.Combat = false;
                time.Timer();
                time.time.Start();
                parentFrame.Navigate(new Location(parentFrame, "Old_wyzima2", time));
            }else if (CutsceneName == "PrologueCut2") {
                CutsceneName = "Chapter1Cut1";
                CutPlay();
            }else if(CutsceneName == "Chapter1Cut1") {
                parentFrame.Navigate(new Combat(parentFrame, false, time, false, null, "Barghest", "Chapter1Cut2"));
            }else if (CutsceneName == "Chapter1Cut2") {
                Globals.Hour = 4;
                Globals.Chapter = 1;
                Globals.DialoguePath = @"../../dialogues/DialogueChapter1.json";
                Globals.Combat = false;
                Globals.location = "Village_Outside2";
                time.Visibility = Visibility.Visible;
                parentFrame.Navigate(new Dialogue(parentFrame, "Vesničan", time, true));
            }else if (CutsceneName == "Chapter1Cut3Begin") {
                parentFrame.Navigate(new DecisionPage(parentFrame,  time));
            }else if (CutsceneName == "Chapter1Cut3Die" || CutsceneName == "Chapter1Cut3Live") {
                Globals.Combat = false;
                parentFrame.Navigate(new Location(parentFrame, time));
                time.Visibility = Visibility.Visible;
                quest.UpdateQuest("Záhadná vesnice", QuestPop, QueName, QueGoal);
            } else if (CutsceneName == "Chapter1Cut4") {
                CutsceneName = "Chapter2Cut1";
                CutPlay();
            } else if (CutsceneName == "Chapter2Cut1") {
                Globals.Hour = 13;
                Globals.Chapter = 2;
                Globals.DialoguePath = @"../../dialogues/DialogueChapter2.json";
                Globals.Combat = false;
                Globals.location = "Novigrad_Outside2";
                time.Visibility = Visibility.Visible;
                parentFrame.Navigate(new Dialogue(parentFrame, "Messenger", time, true));
            }else if (CutsceneName == "Chapter2Cut2") {
                Globals.location = "Novigrad_Prison";
                time.Visibility = Visibility.Visible;
                Globals.Combat = false;
                parentFrame.Navigate(new Location(parentFrame, time));
                
            }else if (CutsceneName == "Chapter2Cut3") {
                time.Visibility = Visibility.Visible;
                Globals.Combat = false;
                Globals.Hour = 0;
                Globals.Minute = 0;
                parentFrame.Navigate(new Location(parentFrame, time));
                quest.UpdateQuest("Tajná organizace", QuestPop, QueName, QueGoal);
            }else if (CutsceneName == "Chapter2Cut4") {
                CutsceneName = "Chapter2Cut5";
                CutPlay();
            }else if (CutsceneName == "Chapter2Cut5") {
                time.Visibility = Visibility.Visible;
                Globals.Combat = false;
                Globals.Hour = 10;
                Globals.Minute = 21;
                Globals.location = "Novigrad_House1";
                parentFrame.Navigate(new Dialogue(parentFrame, "Triss", time, true));
            } else if (CutsceneName == "Chapter2Cut6") {
                //CutsceneName = "Chapter3Cut1";
                //CutPlay();
                parentFrame.Navigate(new EndPage(parentFrame, time));
            }

        }
        private void RemoveHandler() {
            Application.Current.MainWindow.KeyDown -= new KeyEventHandler(Keys);
        }
    }
}
