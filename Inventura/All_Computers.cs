using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Inventura
{
    public partial class All_Computers : Form
    {
        public All_Computers()
        {
            InitializeComponent();
        }

        private void All_Computers_Load(object sender, EventArgs e)
        {
            SQLiteConnection Conn = new SQLiteConnection("data source = database.sqlite");

            Conn.Open();

            SQLiteCommand command = new SQLiteCommand(Conn);

            const string database = @"database.sqlite";
            const string sql = "SELECT * FROM Computers";

            var connection = new SQLiteConnection("Data Source=" + database);

            try
            {
                DataSet newDataSet = new DataSet();
                var data = new SQLiteDataAdapter(sql, connection);
                data.Fill(newDataSet);
                computerDataGridView.DataSource = newDataSet.Tables[0].DefaultView;
                Conn.Close();
            }

            catch (Exception)
            {
                MessageBox.Show("There is an error!");
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainMenu log = new MainMenu();
            log.ShowDialog();
            this.Close();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Edit_Computers log = new Edit_Computers();
            log.ShowDialog();
            this.Close();
        }
    }
}
