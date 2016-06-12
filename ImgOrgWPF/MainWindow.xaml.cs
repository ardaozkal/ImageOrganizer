using System;
using System.Collections.Generic;
using System.IO;
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

namespace ImgOrgWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    string folder = ""; //YOUR FOLDER HERE.

    public partial class MainWindow : Window
    {
        List<string> files = new List<string>();
        int currentfile = -1;
        public MainWindow()
        {
            InitializeComponent();
            files = Directory.EnumerateFiles(folder).ToList();
            loadnextfile();
        }

        void loadnextfile()
        {
            textBlock.Text = (currentfile + 1) + "/" + files.Count;
            if (currentfile < files.Count)
            {
                currentfile++;
                try
                {
                    image.Source = LoadBitmapImage(files[currentfile]);
                }
                catch
                {
                    loadnextfile();
                }
            }
        }

        void savecurrentto(string to)
        {
            try
            {
                var _from = files[currentfile];
                if (_from.EndsWith("'") || _from.EndsWith(")"))
                {
                    _from = _from.Substring(_from.Length - 2);
                }
                var _to = folder + to + "\\" + System.IO.Path.GetFileName(_from);

                if (File.Exists(_to))
                {
                    File.Delete(_from);
                }
                else
                {
                    File.Move(_from, _to);
                }
            }
            catch
            {

            }
        }

        public BitmapImage LoadBitmapImage(string fileName)
        {//http://stackoverflow.com/questions/18167280/image-file-copy-is-being-used-by-another-process
            try
            {
                if (fileName.EndsWith("'") || fileName.EndsWith(")"))
                {
                    fileName = fileName.Substring(fileName.Length - 2);
                }
                using (var stream = new FileStream(fileName, FileMode.Open))
                {
                    var bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.StreamSource = stream;
                    bitmapImage.EndInit();
                    bitmapImage.Freeze();
                    return bitmapImage;
                }
            }
            catch
            {
                return null;
            }
        }

        void saveandmove()
        {
            savecurrentto(textBox.Text);
            textBox.Text = "";
            loadnextfile();
        }

        private void textBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                saveandmove();
            }
        }
    }
}
