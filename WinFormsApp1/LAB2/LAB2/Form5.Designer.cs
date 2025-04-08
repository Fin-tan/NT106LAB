namespace lab2
{
    partial class Form5
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
            listView1 = new ListView();
            clnName = new ColumnHeader();
            clnSize = new ColumnHeader();
            clnTail = new ColumnHeader();
            clnDate = new ColumnHeader();
            button1 = new Button();
            SuspendLayout();
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new ColumnHeader[] { clnName, clnSize, clnTail, clnDate });
            listView1.Location = new Point(53, 61);
            listView1.Name = "listView1";
            listView1.Size = new Size(398, 267);
            listView1.TabIndex = 0;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            listView1.SelectedIndexChanged += listViewFile_SelectedIndexChanged;
            // 
            // clnName
            // 
            clnName.Text = "Tên file";
            clnName.Width = 80;
            // 
            // clnSize
            // 
            clnSize.Text = "Kích thước";
            clnSize.Width = 80;
            // 
            // clnTail
            // 
            clnTail.Text = "Đuôi";
            // 
            // clnDate
            // 
            clnDate.Text = "Ngày tạo";
            clnDate.Width = 100;
            // 
            // button1
            // 
            button1.Location = new Point(654, 76);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 1;
            button1.Text = "Open";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Form5
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(listView1);
            Name = "Form5";
            Text = "Form5";
            ResumeLayout(false);
        }

        #endregion

        private ListView listView1;
        private Button button1;
        private ColumnHeader clnName;
        private ColumnHeader clnSize;
        private ColumnHeader clnTail;
        private ColumnHeader clnDate;
    }
}