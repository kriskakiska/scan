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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            createPassDict();
        }

        public List<string> logins = new List<string>();
        public List<string> passwords = new List<string>();
        public static Dictionary<string, string> passDict = new Dictionary<string, string>();
        public static List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
        public static string passDictFile = @"C:\Kristina\SSAU\7\ТП\ScanLastVersion\Scan\Dict\pass.dict";

        public void createPassDict()
        {
            if (!File.Exists(passDictFile))
            {
                File.Create(passDictFile);
            }
            else
            {
                parseDict(passDictFile);
                list.AddRange(passDict);
            }

        }

        private void button1_Click(object sender, EventArgs e) // кнопка входа при авторизации
        {
            string inputLogin = textBox1.Text;
            string inputPassword = textBox2.Text;
            if (inputLogin == "Admin" && inputPassword == "1234")
            {
                MakeScan ms = new MakeScan();
                this.Hide();
                ms.ShowDialog();
                this.Show();
                Form1.ActiveForm.Close();
            }
            else if (inputLogin == "Admin" && inputPassword != "1234")
            {
                MessageBox.Show("Неверный пароль!");
                textBox2.Text = "";
            }
            else
            {
                parseDict(passDictFile);
                list.AddRange(passDict);
                bool exist = false;
                bool correct = false;

                foreach (var item in list)
                {
                    if (textBox1.Text == item.Key)
                    {
                        exist = true;
                    }
                }

                if (exist == true)
                {
                    foreach (var item in list)
                    {
                        if (textBox1.Text == item.Key)
                        {
                            if (textBox2.Text == item.Value)
                            {
                                correct = true;
                            }
                        }
                    }
                    if (correct == true)
                    {
                        Welcome w = new Welcome();
                        this.Hide();
                        w.ShowDialog();
                        this.Show();
                        Form1.ActiveForm.Close();
                        correct = false;
                    }
                    else
                    {
                        textBox2.Text = "";
                        MessageBox.Show("Неверный пароль!");
                    }
                    exist = false;
                }
                else
                {
                    MessageBox.Show("Данного логина не существует.");
                }
            }
        }

        private void parseDict(string passDictFile)
        {
            string[] words = File.ReadAllLines(passDictFile, Encoding.GetEncoding("windows-1251")).Take(500).ToArray();
            for (int i = 0; i < words.Length - 1; i++)
            {
                string login = words[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
                string pass = words[i].Substring(words[i].IndexOf(' ') + 1); //TODO убрать костыль
                try
                {
                    Form1.passDict.Add(login, pass);
                }
                catch (ArgumentException)
                {
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            {
                Registration reg = new Registration();
                this.Hide();
                reg.ShowDialog();
                this.Show();
                Form1.ActiveForm.Close();
            }
        }
    }
}
