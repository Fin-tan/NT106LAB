namespace lab4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bai1 bai1 = new bai1();
            bai1.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bai2 bai2 = new bai2();
            bai2.ShowDialog();
        }
    }
}
