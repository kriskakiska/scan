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
    public partial class ScanUser : Form
    {
        static List<Task> tasks = new List<Task>();
        public static DataGridViewTextBoxCell selectedCell;
        public static DataGridView dgvScan = new DataGridView();
        public static int x;
        public static int y;
        static Task selectedTask = null;

        public ScanUser()
        {
            InitializeComponent();
            dataGridView1.CellClick += new DataGridViewCellEventHandler(dataGridView1_CellClick);
            dgvScan = dataGridView1;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Syst df = new Syst();
            df.ShowDialog();
            this.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            SaveScan df = new SaveScan();
            SaveScan.userScan = true;
            this.Hide();
            df.ShowDialog();
            this.Show();
        }

        private void ScanUser_Load(object sender, EventArgs e)
        {

        }

        public void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        { 
            selectedCell = (DataGridViewTextBoxCell)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (selectedCell.Value != null && selectedCell.Value.GetType() == typeof(int))
            {
                selectedTask = DownloadScan.ScanUser.getTask(Convert.ToInt32(selectedCell.Value));
                button1.Visible = true;
                button1.Enabled = true;

                string question = DownloadScan.ScanUser.getTask(Convert.ToInt32(selectedCell.Value)).getQuestion();
                string answer = DownloadScan.ScanUser.getTask(Convert.ToInt32(selectedCell.Value)).getAnswer();
                MessageBox.Show(question);
                x = DownloadScan.ScanUser.getTask(Convert.ToInt32(selectedCell.Value)).getX();
                y = DownloadScan.ScanUser.getTask(Convert.ToInt32(selectedCell.Value)).getY();

                if (DownloadScan.ScanUser.getTask(Convert.ToInt32(selectedCell.Value)).getDirection() == 3) // если напрваление вправо
                {
                    for (int i = 0; i < answer.Length; i++)
                    {
                        dgvScan.Rows[x].Cells[y + i + 1].Style.BackColor = System.Drawing.Color.Blue;
                    }
                }

                if (DownloadScan.ScanUser.getTask(Convert.ToInt32(selectedCell.Value)).getDirection() == 4) // если напрваление вниз вправо
                {
                    for (int i = 0; i < answer.Length; i++)
                    {
                        dgvScan.Rows[x + 1].Cells[y + i].Style.BackColor = System.Drawing.Color.Blue;
                    }
                }

                if (DownloadScan.ScanUser.getTask(Convert.ToInt32(selectedCell.Value)).getDirection() == 5) // если напрваление вправо вниз
                {
                    for (int i = 0; i < answer.Length; i++)
                    {
                        dgvScan.Rows[x + i].Cells[y + 1].Style.BackColor = System.Drawing.Color.Blue;
                    }
                }

                if (DownloadScan.ScanUser.getTask(Convert.ToInt32(selectedCell.Value)).getDirection() == 6) // если напрваление вниз 
                {
                    for (int i = 0; i < answer.Length; i++)
                    {
                        dgvScan.Rows[x + i + 1].Cells[y].Style.BackColor = System.Drawing.Color.Blue;
                    }
                }

                if (DownloadScan.ScanUser.getTask(Convert.ToInt32(selectedCell.Value)).getDirection() == 7) // если напрваление вверх право
                {
                    for (int i = 0; i < answer.Length; i++)
                    {
                        dgvScan.Rows[x - 1].Cells[y + i].Style.BackColor = System.Drawing.Color.Blue;
                    }
                }

                if (DownloadScan.ScanUser.getTask(Convert.ToInt32(selectedCell.Value)).getDirection() == 8) // если напрваление влево вниз 
                {
                    for (int i = 0; i < answer.Length; i++)
                    {
                        dgvScan.Rows[x + i].Cells[y - 1].Style.BackColor = System.Drawing.Color.Blue;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            x = selectedTask.getX();
            y = selectedTask.getY();
            int dir = selectedTask.getDirection();
            string answer = selectedTask.getAnswer();

            string newAnswer = "";

            if (dir == 3) // если напрваление вправо
            {
                for (int i = 0; i < answer.Length; i++)
                {
                    newAnswer += dgvScan.Rows[x].Cells[y + i + 1].Value;
                    dgvScan.Rows[x].Cells[y + i + 1].Style.BackColor = System.Drawing.Color.White;
                }
            }

            if (dir == 4) // если напрваление вниз вправо
            {
                for (int i = 0; i < answer.Length; i++)
                {
                    newAnswer += dgvScan.Rows[x + 1].Cells[y + i].Value;
                    dgvScan.Rows[x + 1].Cells[y + i].Style.BackColor = System.Drawing.Color.White;
                }
            }

            if (dir == 5) // если напрваление вправо вниз
            {
                for (int i = 0; i < answer.Length; i++)
                {
                    newAnswer += dgvScan.Rows[x + i].Cells[y + 1].Value;
                    dgvScan.Rows[x + i].Cells[y + 1].Style.BackColor = System.Drawing.Color.White;
                }
            }

            if (dir == 6) // если напрваление вниз 
            {
                for (int i = 0; i < answer.Length; i++)
                {
                    newAnswer += dgvScan.Rows[x + i + 1].Cells[y].Value;
                    dgvScan.Rows[x + i + 1].Cells[y].Style.BackColor = System.Drawing.Color.White;
                }
            }

            if (dir == 7) // если напрваление вверх право
            {
                for (int i = 0; i < answer.Length; i++)
                {
                    newAnswer += dgvScan.Rows[x - 1].Cells[y + i].Value;
                    dgvScan.Rows[x - 1].Cells[y + i].Style.BackColor = System.Drawing.Color.White;
                }
            }

            if (dir == 8) // если напрваление влево вниз 
            {
                for (int i = 0; i < answer.Length; i++)
                {
                    newAnswer += dgvScan.Rows[x + i].Cells[y - 1].Value;
                    dgvScan.Rows[x + i].Cells[y - 1].Style.BackColor = System.Drawing.Color.White;
                }
            }
            DownloadScan.ScanUser.getTask(selectedTask.getID()).setTaskAnswer(newAnswer);
            selectedTask = null;
        }
    }
}
