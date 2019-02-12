using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Items;

namespace Inventura
{
    public partial class Add_Program : Form
    {
        public Add_Program()
        {
            InitializeComponent();
        }

        private void Add_Program_Load(object sender, EventArgs e)
        {
            currencyComboBox.Items.Add("€");
            currencyComboBox.Items.Add("$");

            memorySizeComboBox.Items.Add("KB");
            memorySizeComboBox.Items.Add("MB");
            memorySizeComboBox.Items.Add("GB");
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainMenu log = new MainMenu();
            log.ShowDialog();
            this.Close();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nameTextBox.Text) || string.IsNullOrWhiteSpace(codeTextBox.Text) ||
                string.IsNullOrWhiteSpace(developerTextBox.Text) || string.IsNullOrWhiteSpace(priceTextBox.Text) ||
                string.IsNullOrWhiteSpace(sizeTextBox.Text) || string.IsNullOrWhiteSpace(licenseTextBox.Text) ||
                string.IsNullOrWhiteSpace(versionTextBox.Text))
            {
                MessageBox.Show("You forgot to fill in all the boxes!");
            }

            else
            {
                string Price = priceTextBox.Text + currencyComboBox.SelectedItem.ToString();
                string Size = sizeTextBox.Text + memorySizeComboBox.SelectedIndex.ToString();

                SoftwareItem newSotfwareItem = new SoftwareItem(nameTextBox.Text, codeTextBox.Text, developerTextBox.Text, Price, Size,
                    licenseTextBox.Text, versionTextBox.Text);

                newSotfwareItem.addToDatabase();

                int error = newSotfwareItem.addToDatabase();

                if (error == 1)
                {
                    MessageBox.Show("This object already exists within the database");
                }

                if (error == 0)
                {
                    MessageBox.Show("You succesfully added a new Program!");
                }
            }
        }
    }
}
