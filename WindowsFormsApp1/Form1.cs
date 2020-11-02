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

namespace WorkingWithDB
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection;

        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            //Change this path "C:\Users\HOME\Desktop\c#\WorkingWithDB\WorkingWithDB\Database.mdf" for your own computer
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Elizaveta\source\repos\WindowsFormsApp1\WindowsFormsApp1\Database1.mdf;Integrated Security=True";
            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM [Мебельная мастерская ]", sqlConnection);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader[" Код товара"]) + "    " + Convert.ToString(sqlReader["Товар "]));

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (label6.Visible)
                label6.Visible = false;

            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text) &&
                !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text))
            {
                SqlCommand command = new SqlCommand("INSERT INTO [Мебельная мастерская] (Код заказа,Товар )VALUES(@Код заказа, @Товар)", sqlConnection);

                command.Parameters.AddWithValue("Код заказа", textBox1.Text);

                command.Parameters.AddWithValue("Товар", textBox2.Text);

                await command.ExecuteNonQueryAsync();
            }
            else
            {
                label6.Visible = true;
            }
        }

        private async void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM [Мебельная мастерская]", sqlConnection);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["Код товара"]) + "    " + Convert.ToString(sqlReader["Товар "]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (label6.Visible)
                label6.Visible = false;

            if (!string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox3.Text) &&
                !string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text) &&
                !string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox6.Text))
            {
                SqlCommand command = new SqlCommand("UPDATE [пункт обмена валюты] SET [Кассир]=@Кассир, [Валюта]=@Валюта  WHERE [Сделка]=@Сделка", sqlConnection);

                command.Parameters.AddWithValue("Валюта", textBox6.Text);
                command.Parameters.AddWithValue("Кассир", textBox4.Text);
                command.Parameters.AddWithValue("Сделка", textBox3.Text);

                await command.ExecuteNonQueryAsync();
            }
            else if (!string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text))
            {
                label5.Visible = true;
            }
            else
            {
                label5.Visible = true;
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (label6.Visible)
                label6.Visible = false;

            if (!string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox6.Text))
            {
                SqlCommand command = new SqlCommand("DELETE FROM [Мебельная мастерская] WHERE [Цена изделия]=@Цена изделия", sqlConnection);

                command.Parameters.AddWithValue("Цена изделия", textBox6.Text);

                await command.ExecuteNonQueryAsync();
            }
            else
            {
                label6.Visible = true;
            }
        }
    }
}

