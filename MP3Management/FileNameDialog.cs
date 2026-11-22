using System.Windows.Forms;

namespace MP3Management
{
    public partial class FileNameDialog : Form
    {
        private string _fileName;

        public string FileName
        {
            get { return _fileName; }
        }

        public FileNameDialog()
        {
            InitializeComponent();
        }

        public FileNameDialog(string defaultFileName)
        {
            InitializeComponent();
            txtFileName.Text = defaultFileName;
            _fileName = defaultFileName;
        }

        private void cmdOk_Click(object sender, System.EventArgs e)
        {
            _fileName = txtFileName.Text;
            Close();
        }
    }
}
