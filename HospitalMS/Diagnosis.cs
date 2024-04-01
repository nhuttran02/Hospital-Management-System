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
    public partial class Diagnosis : Form
    {
        function fn = new function();
        String query;
        public Diagnosis()
        {
            InitializeComponent();
            DisplayDiagnosis();
        }
        readonly SqlConnection Con = new SqlConnection(connectionString: @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\NHUT\OneDrive\Documents\HospitalMS.mdf;Integrated Security=True;Connect Timeout=30");
        private void DisplayDiagnosis()
        {
            try
            {
                Con.Open();
                string Query = "select * from Diagnosis";
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
        void DisplayPatientId()
        {
            string sql = "select * from Patient";
            SqlCommand cmd = new SqlCommand(sql, Con);
            SqlDataReader rdr;
            try
            {
                Con.Open();
                DataTable dt = new DataTable();
                dt.Columns.Add("Pid", typeof(int));
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                comboBox1.ValueMember = "PId";
                comboBox1.DataSource = dt;
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
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (textBox1.Text == "" || comboBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
            //    {
            //        MessageBox.Show("Vui lòng điền đầy đủ thông tin");
            //    }
            //    else
            //    {
            //        Con.Open();
            //        string query = "insert into Diagnosis Values('" + textBox1.Text + "','"+comboBox1.Text+"', N'" + textBox2.Text + "', N'" + textBox3.Text + "', N'" + textBox4.Text + "', N'" + textBox5.Text + "')";
            //        SqlCommand cmd = new SqlCommand(query, Con);
            //        cmd.ExecuteNonQuery();
            //        Con.Close();
            //        MessageBox.Show("Thông tin đã được lưu lại");
            //        DisplayDiagnosis();
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


            if (comboBox1.Text != "" || textBox2.Text != "" || textBox3.Text != "" || textBox4.Text != "" || textBox5.Text != "")
            {
                String PatientId = comboBox1.Text;
                String PatientName = textBox2.Text;
                String Symptoms = textBox3.Text;
                String Diagnostic = textBox4.Text;
                String Medicines = textBox5.Text;

                string query = "INSERT INTO Diagnosis (PatientId, PatientName, Symptoms, Diagnostic, Medicines) VALUES ('" + PatientId + "','" + PatientName + "','" + Symptoms + "','" + Diagnostic + "', '"+ Medicines + "')";
                fn.setData(query, "Adding Successfully");
                DisplayDiagnosis();
            }
            else
            {
                MessageBox.Show("Please fill in the information", "Warning !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "" || comboBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
                {
                    MessageBox.Show("Please complete all information");
                }
                else
                {
                    Con.Open();
                    string query = "update Patient Set PatientId =@PatientId, PatientName=@PatientName, Symptoms = @Symptoms, Diagnosis = @Diagnosis, Medicines = @Medicines  where DId = @DId";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.Parameters.AddWithValue("@PatientId", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@PatientName", textBox2.Text);
                    cmd.Parameters.AddWithValue("@Symptoms", textBox3.Text);
                    cmd.Parameters.AddWithValue("@Diagnosis", textBox4.Text);
                    cmd.Parameters.AddWithValue("@Medicines", textBox5.Text);
                    cmd.Parameters.AddWithValue("@DId", textBox1.Text);
                    cmd.ExecuteNonQuery();
                    Con.Close();
                    MessageBox.Show("Update successfully");
                    DisplayDiagnosis();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Please type Diagnosis ID");
                }
                else
                {
                    Con.Open();
                    string query = "delete from Diagnosis where DId=@id"; //Tránh mã độc hại tiêm vào Textbox

                    using (var cmd = new SqlCommand(query, Con))
                    {
                        cmd.Parameters.AddWithValue("@id", textBox1.Text);
                        cmd.ExecuteNonQuery();
                    }
                    Con.Close();
                    MessageBox.Show("Delete successfully");
                    DisplayDiagnosis();
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

        private void HomeBtn_Click(object sender, EventArgs e)
        {
            Home obj = new Home();
            obj.Show();
            this.Hide();   
        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            comboBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }

        private void dataGridView2_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                comboBox1.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
                textBox2.Text = dataGridView2.SelectedRows[0].Cells[2].Value.ToString();
                textBox3.Text = dataGridView2.SelectedRows[0].Cells[3].Value.ToString();
                textBox4.Text = dataGridView2.SelectedRows[0].Cells[4].Value.ToString();
                textBox5.Text = dataGridView2.SelectedRows[0].Cells[5].Value.ToString();
                label9.Text = dataGridView2.SelectedRows[0].Cells[2].Value.ToString();
                label5.Text = dataGridView2.SelectedRows[0].Cells[3].Value.ToString();
                label10.Text = dataGridView2.SelectedRows[0].Cells[4].Value.ToString();
                label11.Text = dataGridView2.SelectedRows[0].Cells[5].Value.ToString();

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

        private void Diagnosis_Load(object sender, EventArgs e)
        {
            DisplayPatientId();
        }
        string pname;
        void DisplayPatientName()
        {
            try
            {
                Con.Open();
                string ss = "select * from Patient where PId =" + comboBox1.SelectedValue.ToString();

                SqlCommand cmd = new SqlCommand(ss, Con);
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    pname = dr["PName"].ToString();
                    textBox2.Text = pname;
                }
                Con.Close();
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            DisplayPatientName();
        }

        private void CrossBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }   
}
