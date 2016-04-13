using System;
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
using System.Windows.Threading;
using System.IO;
using Microsoft.Win32;

namespace RadioEfirWPF
{
    public partial class MainWindow : Window
    {
        private static List<Song> musicLibrary = new List<Song>();
        private MediaPlayer player = new MediaPlayer();
        public MainWindow()
        {
            InitializeComponent();
            read();
        }
        static void read() //read from file
        {
            WorkWithFiles w = new WorkWithFiles();
            w.readFromFile();
            musicLibrary = w.ReadCollection;
        }
        private void fillSongsBox() //write songs in listbox
        {
            songsBox.Items.Clear();
            for (int i = 0; i < musicLibrary.Count; i++)
            {
                songsBox.Items.Add(musicLibrary[i].Artist + " - " + musicLibrary[i].SongName);
            }
        }
        private void allSongs_Click(object sender, RoutedEventArgs e) //run function to see all songs
        {
            fillSongsBox();
        }
        private void clearSongsBoxButton_Click(object sender, RoutedEventArgs e) //clear songsbox
        {
            //timeLabel.Content = "";
            artistText.Content = "";
            songText.Content = "";
            albumText.Content = "";
            yearText.Content = "";
            ratingText.Content = "";
            songsBox.Items.Clear();
            img.Source = null;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) //writing to xml file, if program is closing
        {
            WorkWithFiles w = new WorkWithFiles();
            w.writeToFile(musicLibrary);
        }
        private void addSong_Click(object sender, RoutedEventArgs e) //open new window to add song
        {
            songsBox.Items.Clear();
            addSongWindow addWindow = new addSongWindow();
            if (addWindow.ShowDialog() != null && addWindow.inputCheck == true)
                musicLibrary.Add(addWindow.tmp);
        }

        private void delSong_Click(object sender, RoutedEventArgs e) //deleting song from collection
        {
            Song tmp = new Song();
            if (songsBox.SelectedItem != null)
            {
                for (int i = 0; i < musicLibrary.Count; i++)
                    if (musicLibrary[i].Artist + " - " + musicLibrary[i].SongName == songsBox.SelectedValue.ToString())
                    {
                        musicLibrary.Remove(musicLibrary[i]);
                        break;
                    }
                fillSongsBox();
            }
            else
                MessageBox.Show("Please, choose song.");
        }
        private void setInfo() //fills information
        {
            img.Source = null;
            if (songsBox.SelectedItem != null)
            {
                for (int i = 0; i < musicLibrary.Count; i++)
                    if (musicLibrary[i].Artist + " - " + musicLibrary[i].SongName == songsBox.SelectedValue.ToString())
                    {
                        artistText.Content = musicLibrary[i].Artist;
                        songText.Content = musicLibrary[i].SongName;
                        albumText.Content = musicLibrary[i].Album;
                        yearText.Content = musicLibrary[i].Year;
                        ratingText.Content = musicLibrary[i].Rating;
                        if (File.Exists(musicLibrary[i].PathToSong))
                            player.Open(new Uri(musicLibrary[i].PathToSong));
                        if (File.Exists(musicLibrary[i].PathToPhoto))
                            img.Source = new BitmapImage(new Uri(musicLibrary[i].PathToPhoto));
                        break;
                    }
            }
        }
        private void songsBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            setInfo();
        }
        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            player.Play();  
        }
        private void pauseButton_Click(object sender, RoutedEventArgs e)
        {
            player.Pause();
        }
        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            player.Stop();
        }
        private void editSong_Click(object sender, RoutedEventArgs e)
        {
            if (songsBox.SelectedItem != null)
            {
                for (int i = 0; i < musicLibrary.Count; i++)
                { 
                    if (musicLibrary[i].Artist + " - " + musicLibrary[i].SongName == songsBox.SelectedValue.ToString())
                    {
                        editSongInfoWindow editSong = new editSongInfoWindow(musicLibrary[i]);
                        if (editSong.ShowDialog() != null && editSong.inputCheck == true)
                            musicLibrary[i] = editSong.tmp;
                        break;
                    }
                }
            }
            else
                MessageBox.Show("Ошибка!");
        }
    }
}
