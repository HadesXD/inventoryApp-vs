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
    public partial class Edit_Monitors : Form
    {
        public Edit_Monitors()
        {
            InitializeComponent();    
        }

        private void Edit_Monitors_Load(object sender, EventArgs e)
        {
            SQLiteConnection Conn = new SQLiteConnection("data source = database.sqlite");

            Conn.Open();

            using (SQLiteCommand createCommand = new SQLiteCommand(Conn))
            {
                createCommand.CommandText = "SELECT name FROM Monitors";
                SQLiteDataReader result = createCommand.ExecuteReader();
                string name;

                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        name = result.GetString(0);
                        if (editMonitorNameComboBox.Items.Contains(name))
                        {
                            name = string.Empty;
                        }
                        else
                        {
                            editMonitorNameComboBox.Items.Add(name);
                        }
                    }
                    createCommand.Dispose();
                }
                Conn.Close();
            }
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(editMonitorNameComboBox.Text))
            {
                nameTextBox.Text = "";
                codeTextBox.Text = "";
                manufacturerTextBox.Text = "";
                priceTextBox.Text = "";
                weightTextBox.Text = "";
                resolutionTextBox.Text = "";
                aspectRatioTextBox.Text = "";
                typeTextBox.Text = "";

            }
            else
            {
                try
                {
                    SQLiteConnection Conn = new SQLiteConnection("data source = database.sqlite");

                    Conn.Open();

                    using (SQLiteCommand createCommand = new SQLiteCommand(Conn))
                    {
                        string name = editMonitorNameComboBox.SelectedItem.ToString();

                        nameTextBox.Text = name;

                        createCommand.CommandText = "SELECT code FROM Monitors WHERE name = '" + editMonitorNameComboBox.SelectedItem.ToString() + "'";
                        SQLiteDataReader sql_code = createCommand.ExecuteReader();
                        sql_code.Read();
                        string code = sql_code.GetString(0);
                        createCommand.Dispose();

                        codeTextBox.Text = code;

                        createCommand.CommandText = "SELECT manufacturer FROM Monitors WHERE name = '" + editMonitorNameComboBox.SelectedItem.ToString() + "'";
                        SQLiteDataReader sql_manufacturer = createCommand.ExecuteReader();
                        sql_manufacturer.Read();
                        string manufacturer = sql_manufacturer.GetString(0);
                        createCommand.Dispose();

                        manufacturerTextBox.Text = manufacturer;

                        createCommand.CommandText = "SELECT price FROM Monitors WHERE name = '" + editMonitorNameComboBox.SelectedItem.ToString() + "'";
                        SQLiteDataReader sql_price = createCommand.ExecuteReader();
                        sql_price.Read();
                        string price = sql_price.GetString(0);
                        createCommand.Dispose();

                        priceTextBox.Text = price;

                        createCommand.CommandText = "SELECT image FROM Monitors WHERE name = '" + editMonitorNameComboBox.SelectedItem.ToString() + "'";
                        SQLiteDataReader sql_image = createCommand.ExecuteReader();
                        sql_image.Read();
                        string image = sql_image.GetString(0);
                        createCommand.Dispose();

                        monitorPictureBox.ImageLocation = image;

                        createCommand.CommandText = "SELECT weight FROM Monitors WHERE name = '" + editMonitorNameComboBox.SelectedItem.ToString() + "'";
                        SQLiteDataReader sql_weight = createCommand.ExecuteReader();
                        sql_weight.Read();
                        string weight = sql_weight.GetString(0);
                        createCommand.Dispose();

                        weightTextBox.Text = weight;

                        createCommand.CommandText = "SELECT resolution FROM Monitors WHERE name = '" + editMonitorNameComboBox.SelectedItem.ToString() + "'";
                        SQLiteDataReader sql_resolution = createCommand.ExecuteReader();
                        sql_resolution.Read();
                        string resolution = sql_resolution.GetString(0);
                        createCommand.Dispose();

                        resolutionTextBox.Text = resolution;

                        createCommand.CommandText = "SELECT aspect_ratio FROM Monitors WHERE name = '" + editMonitorNameComboBox.SelectedItem.ToString() + "'";
                        SQLiteDataReader sql_aspectratio = createCommand.ExecuteReader();
                        sql_aspectratio.Read();
                        string aspectratio = sql_aspectratio.GetString(0);
                        createCommand.Dispose();

                        aspectRatioTextBox.Text = aspectratio;

                        createCommand.CommandText = "SELECT monitor_type FROM Monitors WHERE name = '" + editMonitorNameComboBox.SelectedItem.ToString() + "'";
                        SQLiteDataReader sql_type = createCommand.ExecuteReader();
                        sql_type.Read();
                        string type = sql_type.GetString(0);
                        createCommand.Dispose();

                        typeTextBox.Text = type;
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
                    createCommand.CommandText = "UPDATE Monitors SET name = '" + nameTextBox.Text + "', code = '" + codeTextBox.Text + "', manufacturer = '" + manufacturerTextBox.Text + "'," +
                        "price = '" + priceTextBox.Text + "', image = '" + monitorPictureBox.ImageLocation.ToString() + "', weight = '" + weightTextBox.Text + "', resolution = '" + resolutionTextBox.Text + "'," +
                        "aspect_ratio = '" + aspectRatioTextBox.Text + "', monitor_type = '" + typeTextBox.Text + "' WHERE name = '"+ editMonitorNameComboBox.Text +"'";
                    createCommand.ExecuteNonQuery();
                    
                    MessageBox.Show("You successfully edited the Monitor!");

                    editMonitorNameComboBox.Text = string.Empty;
                    editMonitorNameComboBox.Items.Clear();
                    editMonitorNameComboBox.SelectedIndex = -1;

                    createCommand.CommandText = "SELECT name FROM Monitors";
                    SQLiteDataReader result = createCommand.ExecuteReader();
                    string name;

                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            name = result.GetString(0);
                            if (editMonitorNameComboBox.Items.Contains(name))
                            {
                                name = string.Empty;
                            }
                            else
                            {
                                editMonitorNameComboBox.Items.Add(name);
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
                monitorPictureBox.ImageLocation = imgLocation;
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
                    createCommand.CommandText = "DELETE FROM Monitors WHERE name = '" + editMonitorNameComboBox.Text + "'";
                    createCommand.ExecuteNonQuery();

                    MessageBox.Show("You successfully deleted the Monitor!");

                    editMonitorNameComboBox.Text = string.Empty;
                    editMonitorNameComboBox.Items.Clear();
                    editMonitorNameComboBox.SelectedIndex = -1;

                    createCommand.CommandText = "SELECT name FROM Monitors";
                    SQLiteDataReader result = createCommand.ExecuteReader();
                    string name;

                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            name = result.GetString(0);
                            if (editMonitorNameComboBox.Items.Contains(name))
                            {
                                name = string.Empty;
                            }
                            else
                            {
                                editMonitorNameComboBox.Items.Add(name);
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
            All_Monitors log = new All_Monitors();
            log.ShowDialog();
            this.Close();
        }
    }
}
