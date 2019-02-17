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

namespace Scan
{
    public partial class DownloadGal : Form
    {
        public static string wayPicture = "";
        public static Dictionary<string, string> dictGal = new Dictionary<string, string>();
        public static List<KeyValuePair<string, string>> listGal = new List<KeyValuePair<string, string>>();
        public DownloadGal()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.DefaultExt = ".pic";
            openFileDialog1.InitialDirectory = @"..\..\Gallery\";
            openFileDialog1.AddExtension = true;
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Файл галереи (*.pic)|*.pic";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                dictGal.Clear();
                listGal.Clear();

                parseGal(openFileDialog1.FileName);
                listGal.AddRange(dictGal);
                /* foreach (var item in listGal) {
                     string wayPicture = item.Value;
                     textBox1.Text = openFileDialog1.FileName;
                     System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(wayPicture);
                     pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                     pictureBox1.Image = bitmap;
                 }*/
            }
            textBox1.Text = openFileDialog1.FileName;
        }

        public static void parseGal(string filename)//парс для галереи
        {
            string[] words = File.ReadAllLines(filename, Encoding.GetEncoding("windows-1251")).Take(500).ToArray();
            for (int i = 0; i < words.Length; i++)
            {
                string idPicture = words[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
                string wayForPicture = words[i].Substring(words[i].IndexOf(' ') + 1); //TODO убрать костыль
                try
                {
                    dictGal.Add(idPicture, wayForPicture);
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("В галерее имеются одинаковые изображения");
                    return;
                }
            }
        }
    }
}
