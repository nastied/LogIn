using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Longin
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            textBox1.Enter += TextBox_Enter;
            textBox2.Enter += TextBox_Enter;
        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;

            if (tb != null && tb.ForeColor == Color.Red)
            {
                tb.Text = string.Empty;
                tb.ForeColor = Color.Black;

                if (tb == textBox2)
                {
                    tb.PasswordChar = '*';
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool isValid = true;

            //username: letters, numbers, dots, and underscores
            if (!Regex.IsMatch(textBox1.Text, @"^[a-zA-Z0-9._]+$") || textBox1.Text == string.Empty)
            {
                textBox1.Text = "Only letters, numbers, . and _ allowed";
                textBox1.ForeColor = Color.Red;
                isValid = false;
            }

            //password: 1 uppercase, 1 lowercase/letter, 1 special character
            if (!Regex.IsMatch(textBox2.Text, @"^(?=.*[A-Z])(?=.*[a-z])(?=.*[\W_]).+$") || textBox2.Text == string.Empty)
            {
                if (textBox2.ForeColor != Color.Red)
                {
                    textBox2.UseSystemPasswordChar = false;
                    textBox2.PasswordChar = '\0';
                    textBox2.Text = "Needs 1 uppercase, 1 letter, and 1 special char";
                    textBox2.ForeColor = Color.Red;
                }
            }

            if (!isValid) return;

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DataLogIn.mdf;Integrated Security=True;Connect Timeout=30;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sqlCommand = "INSERT INTO Log (username, password) VALUES (@user, @pass)";

                using (SqlCommand cmd = new SqlCommand(sqlCommand, conn))
                {
                    cmd.Parameters.AddWithValue("@user", textBox1.Text);
                    cmd.Parameters.AddWithValue("@pass", textBox2.Text);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    this.Hide();
                }
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}