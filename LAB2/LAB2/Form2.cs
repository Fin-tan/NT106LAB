using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
            using (OpenFileDialog openFile = new OpenFileDialog())
            {
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                   
                    string fileContent;
                    using (StreamReader reader = new StreamReader(openFile.FileName, Encoding.UTF8))
                    {
                        fileContent = reader.ReadToEnd();
                    }

                  
                    textBox1.Text = fileContent;
                    textBox2.Text = openFile.SafeFileName;
                    textBox4.Text = openFile.FileName;
                    textBox6.Text = fileContent.Length.ToString();

                   
                    string[] lines = fileContent.Split(new[] {"\rn", "\n" }, StringSplitOptions.None);
                    int realLineCount = lines.Count(line => !string.IsNullOrWhiteSpace(line));
                    textBox3.Text = realLineCount.ToString();

                   
                    char[] wordDelimiters = { ' ', '\r', '\n', '\t', '.', '?', '!', ':', ';', ',' };
                    int wordCount = fileContent.Split(wordDelimiters, StringSplitOptions.RemoveEmptyEntries).Length;
                    textBox5.Text = wordCount.ToString();
                }
            }
        }
    }
}