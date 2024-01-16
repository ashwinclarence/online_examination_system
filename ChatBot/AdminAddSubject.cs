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
    public partial class AdminAddSubject : Form
    {
        BaseConnection con = new BaseConnection();
        public static string tid = "";
        public AdminAddSubject()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void subjectid()
        {
            try
            {
                string query = "select isnull(max(subid),400)+1 from Subject_details";
                SqlDataReader dr = con.ret_dr(query);
                if (dr.Read())
                {
                    tid = dr[0].ToString();
                    textBox1.Text = tid.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while generating user Id........");
            }
        }
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
               
                
                    string query1 = "insert into Subject_details values(" + textBox1.Text + ",'" + tname.Text + "','" + dept.Text + "','" + mob.Text + "','" + mail.Text + "','"+deuserid.Text+"','"+depassword.Text+"',0)";
                    if (con.exec1(query1) > 0)
                    {
                        MessageBox.Show("Subject details Inserted Successfully...");
                        this.Close();
                    }

                



            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occured....");

            }
        }

        private void AdminAddSubject_Load(object sender, EventArgs e)
        {
            subjectid();
        }
    }
}
