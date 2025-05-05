namespace LAB3
{
    partial class Form2
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
            listen = new Button();
            listView = new ListView();
            SuspendLayout();
            // 
            // listen
            // 
            listen.Location = new Point(566, 27);
            listen.Name = "listen";
            listen.Size = new Size(119, 41);
            listen.TabIndex = 1;
            listen.Text = "listen";
            listen.UseVisualStyleBackColor = true;
            listen.Click += listen_Click;
            // 
            // listView
            // 
            listView.Location = new Point(58, 149);
            listView.Name = "listView";
            listView.Size = new Size(627, 251);
            listView.TabIndex = 2;
            listView.TileSize = new Size(228, 60);
            listView.UseCompatibleStateImageBehavior = false;
            listView.View = View.List;
            listView.SelectedIndexChanged += listView_SelectedIndexChanged;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(listView);
            Controls.Add(listen);
            Name = "Form2";
            Text = "Form2";
            ResumeLayout(false);
        }

        #endregion
        private Button listen;
        private ListView listView;
    }
}