using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Helicopter.Core
{
	public class SongManager
	{
		private const string MusicContentRoot = "Content/Music";
		private ContentManager Content;

		private Song song;

		public Song CurrentSong => this.song;

		public static bool IsNyanPack = false;
        public static bool IsMeatPack = false;

        public SongManager(Game1 game)
		{
			this.Content = new ContentManager((IServiceProvider)game.Services, MusicContentRoot);
			this.song = LoadSong("MenuSong");
		}

        private Song LoadSong(string songName)
        {
            if (Game1.IsWeb)
            {
                try
                {
                    return Song.FromUri(songName, new Uri($"{MusicContentRoot}/{songName}.ogg", UriKind.Relative));
                }
                catch (Exception e) when (e is InvalidOperationException || e is ArgumentException || e is UriFormatException || e is NotSupportedException)
                {
                    Console.WriteLine($"Failed to load web song '{songName}.ogg':");
                    Console.WriteLine(e);
                }

                try
                {
                    return Song.FromUri(songName, new Uri($"{MusicContentRoot}/{songName}.mp3", UriKind.Relative));
                }
                catch (Exception e) when (e is InvalidOperationException || e is ArgumentException || e is UriFormatException || e is NotSupportedException)
                {
                    Console.WriteLine($"Failed to load web song '{songName}.mp3':");
                    Console.WriteLine(e);
                }
            }

            return this.Content.Load<Song>(songName);
        }

		public void LoadNewSong(int currentLevel)
		{
			this.song.Dispose();
			this.Content.Unload();
			switch (currentLevel)
			{
			case -1:
				this.song = LoadSong("MenuSong");
				IsNyanPack = false;
				IsMeatPack = false;
                break;
			case 0:
				this.song = LoadSong("SeaOfLove");
                IsNyanPack = false;
                IsMeatPack = false;
                Global.BPM = 15f / 44f; //176
				break;
			case 1:
				this.song = LoadSong("LikeARainbow");
                IsNyanPack = false;
                IsMeatPack = false;
                Global.BPM = 12f / 35f; //175
				break;
			case 2:
				this.song = LoadSong("YoureShining");
                IsNyanPack = false;
                IsMeatPack = false;
                Global.BPM = 0.3529412f; //170
				break;
			case 3:
				this.song = LoadSong("TasteOfHeaven");
                IsNyanPack = false;
				IsMeatPack = true;
                Global.BPM = 0.333333343f; //180
				break;
			case 4:
				this.song = LoadSong("IntergalacticalHigh");
                IsNyanPack = false;
                IsMeatPack = false;
                Global.BPM = 0.3448276f; //174
				break;
			case 5:
				this.song = LoadSong("MyRainbow");
				IsNyanPack = true;
                IsMeatPack = false;
                Global.BPM = 60f / 170f; //170
				break;
			}
		}
	}
}
