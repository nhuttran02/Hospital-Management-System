using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace HospitalMS
{
    public partial class Patient : Form
    {
        function fn = new function();
        String query;
        public Patient()
        {
            InitializeComponent();
            DisplayPatient();
        }
        readonly SqlConnection Con = new SqlConnection(connectionString: @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\NHUT\OneDrive\Documents\HospitalMS.mdf;Integrated Security=True;Connect Timeout=30");
        private void DisplayPatient()
        {
            try
            {
                Con.Open();
                string Query = "select * from Patient";
                SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                dataGridView2.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }
        private void AddBtn_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || comboBox1.Text == "" || comboBox2.Text == "" || textBox6.Text == "")
            //    {
            //        MessageBox.Show("Vui lòng điền đầy đủ thông tin");
            //    }
            //    else
            //    {
            //        Con.Open();
            //        string query = "insert into Patient Values('" + textBox1.Text + "', N'" + textBox2.Text + "', N'" + textBox3.Text + "', '" + textBox4.Text + "', '" + textBox5.Text + "', N'"+comboBox1.Text+"', '"+comboBox2.Text+"', N'"+textBox6.Text+"')";
            //        SqlCommand cmd = new SqlCommand(query, Con);
            //        cmd.ExecuteNonQuery();
            //        Con.Close();
            //        MessageBox.Show("Thông tin đã được lưu lại");
            //        DisplayPatient();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{
            //    Con.Close();
            //}



            if (textBox2.Text != "" || textBox3.Text != "" || textBox4.Text != "" || textBox5.Text != "" || comboBox1.Text == "" || comboBox2.Text == "" || textBox6.Text == "")
            {
                String PName = textBox2.Text;
                String PAddress = textBox3.Text;
                String PAge = textBox4.Text;
                String PPhone = textBox5.Text;
                String PGen = comboBox1.Text;
                String BloodGroup = comboBox2.Text;
                String Disease = textBox6.Text;

                string query = "INSERT INTO Patient (PName, PAddress, PAge, PPhone, PGen, BloodGroup, Disease) VALUES ('" + PName + "','" + PAddress + "','" + PAge + "','" + PPhone + "','" + PGen + "','"+ BloodGroup +"', '"+ Disease +"')";
                fn.setData(query, "Adding Successfully");
                DisplayPatient();
            }
            else
            {
                MessageBox.Show("Please fill in the information", "Warning !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Patient_Load(object sender, EventArgs e)
        {
            DisplayPatient();
        }

        private void CrossBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || comboBox1.Text == "" || comboBox2.Text == "" || textBox6.Text == "")
                {
                    MessageBox.Show("Please complete all information");
                }
                else
                {
                    Con.Open();
                    string query = "update Patient Set PName =@PName, PAddress= @PAddress, PAge = @PAge, PPhone = @PPhone, PGen = @PGen, BloodGroup = @BloodGroup,Disease = @Disease  where PId = @PId";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.Parameters.AddWithValue("@PName", textBox2.Text);
                    cmd.Parameters.AddWithValue("@PAddress", textBox3.Text);
                    cmd.Parameters.AddWithValue("@PAge", textBox4.Text);
                    cmd.Parameters.AddWithValue("@PPhone", textBox5.Text);
                    cmd.Parameters.AddWithValue("@PGen", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@BloodGroup", comboBox2.Text);
                    cmd.Parameters.AddWithValue("@Disease", textBox6.Text);
                    cmd.Parameters.AddWithValue("@PId", textBox1.Text);
                    cmd.ExecuteNonQuery();
                    Con.Close();
                    MessageBox.Show("Update successfully");
                    DisplayPatient();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Please type Patient ID");
                }
                else
                {
                    Con.Open();
                    string query = "delete from Patient where PId=@id"; //Tránh mã độc hại tiêm vào Textbox

                    using (var cmd = new SqlCommand(query, Con))
                    {
                        cmd.Parameters.AddWithValue("@id", textBox1.Text);
                        cmd.ExecuteNonQuery();
                    }
                    Con.Close();
                    MessageBox.Show("Delete successfully");
                    DisplayPatient();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            textBox6.Text = "";
        }

        private void HomeBtn_Click(object sender, EventArgs e)
        {
            Home obj = new Home();
            obj.Show();
            this.Hide();
        }

        private void dataGridView2_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                textBox2.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
                textBox3.Text = dataGridView2.SelectedRows[0].Cells[2].Value.ToString();
                textBox4.Text = dataGridView2.SelectedRows[0].Cells[3].Value.ToString();
                textBox5.Text = dataGridView2.SelectedRows[0].Cells[4].Value.ToString();
                comboBox1.Text = dataGridView2.SelectedRows[0].Cells[5].Value.ToString();
                comboBox2.Text = dataGridView2.SelectedRows[0].Cells[6].Value.ToString();
                textBox6.Text = dataGridView2.SelectedRows[0].Cells[7].Value.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Home obj = new Home();
            obj.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
