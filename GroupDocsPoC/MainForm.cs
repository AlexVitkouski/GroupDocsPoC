using System;
using System.Windows.Forms;

namespace GroupDocsPoC
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            button.Text = "Testing...";
            var testRunner = new TestRunner(Common.PathTSrcFolder, pictureBox1);
            testRunner.Run();
            MessageBox.Show("Done.");
            button.Text = "Start";
        }
    }
}
