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
    public partial class TaskList : Form
    {
        public TaskList()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void TaskList_Load(object sender, EventArgs e)
        {
            Autoriz aut = (Autoriz)this.Owner;
            if (aut.user == "manager")
            {
                label1.Visible = true;
                label5.Visible = true;
                comboBox2.Visible = true;
                button1.Visible = true;

                try
                {
                    using (SqlConnection con = new SqlConnection(@"Data Source = .\SQLSERVER; Initial Catalog = EsoftSalary; Integrated Security = true"))
                    {
                        con.Open();
                        SqlCommand com = new SqlCommand("SELECT        Задачи.Заголовок, Задачи.Статус, Исполнители.ФИО_исполнителя, Менаджеры.ФИО_менеджера FROM            Задачи INNER JOIN Исполнители ON Задачи.ID_исполнителя = Исполнители.ID_исполнителя INNER JOIN Менаджеры ON Исполнители.ID_менаджера = Менаджеры.ID_менеджера WHERE Менаджеры.Логин_менеджера = '" + aut.userName + "'", con);

                        SqlDataReader dr = com.ExecuteReader();
                        int i = 0;
                        while (dr.Read())
                        {

                            dataGridView1.Rows.Add();
                            dataGridView1.Rows[i].Cells[0].Value = dr[0].ToString();

                            switch (dr[1].ToString())
                            {
                                case "plan":
                                    dataGridView1.Rows[i].Cells[1].Value = "Запланирована";
                                    break;
                                case "exec":
                                    dataGridView1.Rows[i].Cells[1].Value = "Исполняется";
                                    break;
                                case "completed":
                                    dataGridView1.Rows[i].Cells[1].Value = "Выполнена";
                                    break;
                                case "cancel":
                                    dataGridView1.Rows[i].Cells[1].Value = "Отменена";
                                    break;
                            }

                            dataGridView1.Rows[i].Cells[2].Value = dr[2].ToString();
                            dataGridView1.Rows[i].Cells[3].Value = dr[3].ToString();
                            i++;

                        }
                        

                        con.Close();

                    }
                }
                catch (SqlException E)
                {
                    MessageBox.Show("Возникла ошибка: " + E.Message);
                }




                try
                {
                    using (SqlConnection con = new SqlConnection(@"Data Source = .\SQLSERVER; Initial Catalog = EsoftSalary; Integrated Security = true"))
                    {
                        con.Open();
                        SqlCommand com = new SqlCommand("SELECT        Задачи.Статус FROM            Задачи INNER JOIN Исполнители ON Задачи.ID_исполнителя = Исполнители.ID_исполнителя INNER JOIN Менаджеры ON Исполнители.ID_менаджера = Менаджеры.ID_менеджера WHERE Менаджеры.Логин_менеджера = '" + aut.userName + "' GROUP BY Задачи.Статус", con);
                        SqlDataReader dr = com.ExecuteReader();
                        int i = 0;
                        while (dr.Read())
                        {

                            switch (dr[0].ToString())
                            {
                                case "plan":
                                    comboBox1.Items.Add("Запланирована");
                                    break;
                                case "exec":
                                    comboBox1.Items.Add("Исполняется");
                                    break;
                                case "completed":
                                    comboBox1.Items.Add("Выполнена");
                                    break;
                                case "cancel":
                                    comboBox1.Items.Add("Отменена");
                                    break;
                            }
                            i++;
                        }
                        con.Close();



                    }
                }
                catch (SqlException E)
                {
                    MessageBox.Show("Возникла ошибка: " + E.Message);
                }


                try
                {
                    using (SqlConnection con = new SqlConnection(@"Data Source = .\SQLSERVER; Initial Catalog = EsoftSalary; Integrated Security = true"))
                    {
                        con.Open();
                        SqlCommand com = new SqlCommand("SELECT        Исполнители.ФИО_исполнителя FROM            Задачи INNER JOIN Исполнители ON Задачи.ID_исполнителя = Исполнители.ID_исполнителя INNER JOIN Менаджеры ON Исполнители.ID_менаджера = Менаджеры.ID_менеджера WHERE Менаджеры.Логин_менеджера = '" + aut.userName + "' GROUP BY Исполнители.ФИО_исполнителя", con);
                        SqlDataReader dr = com.ExecuteReader();
                        int i = 0;
                        while (dr.Read())
                        {
                            
                            comboBox2.Items.Add(dr[0].ToString());
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
            else if (aut.user == "executor")
            {
                label2.Visible = true;

                try
                {
                    using (SqlConnection con = new SqlConnection(@"Data Source = .\SQLSERVER; Initial Catalog = EsoftSalary; Integrated Security = true"))
                    {
                        con.Open();
                        SqlCommand com = new SqlCommand("SELECT        Задачи.Заголовок, Задачи.Статус, Исполнители.ФИО_исполнителя, Менаджеры.ФИО_менеджера FROM            Задачи INNER JOIN Исполнители ON Задачи.ID_исполнителя = Исполнители.ID_исполнителя INNER JOIN Менаджеры ON Исполнители.ID_менаджера = Менаджеры.ID_менеджера WHERE Исполнители.Логин_исполнителя = '" + aut.userName + "'", con);

                        SqlDataReader dr = com.ExecuteReader();
                        int i = 0;
                        while (dr.Read())
                        {

                            dataGridView1.Rows.Add();
                            dataGridView1.Rows[i].Cells[0].Value = dr[0].ToString();

                            switch (dr[1].ToString())
                            {
                                case "plan":
                                    dataGridView1.Rows[i].Cells[1].Value = "Запланирована";
                                    break;
                                case "exec":
                                    dataGridView1.Rows[i].Cells[1].Value = "Исполняется";
                                    break;
                                case "completed":
                                    dataGridView1.Rows[i].Cells[1].Value = "Выполнена";
                                    break;
                                case "cancel":
                                    dataGridView1.Rows[i].Cells[1].Value = "Отменена";
                                    break;
                            }

                            dataGridView1.Rows[i].Cells[2].Value = dr[2].ToString();
                            dataGridView1.Rows[i].Cells[3].Value = dr[3].ToString();
                            i++;

                        }
                        con.Close();
                    }
                }
                catch (SqlException E)
                {
                    MessageBox.Show("Возникла ошибка: " + E.Message);
                }




                try
                {
                    using (SqlConnection con = new SqlConnection(@"Data Source = .\SQLSERVER; Initial Catalog = EsoftSalary; Integrated Security = true"))
                    {
                        con.Open();
                        SqlCommand com = new SqlCommand("SELECT        Задачи.Статус FROM            Задачи INNER JOIN Исполнители ON Задачи.ID_исполнителя = Исполнители.ID_исполнителя INNER JOIN Менаджеры ON Исполнители.ID_менаджера = Менаджеры.ID_менеджера WHERE Исполнители.Логин_исполнителя = '" + aut.userName + "' GROUP BY Задачи.Статус", con);
                        SqlDataReader dr = com.ExecuteReader();
                        int i = 0;
                        while (dr.Read())
                        {

                            switch (dr[0].ToString())
                            {
                                case "plan":
                                    comboBox1.Items.Add("Запланирована");
                                    break;
                                case "exec":
                                    comboBox1.Items.Add("Исполняется");
                                    break;
                                case "completed":
                                    comboBox1.Items.Add("Выполнена");
                                    break;
                                case "cancel":
                                    comboBox1.Items.Add("Отменена");
                                    break;
                            }
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

        private void button6_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
            label5.Visible = false;
            comboBox2.Visible = false;
            button1.Visible = false;
            label2.Visible = false;
            Application.OpenForms[0].Show();
            this.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Visible = false;
                if (row.Cells[1].Value.ToString() == comboBox1.Text)
                {
                    row.Visible = true;
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Visible = true;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Visible = false;
            if (row.Cells[2].Value.ToString() == comboBox2.Text)
            {
                row.Visible = true;
            }
            }
        }
    }
}
