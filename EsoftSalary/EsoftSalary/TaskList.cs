﻿using System;
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

        public string managerName = "";
        private void button2_Click(object sender, EventArgs e)
        {
            TaskAdd tl = new TaskAdd();
            tl.Show(this);
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void TaskList_Load(object sender, EventArgs e)
        {
            Autoriz aut = (Autoriz)this.Owner;
            if (Properties.Settings.Default.user == "manager")
            {
                label1.Visible = true;
                label5.Visible = true;
                comboBox2.Visible = true;
                comboBox3.Visible = true;
                button1.Visible = true;
                button9.Visible = true;

                try
                {
                    using (SqlConnection con = new SqlConnection(@"Data Source = .\SQLSERVER; Initial Catalog = EsoftSalary; Integrated Security = true"))
                    {
                        con.Open();
                        SqlCommand com = new SqlCommand("SELECT  Задачи.ID_задачи,  Задачи.Заголовок, Задачи.Статус, Исполнители.ФИО_исполнителя, Менаджеры.ФИО_менеджера FROM            Задачи INNER JOIN Исполнители ON Задачи.ID_исполнителя = Исполнители.ID_исполнителя INNER JOIN Менаджеры ON Исполнители.ID_менаджера = Менаджеры.ID_менеджера WHERE Менаджеры.Логин_менеджера = '" + Properties.Settings.Default.userName + "' AND Задачи.deleted = 0", con);

                        SqlDataReader dr = com.ExecuteReader();
                        int i = 0;
                        while (dr.Read())
                        {

                            dataGridView1.Rows.Add();
                            dataGridView1.Rows[i].Cells[0].Value = dr[0].ToString();
                            dataGridView1.Rows[i].Cells[1].Value = dr[1].ToString();

                            switch (dr[2].ToString())
                            {
                                case "plan":
                                    dataGridView1.Rows[i].Cells[2].Value = "Запланирована";
                                    break;
                                case "exec":
                                    dataGridView1.Rows[i].Cells[2].Value = "Исполняется";
                                    break;
                                case "completed":
                                    dataGridView1.Rows[i].Cells[2].Value = "Выполнена";
                                    break;
                                case "cancel":
                                    dataGridView1.Rows[i].Cells[2].Value = "Отменена";
                                    break;
                            }

                            dataGridView1.Rows[i].Cells[3].Value = dr[3].ToString();
                            dataGridView1.Rows[i].Cells[4].Value = dr[4].ToString();
                            i++;

                        }


                        con.Close();

                    }
                    //dataGridView1.Columns[0].Visible = false;
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
                        SqlCommand com = new SqlCommand("SELECT        Задачи.Статус FROM            Задачи INNER JOIN Исполнители ON Задачи.ID_исполнителя = Исполнители.ID_исполнителя INNER JOIN Менаджеры ON Исполнители.ID_менаджера = Менаджеры.ID_менеджера WHERE Менаджеры.Логин_менеджера = '" + Properties.Settings.Default.userName + "' AND Задачи.deleted = 0 GROUP BY Задачи.Статус", con);
                        SqlDataReader dr = com.ExecuteReader();
                        int i = 0;
                        comboBox3.Items.Add("-");
                        comboBox3.SelectedIndex = 0;
                        while (dr.Read())
                        {

                            switch (dr[0].ToString())
                            {
                                case "plan":
                                    comboBox3.Items.Add("Запланирована");
                                    break;
                                case "exec":
                                    comboBox3.Items.Add("Исполняется");
                                    break;
                                case "completed":
                                    comboBox3.Items.Add("Выполнена");
                                    break;
                                case "cancel":
                                    comboBox3.Items.Add("Отменена");
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
                        SqlCommand com = new SqlCommand("SELECT        Исполнители.ФИО_исполнителя FROM            Задачи INNER JOIN Исполнители ON Задачи.ID_исполнителя = Исполнители.ID_исполнителя INNER JOIN Менаджеры ON Исполнители.ID_менаджера = Менаджеры.ID_менеджера WHERE Менаджеры.Логин_менеджера = '" + Properties.Settings.Default.userName + "' AND Задачи.deleted = 0 GROUP BY Исполнители.ФИО_исполнителя", con);
                        SqlDataReader dr = com.ExecuteReader();
                        int i = 0;
                        comboBox2.Items.Add("-");
                        comboBox2.SelectedIndex = 0;
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
            else if (Properties.Settings.Default.user == "executor")
            {
                comboBox1.Visible = true;
                label2.Visible = true;

                try
                {
                    using (SqlConnection con = new SqlConnection(@"Data Source = .\SQLSERVER; Initial Catalog = EsoftSalary; Integrated Security = true"))
                    {
                        con.Open();
                        SqlCommand com = new SqlCommand("SELECT       Задачи.ID_задачи, Задачи.Заголовок, Задачи.Статус, Исполнители.ФИО_исполнителя, Менаджеры.ФИО_менеджера FROM            Задачи INNER JOIN Исполнители ON Задачи.ID_исполнителя = Исполнители.ID_исполнителя INNER JOIN Менаджеры ON Исполнители.ID_менаджера = Менаджеры.ID_менеджера WHERE Исполнители.Логин_исполнителя = '" + Properties.Settings.Default.userName + "' AND Задачи.deleted = 0 ", con);

                        SqlDataReader dr = com.ExecuteReader();
                        int i = 0;
                        while (dr.Read())
                        {

                            dataGridView1.Rows.Add();
                            dataGridView1.Rows[i].Cells[0].Value = dr[0].ToString();
                            dataGridView1.Rows[i].Cells[1].Value = dr[1].ToString();

                            switch (dr[2].ToString())
                            {
                                case "plan":
                                    dataGridView1.Rows[i].Cells[2].Value = "Запланирована";
                                    break;
                                case "exec":
                                    dataGridView1.Rows[i].Cells[2].Value = "Исполняется";
                                    break;
                                case "completed":
                                    dataGridView1.Rows[i].Cells[2].Value = "Выполнена";
                                    break;
                                case "cancel":
                                    dataGridView1.Rows[i].Cells[2].Value = "Отменена";
                                    break;
                            }

                            dataGridView1.Rows[i].Cells[3].Value = dr[3].ToString();
                            dataGridView1.Rows[i].Cells[4].Value = dr[4].ToString();
                            i++;

                        }
                        con.Close();
                    }
                    //dataGridView1.Columns[0].Visible = false;
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
                        SqlCommand com = new SqlCommand("SELECT        Задачи.Статус FROM            Задачи INNER JOIN Исполнители ON Задачи.ID_исполнителя = Исполнители.ID_исполнителя INNER JOIN Менаджеры ON Исполнители.ID_менаджера = Менаджеры.ID_менеджера WHERE Исполнители.Логин_исполнителя = '" + Properties.Settings.Default.userName + "' AND Задачи.deleted = 0 GROUP BY Задачи.Статус", con);
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
                if (row.Cells[2].Value.ToString() == comboBox1.Text)
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

        }

        private void button4_Click(object sender, EventArgs e)
        {
            TaskAdd tl = new TaskAdd();
            tl.Show(this);
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ExecutorList el = new ExecutorList();
            el.Show(this);
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Autoriz aut = (Autoriz)this.Owner;
            managerName = Properties.Settings.Default.userName;
            OddsManagement om = new OddsManagement();
            om.Show(this);
            this.Hide();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (comboBox3.Text == "-")
            {
                if (comboBox2.Text == "-")
                {
                    MessageBox.Show("Укажите параметры фильтрации");
                }
                else
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        row.Visible = false;
                        if (row.Cells[3].Value.ToString() == comboBox2.Text)
                        {
                            row.Visible = true;
                        }
                    }
                }
            }
            else
            {
                if (comboBox2.Text == "-")
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        row.Visible = false;
                        if (row.Cells[2].Value.ToString() == comboBox3.Text)
                        {
                            row.Visible = true;
                        }
                    }
                }
                else
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        row.Visible = false;
                        if (row.Cells[2].Value.ToString() == comboBox3.Text && row.Cells[3].Value.ToString() == comboBox2.Text)
                        {
                            row.Visible = true;
                        }
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult resualt = MessageBox.Show("Удалить задачу?", "Удаление", MessageBoxButtons.OKCancel);

            if (resualt.ToString() == "OK")
            {
                string del = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                try
                {
                    using (SqlConnection con = new SqlConnection(@"Data Source = .\SQLSERVER; Initial Catalog = EsoftSalary; Integrated Security = true"))
                    {
                        con.Open();

                        SqlCommand com = new SqlCommand("UPDATE [dbo].[Задачи] SET [deleted] = 1 WHERE [ID_задачи] = " + del + ";", con);
                        com.ExecuteNonQuery();
                        con.Close();
                    }

                    int delet = dataGridView1.SelectedCells[0].RowIndex;
                    dataGridView1.Rows.RemoveAt(delet);
                    comboBox1.Items.Clear();
                    comboBox2.Items.Clear();
                    comboBox3.Items.Clear();
                    try
                    {
                        using (SqlConnection con = new SqlConnection(@"Data Source = .\SQLSERVER; Initial Catalog = EsoftSalary; Integrated Security = true"))
                        {
                            con.Open();
                            SqlCommand com = new SqlCommand("SELECT        Задачи.Статус FROM            Задачи INNER JOIN Исполнители ON Задачи.ID_исполнителя = Исполнители.ID_исполнителя INNER JOIN Менаджеры ON Исполнители.ID_менаджера = Менаджеры.ID_менеджера WHERE Менаджеры.Логин_менеджера = '" + Properties.Settings.Default.userName + "' AND Задачи.deleted = 0 GROUP BY Задачи.Статус", con);
                            SqlDataReader dr = com.ExecuteReader();
                            int i = 0;
                            comboBox3.Items.Add("-");
                            comboBox3.SelectedIndex = 0;
                            while (dr.Read())
                            {

                                switch (dr[0].ToString())
                                {
                                    case "plan":
                                        comboBox3.Items.Add("Запланирована");
                                        break;
                                    case "exec":
                                        comboBox3.Items.Add("Исполняется");
                                        break;
                                    case "completed":
                                        comboBox3.Items.Add("Выполнена");
                                        break;
                                    case "cancel":
                                        comboBox3.Items.Add("Отменена");
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
                            SqlCommand com = new SqlCommand("SELECT        Исполнители.ФИО_исполнителя FROM            Задачи INNER JOIN Исполнители ON Задачи.ID_исполнителя = Исполнители.ID_исполнителя INNER JOIN Менаджеры ON Исполнители.ID_менаджера = Менаджеры.ID_менеджера WHERE Менаджеры.Логин_менеджера = '" + Properties.Settings.Default.userName + "' AND Задачи.deleted = 0 GROUP BY Исполнители.ФИО_исполнителя", con);
                            SqlDataReader dr = com.ExecuteReader();
                            int i = 0;
                            comboBox2.Items.Add("-");
                            comboBox2.SelectedIndex = 0;
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

                    try
                    {
                        using (SqlConnection con = new SqlConnection(@"Data Source = .\SQLSERVER; Initial Catalog = EsoftSalary; Integrated Security = true"))
                        {
                            con.Open();
                            SqlCommand com = new SqlCommand("SELECT        Задачи.Статус FROM            Задачи INNER JOIN Исполнители ON Задачи.ID_исполнителя = Исполнители.ID_исполнителя INNER JOIN Менаджеры ON Исполнители.ID_менаджера = Менаджеры.ID_менеджера WHERE Исполнители.Логин_исполнителя = '" + Properties.Settings.Default.userName + "' AND Задачи.deleted = 0 GROUP BY Задачи.Статус", con);
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
                catch (System.Data.SqlClient.SqlException E)
                {
                    MessageBox.Show("Внимание, возникла ошибка: " + E.Message);
                }

            }
        }
    }
}
