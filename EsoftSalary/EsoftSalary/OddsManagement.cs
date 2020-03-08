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
    public partial class OddsManagement : Form
    {
        public OddsManagement()
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




        private void OddsManagement_Load(object sender, EventArgs e)
        {
            TaskList tl = (TaskList)this.Owner;

            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source = .\SQLSERVER; Initial Catalog = EsoftSalary; Integrated Security = true"))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("SELECT        Коэффициенты.Junior_мин_ЗП, Коэффициенты.Middle_мин_ЗП, Коэффициенты.Senior_мин_ЗП, Коэффициенты.Коэффициент_для_Анализ_и_проектирование, Коэффициенты.Коэффициент_для_Установка_оборудования, Коэффициенты.Коэффициент_для_Техническое_обслуживание_и_сопровождение, Коэффициенты.Коэффициент_времени, Коэффициенты.Коэффициент_сложности, Коэффициенты.Коэффициент_для_перевода_в_денежный_эквивалент FROM            Коэффициенты INNER JOIN Менаджеры ON Коэффициенты.ID_менаджера = Менаджеры.ID_менеджера WHERE Менаджеры.Логин_менеджера = '" + tl.managerName + "'", con);

                    SqlDataReader dr = com.ExecuteReader();
                    int i = 0;
                    while (dr.Read())
                    {

                        textBox1.Text = dr[0].ToString();
                        textBox2.Text = dr[1].ToString();
                        textBox3.Text = dr[2].ToString();
                        textBox4.Text = dr[3].ToString();
                        textBox5.Text = dr[4].ToString();
                        textBox6.Text = dr[5].ToString();
                        textBox7.Text = dr[6].ToString();
                        textBox8.Text = dr[7].ToString();
                        textBox9.Text = dr[8].ToString();

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

        private void button3_Click(object sender, EventArgs e)
        {
            bool koef = true;

            try
            {
                double value = Convert.ToDouble(textBox4.Text.Replace(".", ","));
                if (value > 1)
                {
                    koef = false;
                    MessageBox.Show("Значение Коэффициент характера выполненных работ для \"Анализ и проектирование\" должно быть больше нуля и меньше единицы ");
                }
            }
            catch (FormatException E)
            {
                MessageBox.Show("Неверный формат строки Коэффициент характера выполненных работ для \"Анализ и проектирование\"");
                koef = false;
            }
            catch (OverflowException E)
            {
                MessageBox.Show("Значение Коэффициент характера выполненных работ для \"Анализ и проектирование\" было недопустимо малым или недопустимо большим");
                koef = false;
            }


            try
            {
                double value = Convert.ToDouble(textBox5.Text.Replace(".", ","));
                if (value > 1)
                {
                    koef = false;
                    MessageBox.Show("Значение Коэффициент характера выполненных работ для \"Установка оборудования\" должно быть больше нуля и меньше единицы ");
                }
            }
            catch (OverflowException E)
            {
                MessageBox.Show("Значение Коэффициент характера выполненных работ для \"Установка оборудования\" было недопустимо малым или недопустимо большим");
                koef = false;
            }
            catch (FormatException E)
            {
                MessageBox.Show("Неверный формат строки Коэффициент характера выполненных работ для \"Установка оборудования\"");
                koef = false;
            }


            try
            {
                double value = Convert.ToDouble(textBox6.Text.Replace(".", ","));
                if (value > 1)
                {
                    koef = false;
                    MessageBox.Show("Значение Коэффициент характера выполненных работ для \"Техническое обслуживание и сопровождение\" должно быть больше нуля и меньше единицы ");
                }

            }
            catch (OverflowException E)
            {
                MessageBox.Show("Значение Коэффициент характера выполненных работ для \"Техническое обслуживание и сопровождение\" было недопустимо малым или недопустимо большим");
                koef = false;
            }
            catch (FormatException E)
            {
                MessageBox.Show("Неверный формат строки Коэффициент характера выполненных работ для \"Техническое обслуживание и сопровождение\"");
                koef = false;
            }

            try
            {
                int value = Convert.ToInt32(textBox1.Text);
            }
            catch (OverflowException E)
            {
                MessageBox.Show("Коэффициент гарантированного минимума зарплаты за месяц для junior слишком велик. Используйте число до 2 147 483 647");
                koef = false;
            }

            try
            {
                int value = Convert.ToInt32(textBox2.Text);
            }
            catch (OverflowException E)
            {
                MessageBox.Show("Коэффициент гарантированного минимума зарплаты за месяц для middle слишком велик. Используйте число до 2 147 483 647");
                koef = false;
            }

            try
            {
                int value = Convert.ToInt32(textBox3.Text);
            }
            catch (OverflowException E)
            {
                MessageBox.Show("Коэффициент гарантированного минимума зарплаты за месяц для senior слишком велик. Используйте число до 2 147 483 647");
                koef = false;
            }

            try
            {
                double value = Convert.ToDouble(textBox7.Text.Replace(".", ","));
            }
            catch (OverflowException E)
            {
                MessageBox.Show("Значение Коэффициент времени было недопустимо малым или недопустимо большим");
                koef = false;
            }
            catch (FormatException E)
            {
                MessageBox.Show("Неверный формат строки Коэффициент времени");
                koef = false;
            }


            try
            {
                double value = Convert.ToDouble(textBox8.Text.Replace(".", ","));
            }
            catch (OverflowException E)
            {
                MessageBox.Show("Значение Коэффициент сложности было недопустимо малым или недопустимо большим");
                koef = false;
            }
            catch (FormatException E)
            {
                MessageBox.Show("Неверный формат строки Коэффициент сложности");
                koef = false;
            }

            try
            {
                double value = Convert.ToDouble(textBox9.Text.Replace(".", ","));
            }
            catch (OverflowException E)
            {
                MessageBox.Show("Значение Коэффициент для перевода в денежный эквивалент было недопустимо малым или недопустимо большим");
                koef = false;
            }
            catch (FormatException E)
            {
                MessageBox.Show("Неверный формат строки Коэффициент для перевода в денежный эквивалент");
                koef = false;
            }

            TaskList tl = (TaskList)this.Owner;
            if (koef != false)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(@"Data Source = .\SQLSERVER; Initial Catalog = EsoftSalary; Integrated Security = true"))
                    {
                        con.Open();

                        SqlCommand com = new SqlCommand("UPDATE       Коэффициенты     SET [Junior_мин_ЗП] = " + textBox1.Text + ", [Middle_мин_ЗП] = " + textBox2.Text + ", [Senior_мин_ЗП] = " + textBox3.Text + ", [Коэффициент_для_Анализ_и_проектирование] = " + textBox4.Text.Replace(",", ".") + ", [Коэффициент_для_Установка_оборудования] = " + textBox5.Text.Replace(",", ".") + ", [Коэффициент_для_Техническое_обслуживание_и_сопровождение] = " + textBox6.Text.Replace(",", ".") + ", [Коэффициент_времени] = " + textBox7.Text.Replace(",", ".") + ", [Коэффициент_сложности] = " + textBox8.Text.Replace(",", ".") + ", [Коэффициент_для_перевода_в_денежный_эквивалент] = " + textBox9.Text.Replace(",", ".") + " FROM            Коэффициенты INNER JOIN Менаджеры ON Коэффициенты.ID_менаджера = Менаджеры.ID_менеджера Where Менаджеры.Логин_менеджера = '" + tl.managerName + "'", con);

                        com.ExecuteNonQuery();
                        MessageBox.Show("Редактирование выполнено", "Все прошло успешно!", MessageBoxButtons.OK);
                        con.Close();

                    }
                }
                catch (SqlException E)
                {
                    MessageBox.Show("Возникла ошибка Базы данных: " + E.Message);
                }
            }
            else
            {
                koef = true;
            }

         
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8)
                e.Handled = true;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8)
                e.Handled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8)
                e.Handled = true;
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8 && e.KeyChar != 44 && e.KeyChar != 46)
                e.Handled = true;
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8 && e.KeyChar != 44 && e.KeyChar != 46)
                e.Handled = true;
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8 && e.KeyChar != 44 && e.KeyChar != 46)
                e.Handled = true;
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8 && e.KeyChar != 44 && e.KeyChar != 46)
                e.Handled = true;
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8 && e.KeyChar != 44 && e.KeyChar != 46)
                e.Handled = true;
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8 && e.KeyChar != 44 && e.KeyChar != 46)
                e.Handled = true;
        }
    }
}
