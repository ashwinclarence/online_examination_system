using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ChatBot
{
    public partial class Student_Home : Form
    {
        BaseConnection con = new BaseConnection();
        public Student_Home()
        {
            InitializeComponent();
           // Program.username = "anu123";
           // Program.picpath = Application.StartupPath + "\\ProfilePictures\\Thumbnails\\106.jpg";
            toolStripButton8.Image = (Image)Image.FromFile(Program.picpath);
            toolStripButton8.Text = "Welcome " + Program.username;
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {

            Startup obj = new Startup();
            obj.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            botcheck obj = new botcheck();
            obj.Show();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            StudentProfile obj = new StudentProfile();
            obj.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            StudentViewSubject obj = new StudentViewSubject();
            obj.Show();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            StudentScretCode obj = new StudentScretCode();
            obj.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            StudentViewExam obj = new StudentViewExam();
            obj.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            StudentViewMark obj = new StudentViewMark();
            obj.Show();
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            StudentMessage obj = new StudentMessage();
            obj.Show();
        }
    }
}
