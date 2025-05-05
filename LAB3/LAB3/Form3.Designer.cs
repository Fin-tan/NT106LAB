namespace LAB3
{
    partial class Form3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            TCP_client = new Button();
            TCP_sv = new Button();
            SuspendLayout();
            // 
            // TCP_client
            // 
            TCP_client.Location = new Point(78, 87);
            TCP_client.Name = "TCP_client";
            TCP_client.Size = new Size(107, 51);
            TCP_client.TabIndex = 0;
            TCP_client.Text = "TCPclient";
            TCP_client.UseVisualStyleBackColor = true;
            TCP_client.Click += TCP_client_Click;
            // 
            // TCP_sv
            // 
            TCP_sv.Location = new Point(269, 87);
            TCP_sv.Name = "TCP_sv";
            TCP_sv.Size = new Size(109, 51);
            TCP_sv.TabIndex = 1;
            TCP_sv.Text = "TCP_sv";
            TCP_sv.UseVisualStyleBackColor = true;
            TCP_sv.Click += TCP_sv_Click;
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(453, 237);
            Controls.Add(TCP_sv);
            Controls.Add(TCP_client);
            Name = "Form3";
            Text = "Form3";
            ResumeLayout(false);
        }

        #endregion

        private Button TCP_client;
        private Button TCP_sv;
    }
}