using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HospitalMS
{
    public partial class User : Form
    {
        function fn = new function();
        String query;
        public User()
        {
            InitializeComponent();
        }

        private void User_Load(object sender, EventArgs e)
        {

        }
        readonly SqlConnection Con = new SqlConnection(connectionString: @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\NHUT\OneDrive\Documents\HospitalMS.mdf;Integrated Security=True;Connect Timeout=30");

        //private void btnAdd_Click(object sender, EventArgs e)
        //{

        //    if (txtname.Text != "" && txtusername.Text != "" && txtpassword.Text != "")
        //    {
        //        String uname = txtname.Text;
        //        String uusername = txtusername.Text;
        //        String upassword = txtpassword.Text;


        //        query = "insert into User(uname, uusername, upassword) values ('" + uname + "','" + uusername + "','" + upassword + "')";
        //        fn.setData(query, "Adding Successfully");
        //    }
        //    else
        //    {
        //        MessageBox.Show("Please fill in the information", "Warning !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //    }
        //}

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtname.Text != "" && txtusername.Text != "" && txtpassword.Text != "")
            {
                String uname = txtname.Text;
                String uusername = txtusername.Text;
                String upassword = txtpassword.Text;

                string query = "INSERT INTO [User] (uname, uusername, upassword) VALUES ('" + uname + "','" + uusername + "','" + upassword + "')";
                fn.setData(query, "Adding Successfully");
            }
            else
            {
                MessageBox.Show("Please fill in the information", "Warning !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


    }
}
