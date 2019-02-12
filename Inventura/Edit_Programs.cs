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
    public partial class Edit_Programs : Form
    {
        public Edit_Programs()
        {
            InitializeComponent();
        }

        private void Edit_Programs_Load(object sender, EventArgs e)
        {
            SQLiteConnection Conn = new SQLiteConnection("data source = database.sqlite");

            Conn.Open();

            using (SQLiteCommand createCommand = new SQLiteCommand(Conn))
            {
                createCommand.CommandText = "SELECT name FROM Programs";
                SQLiteDataReader result = createCommand.ExecuteReader();
                string name;

                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        name = result.GetString(0);
                        if (editProgramNameComboBox.Items.Contains(name))
                        {
                            name = string.Empty;
                        }
                        else
                        {
                            editProgramNameComboBox.Items.Add(name);
                        }
                    }
                    createCommand.Dispose();
                }
                Conn.Close();
            }
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(editProgramNameComboBox.Text))
            {
                nameTextBox.Text = "";
                codeTextBox.Text = "";
                developerTextBox.Text = "";
                priceTextBox.Text = "";
                sizeTextBox.Text = "";
                licenseTextBox.Text = "";
                versionTextBox.Text = "";

            }
            else
            {
                try
                {
                    SQLiteConnection Conn = new SQLiteConnection("data source = database.sqlite");

                    Conn.Open();

                    using (SQLiteCommand createCommand = new SQLiteCommand(Conn))
                    {
                        string name = editProgramNameComboBox.SelectedItem.ToString();

                        nameTextBox.Text = name;

                        createCommand.CommandText = "SELECT code FROM Programs WHERE name = '" + editProgramNameComboBox.SelectedItem.ToString() + "'";
                        SQLiteDataReader sql_code = createCommand.ExecuteReader();
                        sql_code.Read();
                        string code = sql_code.GetString(0);
                        createCommand.Dispose();

                        codeTextBox.Text = code;

                        createCommand.CommandText = "SELECT developer FROM Programs WHERE name = '" + editProgramNameComboBox.SelectedItem.ToString() + "'";
                        SQLiteDataReader sql_developer = createCommand.ExecuteReader();
                        sql_developer.Read();
                        string developer = sql_developer.GetString(0);
                        createCommand.Dispose();

                        developerTextBox.Text = developer;

                        createCommand.CommandText = "SELECT price FROM Programs WHERE name = '" + editProgramNameComboBox.SelectedItem.ToString() + "'";
                        SQLiteDataReader sql_price = createCommand.ExecuteReader();
                        sql_price.Read();
                        string price = sql_price.GetString(0);
                        createCommand.Dispose();

                        priceTextBox.Text = price;

                        createCommand.CommandText = "SELECT memory_size FROM Programs WHERE name = '" + editProgramNameComboBox.SelectedItem.ToString() + "'";
                        SQLiteDataReader sql_memory = createCommand.ExecuteReader();
                        sql_memory.Read();
                        string size = sql_memory.GetString(0);
                        createCommand.Dispose();

                        sizeTextBox.Text = size;

                        createCommand.CommandText = "SELECT license FROM Programs WHERE name = '" + editProgramNameComboBox.SelectedItem.ToString() + "'";
                        SQLiteDataReader sql_license = createCommand.ExecuteReader();
                        sql_license.Read();
                        string license = sql_license.GetString(0);
                        createCommand.Dispose();

                        licenseTextBox.Text = license;

                        createCommand.CommandText = "SELECT version FROM Programs WHERE name = '" + editProgramNameComboBox.SelectedItem.ToString() + "'";
                        SQLiteDataReader sql_version = createCommand.ExecuteReader();
                        sql_version.Read();
                        string version = sql_version.GetString(0);
                        createCommand.Dispose();

                        versionTextBox.Text = version;
                    }
                    Conn.Close();
                }

                catch (Exception Ex)
                {
                    MessageBox.Show("Error: " + Ex.Message);
                }
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            SQLiteConnection Conn = new SQLiteConnection("data source = database.sqlite");

            Conn.Open();

            using (SQLiteCommand createCommand = new SQLiteCommand(Conn))
            {
                try
                {
                    createCommand.CommandText = "UPDATE Programs SET name = '" + nameTextBox.Text + "', code = '" + codeTextBox.Text + "', developer = '" + developerTextBox.Text + "'," +
                        "price = '" + priceTextBox.Text + "', memory_size = '" + sizeTextBox.Text + "', license = '" + licenseTextBox.Text + "'," +
                        "version = '" + versionTextBox.Text + "' WHERE name = '" + editProgramNameComboBox.Text + "'";
                    createCommand.ExecuteNonQuery();

                    MessageBox.Show("You successfully edited the Program!");

                    editProgramNameComboBox.Text = string.Empty;
                    editProgramNameComboBox.Items.Clear();
                    editProgramNameComboBox.SelectedIndex = -1;

                    createCommand.CommandText = "SELECT name FROM Programs";
                    SQLiteDataReader result = createCommand.ExecuteReader();
                    string name;

                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            name = result.GetString(0);
                            if (editProgramNameComboBox.Items.Contains(name))
                            {
                                name = string.Empty;
                            }
                            else
                            {
                                editProgramNameComboBox.Items.Add(name);
                            }
                        }
                        createCommand.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            Conn.Close();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            SQLiteConnection Conn = new SQLiteConnection("data source = database.sqlite");

            Conn.Open();

            using (SQLiteCommand createCommand = new SQLiteCommand(Conn))
            {
                try
                {
                    createCommand.CommandText = "DELETE FROM Programs WHERE name = '" + editProgramNameComboBox.Text + "'";
                    createCommand.ExecuteNonQuery();

                    MessageBox.Show("You successfully deleted the Program!");

                    editProgramNameComboBox.Text = string.Empty;
                    editProgramNameComboBox.Items.Clear();
                    editProgramNameComboBox.SelectedIndex = -1;

                    createCommand.CommandText = "SELECT name FROM Programs";
                    SQLiteDataReader result = createCommand.ExecuteReader();
                    string name;

                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            name = result.GetString(0);
                            if (editProgramNameComboBox.Items.Contains(name))
                            {
                                name = string.Empty;
                            }
                            else
                            {
                                editProgramNameComboBox.Items.Add(name);
                            }
                        }
                        createCommand.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            Conn.Close();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            All_Programs log = new All_Programs();
            log.ShowDialog();
            this.Close();
        }
    }
}
