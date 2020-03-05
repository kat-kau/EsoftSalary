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

namespace EsoftSalary
{
    public partial class Autoriz : Form
    {
        public Autoriz()
        {
            InitializeComponent();
        }

        public string user = "";
        public string userName = "";
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source = .\SQLSERVER; Initial Catalog = EsoftSalary; Integrated Security = true"))
                {
                    con.Open();
                    string sqlcommand = "SELECT COUNT(*) FROM Исполнители WHERE Логин_исполнителя='" + textBox1.Text + "' AND Пароль='" + textBox2.Text + "'";
                    SqlCommand com = new SqlCommand(sqlcommand, con);
                    object value = com.ExecuteScalar();

                    if (value.ToString() != "0")
                    {
                        MessageBox.Show("Авторизация успешна! Вы вошли как исполнитель");
                        user = "executor";
                        userName = textBox1.Text;
                        TaskList tl = new TaskList();
                        tl.Show(this);
                        this.Hide();
                    }
                    else
                    {
                        sqlcommand = "SELECT COUNT(*) FROM Менаджеры WHERE Логин_менеджера='" + textBox1.Text + "' AND Пароль='" + textBox2.Text + "'";
                        com = new SqlCommand(sqlcommand, con);
                        value = com.ExecuteScalar();
                        if (value.ToString() != "0")
                        {
                            MessageBox.Show("Авторизация успешна! Вы вошли как менеджер");
                            user = "manager";
                            userName = textBox1.Text;


                            TaskList tl = new TaskList();
                            tl.Show(this);
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Ошибка логина или пароля! Введите данные заново");
                        }

                    }
                    con.Close();
                }
            } catch (SqlException E)
            {
                MessageBox.Show("Возникла ошибка: "+E.Message);
            }
        }

        private void Autoriz_Load(object sender, EventArgs e)
        {

        }
    }
}
