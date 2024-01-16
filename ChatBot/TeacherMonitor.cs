using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace ChatBot
{
    public partial class TeacherMonitor : Form
    {
        BaseConnection con = new BaseConnection();
        public TeacherMonitor()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TeacherMonitor_Load(object sender, EventArgs e)
        {
           // Program.username = "bismi";

            string query1 = "select distinct suname from oexam where tuname='" + Program.username + "' and status=0";
            SqlDataReader dr1 = con.ret_dr(query1);
            while (dr1.Read())
            {
                comboBox2.Items.Add(dr1[0].ToString());
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string path = Application.StartupPath + "\\" + comboBox2.SelectedItem.ToString();
            imageList1.Images.Clear();
            if (Directory.Exists(path))
            {
                DirectoryInfo directory = new DirectoryInfo(path);
                FileInfo[] Archives = directory.GetFiles("*.jpg");
                foreach (FileInfo fileinfo in Archives)
                {
                    string s = fileinfo.FullName;
                    string s1=Path.GetFileName(s);
                    imageList1.Images.Add(s1,Image.FromFile(fileinfo.FullName));
                }
               // listView1.LargeImageList = imageList1;
                for (int j = 0; j < this.imageList1.Images.Count; j++)
                {
                    ListViewItem item = new ListViewItem();
                  //  item.Text=imageList1.Images[0].ToString();
                   item.Text =j.ToString();
                    item.ImageIndex = j;
                    this.listView1.Items.Add(item);
                }

            }
            else
            {
                MessageBox.Show("Data Not Available");
            }

           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            string path = Application.StartupPath + "\\" + comboBox2.SelectedItem.ToString();
            imageList1.Images.Clear();
            if (Directory.Exists(path))
            {
                DirectoryInfo directory = new DirectoryInfo(path);
                FileInfo[] Archives = directory.GetFiles("*.jpg");
                foreach (FileInfo fileinfo in Archives)
                {
                    string s = fileinfo.FullName;
                    imageList1.Images.Add(Image.FromFile(fileinfo.FullName));
                }
                // listView1.LargeImageList = imageList1;
                for (int j = 0; j < this.imageList1.Images.Count; j++)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = j.ToString();
                    item.ImageIndex = j;
                    this.listView1.Items.Add(item);
                }

            }
            else
            {
                MessageBox.Show("Data Not Available");
            }

        }
    }
}
