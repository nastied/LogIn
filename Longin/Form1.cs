using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Longin
{
    public partial class Form1 : Form
    {
        int i = 0;
        bool ok = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DataLogIn.mdf;Integrated Security=True;Connect Timeout=30;");
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) From Log where username='" + textBox1.Text + "' and password='" + textBox2.Text + "'", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            if (dt.Rows[0][0].ToString() == "1")
            {
                this.Hide();
                Form2 ss = new Form2();
                ss.Show();
            }
            else
            {
                MessageBox.Show("username or/and password incorrect");
            } 
            */

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DataLogIn.mdf;Integrated Security=True;Connect Timeout=30;";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sqlCommand = "SELECT * from Log;";
            //MessageBox.Show(sqlCommand);
            SqlCommand cmd = new SqlCommand(sqlCommand, conn);
            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {
                if (reader[1].ToString() == textBox1.Text && reader[2].ToString() == textBox2.Text)
                {
                    i = Convert.ToInt32(reader[0]);
                    ok = true;
                    break;
                }
                else
                { 
                    ok = false;
                }
            }
            conn.Close();

            if (ok == true)
            {
                conn.Open();

                string sqlCommand1 = "SELECT username and password from Log where id ='" + i + "'";
                //MessageBox.Show(sqlCommand1);
                SqlCommand cmd1 = new SqlCommand(sqlCommand, conn);
                SqlDataReader reader1 = cmd.ExecuteReader();

                this.Hide();
                Form2 ss = new Form2();
                ss.Show();
                ok = true;
            }
            else
            {
                MessageBox.Show("username or/and password incorrect");
                textBox1.Text = string.Empty;
                textBox2.Text = string.Empty;
            }

        }

        private void label4_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Form3 ss1 = new Form3();
            ss1.Show();
        }
    }
}
