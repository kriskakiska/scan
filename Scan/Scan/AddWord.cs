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
    public partial class AddWord : Form
    {
        public AddWord()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string word = textBox1.Text;
            string answer = textBox2.Text;
           // MakeScan ms = new MakeScan();


            DataGridViewRow row = new DataGridViewRow();
            DataGridViewColumn col1 = new DataGridViewColumn();
            DataGridViewColumn col2 = new DataGridViewColumn();

            row.CreateCells(MakeScan.dgv);
            col1.CellTemplate = new DataGridViewTextBoxCell();
            col2.CellTemplate = new DataGridViewTextBoxCell();

            MakeScan.dgv.Columns.Add(col1);
            MakeScan.dgv.Columns.Add(col2);
            MakeScan.dgv.Rows.Add(row);
            //ms.dataGridView2.Rows[ms.dataGridView2.RowCount-1].Cells[0].Value = word;
            MakeScan.dgv.Rows[0].Cells[0].Value = word;
            MakeScan.dgv.Rows[0].Cells[1].Value = answer;
            // ms.dataGridView2.Rows[ms.dataGridView2.RowCount-1].Cells[1].Value = answer;
            MakeScan.dgv.Visible = true;
            this.Hide();
            //ms.Hide();
             // MakeScan.ActiveForm.Visible = true;
           // ms.ShowDialog();
        }
    }
    
}
