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
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;

namespace RadioEfirWPF
{
    public partial class addSongWindow : Window
    {
        public Song tmp = new Song();
        public bool inputCheck = true;
        private string photo = "";
        public addSongWindow()
        {
            InitializeComponent();
        }
        private void setInfo()
        {
            tmp.Artist = artistTextBox.Text;
            tmp.SongName = songNameTextBox.Text;
            tmp.Album = albumTextBox.Text;
            tmp.Rating = int.Parse(ratingTextBox.Text);
            tmp.Year = int.Parse(yearTextBox.Text);
            tmp.PathToSong = songPathTextBox.Text;
            tmp.PathToPhoto = photo;
        }
        public void check()
        {
            if (artistTextBox.Text == "")
                inputCheck = false;
            if (songNameTextBox.Text == "")
                inputCheck = false;
            if (albumTextBox.Text == "")
                inputCheck = false;
            if (photo == "")
                inputCheck = false;
            if (songPathTextBox.Text == "")
                inputCheck = false;
            if (ratingTextBox.Text == "")
                inputCheck = false;
            if (yearTextBox.Text == "")
                inputCheck = false;
        }
        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            check();
            if (inputCheck)
            {
                setInfo();
                MessageBox.Show("Sucsessful!");
                Close();
            }
            else
            {
                MessageBox.Show("Fill all information currectly!");
                inputCheck = true;
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            check();
        }
        private void pathButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog() { Filter = "Музыка(*.MP3)|*.MP3;" };
            if (openFileDialog1.ShowDialog() != null)
                songPathTextBox.Text = openFileDialog1.FileName;
        }

        private void photoButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog() { Filter = "Картинки(*.BMP, *.JPG, *.JPEG, *.PNG)|*.BMP;*.JPG;*.JPEG;*.PNG" };
            if (openFileDialog1.ShowDialog() != null)
            {
                photo = openFileDialog1.FileName;
                img.Source = new BitmapImage(new Uri(photo));
            }
        }
    }
}
