// Form1.cs - Updated to match Client.cs functionality
using System;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement; // This line might be removable if no longer needed.

namespace lab06
{
    public partial class Form1 : Form
    {
        private TcpClient? client; // Changed to nullable
        private NetworkStream? stream; // Changed to nullable
        private BinaryReader? reader; // Changed to nullable
        private BinaryWriter? writer; // Changed to nullable
        private Thread? listenThread; // Changed to nullable

        private Bitmap? canvasBmp; // Changed to nullable
        private Graphics? canvasG; // Changed to nullable
        private readonly object canvasLock = new object();

        private bool drawing = false;
        private Point lastPoint;
        private Color currentColor = Color.Black;
        private int currentThickness = 2;
        private bool isEraser = false; // Added for eraser functionality

        // New fields for image pasting/manipulation
        private PictureBox? tempBox;
        private bool isDragging = false;
        private Point dragOffset;
        private bool isResizing = false;
        private Size resizeStartSize;
        private Point resizeStartPoint;
        private const int ResizeHandleSize = 10;

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            typeof(Panel).InvokeMember("DoubleBuffered", System.Reflection.BindingFlags.SetProperty | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic, null, panelCanvas, new object[] { true }); // Enable double buffering for smoother drawing
            // Gán sự kiện
            this.Load += Form1_Load;
            this.KeyPreview = true; // Enable form-level key events for Ctrl+V
            this.KeyDown += Form1_KeyDown; // Added for Ctrl+V handling

            btnConnect.Click += BtnConnect_Click;
            btnColor.Click += BtnColor_Click;
            nudThickness.Minimum = 1;
            nudThickness.Maximum = 20;
            nudThickness.Value = 2;
            nudThickness.ValueChanged += NudThickness_ValueChanged;
            btnEnd.Click += BtnEnd_Click;

            panelCanvas.Paint += PanelCanvas_Paint;
            panelCanvas.MouseDown += PanelCanvas_MouseDown;
            panelCanvas.MouseMove += PanelCanvas_MouseMove;
            panelCanvas.MouseUp += PanelCanvas_MouseUp;

            lblClientCount.Text = "Clients: 0";
            // NOTE: To fully match the sample, you might need to add a CheckBox
            // named 'cbEraser' and a Button named 'btnSave' in your Form1.Designer.cs
            // and uncomment the following lines.
            // cbEraser.CheckedChanged += CbEraser_CheckedChanged;
            // btnSave.Click += BtnSave_Click;
        }

        // Placeholder for BtnSave_Click. User needs to add btnSave control.
        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveWhiteboardToFile();
        }

        // Placeholder for CbEraser_CheckedChanged. User needs to add cbEraser control.
        private void CbEraser_CheckedChanged(object sender, EventArgs e)
        {
            // Assuming cbEraser is a CheckBox control
            isEraser = ((CheckBox)sender).Checked;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            // Initialize bitmap after panelCanvas has acquired its size from Designer
            canvasBmp = new Bitmap(panelCanvas.Width, panelCanvas.Height);
            canvasG = Graphics.FromImage(canvasBmp);
            canvasG.Clear(Color.White);
            panelCanvas.Invalidate();
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

            string ip = "127.0.0.1";
            int port = 4871; // Must match server port

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
                        cmd = reader!.ReadString(); // Use null-forgiving operator as it's checked by while condition
                    }
                    catch
                    {
                        break; // Exit loop on read error (e.g., server disconnected)
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
                                ClientClose(false); // Call updated ClientClose without sending END again
                            }));
                            return; // Exit thread after server closes
                        default:
                            break;
                    }
                }
            }
            catch
            {
                // Ignore other exceptions in the listen thread
            }
            finally
            {
                // This will be invoked after the loop breaks or an error occurs,
                // ensuring cleanup even if the server disconnects unexpectedly.
                Invoke(new Action(() => ClientClose(false))); // Call updated ClientClose
            }
        }

        private void ReceiveDraw()
        {
            try
            {
                int x1 = reader!.ReadInt32();
                int y1 = reader.ReadInt32();
                int x2 = reader.ReadInt32();
                int y2 = reader.ReadInt32();
                int argb = reader.ReadInt32();
                int width = reader.ReadInt32();

                Color color = Color.FromArgb(argb);
                lock (canvasLock)
                {
                    using var pen = new Pen(color, width);
                    canvasG!.DrawLine(pen, new Point(x1, y1), new Point(x2, y2));
                }
                Invoke(new Action(() => panelCanvas.Invalidate()));
            }
            catch
            {
                // ignore
            }
        }

        // Form1.cs
        private void ReceiveImage()
        {
            try
            {
                int length = reader!.ReadInt32();
                byte[] data = reader.ReadBytes(length);
                Bitmap bmp;
                using (var ms = new MemoryStream(data))
                {
                    bmp = new Bitmap(ms);
                }
                lock (canvasLock)
                {
                    canvasG?.Dispose();
                    canvasBmp?.Dispose();
                    canvasBmp = bmp; // Gán bitmap mới nhận được
                    canvasG = Graphics.FromImage(canvasBmp);
                }
                Invoke(new Action(() => panelCanvas.Invalidate())); // Cập nhật hiển thị trên panelCanvas
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
                int count = reader!.ReadInt32();
                Invoke(new Action(() => lblClientCount.Text = $"Clients: {count}"));
            }
            catch
            {
                // ignore
            }
        }

        private void PanelCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (client == null || !client.Connected || canvasBmp == null) return; // Added null check for canvasBmp
            drawing = true;
            lastPoint = e.Location;
            // Send a zero-length line to notify server of start; your server handles continuous DRAW
            SendDraw(lastPoint, lastPoint);
        }

        private void PanelCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!drawing || client == null || !client.Connected || canvasBmp == null) return; // Added null check for canvasBmp

            Point newPoint = e.Location;
            SendDraw(lastPoint, newPoint); // Send to server

            // Draw locally for smooth experience
            lock (canvasLock)
            {
                using (var pen = new Pen(isEraser ? Color.White : currentColor, currentThickness)) // Use isEraser
                {
                    canvasG!.DrawLine(pen, lastPoint, newPoint);
                }
            }
            panelCanvas.Invalidate(); // Redraw the panel with the local drawing
            lastPoint = newPoint;
        }

        private void PanelCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (client == null || !client.Connected) return;
            drawing = false;
        }

        private void SendDraw(Point p1, Point p2)
        {
            if (writer == null) return; // Added null check for writer
            try
            {
                writer.Write("DRAW");
                writer.Write(p1.X);
                writer.Write(p1.Y);
                writer.Write(p2.X);
                writer.Write(p2.Y);
                writer.Write((isEraser ? Color.White : currentColor).ToArgb()); // Use isEraser
                writer.Write(currentThickness);
                writer.Flush();
            }
            catch (Exception ex)
            {
                // The sample code has a MessageBox here.
                MessageBox.Show("Lỗi khi gửi DRAW: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Consider calling ClientClose() here if the error indicates a broken connection
            }
        }

        private void BtnColor_Click(object sender, EventArgs e)
        {
            using (var cld = new ColorDialog())
            {
                if (cld.ShowDialog() == DialogResult.OK)
                {
                    currentColor = cld.Color;
                }
            }
        }

        private void NudThickness_ValueChanged(object sender, EventArgs e)
        {
            currentThickness = (int)nudThickness.Value;
        }

        private void BtnEnd_Click(object sender, EventArgs e)
        {
            ClientClose();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            ClientClose(); // Call updated ClientClose
            base.OnFormClosing(e);
        }

        private void ClientClose(bool sendServerEnd = true)
        {
            // Prompt to save whiteboard before closing
            var res = MessageBox.Show("Lưu whiteboard trước khi thoát?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (res == DialogResult.Yes)
                SaveWhiteboardToFile();

            try
            {
                if (client != null && writer != null && sendServerEnd)
                {
                    writer.Write("END");
                    writer.Flush();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi gửi lệnh END: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Dispose and nullify network objects
                client?.Close(); // Use Close() directly if Dispose() is not explicitly needed for TcpClient
                                 // TcpClient's Close() method typically handles stream disposal.
                client = null;
                stream?.Dispose(); // Dispose NetworkStream
                stream = null;
                reader?.Dispose(); // Dispose BinaryReader
                reader = null;
                writer?.Dispose(); // Dispose BinaryWriter
                writer = null;

                // Reset UI elements
                Invoke(new Action(() =>
                {
                    btnConnect.Enabled = true;
                    // Reset canvas to white
                    lock (canvasLock)
                    {
                        if (canvasBmp != null)
                        {
                            canvasG?.Clear(Color.White);
                        }
                    }
                    lblClientCount.Text = "Clients: 0";
                    panelCanvas.Invalidate();
                }));
            }
            this.Hide(); // Hide the form instead of closing directly based on sample's ClientClose
        }

        private void SaveWhiteboardToFile()
        {
            if (canvasBmp == null) return;
            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "PNG Image|*.png|JPEG Image|*.jpg;*.jpeg";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Bitmap clone;
                    lock (canvasLock) clone = new Bitmap(canvasBmp); // Create a clone to save

                    var ext = Path.GetExtension(sfd.FileName).ToLower();
                    var fmt = ext == ".jpg" || ext == ".jpeg"
                                  ? System.Drawing.Imaging.ImageFormat.Jpeg
                                  : System.Drawing.Imaging.ImageFormat.Png;

                    clone.Save(sfd.FileName, fmt);
                    clone.Dispose(); // Dispose the clone
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V && Clipboard.ContainsImage())
            {
                var img = Clipboard.GetImage();
                if (img != null) CreateTempPictureBox(img);
            }
        }

        private void CreateTempPictureBox(Image image)
        {
            // Remove any existing tempBox first
            if (tempBox != null)
            {
                panelCanvas.Controls.Remove(tempBox);
                tempBox.Dispose();
                tempBox = null;
            }

            tempBox = new PictureBox
            {
                Image = image,
                SizeMode = PictureBoxSizeMode.StretchImage,
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(200, 150), // Initial size
                Location = new Point(100, 100), // Initial location
                Cursor = Cursors.SizeAll
            };

            tempBox.MouseDown += (s, e) =>
            {
                Point resizeHandleArea = new Point(tempBox.Width - ResizeHandleSize, tempBox.Height - ResizeHandleSize);
                Rectangle resizeRect = new Rectangle(resizeHandleArea.X, resizeHandleArea.Y, ResizeHandleSize, ResizeHandleSize);

                if (resizeRect.Contains(e.Location))
                {
                    isResizing = true;
                    resizeStartSize = tempBox.Size;
                    resizeStartPoint = e.Location;
                    tempBox.Cursor = Cursors.SizeNWSE;
                }
                else
                {
                    isDragging = true;
                    dragOffset = e.Location;
                    tempBox.Cursor = Cursors.SizeAll;
                }
            };

            tempBox.MouseMove += (s, e) =>
            {
                if (isResizing)
                {
                    int newWidth = resizeStartSize.Width + (e.Location.X - resizeStartPoint.X);
                    int newHeight = resizeStartSize.Height + (e.Location.Y - resizeStartPoint.Y);

                    newWidth = Math.Max(newWidth, 20); // Minimum size
                    newHeight = Math.Max(newHeight, 20); // Minimum size

                    tempBox.Size = new Size(newWidth, newHeight);
                }
                else if (isDragging)
                {
                    tempBox.Left += e.X - dragOffset.X;
                    tempBox.Top += e.Y - dragOffset.Y;
                }
                else
                {
                    // Change cursor when hovering over resize handle
                    Point checkResizeHandleArea = new Point(tempBox.Width - ResizeHandleSize, tempBox.Height - ResizeHandleSize);
                    Rectangle checkResizeRect = new Rectangle(checkResizeHandleArea.X, checkResizeHandleArea.Y, ResizeHandleSize, ResizeHandleSize);

                    if (checkResizeRect.Contains(e.Location))
                    {
                        tempBox.Cursor = Cursors.SizeNWSE;
                    }
                    else
                    {
                        tempBox.Cursor = Cursors.Default;
                    }
                }
            };

            tempBox.MouseUp += (s, e) =>
            {
                isDragging = false;
                isResizing = false;
                tempBox.Cursor = Cursors.Default;
            };

            panelCanvas.Controls.Add(tempBox);
            tempBox.BringToFront();

            // Temporarily disable panelCanvas drawing events and enable image commit event
            panelCanvas.MouseDown -= PanelCanvas_MouseDown;
            panelCanvas.MouseMove -= PanelCanvas_MouseMove;
            panelCanvas.MouseUp -= PanelCanvas_MouseUp;
            panelCanvas.MouseDown += Whiteboard_MouseDown_ForImageCommit;
        }

        private void Whiteboard_MouseDown_ForImageCommit(object sender, MouseEventArgs e)
        {
            if (tempBox != null && !tempBox.Bounds.Contains(e.Location)) // If click is outside tempBox
            {
                CommitTempImage();

                // Re-enable panelCanvas drawing events
                panelCanvas.MouseDown -= Whiteboard_MouseDown_ForImageCommit;
                panelCanvas.MouseDown += PanelCanvas_MouseDown;
                panelCanvas.MouseMove += PanelCanvas_MouseMove;
                panelCanvas.MouseUp += PanelCanvas_MouseUp;
            }
        }

        private void CommitTempImage()
        {
            if (tempBox == null || canvasBmp == null) return;

            // Draw the temporary image onto the main canvas bitmap
            lock (canvasLock)
            {
                using (var g2 = Graphics.FromImage(canvasBmp))
                {
                    g2.DrawImage(tempBox.Image, tempBox.Bounds);
                }
            }

            // Remove the temporary PictureBox
            panelCanvas.Controls.Remove(tempBox);
            tempBox.Dispose(); // Dispose the tempBox to free resources
            tempBox = null;
            panelCanvas.Invalidate(); // Redraw the panel with the committed image

            // Send the entire updated whiteboard image to the server
            if (writer != null)
            {
                try
                {
                    byte[] data;
                    lock (canvasLock)
                    {
                        using (var clone = new Bitmap(canvasBmp)) // Clone to avoid issues during serialization
                        using (var ms = new MemoryStream())
                        {
                            clone.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            data = ms.ToArray();
                        }
                    }

                    writer.Write("IMAGE");
                    writer.Write(data.Length);
                    writer.Write(data);
                    writer.Flush();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi gửi ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}