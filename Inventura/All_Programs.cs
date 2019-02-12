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
    public partial class All_Programs : Form
    {
        public All_Programs()
        {
            InitializeComponent();
        }

        private void All_Programs_Load(object sender, EventArgs e)
        {
            SQLiteConnection Conn = new SQLiteConnection("data source = database.sqlite");

            Conn.Open();

            SQLiteCommand command = new SQLiteCommand(Conn);

            const string sql = "SELECT * FROM Programs";

            try
            {
                DataSet newDataSet = new DataSet();
                var data = new SQLiteDataAdapter(sql, Conn);
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
            Edit_Programs log = new Edit_Programs();
            log.ShowDialog();
            this.Close();
        }
    }
}
