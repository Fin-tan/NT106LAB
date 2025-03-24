namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int num1, num2;
            int lsum;
            if (!int.TryParse(textBox1.Text, out num1))
            {
                MessageBox.Show("Vui lòng nhập số nguyên");
                return;
            }

            if (!int.TryParse(textBox2.Text, out num2))
            {
                MessageBox.Show("Vui lòng nhập số nguyên");
                return;
            }
            lsum = num1 + num2;
            textBox3.Text = lsum.ToString();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
