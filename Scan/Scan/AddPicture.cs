﻿using System;
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
    public partial class AddPicture : Form
    {
        ImageList imageList = new ImageList();
        int i = 0;
         MakeScan msk = new MakeScan();
        public static Dictionary<string, string> dictGal2 = new Dictionary<string, string>();
        public static List<KeyValuePair<string, string>> listGal2 = new List<KeyValuePair<string, string>>();
       
        public AddPicture()
        {
            InitializeComponent();
            dictGal2.Clear();
            listGal2.Clear();
        }

        private void button1_Click(object sender, EventArgs e)//обзор на добавление картинки
        {
            openFileDialog1.DefaultExt = ".png";
            openFileDialog1.InitialDirectory = @"..\..\Gallery\";
            openFileDialog1.AddExtension = true;
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Файл изображения (*.png)|*.png";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
              //  string answerPicture = textBox2.Text;
               // string wayPicture = textBox1.Text;
            }
        }

        public void button2_Click(object sender, EventArgs e)
        {
            //this.Hide();
            dictGal2.Clear();
            string answerPicture = textBox2.Text;
            string wayPicture = textBox1.Text;
            imageList.ImageSize = new Size(100, 100);
            dictGal2.Add(answerPicture, wayPicture);
            listGal2.AddRange(dictGal2);
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(wayPicture);
            imageList.Images.Add(bitmap);
            ListViewItem listViewItem = new ListViewItem(new string[] { "", answerPicture });
            listViewItem.ImageIndex = i;
            //MakeScan msk = new MakeScan();
            msk.listView1.Items.Add(listViewItem);
            msk.listView1.SmallImageList = imageList;
            msk.Show();
            i++;
        }
    }
}
