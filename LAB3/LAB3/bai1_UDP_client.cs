using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAB3
{
    public partial class bai1_UDP_client : Form
    {
        public bai1_UDP_client()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            UdpClient udpClient = new UdpClient();
            udpClient.Connect(tbHost.Text, int.Parse(textport.Text));
            Byte[] sendBytes = Encoding.UTF8.GetBytes(text.Text);
            udpClient.Send(sendBytes, sendBytes.Length);
            udpClient.Close();
        }
    }
}
