using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WitcherWPF {
    public class Music {

        public Dictionary<string, Uri> musicday = new Dictionary<string, Uri>();
        public Dictionary<string, Uri> musicnight = new Dictionary<string, Uri>();
        public Dictionary<string, Uri> sounds = new Dictionary<string, Uri>();
        MediaPlayer music = new MediaPlayer();
        MediaPlayer battle = new MediaPlayer();
        bool isplayingday = false;
        bool isplayingnight = false;
        string locationmusic = "";
        bool musicstopped = false;
        Uri currentlocationmusic;
        

        public Music() {
            locationmusic = Globals.location;
            this.music.MediaEnded += new EventHandler(Music_Ended);
            this.musicday.Add("Old_wyzima1", new Uri(@"../../sounds/music/Castle.mp3", UriKind.Relative));
            this.musicday.Add("Old_wyzima2", new Uri(@"../../sounds/music/Castle.mp3", UriKind.Relative));
            this.musicday.Add("Old_wyzima3", new Uri(@"../../sounds/music/wyzima_day.mp3", UriKind.Relative));
            this.musicday.Add("Old_wyzima4", new Uri(@"../../sounds/music/wyzima_day.mp3", UriKind.Relative));
            this.musicday.Add("Old_wyzima5", new Uri(@"../../sounds/music/wyzima_day.mp3", UriKind.Relative));
            this.musicday.Add("Village_Inn", new Uri(@"../../sounds/music/Tavern1.mp3", UriKind.Relative));
            this.musicday.Add("Village_Outside1", new Uri(@"../../sounds/music/Village_day.mp3", UriKind.Relative));
            this.musicday.Add("Village_Outside2", new Uri(@"../../sounds/music/Village_day.mp3", UriKind.Relative));
            this.musicday.Add("Village_Outside3", new Uri(@"../../sounds/music/Village_day.mp3", UriKind.Relative));

            this.musicnight.Add("Old_wyzima1", new Uri(@"../../sounds/music/The_Order.mp3", UriKind.Relative));
            this.musicnight.Add("Old_wyzima2", new Uri(@"../../sounds/music/The_Order.mp3", UriKind.Relative));
            this.musicnight.Add("Old_wyzima3", new Uri(@"../../sounds/music/wyzima_night.mp3", UriKind.Relative));
            this.musicnight.Add("Old_wyzima4", new Uri(@"../../sounds/music/wyzima_night.mp3", UriKind.Relative));
            this.musicnight.Add("Old_wyzima5", new Uri(@"../../sounds/music/wyzima_night.mp3", UriKind.Relative));
            this.musicnight.Add("Village_Inn", new Uri(@"../../sounds/music/Tavern1.mp3", UriKind.Relative));
            this.musicnight.Add("Village_Outside1", new Uri(@"../../sounds/music/Village_night.mp3", UriKind.Relative));
            this.musicnight.Add("Village_Outside2", new Uri(@"../../sounds/music/Village_night.mp3", UriKind.Relative));
            this.musicnight.Add("Village_Outside3", new Uri(@"../../sounds/music/Village_night.mp3", UriKind.Relative));

            this.sounds.Add("NewPage", new Uri(@"../../sounds/UI/newpage.wav", UriKind.Relative));
            this.sounds.Add("OpenDoor", new Uri(@"../../sounds/UI/opendoor.wav", UriKind.Relative));
            this.sounds.Add("Steps", new Uri(@"../../sounds/UI/footsteps.wav", UriKind.Relative));
            this.sounds.Add("LevelUP", new Uri(@"../../sounds/misc/levelup.wav", UriKind.Relative));
            this.sounds.Add("QuestUpdate", new Uri(@"../../sounds/misc/questupdate.wav", UriKind.Relative));
            this.sounds.Add("QuestComplete", new Uri(@"../../sounds/misc/questcomplete.wav", UriKind.Relative));
            this.sounds.Add("GrabArmor", new Uri(@"../../sounds/UI/armor_grab.wav", UriKind.Relative));
            this.sounds.Add("GrabSword", new Uri(@"../../sounds/UI/sword_grab.wav", UriKind.Relative));
            this.sounds.Add("BuySkill", new Uri(@"../../sounds/UI/buyskill.wav", UriKind.Relative));
            this.sounds.Add("Alchemy", new Uri(@"../../sounds/UI/enteralchemy.wav", UriKind.Relative));
            this.sounds.Add("Inventory", new Uri(@"../../sounds/UI/enterinventory.wav", UriKind.Relative));
            this.sounds.Add("ChooseTree", new Uri(@"../../sounds/UI/tree.wav", UriKind.Relative));
        }

        public void AmbientMusic(bool day) {
            if (Globals.Combat == false) {
                if (day == true) {
                    if (isplayingday == false || locationmusic != Globals.location || musicstopped == true) {
                        if (musicday[Globals.location] != currentlocationmusic || musicstopped == true) {
                            music.Open(musicday[Globals.location]);
                            music.Play();
                            musicstopped = false;
                            isplayingday = true;
                            isplayingnight = false;
                            currentlocationmusic = musicday[Globals.location];
                        }
                    } else {

                    }
                } else {
                    if (isplayingnight == false || locationmusic != Globals.location || musicstopped == true) {
                        if (musicnight[Globals.location] != currentlocationmusic || musicstopped == true) {
                            music.Open(musicnight[Globals.location]);
                            music.Play();
                            musicstopped = false;
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
                musicstopped = true;
            }
        }
        public void BattleMusic() {
            //combat = true;
            music.Pause();
            isplayingday = false;
            isplayingnight = false;
            
        }
        public void StopMusic() {
            music.Stop();
        }
        private void Music_Ended(object sender, EventArgs e) {
            isplayingday = false;
            isplayingnight = false;
            musicstopped = true;
        }
        public void PlaySound(string Key) {
            music.Open(sounds[Key]);
            music.Play();
        }
        public void MainMenuMusic() {
            music.Open(new Uri("sounds/music/menu.mp3", UriKind.Relative));
            music.Play();
        }
        
    }
}
