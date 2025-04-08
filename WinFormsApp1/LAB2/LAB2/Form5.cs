using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab2
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            listView1.Items.Clear();
            FolderBrowserDialog opf = new FolderBrowserDialog();
            if (opf.ShowDialog() == DialogResult.OK)
            {

                string[] pathList = Directory.GetFiles(opf.SelectedPath, "*", SearchOption.AllDirectories);
                List<FileInfo> ListFileInfo = new List<FileInfo>();

                foreach (string item in pathList)
                {
                    ListFileInfo.Add(new FileInfo(item));
                }


                foreach (FileInfo fi in ListFileInfo)
                {
                    ListViewItem item = new ListViewItem
                    {
                        Text = fi.Name
                    };
                    listView1.Items.Add(item);

                    ListViewItem.ListViewSubItem kichthuoc = new ListViewItem.ListViewSubItem(item, ((fi.Length)).ToString());
                    ListViewItem.ListViewSubItem DuoiMoRong = new ListViewItem.ListViewSubItem(item, ((fi.Extension)).ToString());
                    ListViewItem.ListViewSubItem NgayTao = new ListViewItem.ListViewSubItem(item, ((fi.CreationTime)).ToString());
                    item.SubItems.Add(kichthuoc);
                    item.SubItems.Add(DuoiMoRong);
                    item.SubItems.Add(NgayTao);

                }

            }
        }

        private void listViewFile_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

}
