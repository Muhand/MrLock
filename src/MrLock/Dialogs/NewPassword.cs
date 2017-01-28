////////////////////////////////////////////
//This window is designed to only run if the
//Program is starting for the first time.
//In order to setup an administrator password.
////////////////////////////////////////////
using System;
using System.Windows.Forms;

namespace MrLock.Dialogs
{
    public partial class NewPassword : Form
    {
        public NewPassword()
        {
            InitializeComponent();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            //Exit the application
            Environment.Exit(0);
        }

        /// <summary>
        /// Save the created password
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_save_Click(object sender, EventArgs e)
        {
            //Check if passwords match
            if (password.Text == confirmPassword.Text)
            {
                //If they match then encrypt the password with bcrypt
                string hashedpw = BCrypt.Net.BCrypt.HashPassword(password.Text);

                //Save the hashed password
                Properties.Settings.Default.password = hashedpw;
                Properties.Settings.Default.Save();

                //Close this dialog
                this.Close();
            }
            else
            {
                //If passwords don't match then show an error message
                MessageBox.Show("The passwords you entered do not match.","Passwords don't match", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}
