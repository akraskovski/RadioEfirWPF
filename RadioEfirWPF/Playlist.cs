using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioEfirWPF
{
    public class Playlist
    {
        List<Song> songs;
        private string name;
        private int countOfSongs;
        public List<Song> Songs
        {
            get
            {
                return songs;
            }
            set
            {
                songs = value;
            }
        }
        public int CountOfSongs { get; set; }
        public string Name { get; set; }
        public Playlist()
        {

        }
        public Playlist(List<Song> songs, int countOfSongs, string name)
        {
            this.songs = songs;
            this.countOfSongs = songs.Count;
            this.name = name;
        }
    }
}
