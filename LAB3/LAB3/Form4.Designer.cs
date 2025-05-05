namespace LAB3
{
    partial class Form4
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
            server = new Button();
            client = new Button();
            SuspendLayout();
            // 
            // server
            // 
            server.Location = new Point(24, 50);
            server.Name = "server";
            server.Size = new Size(97, 51);
            server.TabIndex = 0;
            server.Text = "server";
            server.UseVisualStyleBackColor = true;
            server.Click += server_Click;
            // 
            // client
            // 
            client.Location = new Point(211, 50);
            client.Name = "client";
            client.Size = new Size(97, 51);
            client.TabIndex = 1;
            client.Text = "client";
            client.UseVisualStyleBackColor = true;
            client.Click += client_Click;
            // 
            // Form4
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(405, 152);
            Controls.Add(client);
            Controls.Add(server);
            Name = "Form4";
            Text = "Form4";
            ResumeLayout(false);
        }

        #endregion

        private Button server;
        private Button client;
    }
}