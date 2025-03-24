using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {


        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            float num1, num2, num3;
            if (!float.TryParse(textBox1.Text, CultureInfo.InvariantCulture, out num1))
            {
                MessageBox.Show("vui long nhap so nguyen");
                return;
            }


            if (!float.TryParse(textBox2.Text, CultureInfo.InvariantCulture, out num2))
            {
                MessageBox.Show("vui long nhap so nguyen");
                return;
            }

            if (!float.TryParse(textBox3.Text, CultureInfo.InvariantCulture, out num3))
            {
                MessageBox.Show("vui long nhap so nguyen");
                return;
            }

            float Max = Math.Max(num1, Math.Max(num2, num3));
            float Min = Math.Min(num1, Math.Min(num2, num3));
            textBox4.Text = Max.ToString(CultureInfo.InvariantCulture);

            textBox5.Text = Min.ToString(CultureInfo.InvariantCulture);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
