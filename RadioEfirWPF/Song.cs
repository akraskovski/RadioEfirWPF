using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioEfirWPF
{
    public class Song
    {
        private string artist;
        private string songName;
        private string album;
        private int year;
        private int rating;
        private string pathToSong;
        private string pathToPhoto;
        public string Artist { get; set; }
        public string SongName { get; set; }
        public string Album { get; set; }
        public int Year { get; set; }
        public int Rating { get; set; }
        public string PathToSong { get; set; }
        public string PathToPhoto { get; set; }
        public Song()
        {

        }
        public Song(string artist, string songName, string album, int year, int rating, string pathToSong, string pathToPhoto)
        {
            this.artist = artist;
            this.songName = songName;
            this.album = album;
            this.year = year;
            this.rating = rating;
            this.pathToSong = pathToSong;
            this.pathToPhoto = pathToPhoto;
        }
    }
}
