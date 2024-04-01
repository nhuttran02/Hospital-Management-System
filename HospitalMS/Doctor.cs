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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HospitalMS
{
    public partial class Doctor : Form
    {
        function fn = new function();
        String query;
        public Doctor()
        {
            InitializeComponent();
            DisplayDoctor();
        }
        readonly SqlConnection Con = new SqlConnection(connectionString: @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\NHUT\OneDrive\Documents\HospitalMS.mdf;Integrated Security=True;Connect Timeout=30");
        private void DisplayDoctor()
        {
            try
            {
                Con.Open();
                string Query = "select * from Doctor";
                SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                Con.Close();
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void CrossBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void HomeBtn_Click(object sender, EventArgs e)
        {
            Home obj = new Home();
            obj.Show();
            this.Hide();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (textBox1.Text == "" || textBox2.Text == "" || comboBox1.Text == "" || textBox4.Text == "" || textBox5.Text == "")
            //    {
            //        MessageBox.Show("Vui lòng điền đầy đủ thông tin");
            //    }
            //    else
            //    {
            //        Con.Open();
            //        string query = "insert into Doctor Values('"+textBox1.Text+"', N'"+textBox2.Text+"', '"+comboBox1.Text+"', '"+textBox4.Text+"', '"+textBox5.Text+"')";
            //        SqlCommand cmd = new SqlCommand(query, Con);
            //        cmd.ExecuteNonQuery();
            //        Con.Close();
            //        MessageBox.Show("Thông tin đã được lưu lại");
            //        DisplayDoctor();
            //    }
            //}catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{
            //    Con.Close();
            //}


            if (textBox2.Text != "" || comboBox1.Text != "" || textBox4.Text != "" || textBox5.Text != "")
            {
                String DocName = textBox2.Text;
                String DocGen = comboBox1.Text;
                String Experience = textBox4.Text;
                String Licensce = textBox5.Text;

                string query = "INSERT INTO Doctor (DocName, DocGen, Experience, Licensce) VALUES ('" + DocName + "','" + DocGen + "','" + Experience + "','" + Licensce + "')";
                fn.setData(query, "Adding Successfully");
                DisplayDoctor();
            }
            else
            {
                MessageBox.Show("Please fill in the information", "Warning !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void Doctor_Load(object sender, EventArgs e)
        {
            DisplayDoctor();
        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            comboBox1.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Please type Doctor ID");
                }
                else
                {
                    Con.Open();
                    //string query = "delete from Doctor where DocId=' " + textBox1.Text + "';";
                    //string query = "delete from Doctor where DocId='" + (textBox1.Text ?? string.Empty) + "'";
                    string query = "delete from Doctor where DocId=@id"; //Tránh mã độc hại tiêm vào Textbox

                    using (var cmd = new SqlCommand(query, Con))
                    {
                        cmd.Parameters.AddWithValue("@id", textBox1.Text);
                        cmd.ExecuteNonQuery();
                    }



                    //SqlCommand cmd = new SqlCommand(query, Con);
                    //cmd.ExecuteNonQuery();
                    Con.Close();
                    MessageBox.Show("Delete successfully");
                    DisplayDoctor();
                }
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }finally 
            {
                Con.Close();             
            }
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "" || textBox2.Text == "" || comboBox1.Text == "" || textBox4.Text == "" || textBox5.Text == "")
                {
                    MessageBox.Show("Please complete all information");
                }
                else
                {
                    Con.Open();
                    string query = "update Doctor Set DocName =@DocName, DocGen= @DocGen, Experience = @Experience, Licensce = @Licensce where DocId = @DocId";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.Parameters.AddWithValue("@DocName", textBox2.Text);
                    cmd.Parameters.AddWithValue("@DocGen", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@Experience", textBox4.Text);
                    cmd.Parameters.AddWithValue("@Licensce", textBox5.Text);
                    cmd.Parameters.AddWithValue("@DocId", textBox1.Text);
                    cmd.ExecuteNonQuery();
                    Con.Close();
                    MessageBox.Show("Update successfully");
                    DisplayDoctor();
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

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                comboBox1.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                textBox5.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();

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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
