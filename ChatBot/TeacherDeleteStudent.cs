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
    public partial class TeacherDeleteStudent : Form
    {
        BaseConnection con = new BaseConnection();
        BaseConnection con1 = new BaseConnection();
        public TeacherDeleteStudent()
        {
            InitializeComponent();
           // Program.username = "bismi";
        }
        public void addstudent()
        {
            try
            {
                string query = "select suname from Student_Details where tuname='" + Program.username + "'";
                SqlDataReader dr = con.ret_dr(query);
                while (dr.Read())
                {
                    comboBox2.Items.Add(dr[0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while generating subject Id........");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TeacherDeleteStudent_Load(object sender, EventArgs e)
        {
            addstudent();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "select * from Student_Details where suname='" + comboBox2.SelectedItem.ToString() + "'";
            SqlDataReader dr = con.ret_dr(query);
            if (dr.Read())
            {
                admno.Text = dr[2].ToString();
                textBox2.Text = dr[4].ToString();
                pwd.Text = dr[3].ToString();
                textBox3.Text = dr[10].ToString();
                mob.Text = dr[5].ToString();
                mail.Text = dr[6].ToString();
                textBox1.Text = dr[8].ToString();


            }
            string query1 = "select * from Login where username='" + comboBox2.SelectedItem.ToString() + "'";
            SqlDataReader dr1 = con1.ret_dr(query1);
            if (dr1.Read())
            {
                profilepic.Image=Image.FromFile(Application.StartupPath+"\\"+dr1[4].ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query1 = "delete from Student_details where suname='" + comboBox2.SelectedItem.ToString() + "'";
            if (con.exec1(query1) > 0)
            {
                string query2 = "delete from Login where username='" + comboBox2.SelectedItem.ToString() + "'";
                if (con1.exec1(query2) > 0)
                {
                    MessageBox.Show("Students details deleted  Successfully...");
                    this.Close();
                }
            }
        }
    }
    
}
