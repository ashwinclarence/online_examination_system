using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChatBot
{
    public partial class Teacher_Home : Form
    {
        public Teacher_Home()
        {
            InitializeComponent();
           // Program.username = "bismi";
           // Program.picpath = Application.StartupPath + "\\ProfilePictures\\Thumbnails\\101.jpg";
            toolStripButton8.Image = (Image)Image.FromFile(Program.picpath);
            toolStripButton8.Text = "Welcome " + Program.username;
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            TeacherMessage obj=new TeacherMessage();
            obj.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            TeacherUpdateResult obj = new TeacherUpdateResult();
            obj.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            TeacherAssignExam1 obj = new TeacherAssignExam1();
            obj.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            TeacherUpdateQuestion obj = new TeacherUpdateQuestion();
            obj.Show();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            TeacherAddQuestion obj = new TeacherAddQuestion();
            obj.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            TeacherMonitor obj = new TeacherMonitor();
            obj.Show();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            Teacher_students obj = new Teacher_students();
            obj.Show();
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            TeacherDeleteStudent obj = new TeacherDeleteStudent();
            obj.Show();
        }
    }
}
