namespace LAB3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void UDP_client_TextChanged(object sender, EventArgs e)
        {

        }

        private void UDPsv_TextChanged(object sender, EventArgs e)
        {

        }

        private void UDPsv_Click(object sender, EventArgs e)
        {
             bai1_UDP_server Server = new bai1_UDP_server();
            Server.Show();
        }

        private void UDP_client_Click(object sender, EventArgs e)
        {
            bai1_UDP_client client = new bai1_UDP_client();
            client.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
