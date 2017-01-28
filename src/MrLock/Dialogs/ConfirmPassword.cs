using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrLock.Dialogs
{
    public partial class ConfirmPassword : Form
    {
        public ConfirmPassword()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            //Verify the password with what has been hashed
            bool validPassword = BCrypt.Net.BCrypt.Verify(password.Text, Properties.Settings.Default.password);

            //If it's not the valid then show error message
            if (!validPassword)
                MessageBox.Show("Looks like the password you entered doesn't match the correct password.",
                    "Invalid Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                this.Close();                               //Otherwise just close out this window
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
