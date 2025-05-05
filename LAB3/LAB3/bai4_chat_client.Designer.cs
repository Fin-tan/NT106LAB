namespace LAB3
{
    partial class bai4_chat_client
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
            list = new ListView();
            send = new Button();
            name = new TextBox();
            tbmessage = new TextBox();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // list
            // 
            list.Location = new Point(12, 12);
            list.Name = "list";
            list.Size = new Size(521, 225);
            list.TabIndex = 0;
            list.UseCompatibleStateImageBehavior = false;
            list.View = View.List;
            // 
            // send
            // 
            send.Location = new Point(433, 344);
            send.Name = "send";
            send.Size = new Size(75, 23);
            send.TabIndex = 1;
            send.Text = "send";
            send.UseVisualStyleBackColor = true;
            send.Click += send_Click;
            // 
            // name
            // 
            name.Location = new Point(12, 276);
            name.Name = "name";
            name.Size = new Size(100, 23);
            name.TabIndex = 2;
            // 
            // tbmessage
            // 
            tbmessage.Location = new Point(12, 345);
            tbmessage.Name = "tbmessage";
            tbmessage.Size = new Size(380, 23);
            tbmessage.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 258);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 4;
            label1.Text = "Name";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 327);
            label2.Name = "label2";
            label2.Size = new Size(53, 15);
            label2.TabIndex = 5;
            label2.Text = "Message";
            // 
            // bai4_chat_client
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(545, 380);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(tbmessage);
            Controls.Add(name);
            Controls.Add(send);
            Controls.Add(list);
            Name = "bai4_chat_client";
            Text = "bai4_chat_client";
            Load += bai4_chat_client_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView list;
        private Button send;
        private TextBox name;
        private TextBox tbmessage;
        private Label label1;
        private Label label2;
    }
}