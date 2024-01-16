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
    public partial class TeacherUpdateQuestion : Form
    {
        BaseConnection con = new BaseConnection();
        public TeacherUpdateQuestion()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query1 = "update Questions set sem='" + textBox2.Text + "', dept='" + mob.Text + "',qdate='" + pwd.Text + "',question='" + textBox3.Text + "',answer='" + textBox1.Text + "' where qcode=" + comboBox1.SelectedItem.ToString() + "";
            if (con.exec1(query1) > 0)
            {
                MessageBox.Show("Questions details Updated Successfully...");
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TeacherUpdateQuestion_Load(object sender, EventArgs e)
        {
            pwd.Text = DateTime.Now.ToShortDateString();
           // Program.username = "bismi";
            string query = "select distinct subcode from Questions where tuname='"+Program.username+"'";
            SqlDataReader dr = con.ret_dr(query);
            while (dr.Read())
            {
                comboBox2.Items.Add(dr[0].ToString());
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "select qcode from Questions where tuname='" + Program.username + "' and subcode='"+comboBox2.SelectedItem.ToString()+"'";
            SqlDataReader dr = con.ret_dr(query);
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString());
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "select * from Questions where qcode='" + comboBox1.SelectedItem.ToString() + "'";
            SqlDataReader dr = con.ret_dr(query);
            while (dr.Read())
            {
                textBox2.Text = dr[3].ToString();
                mob.Text = dr[2].ToString();
                textBox3.Text = dr[6].ToString();
                textBox1.Text = dr[7].ToString();
            }
        }
    }
}
