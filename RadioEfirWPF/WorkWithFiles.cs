using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace RadioEfirWPF
{
    class WorkWithFiles
    {
        public List<Song> ReadSongsCollection = new List<Song>();
        public List<Playlist> ReadPlaylistsCollection = new List<Playlist>();
        private string pathToSongs = "Songs.xml";
        private string pathToPlaylists = "Playlists.xml";

        public void readSongsFromFile()
        {
            if (File.Exists(pathToSongs))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Song>));
                using (FileStream reader = File.OpenRead(pathToSongs))
                {
                    ReadSongsCollection = (List<Song>)serializer.Deserialize(reader);
                }
            }
        }
        public void readPlaylistsFromFile()
        {
            if (File.Exists(pathToPlaylists))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Playlist>));
                using (FileStream reader = File.OpenRead(pathToPlaylists))
                {
                    ReadPlaylistsCollection = (List<Playlist>)serializer.Deserialize(reader);
                }
            }
        }
        public void writePlaylistsToFile(List<Playlist> playlists)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Playlist>));
            using (StreamWriter writer = File.CreateText(pathToPlaylists))
            {
                serializer.Serialize(writer, playlists);
            }
        }
        public void writeSongsToFile(List<Song> songs)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Song>));
            using (StreamWriter writer = File.CreateText(pathToSongs))
            {
                serializer.Serialize(writer, songs);
            }
        }
    }
}
