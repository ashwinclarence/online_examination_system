using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;

namespace ChatBot
{
    public partial class StudentViewExam : Form
    {
        BaseConnection con = new BaseConnection();
        public StudentViewExam()
        {
            InitializeComponent();
            
        }
        public void fillgrid()
        {
            try
            {
             //   Program.username = "bismi";

                //  string query1 = "select * from Student_details where department=(select department from Teacher_details where userid='"+Program.userid+"')";
                string query1 = "select studentid,admno,name,department,mobile,mailid,address,suname from Student_details where tuname='" + Program.username + "'";
                DataSet ds1 = con.ret_ds(query1);
                dataGridView2.DataSource = ds1.Tables[0].DefaultView;



            }
            catch (Exception ex)
            {
                MessageBox.Show("exception occured....");
            }
        }

        private void StudentViewExam_Load(object sender, EventArgs e)
        {
           // Program.username = "anu123";
            var dtWriteoffUpload = new DataTable();
            dtWriteoffUpload.Columns.Add("examid");
            dtWriteoffUpload.Columns.Add("subcode");
            dtWriteoffUpload.Columns.Add("Starting Time");
            dtWriteoffUpload.Columns.Add("Ending Time");
            dtWriteoffUpload.Columns.Add("Exam Date");
            dtWriteoffUpload.Columns.Add("Login Code");
           
            string query = "select * from assignexam";
            SqlDataReader dr = con.ret_dr(query);
            while (dr.Read())
            {
                string str = dr[3].ToString();
                string[] suname = str.Split(':');
                for (int i = 0; i < suname.Count()-1; i++)
                {
                    if (suname[i].ToString() == Program.username)
                    {
                        dtWriteoffUpload.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[9].ToString());
                     
                    }
                }

            }
            dataGridView2.DataSource = dtWriteoffUpload.DefaultView ;
           // fillgrid();
        }
    }
}
