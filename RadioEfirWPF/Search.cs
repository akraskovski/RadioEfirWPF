using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioEfirWPF
{
    class Search
    {
        List<Song> findCollection = new List<Song>();
        public List<Song> searchByArtist(List<Song> music, string artist)
        {
            findCollection.Clear();
            foreach (Song s in music)
            {
                if (s.Artist == artist)
                    findCollection.Add(s);
            }
            return findCollection;
        }
        private void searchByName(List<Song> radio)
        {

        }
        private void searchByAlbum(List<Song> radio)
        {

        }
        private void searchByYear(List<Song> radio)
        {

        }
        private void searchByRating(List<Song> radio)
        {

        }
    }
}
