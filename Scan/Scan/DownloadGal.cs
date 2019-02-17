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
        MakeScan ms = new MakeScan();
        public DownloadGal()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)//загрузка галереи на форме makeScan-галерея
        {
            openFileDialog1.DefaultExt = ".pic";
            openFileDialog1.InitialDirectory = @"..\..\Gallery\";
            openFileDialog1.AddExtension = true;
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Файл галереи (*.pic)|*.pic";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                AddPicture.dictGal2.Clear();
                AddPicture.listGal2.Clear();
                DownloadGal.parseGal(openFileDialog1.FileName);
                AddPicture.listGal2.AddRange(AddPicture.dictGal2);
                textBox1.Text = openFileDialog1.FileName;
            }
        }
        public static void parseGal(string filename)// парс для галереи
        {
            string[] words = File.ReadAllLines(filename, Encoding.GetEncoding("windows-1251")).Take(500).ToArray();
            for (int i = 0; i <= words.Length - 2; i++)
            {
                string id = words[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
                string wayForPicture = words[i].Substring(words[i].IndexOf(' ') + 1); //TODO убрать костыль
                try
                {
                    AddPicture.dictGal2.Add(id, wayForPicture);
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("В галереи имеются одинаковые изображения");
                    return;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = 0;
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(100, 100);
            ms.listView1.Items.Clear();
            foreach (var item in AddPicture.listGal2)
            {
                string answerPicture = item.Key;
                string wayPicture = item.Value;
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(wayPicture);
                
                imageList.Images.Add(bitmap);
                ListViewItem listViewItem = new ListViewItem(new string[] { "", answerPicture });
                listViewItem.ImageIndex = i;
                ms.listView1.Items.Add(listViewItem);
                ms.listView1.SmallImageList = imageList;
                i++;
            }
            ms.Show();
        }
    }
}
