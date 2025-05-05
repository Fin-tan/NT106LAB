namespace LAB3
{
    partial class bai1_UDP_client
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
            button1 = new Button();
            tbHost = new TextBox();
            text = new TextBox();
            textport = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(97, 340);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "Send";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // tbHost
            // 
            tbHost.Location = new Point(98, 110);
            tbHost.Name = "tbHost";
            tbHost.Size = new Size(373, 23);
            tbHost.TabIndex = 1;
            // 
            // text
            // 
            text.Location = new Point(98, 204);
            text.Multiline = true;
            text.Name = "text";
            text.Size = new Size(634, 119);
            text.TabIndex = 2;
            // 
            // textport
            // 
            textport.Location = new Point(496, 110);
            textport.Name = "textport";
            textport.Size = new Size(236, 23);
            textport.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(98, 82);
            label1.Name = "label1";
            label1.Size = new Size(84, 15);
            label1.TabIndex = 4;
            label1.Text = "IP remote host";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(496, 82);
            label2.Name = "label2";
            label2.Size = new Size(29, 15);
            label2.TabIndex = 5;
            label2.Text = "port";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(98, 175);
            label3.Name = "label3";
            label3.Size = new Size(53, 15);
            label3.TabIndex = 6;
            label3.Text = "message";
            label3.Click += label3_Click;
            // 
            // bai1_UDP_client
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textport);
            Controls.Add(text);
            Controls.Add(tbHost);
            Controls.Add(button1);
            Name = "bai1_UDP_client";
            Text = "bai1_UDP_client";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private TextBox tbHost;
        private TextBox text;
        private TextBox textport;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}