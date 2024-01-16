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
    public partial class AdminDeleteTeacher : Form
    {
        BaseConnection con = new BaseConnection();
        BaseConnection con1 = new BaseConnection();
        public AdminDeleteTeacher()
        {
            InitializeComponent();
        }
        public void addteacher()
        {
            try
            {
                string query = "select username from Teacher_Details";
                SqlDataReader dr = con.ret_dr(query);
                while (dr.Read())
                {
                    dept.Items.Add(dr[0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while generating subject Id........");
            }
        }
        private void AdminDeleteTeacher_Load(object sender, EventArgs e)
        {
            addteacher();
        }

        private void dept_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "select * from Teacher_Details where username='" + dept.SelectedItem.ToString() + "'";
            SqlDataReader dr = con.ret_dr(query);
            if (dr.Read())
            {
                tname.Text = dr[4].ToString();
                textBox1.Text = dr[6].ToString();
                mob.Text = dr[3].ToString();
                mail.Text = dr[5].ToString();
                deuserid.Text = dr[2].ToString();
                
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string query1 = "delete from Teacher_details where username='" + dept.SelectedItem.ToString() + "'";
            if (con.exec1(query1) > 0)
            {
                string query2 = "delete from Login where username='" + dept.SelectedItem.ToString() + "'";
                if (con1.exec1(query2) > 0)
                {
                    MessageBox.Show("Teacher details Deleted Successfully...");
                    this.Close();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
