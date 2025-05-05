using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAB3
{
    public partial class control : Form
    {
        public control()
        {
            InitializeComponent();
        }

        private void bai1_Click(object sender, EventArgs e)
        {
            Form1 bai1 = new Form1();
            bai1.ShowDialog();
        }

        private void bai2_Click(object sender, EventArgs e)
        {
            Form2 bai2 = new Form2();
            bai2.ShowDialog();
        }

        private void bai3_Click(object sender, EventArgs e)
        {
            Form3 bai3 = new Form3();
            bai3.ShowDialog();
        }

        private void bai4_Click(object sender, EventArgs e)
        {
            Form4 bai4 = new Form4();
            bai4.ShowDialog();
        }
    }
}
