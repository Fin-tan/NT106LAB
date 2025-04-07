using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab2
{
    public partial class Formchung : Form
    {
        public Formchung()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 bai1 = new Form1();
            bai1.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 bai2 = new Form2();
            bai2.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form4 bai4 = new Form4();
            bai4.ShowDialog();
        }
    }
}
