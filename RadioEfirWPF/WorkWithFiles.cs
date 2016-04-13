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
        public List<Song> ReadCollection = new List<Song>();
        private string path = "Songs.xml";
        public void readFromFile()
        {
            if (File.Exists(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Song>));
                using (FileStream reader = File.OpenRead(path))
                {
                    ReadCollection = (List<Song>)serializer.Deserialize(reader);
                }
            }
        }
        public void writeToFile(List<Song> songs)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Song>));
            using (StreamWriter writer = File.CreateText(path))
            {
                serializer.Serialize(writer, songs);
            }
        }
    }
}
