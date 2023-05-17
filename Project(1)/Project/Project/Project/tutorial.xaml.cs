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

namespace Project
{
    /// <summary>
    /// Interaction logic for tutorial.xaml
    /// </summary>
    public partial class tutorial : Window
    {
        public tutorial()
        {
            InitializeComponent();
            PlayVideo(@"C:\Users\jingw\Desktop\Project\video.mp4");
        }

        private void PlayVideo(string videoPath)
        {
            myVideo.Source = new Uri(videoPath, UriKind.RelativeOrAbsolute);
            myVideo.Play();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            //MainWindow mainPage = new MainWindow();
            //mainPage.Show();
            this.Close();
        }

        private void btnPlay1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPlay2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPlay3_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPlay4_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPlay5_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPlay6_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
