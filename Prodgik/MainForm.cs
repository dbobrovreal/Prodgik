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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            textGenre.Text = "Жанр";
            textGenre.ForeColor = Color.Gray;

        }

        private void closeButten_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        Point lastPoint;
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
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

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;

            }
        }

        private void textGenre_Enter(object sender, EventArgs e)
        {
            if (textGenre.Text == "Жанр")
            {
                textGenre.Text = "";
                textGenre.ForeColor = Color.Black;
            }
        }

        private void textGenre_Leave(object sender, EventArgs e)
        {
            if (textGenre.Text == "")
            {
                textGenre.Text = "Жанр";
                textGenre.ForeColor = Color.Gray;

            }
        }


        private void buttonSearch_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            String genret = textGenre.Text;
            String theDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");

            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            db.openConnection();
            MySqlCommand command = new MySqlCommand("SELECT time, name, canal FROM `movie` WHERE `genre`  = @tg AND `date`  = @dtp", db.getConnection());

            command.Parameters.Add("@tg", MySqlDbType.VarChar).Value = genret;
            command.Parameters.Add("@dtp", MySqlDbType.VarChar).Value = theDate;

            adapter.SelectCommand = command;
            adapter.Fill(table);
            
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string[] row = { Convert.ToString(reader.GetValue(0)), reader.GetString(1), reader.GetString(2)};
                    dataGridView1.Rows.Add(row);
                }
            }            
        }
    }
}
