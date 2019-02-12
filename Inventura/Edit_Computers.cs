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
    public partial class Edit_Computers : Form
    {
        public Edit_Computers()
        {
            InitializeComponent();
        }

        private void Edit_Computers_Load(object sender, EventArgs e)
        {
            SQLiteConnection Conn = new SQLiteConnection("data source = database.sqlite");

            Conn.Open();

            using (SQLiteCommand createCommand = new SQLiteCommand(Conn))
            {
                createCommand.CommandText = "SELECT name FROM Computers";
                SQLiteDataReader result = createCommand.ExecuteReader();
                string name;

                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        name = result.GetString(0);
                        if (editComputerNameComboBox.Items.Contains(name))
                        {
                            name = string.Empty;
                        }
                        else
                        {
                            editComputerNameComboBox.Items.Add(name);
                        }
                    }
                    createCommand.Dispose();
                }
                Conn.Close();
            }
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(editComputerNameComboBox.Text))
            {
                nameTextBox.Text = "";
                codeTextBox.Text = "";
                manufacturerTextBox.Text = "";
                priceTextBox.Text = "";
                weightTextBox.Text = "";
                numCoresTextBox.Text = "";
                ramTextBox.Text = "";
                diskSizeTextBox.Text = "";

            }
            else
            {
                try
                {
                    SQLiteConnection Conn = new SQLiteConnection("data source = database.sqlite");

                    Conn.Open();

                    using (SQLiteCommand createCommand = new SQLiteCommand(Conn))
                    {
                        string name = editComputerNameComboBox.SelectedItem.ToString();

                        nameTextBox.Text = name;

                        createCommand.CommandText = "SELECT code FROM Computers WHERE name = '" + editComputerNameComboBox.SelectedItem.ToString() + "'";
                        SQLiteDataReader sql_code = createCommand.ExecuteReader();
                        sql_code.Read();
                        string code = sql_code.GetString(0);
                        createCommand.Dispose();

                        codeTextBox.Text = code;

                        createCommand.CommandText = "SELECT manufacturer FROM Computers WHERE name = '" + editComputerNameComboBox.SelectedItem.ToString() + "'";
                        SQLiteDataReader sql_manufacturer = createCommand.ExecuteReader();
                        sql_manufacturer.Read();
                        string manufacturer = sql_manufacturer.GetString(0);
                        createCommand.Dispose();

                        manufacturerTextBox.Text = manufacturer;

                        createCommand.CommandText = "SELECT price FROM Computers WHERE name = '" + editComputerNameComboBox.SelectedItem.ToString() + "'";
                        SQLiteDataReader sql_price = createCommand.ExecuteReader();
                        sql_price.Read();
                        string price = sql_price.GetString(0);
                        createCommand.Dispose();

                        priceTextBox.Text = price;

                        createCommand.CommandText = "SELECT image FROM Computers WHERE name = '" + editComputerNameComboBox.SelectedItem.ToString() + "'";
                        SQLiteDataReader sql_image = createCommand.ExecuteReader();
                        sql_image.Read();
                        string image = sql_image.GetString(0);
                        createCommand.Dispose();

                        computerPictureBox.ImageLocation = image;

                        createCommand.CommandText = "SELECT weight FROM Computers WHERE name = '" + editComputerNameComboBox.SelectedItem.ToString() + "'";
                        SQLiteDataReader sql_weight = createCommand.ExecuteReader();
                        sql_weight.Read();
                        string weight = sql_weight.GetString(0);
                        createCommand.Dispose();

                        weightTextBox.Text = weight;

                        createCommand.CommandText = "SELECT num_of_cores FROM Computers WHERE name = '" + editComputerNameComboBox.SelectedItem.ToString() + "'";
                        SQLiteDataReader sql_cores = createCommand.ExecuteReader();
                        sql_cores.Read();
                        string cores = sql_cores.GetString(0);
                        createCommand.Dispose();

                        numCoresTextBox.Text = cores;

                        createCommand.CommandText = "SELECT ram FROM Computers WHERE name = '" + editComputerNameComboBox.SelectedItem.ToString() + "'";
                        SQLiteDataReader sql_ram = createCommand.ExecuteReader();
                        sql_ram.Read();
                        string ram = sql_ram.GetString(0);
                        createCommand.Dispose();

                        ramTextBox.Text = ram;

                        createCommand.CommandText = "SELECT disk_size FROM Computers WHERE name = '" + editComputerNameComboBox.SelectedItem.ToString() + "'";
                        SQLiteDataReader sql_disk = createCommand.ExecuteReader();
                        sql_disk.Read();
                        string disk = sql_disk.GetString(0);
                        createCommand.Dispose();

                        diskSizeTextBox.Text = disk;
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
                    createCommand.CommandText = "UPDATE Computers SET name = '" + nameTextBox.Text + "', code = '" + codeTextBox.Text + "', manufacturer = '" + manufacturerTextBox.Text + "'," +
                        "price = '" + priceTextBox.Text + "', image = '" + computerPictureBox.ImageLocation.ToString() + "', weight = '" + weightTextBox.Text + "', num_of_cores = '" + numCoresTextBox.Text + "'," +
                        "ram = '" + ramTextBox.Text + "', disk_size = '" + diskSizeTextBox.Text + "' WHERE name = '" + editComputerNameComboBox.Text + "'";
                    createCommand.ExecuteNonQuery();

                    MessageBox.Show("You successfully edited the Computer!");

                    editComputerNameComboBox.Text = string.Empty;
                    editComputerNameComboBox.Items.Clear();
                    editComputerNameComboBox.SelectedIndex = -1;

                    createCommand.CommandText = "SELECT name FROM Computers";
                    SQLiteDataReader result = createCommand.ExecuteReader();
                    string name;

                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            name = result.GetString(0);
                            if (editComputerNameComboBox.Items.Contains(name))
                            {
                                name = string.Empty;
                            }
                            else
                            {
                                editComputerNameComboBox.Items.Add(name);
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

        private void imageButton_Click(object sender, EventArgs e)
        {
            string imgLocation = "";
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "png files (*.png)|*.png| jpg files(*.jpg)|*.jpg| All files(*.*)|*.*";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                imgLocation = dialog.FileName.ToString();
                computerPictureBox.ImageLocation = imgLocation;
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            SQLiteConnection Conn = new SQLiteConnection("data source = database.sqlite");

            Conn.Open();

            using (SQLiteCommand createCommand = new SQLiteCommand(Conn))
            {
                try
                {
                    createCommand.CommandText = "DELETE FROM Computers WHERE name = '" + editComputerNameComboBox.Text + "'";
                    createCommand.ExecuteNonQuery();

                    MessageBox.Show("You successfully deleted the Computer!");

                    editComputerNameComboBox.Text = string.Empty;
                    editComputerNameComboBox.Items.Clear();
                    editComputerNameComboBox.SelectedIndex = -1;

                    createCommand.CommandText = "SELECT name FROM Computers";
                    SQLiteDataReader result = createCommand.ExecuteReader();
                    string name;

                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            name = result.GetString(0);
                            if (editComputerNameComboBox.Items.Contains(name))
                            {
                                name = string.Empty;
                            }
                            else
                            {
                                editComputerNameComboBox.Items.Add(name);
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
            All_Computers log = new All_Computers();
            log.ShowDialog();
            this.Close();
        }
    }
}
