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
    public partial class UpdatePassword : Form
    {
        public UpdatePassword()
        {
            InitializeComponent();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            //Check if the entered current password is correct or no
            bool validPassword = BCrypt.Net.BCrypt.Verify(currentPassword.Text, Properties.Settings.Default.password);

            //If it's valid then proceed to check if the new password and it's confirmation match
            if (validPassword)
            {
                //Check if the new password and it's confirmation match
                if (newPassword.Text == confirmPassword.Text)
                {
                    //If so then hash it, store it and close out
                    string hashedpw = BCrypt.Net.BCrypt.HashPassword(newPassword.Text);

                    //Save the hashed password
                    Properties.Settings.Default.password = hashedpw;
                    Properties.Settings.Default.Save();

                    //Show message notifying changes were successful
                    MessageBox.Show("Congratulations, Your password have been updated successfully.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //Close this dialog
                    this.Close();
                }
                //If passwords don't match then show an error message
                else
                { 
                    MessageBox.Show("The new password and it's confirmation don't match.", 
                        "Passwords don't match", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //If it's not the valid then show error message
            else
            {
                MessageBox.Show("Looks like the current password you entered doesn't match the correct password.",
                   "Invalid password", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
