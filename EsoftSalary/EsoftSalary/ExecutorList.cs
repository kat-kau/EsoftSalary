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
    public partial class ExecutorList : Form
    {
        public ExecutorList()
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

        private void ExecutorList_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source = .\SQLSERVER; Initial Catalog = EsoftSalary; Integrated Security = true"))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("SELECT        Исполнители.ФИО_исполнителя, Исполнители.Грейд, Менаджеры.ФИО_менеджера FROM            Менаджеры INNER JOIN Исполнители ON Менаджеры.ID_менеджера = Исполнители.ID_менаджера", con);

                    SqlDataReader dr = com.ExecuteReader();
                    int i = 0;
                    while (dr.Read())
                    {

                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[i].Cells[0].Value = dr[0].ToString();
                        dataGridView1.Rows[i].Cells[1].Value = dr[1].ToString();
                        dataGridView1.Rows[i].Cells[2].Value = dr[2].ToString();
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
    }
}
