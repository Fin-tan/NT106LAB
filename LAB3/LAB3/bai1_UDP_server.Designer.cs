namespace LAB3
{
    partial class bai1_UDP_server
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
            ServerPortB1 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            Listen = new Button();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            messagelist = new ListView();
            SuspendLayout();
            // 
            // ServerPortB1
            // 
            ServerPortB1.Location = new Point(83, 54);
            ServerPortB1.Name = "ServerPortB1";
            ServerPortB1.Size = new Size(100, 23);
            ServerPortB1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(83, 36);
            label1.Name = "label1";
            label1.Size = new Size(29, 15);
            label1.TabIndex = 2;
            label1.Text = "Port";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(83, 126);
            label2.Name = "label2";
            label2.Size = new Size(100, 15);
            label2.TabIndex = 3;
            label2.Text = "received message";
            // 
            // Listen
            // 
            Listen.Location = new Point(662, 54);
            Listen.Name = "Listen";
            Listen.Size = new Size(75, 23);
            Listen.TabIndex = 4;
            Listen.Text = "Listen";
            Listen.UseVisualStyleBackColor = true;
            Listen.Click += Listen_Click;
            // 
            // messagelist
            // 
            messagelist.Location = new Point(83, 175);
            messagelist.Name = "messagelist";
            messagelist.Size = new Size(661, 250);
            messagelist.TabIndex = 5;
            messagelist.UseCompatibleStateImageBehavior = false;
            messagelist.View = View.List;
            // 
            // bai1_UDP_server
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(messagelist);
            Controls.Add(Listen);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(ServerPortB1);
            Name = "bai1_UDP_server";
            Text = "bai1_UDP_server";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox ServerPortB1;
        private Label label1;
        private Label label2;
        private Button Listen;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private ListView messagelist;
    }
}