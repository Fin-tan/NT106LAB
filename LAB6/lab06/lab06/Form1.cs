using System;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace lab06
{
    public partial class Form1 : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        private BinaryReader reader;
        private BinaryWriter writer;
        private Thread listenThread;

        private Bitmap canvasBmp;
        private Graphics canvasG;
        private readonly object canvasLock = new object();

        private bool drawing = false;
        private Point lastPoint;
        private Color currentColor = Color.Black;
        private int currentThickness = 2;

        public Form1()
        {
            InitializeComponent();

            // Gán sự kiện
            this.Load += Form1_Load;

            btnConnect.Click += BtnConnect_Click;
            btnColor.Click += BtnColor_Click;
            nudThickness.Minimum = 1;
            nudThickness.Maximum = 20;
            nudThickness.Value = 2;
            nudThickness.ValueChanged += NudThickness_ValueChanged;
            btnEnd.Click += BtnEnd_Click;

           
            pictureBox1.MouseDown += PanelCanvas_MouseDown;
            pictureBox1.MouseMove += PanelCanvas_MouseMove;
            pictureBox1.MouseUp += PanelCanvas_MouseUp;

            lblClientCount.Text = "Clients: 0";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            canvasBmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            canvasG = Graphics.FromImage(canvasBmp);
            canvasG.Clear(Color.White);
            pictureBox1.Image = canvasBmp; // rất quan trọng!

        }

        private void PanelCanvas_Paint(object sender, PaintEventArgs e)
        {
            lock (canvasLock)
            {
                if (canvasBmp != null)
                {
                    e.Graphics.DrawImage(canvasBmp, Point.Empty);
                }
            }
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            if (client != null && client.Connected)
                return;

            string ip="127.0.0.1";
            int port = 4871; // Phải khớp port server

            try
            {
                client = new TcpClient(ip, port);
                stream = client.GetStream();
                reader = new BinaryReader(stream, System.Text.Encoding.UTF8, leaveOpen: true);
                writer = new BinaryWriter(stream, System.Text.Encoding.UTF8, leaveOpen: true);

                listenThread = new Thread(ListenServer) { IsBackground = true };
                listenThread.Start();

                btnConnect.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kết nối lỗi: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ListenServer()
        {
            try
            {
                while (client != null && client.Connected)
                {
                    string cmd;
                    try
                    {
                        cmd = reader.ReadString();
                    }
                    catch
                    {
                        break;
                    }

                    switch (cmd)
                    {
                        case "DRAW":
                            ReceiveDraw();
                            break;
                        case "IMAGE":
                            ReceiveImage();
                            break;
                        case "COUNT":
                            ReceiveCount();
                            break;
                        case "SERVEREND":
                            Invoke(new Action(() =>
                            {
                                MessageBox.Show("Server đã đóng kết nối.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                CleanupAndReset();
                            }));
                            return;
                        default:
                            break;
                    }
                }
            }
            catch
            {
                // ignore
            }
            finally
            {
                Invoke(new Action(() => CleanupAndReset()));
            }
        }

        private void ReceiveDraw()
        {
            try
            {
                int x1 = reader.ReadInt32();
                int y1 = reader.ReadInt32();
                int x2 = reader.ReadInt32();
                int y2 = reader.ReadInt32();
                int argb = reader.ReadInt32();
                int width = reader.ReadInt32();

                Color color = Color.FromArgb(argb);
                lock (canvasLock)
                {
                    using var pen = new Pen(color, width);
                    canvasG.DrawLine(pen, new Point(x1, y1), new Point(x2, y2));
                }
                Invoke(new Action(() => pictureBox1.Refresh()));

            }
            catch
            {
                // ignore
            }
        }

        private void ReceiveImage()
        {
            try
            {
                int length = reader.ReadInt32();
                byte[] data = reader.ReadBytes(length);
                Bitmap bmp;
                using (var ms = new MemoryStream(data))
                {
                    bmp = new Bitmap(ms);
                }
                lock (canvasLock)
                {
                    canvasG.Dispose();
                    canvasBmp.Dispose();
                    canvasBmp = bmp;
                    canvasG = Graphics.FromImage(canvasBmp);
                }
                Invoke(new Action(() => pictureBox1.Refresh()));

            }
            catch
            {
                // ignore
            }
        }

        private void ReceiveCount()
        {
            try
            {
                int count = reader.ReadInt32();
                Invoke(new Action(() => lblClientCount.Text = $"Clients: {count}"));
            }
            catch
            {
                // ignore
            }
        }

        private void PanelCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (client == null || !client.Connected) return;
            drawing = true;
            lastPoint = e.Location;
            // Gửi một nét zero-length để server biết start; server của bạn xử lý DRAW liên tục
            SendDraw(lastPoint, lastPoint);
        }

        private void PanelCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!drawing || client == null || !client.Connected) return;
            Point newPoint = e.Location;
            SendDraw(lastPoint, newPoint);
            // Vẽ local để mượt
            lock (canvasLock)
            {
                using var pen = new Pen(currentColor, currentThickness);
                canvasG.DrawLine(pen, lastPoint, newPoint);
            }
            Invoke(new Action(() => pictureBox1.Invalidate()));
            lastPoint = newPoint;
        }

        private void PanelCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (client == null || !client.Connected) return;
            drawing = false;
        }

        private void SendDraw(Point p1, Point p2)
        {
            try
            {
                writer.Write("DRAW");
                writer.Write(p1.X);
                writer.Write(p1.Y);
                writer.Write(p2.X);
                writer.Write(p2.Y);
                writer.Write(currentColor.ToArgb());
                writer.Write(currentThickness);
                writer.Flush();
            }
            catch
            {
                Invoke(new Action(() => CleanupAndReset()));
            }
        }

        private void BtnColor_Click(object sender, EventArgs e)
        {
            using var dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                currentColor = dlg.Color;
            }
        }

        private void NudThickness_ValueChanged(object sender, EventArgs e)
        {
            currentThickness = (int)nudThickness.Value;
        }

        private void BtnEnd_Click(object sender, EventArgs e)
        {
            if (client != null && client.Connected)
            {
                // Lưu ảnh local
                try
                {
                    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                        $"whiteboard_{DateTime.Now:yyyyMMdd_HHmmss}.png");
                    lock (canvasLock)
                    {
                        canvasBmp.Save(path);
                    }
                }
                catch
                {
                    // ignore lưu lỗi
                }
                // Gửi END để server xóa client
                try
                {
                    writer.Write("END");
                    writer.Flush();
                }
                catch { }
                CleanupAndReset();
            }
            else
            {
                this.Close();
            }
        }

        private void CleanupAndReset()
        {
            try
            {
                client?.Close();
            }
            catch { }
            client = null;
            stream = null;
            reader = null;
            writer = null;
            // Reset UI
            btnConnect.Enabled = true;
            lock (canvasLock)
            {
                if (canvasBmp != null)
                {
                    canvasG.Clear(Color.White);
                }
            }
            lblClientCount.Text = "Clients: 0";
            pictureBox1.Invalidate();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (client != null && client.Connected)
            {
                try
                {
                    writer.Write("END");
                    writer.Flush();
                }
                catch { }
            }
            base.OnFormClosing(e);
        }
    }
}
