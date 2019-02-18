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
    public partial class Gal : Form
    {
        public static Dictionary<string, string> dictGal1 = new Dictionary<string, string>();
        public static List<KeyValuePair<string, string>> listGal1 = new List<KeyValuePair<string, string>>();
        public Gal()
        {
            InitializeComponent();
            downloadGallery();
        }
        private void downloadGallery()//загрузка галереи при добавлении задания-изображения на сканворд
        {
            string wayGallery= ParamScan.wayGallery;
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(100, 100);
            listView1.Items.Clear();
            parseGal(wayGallery);
            listGal1.AddRange(dictGal1); 
            int i = 0;
            foreach (var item in listGal1)
            {
                string wayPicture = item.Value;
                string answerPicture = item.Key;
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(wayPicture);
                imageList.Images.Add(bitmap);
                ListViewItem listViewItem = new ListViewItem(new string[] { "", answerPicture });
                listViewItem.ImageIndex = i;
                listView1.Items.Add(listViewItem);
                listView1.SmallImageList = imageList;
                i++;
            }
        }
        public static void parseGal(string filename)// парс для галереи
        {
            string[] words = File.ReadAllLines(filename, Encoding.GetEncoding("windows-1251")).Take(500).ToArray();
            for (int i = 0; i <= words.Length-2; i++)
            {
                string id = words[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
                string wayForPicture = words[i].Substring(words[i].IndexOf(' ') + 1); //TODO убрать костыль
                try
                {
                    dictGal1.Add(id, wayForPicture);
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("В галереи имеются одинаковые изображения");
                    return;
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e) // функция добавления задания в выбранную клетку
        {
            if (listView1.SelectedItems.Count > 0)
            {
                string selectedrowindex = listView1.SelectedItems.ToString();
            
              //  DataGridViewRow selectedRow = dgv1.Rows[selectedrowindex];
                int idTask = ParamScan.NewScan.getTasks().Count + 1;
                Task newTask = new Task(idTask, selectedrowindex, "Что изображено на картинке?", MakeScan.x, MakeScan.y);
                ParamScan.NewScan.addTask(newTask);
                MakeScan.lengthAnswer = newTask.getAnswer().Length;
            }
        }

        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}
