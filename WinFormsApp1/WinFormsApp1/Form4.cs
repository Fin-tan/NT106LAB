using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void btndoc_Click(object sender, EventArgs e)
        {
            if (textBox1.Text==" ")
            {
                MessageBox.Show("Vui lòng nhập số vào ô trống !",
                "Errors", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (int.TryParse(textBox1.Text.Trim(), out int number))
            {
                if (number >= 0 && number <= 9999)
                {
                    label1.Text = ConvertNumberToWords(number);
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập số từ 1 đến 9999!",
                    "Errors", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập số hợp lệ!",
                "Errors", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private string ConvertNumberToWords(int number)
        {
            string[] units = { "", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string[] tens = { "", "mười", "hai mươi", "ba mươi", "bốn mươi", "năm mươi", "sáu mươi", "bảy mươi", "tám mươi", "chín mươi" };

            if (number == 0) return "không";

            string words = "";
            int thousand = number / 1000;
            int hundred = (number % 1000) / 100;
            int ten = (number % 100) / 10;
            int unit = number % 10;

            if (thousand > 0)
            {
                words += units[thousand] + " nghìn ";
            }

            if (hundred > 0)
            {
                words += units[hundred] + " trăm ";
            }
            else if (thousand > 0)
            {
                words += "không trăm ";
            }

            if (ten > 1)
            {
                words += tens[ten] + " ";
                if (unit > 0)
                {
                    words += units[unit];
                }
            }
            else if (ten == 1)
            {
                words += "mười ";
                if (unit > 0)
                {
                    words += (unit == 5) ? "lăm" : units[unit];
                }
            }
            else if (unit > 0)
            {
                words += "lẻ " + ((unit == 5) ? "lăm" : units[unit]);
            }

            return words.Trim();
        }


        

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            label1.Text = " ";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
