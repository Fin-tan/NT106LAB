using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAB3
{
    public partial class bai3_TCP_client : Form
    {
        private TcpClient tcpClient;
        private NetworkStream ns;

        public bai3_TCP_client()
        {
            InitializeComponent();
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Byte[] data = Encoding.UTF8.GetBytes("Hello Server \r\n");
            ns.Write(data, 0, data.Length);
        }

        private void bai3_TCP_client_Load(object sender, EventArgs e)
        {
            tcpClient = new TcpClient();
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 8080);

            tcpClient.Connect(ipEndPoint);
            ns = tcpClient.GetStream();
        }
        private void bai3_TCP_client_FormClosed(object sender, EventArgs e)
        {
            Byte[] data = System.Text.Encoding.ASCII.GetBytes("Quit\n");
            ns.Write(data, 0, data.Length);
            ns.Close();
            tcpClient.Close();
        }
    }
}
