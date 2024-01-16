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
    public partial class StudentProfile : Form
    {
        BaseConnection con = new BaseConnection();
        public StudentProfile()
        {
            InitializeComponent();
        }

        private void StudentProfile_Load(object sender, EventArgs e)
        {
           // Program.username = "anu123";
            usert.Text = Program.username;
            string query = "select * from login where username='"+Program.username+"'";
            SqlDataReader dr = con.ret_dr(query);
            if (dr.Read())
            {
                profilepic.Image = Image.FromFile(Application.StartupPath+dr[4].ToString());
            }
            string query1 = "select * from Student_details where suname='" + Program.username + "'";
            SqlDataReader dr1 = con.ret_dr(query1);
            if (dr1.Read())
            {
                name.Text = dr1[3].ToString();
                admno.Text = dr1[2].ToString();
                textBox3.Text = dr1[4].ToString();
                mob.Text = dr1[5].ToString();
                mail.Text = dr1[6].ToString();
                textBox2.Text = dr1[7].ToString();
                textBox1.Text = dr1[8].ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
                    
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
