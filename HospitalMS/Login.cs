using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HospitalMS
{
    public partial class Login : Form
    {
        function fn = new function();
        String query;

        public Login() => InitializeComponent();

        private void CrossBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void LoginBtn_Click(object sender, EventArgs e)
        {
            //if (Admin.Text == "" && Password.Text == "")
            //{
            //    MessageBox.Show("Vui lòng nhập tài khoản và mật khẩu");
            //}
            //else if (Admin.Text == "Admin" && Password.Text == "Password")
            //{
            //    Home obj = new Home();
            //    obj.Show();
            //    this.Hide();
            //}
            //else
            //{
            //    MessageBox.Show("Vui lòng nhập chính xác tài khoản và mật khẩu");
            //}



            query = "select uusername, upassword from [User] where uusername = '" + Admin.Text + "' and upassword = '" + Password.Text + "'";
            DataSet ds = fn.getData(query);

            if (ds.Tables[0].Rows.Count != 0)
            {
                Home dss = new Home();
                this.Hide();
                dss.Show();
            }
            else
            {
                MessageBox.Show("Please enter the correct account and password");
                Password.Clear();
            }

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Admin_TextChanged(object sender, EventArgs e)
        {

        }

        private void Password_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
