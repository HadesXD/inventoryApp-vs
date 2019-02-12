using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;


namespace Inventura
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void computerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Add_Computer log = new Add_Computer();
            log.ShowDialog();
            this.Close();
        }

        private void monitorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Add_Monitor log = new Add_Monitor();
            log.ShowDialog();
            this.Close();
        }

        private void programToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Add_Program log = new Add_Program();
            log.ShowDialog();
            this.Close();
        }

        private void tabelaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            All_Computers log = new All_Computers();
            log.ShowDialog();
            this.Close();
        }

        private void tabelaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Hide();
            All_Monitors log = new All_Monitors();
            log.ShowDialog();
            this.Close();
        }

        private void tabelaToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.Hide();
            All_Programs log = new All_Programs();
            log.ShowDialog();
            this.Close();
        }
    }
}
