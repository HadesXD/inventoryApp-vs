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
    public partial class Add_Monitor : Form
    {
        public Add_Monitor()
        {
            InitializeComponent();
        }

        private void Add_Monitor_Load(object sender, EventArgs e)
        {
            currencyComboBox.Items.Add("€");
            currencyComboBox.Items.Add("$");

            systemComboBox.Items.Add("kg");
            systemComboBox.Items.Add("lbs");

            monitorTypeComboBox.Items.Add("LCD");
            monitorTypeComboBox.Items.Add("LED");
            monitorTypeComboBox.Items.Add("CRT");
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
                monitorPictureBox.ImageLocation = imgLocation;
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nameTextBox.Text) || string.IsNullOrWhiteSpace(codeTextBox.Text) ||
                string.IsNullOrWhiteSpace(manufacturerTextBox.Text) || string.IsNullOrWhiteSpace(priceTextBox.Text) ||
                string.IsNullOrWhiteSpace(weightTextBox.Text) || string.IsNullOrWhiteSpace(resolutionTextBox1.Text) ||
                string.IsNullOrWhiteSpace(resolutionTextBox2.Text) || string.IsNullOrWhiteSpace(aspectRatioTextBox1.Text) ||
                string.IsNullOrWhiteSpace(aspectRatioTextBox2.Text) || string.IsNullOrWhiteSpace(monitorTypeComboBox.Text))
            {
                MessageBox.Show("You forgot to fill in all the boxes!");
            }

            else
            {
                string Price = priceTextBox.Text + currencyComboBox.SelectedItem.ToString();
                string Weight = weightTextBox.Text + systemComboBox.SelectedItem.ToString();
                string Resolution = resolutionTextBox1.Text + "x" + resolutionTextBox2.Text;
                string AspectRatio = aspectRatioTextBox1.Text + ":" + aspectRatioTextBox2.Text;
                string MonitorType = monitorTypeComboBox.SelectedItem.ToString();
                string Image = monitorPictureBox.ImageLocation.ToString();

                Monitor newMonitor = new Monitor(nameTextBox.Text, codeTextBox.Text, manufacturerTextBox.Text, Price, Image, Weight,
                    Resolution, AspectRatio, MonitorType);

                newMonitor.addToDatabase();

                int error = newMonitor.addToDatabase();

                if (error == 1)
                {
                    MessageBox.Show("This object already exists within the database");
                }

                if (error == 0)
                {
                    MessageBox.Show("You succesfully added a new Monitor!");
                }

            }
        }
    }
}
