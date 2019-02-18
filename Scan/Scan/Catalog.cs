using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Media;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scan
{
    public partial class Catalog : Form
    {
        public SoundPlayer player;
        private void InitializeSound()
        {
            player = new SoundPlayer();
           /* player.SoundLocation = Text;
            player.Load();*/

        }
        public static Dictionary<string, string> dictCat1 = new Dictionary<string, string>();
        public static List<KeyValuePair<string, string>> listCat1 = new List<KeyValuePair<string, string>>();
        public Catalog()
        {
            InitializeComponent();
        }

        private void downloadCatalog()//загрузка каталога
        {
            string wayCatalog = ParamScan.wayCatalog;
            //List musicList = new List();
            //imageList.ImageSize = new Size(100, 100);
            listCat.Items.Clear();
            parseCat(wayCatalog);
            listCat1.AddRange(dictCat1);
            int i = 0;
            foreach (var item in listCat1)
            {
                string wayAudio = item.Value;
                string answerAudio = item.Key;
                //System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(wayPicture);
                //imageList.Images.Add(bitmap);
                ListViewItem listViewItem = new ListViewItem(new string[] { "", answerAudio });
                listViewItem.ImageIndex = i;
                listCat.Items.Add(listViewItem);
                //listCat.SmallImageList = imageList;
                i++;
            }
        }

        public static void parseCat(string filename)// парс для галереи
        {
            string[] words = File.ReadAllLines(filename, Encoding.GetEncoding("windows-1251")).Take(500).ToArray();
            for (int i = 0; i <= words.Length - 2; i++)
            {
                string id = words[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
                string wayForMusic = words[i].Substring(words[i].IndexOf(' ') + 1); //TODO убрать костыль
                try
                {
                    dictCat1.Add(id, wayForMusic);
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("В галереи имеются одинаковые изображения");
                    return;
                }
            }
        }
        /*OpenFileDialog ofd = new OpenFileDialog();
        StringBuilder buffer = new StringBuilder(128);
        int second;
        int minutes;
        string CommandString;
        [DllImport("winmm.dll")]
        private static extern long mciSendString(string lpstrCommand, StringBuilder lpstrReturnString, int uReturnLength, int hwndCallback);
        public Form1()
        {
            InitializeComponent();
            txtpath.ReadOnly = true;
        }
 
        private void btnplay_Click(object sender, EventArgs e)
        {
            if (ofd.FileName == "")
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    ofd.Filter = "MP3 Files|*.mp3";
                    CommandString = "open " + "\"" + ofd.FileName + "\"" + " type MPEGVideo alias Mp3File";
                    mciSendString(CommandString, null, 0, 0);
                    CommandString = "play Mp3File";
                    mciSendString(CommandString, null, 0, 0);
                    timer1.Enabled = true;
                }
            }
 
            else
            {
                CommandString = "play Mp3File";
                mciSendString(CommandString, null, 0, 0);
 
                timer1.Enabled = true;
            }
        }
 
        private void btnpause_Click(object sender, EventArgs e)
        {
            CommandString = "pause mp3file";
            mciSendString(CommandString, null, 0, 0);
        }
 
        private void btnbrowse_Click(object sender, EventArgs e)
        {
            ofd.Filter = "Mp3 files |*.mp3";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtpath.Text = ofd.FileName;
                CommandString = "close Mp3File";
                mciSendString(CommandString, null, 0, 0);
                timer1.Enabled = false;
                CommandString = "open " + "\"" + ofd.FileName + "\"" + " type MPEGVideo alias Mp3File";
                mciSendString(CommandString, null, 0, 0);
            }
 
        }
 
        private void timer1_Tick(object sender, EventArgs e)
        {
            CommandString = "Status Mp3File position";
            mciSendString(CommandString, buffer, 128, 0);
            second = int.Parse(buffer.ToString());
            second = second / 1000;
            minutes = second / 60;
            second = second % 60;
            lbltime.Text = minutes.ToString("00") + ":" + second.ToString("00");
        }*/

    }
}
