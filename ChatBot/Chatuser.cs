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
    public partial class Chatuser : Form
    {
        BaseConnection con = new BaseConnection();
        public Chatuser()
        {
            InitializeComponent();
            fillgrid();
        }
        public void fillgrid()
        {
            try
            {
                if (Program.usertype == "Teacher")
                {
                    string query1 = "select userid,username,usertype from login";
                    DataSet ds1 = con.ret_ds(query1);
                    dataGridView2.DataSource = ds1.Tables[0].DefaultView;


                    SqlDataReader dr = con.ret_dr(query1);
                    while (dr.Read())
                    {
                        comboBox1.Items.Add(dr[0].ToString());
                    }
                }
                else if (Program.usertype == "Student")
                {
                    string department = "";

                    string query = "select department from student_details where userid='" + Program.userid + "' ";
                    SqlDataReader dr1 = con.ret_dr(query);
                    if (dr1.Read())
                    {
                        department = dr1[0].ToString();
                    }
                    string query1 = "select userid,username,usertype from login where userid in (select userid from teacher_details where department ='" + department + "') or userid in (select userid from student_details where department ='" + department + "')";
                    DataSet ds1 = con.ret_ds(query1);
                    dataGridView2.DataSource = ds1.Tables[0].DefaultView;


                    SqlDataReader dr = con.ret_dr(query1);
                    while (dr.Read())
                    {
                        comboBox1.Items.Add(dr[0].ToString());
                    }

                }
                else
                {
                    string query1 = "select userid,username,usertype from login where usertype='Teacher'";
                    DataSet ds1 = con.ret_ds(query1);
                    dataGridView2.DataSource = ds1.Tables[0].DefaultView;


                    SqlDataReader dr = con.ret_dr(query1);
                    while (dr.Read())
                    {
                        comboBox1.Items.Add(dr[0].ToString());
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("exception occured....");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query1 = "select * from login where userid='"+comboBox1.Text+"'";
           


            SqlDataReader dr = con.ret_dr(query1);

            if (dr.Read())
            {
                label3.Text = dr[1].ToString();
                label4.Text = dr[3].ToString();
                pictureBox1.ImageLocation = Application.StartupPath +dr[4].ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Chat obj = new Chat(Program.userid, comboBox1.Text);
            ActiveForm.Hide();
            obj.Show();
        }
    }
}
