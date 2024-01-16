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
    public partial class Teacher_students : Form
    {
        BaseConnection con = new BaseConnection();
        public Teacher_students()
        {
            InitializeComponent();
            fillgrid();
        }
        public void fillgrid()
        {
            try
            {
               // Program.username = "bismi";

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
    }
}
