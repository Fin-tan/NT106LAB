namespace LAB3
{
    partial class bai4_chat_server
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
            stop = new Button();
            listen = new Button();
            list = new ListView();
            label1 = new Label();
            SuspendLayout();
            // 
            // stop
            // 
            stop.Location = new Point(242, 33);
            stop.Name = "stop";
            stop.Size = new Size(75, 23);
            stop.TabIndex = 0;
            stop.Text = "stop";
            stop.UseVisualStyleBackColor = true;
            stop.Click += stop_Click;
            // 
            // listen
            // 
            listen.Location = new Point(366, 33);
            listen.Name = "listen";
            listen.Size = new Size(75, 23);
            listen.TabIndex = 1;
            listen.Text = "listen";
            listen.UseVisualStyleBackColor = true;
            listen.Click += listen_Click;
            // 
            // list
            // 
            list.Location = new Point(12, 88);
            list.Name = "list";
            list.Size = new Size(429, 215);
            list.TabIndex = 2;
            list.UseCompatibleStateImageBehavior = false;
            list.View = View.List;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 33);
            label1.Name = "label1";
            label1.Size = new Size(24, 15);
            label1.TabIndex = 3;
            label1.Text = "log";
            // 
            // bai4_chat_server
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(453, 315);
            Controls.Add(label1);
            Controls.Add(list);
            Controls.Add(listen);
            Controls.Add(stop);
            Name = "bai4_chat_server";
            Text = "bai4_chat_server";
            Load += bai4_chat_server_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button stop;
        private Button listen;
        private ListView list;
        private Label label1;
    }
}