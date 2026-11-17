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

namespace Longin
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != string.Empty && textBox2.Text !=  string.Empty)
            {
                string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DataLogIn.mdf;Integrated Security=True;Connect Timeout=30;";
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();

                string sqlCommand = String.Format("INSERT INTO Log (username, password) VALUES('{0}', '{1}');", textBox1.Text.ToString(), textBox2.Text.ToString());
                //MessageBox.Show(sqlCommand);
                SqlCommand cmd = new SqlCommand(sqlCommand, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                conn.Close();
                this.Hide();
            }
            else
            {
                MessageBox.Show("username and/or password invalid");
            }
        }
    }
}
