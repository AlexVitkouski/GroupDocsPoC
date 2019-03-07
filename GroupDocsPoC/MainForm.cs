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
            const string pathToFolder = Common.PathTSrcFolder;
            var testRunner = new TestRunner(pathToFolder, pictureBox1);
            testRunner.Run();
            MessageBox.Show("Done.");
        }
    }
}
