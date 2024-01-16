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
    public partial class Startup : Form
    {
        public Startup()
        {
            InitializeComponent();
        }

        private void Startup_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            botcheck obj = new botcheck();
            obj.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label4_Click(object sender, EventArgs e)
        {
//            DialogResult result = MessageBox.Show("Are You a Student?", "Please Select a Type of User",
//MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
//            if (result == DialogResult.Yes)
////            {
                Student_Registration obj = new Student_Registration();
               
                obj.Show();
            //}
            //else if (result == DialogResult.No)
            //{
            //    Parent_Registration obj = new Parent_Registration();
               
            //    obj.Show();
            //}
            //else if (result == DialogResult.Cancel)
            //{
            //    //code for Cancel
            //}
        }
    }
}
