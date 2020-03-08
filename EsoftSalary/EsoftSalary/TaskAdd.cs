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
    public partial class TaskAdd : Form
    {
        public TaskAdd()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TaskList tl = new TaskList();
            tl.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || maskedTextBox1.Text == "" || maskedTextBox2.Text == "" || comboBox1.Text == "" || comboBox2.Text == "" || comboBox3.Text == "")
            {
                MessageBox.Show("Пожалуйста, заполните все поля!");
            }
            else
            {
                int complexity = Convert.ToInt32(textBox3.Text);
                if (complexity > 0 && complexity <= 50)
                {
                    try
                    {
                        using (SqlConnection con = new SqlConnection(@"Data Source = .\SQLSERVER; Initial Catalog = EsoftSalary; Integrated Security = true"))
                        {
                            string strStatus = "";
                            string strСharacter = "";

                            switch (comboBox1.Text)
                            {
                                case "Запланирована":
                                    strStatus = "plan";
                                    break;
                                case "Исполняется":
                                    strStatus = "exec";
                                    break;
                                case "Выполнена":
                                    strStatus = "completed";
                                    break;
                                case "Отменена":
                                    strStatus = "cancel";
                                    break;
                            }

                            switch (comboBox2.Text)
                            {
                                case "Установка оборудования":
                                    strСharacter = "deployment";
                                    break;
                                case "Анализ и проектирование":
                                    strСharacter = "analysis";
                                    break;
                                case "Техническое обслуживание и сопровождение":
                                    strСharacter = "support";
                                    break;
                            }


                            DateTime d = DateTime.ParseExact(maskedTextBox1.Text, "dd,MM,yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            DateTime d2 = DateTime.ParseExact(maskedTextBox2.Text, "dd,MM,yyyy", System.Globalization.CultureInfo.InvariantCulture);

                            con.Open();
                            SqlCommand com = new SqlCommand("INSERT INTO [dbo].[Задачи] ([ID_исполнителя],[Заголовок],[Сложность],[Статус],[Характер_работы],[Описание],[Срок_исполнения],[Дата_выполнения],[Время_на_выполнение_задачи],[Дата_создания_задачи],[deleted]) VALUES((SELECT        Исполнители.ID_исполнителя FROM            Задачи INNER JOIN Исполнители ON Задачи.ID_исполнителя = Исполнители.ID_исполнителя WHERE Исполнители.ФИО_исполнителя = '" + comboBox3.Text + "' AND Исполнители.deleted = 0 GROUP BY Исполнители.ID_исполнителя),'" + textBox1.Text + "'," + textBox3.Text + ",'" + strStatus + "','" + strСharacter + "','" + textBox2.Text + "','" + d.Date.ToString("yyyy-MM-dd") + "','" + d2.Date.ToString("yyyy-MM-dd") + "'," + textBox3.Text + ",'" + DateTime.Now.ToString("yyyy-MM-dd") + "',0)", con);
                            com.ExecuteNonQuery();
                            MessageBox.Show("Задача добавлена", "Все прошло успешно!", MessageBoxButtons.OK);
                            con.Close();

                        }
                    }
                    catch (Exception E) { MessageBox.Show("Возникла ошибка: " + E.Message); }
                } else
                {
                    MessageBox.Show("Сложность должна быть от 1 до 50");
                }


                
            }
        }

        private void TaskAdd_Load(object sender, EventArgs e)
        {
            textBox6.Text = DateTime.Now.ToString("dd.MM.yyyy");
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source = .\SQLSERVER; Initial Catalog = EsoftSalary; Integrated Security = true"))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("SELECT        ФИО_исполнителя FROM            Исполнители", con);
                    SqlDataReader dr = com.ExecuteReader();
                    int i = 0;
                    while (dr.Read())
                    {

                        comboBox3.Items.Add(dr[0].ToString());
                        i++;
                    }
                    con.Close();



                }
            }
            catch (SqlException E)
            {
                MessageBox.Show("Возникла ошибка: " + E.Message);
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8)
                e.Handled = true;
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8)
                e.Handled = true;
        }
    }
}
