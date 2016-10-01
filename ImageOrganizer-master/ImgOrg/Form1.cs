using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImgOrg
{
   
    public partial class Form1 : Form
    {
        string folder = ""; //YOUR FOLDER HERE
        List<string> files = new List<string>();
        int currentfile = 0;
        public Form1()
        {
            files = Directory.EnumerateFiles(folder).ToList();
            InitializeComponent();
            
            loadnextfile();
        }

        void loadnextfile()
        {
            if (currentfile < files.Count)
            {
                pictureBox1.ImageLocation = files[currentfile];
                currentfile++;
            }
        }

        void savecurrentto(string to)
        {
            File.Move(pictureBox1.ImageLocation, folder + to + "\\" + Path.GetFileName(pictureBox1.ImageLocation));
        }

        void saveandmove()
        {
            savecurrentto(textBox1.Text);
            textBox1.Text = "";
            loadnextfile();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                saveandmove();
            }
        }
    }
}
