using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prodgik
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.passField.AutoSize = false;
            this.passField.Size = new Size(this.passField.Size.Width, 50);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void closeButten_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void closeButten_MouseEnter(object sender, EventArgs e)
        {
            closeButten.ForeColor = Color.Red;
        }

        private void closeButten_MouseLeave(object sender, EventArgs e)
        {
            closeButten.ForeColor = Color.White;
        }
        Point lastPoint;
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;

            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;

            }
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {

            if (loginField.Text == "")
            {
                MessageBox.Show("Введите логин");
                return;
            }

            if (passField.Text == "")
            {
                MessageBox.Show("Введите пароль");
                return;
            }

            if (isUserLogin())
                return;

            DB db = new DB();
            MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`login`, `pass`, `name`, `surname`) VALUES (@login, @pass, @name, @surname)", db.getConnection());

            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = loginField.Text;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = passField.Text;


            db.openConnection();

          /* if (command.ExecuteNonQuery() == 1)
            {
                this.Hide();
                MainForm tvShow = new MainForm();
                tvShow.Show();
            }
            else
            {
                MessageBox.Show("Аккаунт не был создан");
            }*/

            db.closeConnection();
           
        }

        public Boolean isUserLogin()
        {
            DB db = new DB();

            DataTable table = new DataTable();
            DataTable tablepass = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlDataAdapter adapterpass = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @uL AND `pass` =  @uP", db.getConnection());

            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = loginField.Text;
            

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = passField.Text;
                adapterpass.SelectCommand = command;
                adapterpass.Fill(tablepass);
                if (tablepass.Rows.Count > 0)
                {
                    this.Hide();
                    MainForm tvShow = new MainForm();
                    tvShow.Show();
                }
                return true;
            }
            else
                return false;
        }

        private void registerLabel_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
        }
    }
}
