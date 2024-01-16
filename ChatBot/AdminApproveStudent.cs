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
    public partial class AdminApproveStudent : Form
    {
        BaseConnection con = new BaseConnection();
        BaseConnection con1 = new BaseConnection();
        public AdminApproveStudent()
        {
            InitializeComponent();
        }
        public void addstudent()
        {
            string s = "0";
            try
            {
                string query = "select username from login where usertype='"+s+"'";
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
                profilepic.Image = Image.FromFile(Application.StartupPath + "\\" + dr1[4].ToString());
            }
        }

        private void AdminApproveStudent_Load(object sender, EventArgs e)
        {
            addstudent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string utype = "Student";
            string query2 = "update login set usertype='"+utype+"'where username='" + comboBox2.SelectedItem.ToString() + "'";
            if (con1.exec1(query2) > 0)
            {
                MessageBox.Show("Students details Approved Successfully...");
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
