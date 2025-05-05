namespace LAB3
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            UDP_client = new Button();
            UDPsv = new Button();
            SuspendLayout();
            // 
            // UDP_client
            // 
            UDP_client.Location = new Point(88, 92);
            UDP_client.Name = "UDP_client";
            UDP_client.Size = new Size(101, 83);
            UDP_client.TabIndex = 2;
            UDP_client.Text = "UDP_client";
            UDP_client.UseVisualStyleBackColor = true;
            UDP_client.Click += UDP_client_Click;
            // 
            // UDPsv
            // 
            UDPsv.Location = new Point(338, 92);
            UDPsv.Name = "UDPsv";
            UDPsv.Size = new Size(104, 83);
            UDPsv.TabIndex = 3;
            UDPsv.Text = "UDPsv";
            UDPsv.UseVisualStyleBackColor = true;
            UDPsv.Click += UDPsv_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(503, 247);
            Controls.Add(UDPsv);
            Controls.Add(UDP_client);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button UDP_client;
        private Button UDPsv;
    }
}
