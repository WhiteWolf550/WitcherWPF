using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WitcherWPF {
    class Music {

        public Dictionary<string, Uri> musicday = new Dictionary<string, Uri>();
        public Dictionary<string, Uri> musicnight = new Dictionary<string, Uri>();
        public Dictionary<string, Uri> sounds = new Dictionary<string, Uri>();
        MediaPlayer music = new MediaPlayer();
        MediaPlayer battle = new MediaPlayer();
        bool isplayingday = false;
        bool isplayingnight = false;
        string locationmusic = "";
        Uri currentlocationmusic;
        

        public Music() {
            locationmusic = Globals.location;
            this.music.MediaEnded += new EventHandler(Music_Ended);
            this.musicday.Add("Old_wyzima1", new Uri(@"../../sounds/music/Castle.mp3", UriKind.Relative));
            this.musicday.Add("Old_wyzima2", new Uri(@"../../sounds/music/Castle.mp3", UriKind.Relative));
            this.musicday.Add("Old_wyzima3", new Uri(@"../../sounds/music/wyzima_day.mp3", UriKind.Relative));
            this.musicday.Add("Old_wyzima4", new Uri(@"../../sounds/music/wyzima_day.mp3", UriKind.Relative));

            this.musicnight.Add("Old_wyzima1", new Uri(@"../../sounds/music/The_Order.mp3", UriKind.Relative));
            this.musicnight.Add("Old_wyzima2", new Uri(@"../../sounds/music/The_Order.mp3", UriKind.Relative));
            this.musicnight.Add("Old_wyzima3", new Uri(@"../../sounds/music/wyzima_night.mp3", UriKind.Relative));
            this.musicnight.Add("Old_wyzima4", new Uri(@"../../sounds/music/wyzima_night.mp3", UriKind.Relative));

            this.sounds.Add("NewPage", new Uri(@"../../sounds/UI/newpage.wav", UriKind.Relative));
            this.sounds.Add("OpenDoor", new Uri(@"../../sounds/UI/opendoor.wav", UriKind.Relative));
            this.sounds.Add("Steps", new Uri(@"../../sounds/UI/footsteps.wav", UriKind.Relative));
        }

        public void AmbientMusic(bool day) {
            if (Globals.Combat == false) {
                if (day == true) {
                    if (isplayingday == false || locationmusic != Globals.location) {
                        if (musicday[Globals.location] != currentlocationmusic) {
                            music.Open(musicday[Globals.location]);
                            music.Play();
                            isplayingday = true;
                            isplayingnight = false;
                            currentlocationmusic = musicday[Globals.location];
                        }
                    } else {

                    }
                } else {
                    if (isplayingnight == false || locationmusic != Globals.location) {
                        if (musicnight[Globals.location] != currentlocationmusic) {
                            music.Open(musicnight[Globals.location]);
                            music.Play();
                            isplayingnight = true;
                            isplayingday = false;
                            currentlocationmusic = musicnight[Globals.location];
                        }
                    } else {

                    }
                }
                locationmusic = Globals.location;
            }else {
                music.Pause();
            }
        }
        public void BattleMusic() {
            //combat = true;
            music.Pause();
            isplayingday = false;
            isplayingnight = false;
            
        }
        public void StopBattleMusic() {
            //combat = false;
        }
        private void Music_Ended(object sender, EventArgs e) {
            isplayingday = false;
            isplayingnight = false;
        }
        public void PlaySound(string Key) {
            music.Open(sounds[Key]);
            music.Play();
        }
        
    }
}
