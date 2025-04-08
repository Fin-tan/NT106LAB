using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace lab2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            if (openfile.ShowDialog()==DialogResult.OK)
            {
                StreamReader reader = new StreamReader(openfile.FileName,true);
                string content = reader.ReadToEnd();
                textBox1.Text = content;
                textBox2.Text = openfile.SafeFileName.ToString();
                textBox4.Text = openfile.FileName;
                textBox6.Text = content.Length.ToString();
                int linecount = File.ReadLines(openfile.FileName).Count();
                textBox3.Text = linecount.ToString();
                string[] source = content.Split(new char[] { '\r', '\n', '\t', '.', '?', '!', ' ', ':', ';', ',' },StringSplitOptions.RemoveEmptyEntries);
                textBox5.Text = source.Count().ToString();
            }
        }
    }
}
