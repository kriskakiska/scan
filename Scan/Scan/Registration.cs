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
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)//кнопка ок на регистрации
        {
            foreach (var item in Form1.list)
            {
                if(textBox1.Text==item.Key)
                {
                    MessageBox.Show("Логин уже существует");
                    return;
                }
            }
            if (textBox2.Text == textBox3.Text)
            {         
                Form1.passDict.Add(textBox1.Text.ToString(), textBox2.Text.ToString());
                Form1.list.Clear();
                Form1.list.AddRange(Form1.passDict);
                string s = "";
                foreach (var item in Form1.list)
                {
                    string def = item.Value;
                    s += item.Key + " " + def + "\n";
                }

                using (StreamWriter sw = new StreamWriter(Form1.passDictFile, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(s);
                }

                MessageBox.Show("Регистрация прошла успешно!");
                Form1 form = new Form1();
                this.Hide();
                form.ShowDialog();
                this.Show();
                Registration.ActiveForm.Close();
            }
            else
            {
                MessageBox.Show("Пароли не совпадают!");
                textBox2.Text = "";
                textBox3.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e) // кнопка Отмена на окне регистрации
        {
            Form1 form = new Form1();
            form.logins.Add(textBox1.Text);
            form.passwords.Add(textBox2.Text);

            this.Hide();
            form.ShowDialog();
            this.Show();

            Registration.ActiveForm.Close();
        }
    }
}
