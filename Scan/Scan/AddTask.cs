using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scan
{
    public partial class AddTask : Form
    {
        public AddTask()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str = Convert.ToString(comboBox1.SelectedItem);
            if(str == "Изображение")
            {
                Gal df = new Gal();
                this.Hide();
                df.ShowDialog();
                this.Show();
            }
            else if (str == "Слово")
            {
                Dict d = new Dict();
                d.ShowDialog();
                this.Close();
            }
            else
            {
                Catalog c = new Catalog();
                this.Hide();
                c.ShowDialog();
                this.Show();
            }
        }
    }
}
