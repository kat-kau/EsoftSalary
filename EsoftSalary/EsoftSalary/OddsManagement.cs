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
            Application.OpenForms[1].Show();
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

            TaskList tl = (TaskList)this.Owner;
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source = .\SQLSERVER; Initial Catalog = EsoftSalary; Integrated Security = true"))
                {
                    con.Open();
                    
                        SqlCommand com = new SqlCommand("UPDATE       Коэффициенты     SET [Junior_мин_ЗП] = "+ textBox1.Text + ", [Middle_мин_ЗП] = " + textBox2.Text + ", [Senior_мин_ЗП] = " + textBox3.Text + ", [Коэффициент_для_Анализ_и_проектирование] = " + textBox4.Text.Replace(",", ".") + ", [Коэффициент_для_Установка_оборудования] = " + textBox5.Text.Replace(",", ".") + ", [Коэффициент_для_Техническое_обслуживание_и_сопровождение] = " + textBox6.Text.Replace(",", ".") + ", [Коэффициент_времени] = " + textBox7.Text.Replace(",", ".") + ", [Коэффициент_сложности] = " + textBox8.Text.Replace(",", ".") + ", [Коэффициент_для_перевода_в_денежный_эквивалент] = " + textBox9.Text.Replace(",", ".") + " FROM            Коэффициенты INNER JOIN Менаджеры ON Коэффициенты.ID_менаджера = Менаджеры.ID_менеджера Where Менаджеры.Логин_менеджера = '" + tl.managerName + "'", con); 

                    com.ExecuteNonQuery();
                    MessageBox.Show("Редактирование выполнено", "Все прошло успешно!", MessageBoxButtons.OK);
                    con.Close();

                }
            }
            catch (SqlException E)
            {
                MessageBox.Show("Возникла ошибка: " + E.Message);
            }


        }
    }
}
