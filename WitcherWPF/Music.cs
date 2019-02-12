﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WitcherWPF {
    class Music {

        public Dictionary<string, Uri> musicday = new Dictionary<string, Uri>();
        public Dictionary<string, Uri> musicnight = new Dictionary<string, Uri>();
        MediaPlayer music = new MediaPlayer();
        bool isplayingday = false;
        bool isplayingnight = false;

        public Music() {
            this.music.MediaEnded += new EventHandler(Music_Ended);
            this.musicday.Add("Old_wyzima1", new Uri(@"../../sounds/music/Peacefull.mp3", UriKind.Relative));
            this.musicday.Add("Old_wyzima2", new Uri(@"../../sounds/music/Peacefull.mp3", UriKind.Relative));

            this.musicnight.Add("Old_wyzima1", new Uri(@"../../sounds/music/The_Order.mp3", UriKind.Relative));
            this.musicnight.Add("Old_wyzima2", new Uri(@"../../sounds/music/The_Order.mp3", UriKind.Relative));
        }

        public void AmbientMusic(bool day, string location) {
            
            if (day == true) {
                if (isplayingday == false) {
                    music.Open(musicday[location]);
                    music.Play();
                    isplayingday = true;
                    isplayingnight = false;
                }else {

                }
            }else {
                if (isplayingnight == false) {
                    music.Open(musicnight[location]);
                    music.Play();
                    isplayingnight = true;
                    isplayingday = false;
                }else {

                }
            }
        }
        private void Music_Ended(object sender, EventArgs e) {
            isplayingday = false;
            isplayingnight = false;
        }
    }
}