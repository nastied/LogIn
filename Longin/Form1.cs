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
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

            private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DataLogIn.mdf;Integrated Security=True;Connect Timeout=30;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sqlCommand = "SELECT COUNT(1) FROM Log WHERE username = @user AND password = @pass";

                using (SqlCommand cmd = new SqlCommand(sqlCommand, conn))
                {

                    cmd.Parameters.AddWithValue("@user", textBox1.Text);
                    cmd.Parameters.AddWithValue("@pass", textBox2.Text);

                    conn.Open();

                    int matchCount = Convert.ToInt32(cmd.ExecuteScalar());

                    if (matchCount == 1)
                    {
                        this.Hide();
                        Form2 ss = new Form2();
                        ss.Show();
                    }
                    else
                    {
                        MessageBox.Show("username or/and password incorrect");
                        textBox1.Text = string.Empty;
                        textBox2.Text = string.Empty;
                    }
                }
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
