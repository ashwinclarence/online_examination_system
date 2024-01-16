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
    public partial class Admin_Home : Form
    {
        public Admin_Home()
        {
            InitializeComponent();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
          
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
           
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
           
        }

        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            Admin_AddTeacher obj = new Admin_AddTeacher();
            obj.Show();
        }

        private void toolStripButton6_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripButton4_Click_1(object sender, EventArgs e)
        {
            aimlwriter obj = new aimlwriter();
            obj.Show();
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            AdminUsers obj = new AdminUsers();
            obj.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            botcheck obj = new botcheck();
            obj.Show();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            AdminMessage obj = new AdminMessage();
            obj.Show();
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            AdminAddSubject obj = new AdminAddSubject();
            obj.Show();
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            AdminUpdateSubject obj = new AdminUpdateSubject();
            obj.Show();
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            AdminDeleteSubject obj = new AdminDeleteSubject();
            obj.Show();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            AdminUpdateTeacher obj = new AdminUpdateTeacher();
            obj.Show();
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            AdminDeleteTeacher obj = new AdminDeleteTeacher();
            obj.Show();
        }

        private void toolStripButton3_Click_1(object sender, EventArgs e)
        {
            AdminApproveStudent obj = new AdminApproveStudent();
            obj.Show();
        }
    }
}
