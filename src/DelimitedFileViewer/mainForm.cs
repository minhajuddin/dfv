using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using DelimitedFileViewer.Properties;

namespace DelimitedFileViewer
{
    public partial class mainForm : Form
    {

        DelimitedFile df;

        public mainForm()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == mainOpenFileDialog.ShowDialog())
            {
                df = new DelimitedFile(mainOpenFileDialog.FileName, delimiterToolStripTextBox.Text, Encoding.Default, false);
                
                FileStatus fs;
                fs = df.Validate();


                if (fs == FileStatus.Valid)
                {
                    mainDataGridView.DataSource = df.ReadFile();
                }
                else
                {
                    MessageBox.Show(fs.ToString());
                }
            }
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.ToggleHeader = toggleHeaderButton.Checked;
            Settings.Default.Encoding = encodingToolStripComboBox.Text;
            Settings.Default.Delimiter = delimiterToolStripTextBox.Text;
            Settings.Default.Size = this.Size;
            Settings.Default.Save();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            this.Size = Settings.Default.Size;
            this.delimiterToolStripTextBox.Text = Settings.Default.Delimiter;
            this.toggleHeaderButton.Checked = Settings.Default.ToggleHeader;
            this.encodingToolStripComboBox.Text = Settings.Default.Encoding;
        }


    }
}
