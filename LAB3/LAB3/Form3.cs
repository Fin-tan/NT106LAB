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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void TCP_sv_Click(object sender, EventArgs e)
        {
            bai3_TCP_server server = new bai3_TCP_server();
            server.Show();
        }

        private void TCP_client_Click(object sender, EventArgs e)
        {
            bai3_TCP_client client = new bai3_TCP_client();
            client.Show();
        }
    }
}
