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
    public partial class Chatinbox : Form
    {
        BaseConnection con = new BaseConnection();
        public Chatinbox()
        {
            InitializeComponent();
            fillgrid();
        }
        public void fillgrid()
        {
            try
            {


                string query1 = "SELECT distinct chatid,sender,username,[time] FROM chat B LEFT JOIN login A ON B.sender = A.userid where status ='unseen' and receiver='"+Program.userid+"'";
                DataSet ds1 = con.ret_ds(query1);
                dataGridView2.DataSource = ds1.Tables[0].DefaultView;

                string query2 = "SELECT distinct chatid,sender,username,[time] FROM chat B LEFT JOIN login A ON B.sender = A.userid where status ='seen' and receiver='" + Program.userid + "'";
                DataSet ds2 = con.ret_ds(query2);
                dataGridView1.DataSource = ds2.Tables[0].DefaultView;


            }
            catch (Exception ex)
            {
                MessageBox.Show("exception occured....");
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           // if (dataGridView2.SelectedRows.Count > 0)
           // {
            if (dataGridView2[e.ColumnIndex, e.RowIndex].Value != null)
            {
                int i = dataGridView2.CurrentCell.RowIndex;
                string q = "update chat set status='seen' where chatid='" + dataGridView2.Rows[i].Cells[0].Value.ToString() + "'";
                con.exec(q);
              
                Chat obj = new Chat(Program.userid, dataGridView2.Rows[i].Cells[1].Value.ToString(), dataGridView2.Rows[i].Cells[0].Value.ToString());
                ActiveForm.Hide();
                obj.Show();
            }
          //  }
        }
    }
}
