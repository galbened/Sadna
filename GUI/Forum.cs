using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class Forum : Form
    {
        public Forum(string forumName)
        {
            InitializeComponent();
            HeadlineLabel.Text = "Forum/"+forumName;
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            new Login().Show();
        }

        private void signUpButton_Click(object sender, EventArgs e)
        {
            new SignUp().Show();
        }
    }
}
