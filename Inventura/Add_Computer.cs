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
    public partial class Add_Computer : Form
    {
        public Add_Computer()
        {
            InitializeComponent();
        }

        private void Add_Computer_Load(object sender, EventArgs e)
        {
            currencyComboBox.Items.Add("€");
            currencyComboBox.Items.Add("$");

            systemComboBox.Items.Add("kg");
            systemComboBox.Items.Add("lbs");

            coreNumberComboBox.Items.Add("2");
            coreNumberComboBox.Items.Add("4");
            coreNumberComboBox.Items.Add("8");
            coreNumberComboBox.Items.Add("16");
            coreNumberComboBox.Items.Add("32");

            ramComboBox.Items.Add("2");
            ramComboBox.Items.Add("4");
            ramComboBox.Items.Add("8");
            ramComboBox.Items.Add("16");
            ramComboBox.Items.Add("32");

            ramSizeComboBox.Items.Add("MB");
            ramSizeComboBox.Items.Add("GB");

            diskComboBox.Items.Add("2");
            diskComboBox.Items.Add("4");
            diskComboBox.Items.Add("8");
            diskComboBox.Items.Add("16");
            diskComboBox.Items.Add("32");
            diskComboBox.Items.Add("64");
            diskComboBox.Items.Add("128");
            diskComboBox.Items.Add("256");

            diskSizeComboBox.Items.Add("MB");
            diskSizeComboBox.Items.Add("GB");
            diskSizeComboBox.Items.Add("TB");
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainMenu log = new MainMenu();
            log.ShowDialog();
            this.Close();
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

        private void addButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nameTextBox.Text) || string.IsNullOrWhiteSpace(codeTextBox.Text) ||
                string.IsNullOrWhiteSpace(manufacturerTextBox.Text) || string.IsNullOrWhiteSpace(priceTextBox.Text) ||
                string.IsNullOrWhiteSpace(weightTextBox.Text) || string.IsNullOrWhiteSpace(coreNumberComboBox.Text) ||
                string.IsNullOrWhiteSpace(ramComboBox.Text) || string.IsNullOrWhiteSpace(diskComboBox.Text))
            {
                MessageBox.Show("You forgot to fill in all the boxes!");
            }

            else
            {
                string Price = priceTextBox.Text + currencyComboBox.SelectedItem.ToString();
                string Weight = weightTextBox.Text + systemComboBox.SelectedItem.ToString();
                string NumOfCores = coreNumberComboBox.SelectedItem.ToString();
                string Ram = ramComboBox.SelectedItem.ToString() + ramSizeComboBox.SelectedItem.ToString();
                string Disk = diskComboBox.SelectedItem.ToString() + diskSizeComboBox.SelectedItem.ToString();
                string Image = computerPictureBox.ImageLocation.ToString();

                Computer newComputer = new Computer(nameTextBox.Text, codeTextBox.Text, manufacturerTextBox.Text, Price, Image, Weight,
                    NumOfCores, Ram, Disk);

                newComputer.addToDatabase();

                int error = newComputer.addToDatabase();

                if (error == 0)
                {
                    MessageBox.Show("You succesffully added a new computer!");
                }

                if (error == 1)
                {
                    MessageBox.Show("This object already exists within the database");
                }
            }
        }
    }
}
