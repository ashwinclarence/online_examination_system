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
    public partial class AdminUpdateSubject : Form
    {
        BaseConnection con = new BaseConnection();
        public AdminUpdateSubject()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void addsubject()
        {
            try
            {
                string query = "select subid from Subject_Details";
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

        private void AdminUpdateSubject_Load(object sender, EventArgs e)
        {
            addsubject();
        }

        private void dept_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "select * from Subject_Details where subid="+dept.SelectedItem.ToString()+"";
            SqlDataReader dr = con.ret_dr(query);
            if (dr.Read())
            {
                tname.Text = dr[1].ToString();
                textBox1.Text = dr[2].ToString();
                mob.Text = dr[3].ToString();
                mail.Text = dr[4].ToString();
                deuserid.Text = dr[5].ToString();
                depassword.Text = dr[6].ToString();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string query1 = "update Subject_details set subcode='" + tname.Text + "', dept='" + textBox1.Text + "',subname='" + mob.Text + "',semester='" + mail.Text + "',hours='" + deuserid.Text + "',details='" + depassword.Text + "' where subid=" + dept.SelectedItem.ToString() + "";
            if (con.exec1(query1) > 0)
            {
                MessageBox.Show("Subject details Updated Successfully...");
                this.Close();
            }
        }
    }
}
