using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MrLock.Dialogs;

namespace MrLock
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            //Properties.Settings.Default.Reset();                                            //Reset settings; THIS IS JUST FOR DEBUGING

            //Check if there is no password (indicates program was launched for the first time)
            if (Properties.Settings.Default.password == "")
            {
                //If so then show a dialog to create a new password
                Dialogs.NewPassword newpw = new Dialogs.NewPassword();
                newpw.ShowDialog();
            }
            else
            {
                //Otherwise just confirm the login before any changes takes place
                Dialogs.ConfirmPassword confirmPw = new ConfirmPassword();
                confirmPw.ShowDialog();
            }
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dialogs.UpdatePassword updatePw = new UpdatePassword();
            updatePw.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
