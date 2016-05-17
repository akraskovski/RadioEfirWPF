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
        private static List<Playlist> playlists = new List<Playlist>();
        private MediaPlayer player = new MediaPlayer();
        public MainWindow()
        {
            InitializeComponent();
            read();
        }
        static void read() //read from file
        {
            WorkWithFiles w = new WorkWithFiles();
            w.readSongsFromFile();
            musicLibrary = w.ReadSongsCollection;
            w.readPlaylistsFromFile();
            playlists = w.ReadPlaylistsCollection;
        }
        private void fillSongsBox() //write songs in listbox
        {
            musicBox.Items.Clear();
            for (int i = 0; i < musicLibrary.Count; i++)
                musicBox.Items.Add(musicLibrary[i].Artist + " - " + musicLibrary[i].SongName);
        }
        private void fillPlaylists()
        {
            playlistsBox.Items.Clear();
            for (int i = 0; i < playlists.Count; i++)
                playlistsBox.Items.Add(playlists[i].Name);
        } //write playlists in listbox
        private void allSongs_Click(object sender, RoutedEventArgs e) //run function to see all songs
        {
            fillSongsBox();
        }
        private void clearAll()
        {
            artistText.Content = "";
            songText.Content = "";
            albumText.Content = "";
            yearText.Content = "";
            ratingText.Content = "";
            musicBox.Items.Clear();
            img.Source = null;
            playlistsBox.Items.Clear();
            playButton.Visibility = Visibility.Hidden;
            stopButton.Visibility = Visibility.Hidden;
            infoGrid.Visibility = Visibility.Hidden;
        }
        private void clearSongsBoxButton_Click(object sender, RoutedEventArgs e) //clear songsbox
        {
            clearAll();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) //writing to xml file, if program is closing
        {
            WorkWithFiles w = new WorkWithFiles();
            w.writeSongsToFile(musicLibrary);
            w.writePlaylistsToFile(playlists);
        }
        private void addSong_Click(object sender, RoutedEventArgs e) //open new window to add song
        {
            musicBox.Items.Clear();
            addSongWindow addWindow = new addSongWindow();
            if (addWindow.ShowDialog() != null && addWindow.inputCheck == true)
                musicLibrary.Add(addWindow.tmp);
        }
        private void delSong_Click(object sender, RoutedEventArgs e) //deleting song from collection
        {
            Song tmp = new Song();
            if (musicBox.SelectedItem != null)
            {
                for (int i = 0; i < musicLibrary.Count; i++)
                    if (musicLibrary[i].Artist + " - " + musicLibrary[i].SongName == musicBox.SelectedValue.ToString())
                    {
                        musicLibrary.Remove(musicLibrary[i]);
                        break;
                    }
                fillSongsBox();
            }
            else
                MessageBox.Show("Please, choose song.");
        }
        private void setInfoForSongs() //fill information
        {
            img.Source = null;
            if (musicBox.SelectedItem != null)
            {
                playButton.Visibility = Visibility.Visible;
                stopButton.Visibility = Visibility.Visible;
                infoGrid.Visibility = Visibility.Visible;
                for (int i = 0; i < musicLibrary.Count; i++)
                    if (musicLibrary[i].Artist + " - " + musicLibrary[i].SongName == musicBox.SelectedValue.ToString())
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
        private void setInfoForPlaylist(int index) //fill information
        {
            musicBox.Items.Clear();
            for (int i = 0; i < playlists[index].Songs.Count; i++)
                musicBox.Items.Add(playlists[index].Songs[i].Artist + " - " + playlists[index].Songs[i].SongName);
        }
        private void musicBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            setInfoForSongs();
        }
        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            player.Play();
        }
        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            player.Stop();
        }
        private void editSong_Click(object sender, RoutedEventArgs e)
        {
            if (musicBox.SelectedItem != null)
            {
                for (int i = 0; i < musicLibrary.Count; i++)
                    if (musicLibrary[i].Artist + " - " + musicLibrary[i].SongName == musicBox.SelectedValue.ToString())
                    {
                        editSongInfoWindow editSong = new editSongInfoWindow(musicLibrary[i]);
                        if (editSong.ShowDialog() != null && editSong.inputCheck == true)
                            musicLibrary[i] = editSong.tmp;
                        break;
                    }
            }
            else
                MessageBox.Show("Ошибка!");
        }

        private void allPlaylists_Click(object sender, RoutedEventArgs e)
        {
            fillPlaylists();
        }

        private void playlistsBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (playlistsBox.SelectedIndex > -1)
                setInfoForPlaylist(playlistsBox.SelectedIndex);
        }
        private void TabItem_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            playlistsBox.Visibility = Visibility.Hidden;
            playlistLabel.Visibility = Visibility.Hidden;
            clearAll();
        } //for library
        private void TabItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            playlistsBox.Visibility = Visibility.Visible;
            playlistLabel.Visibility = Visibility.Visible;
            clearAll();
        } //for playlists
    }
}
