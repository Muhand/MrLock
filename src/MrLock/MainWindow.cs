using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MrLock.Classes;
using MrLock.Dialogs;

namespace MrLock
{
    public partial class MainWindow : Form
    {
        //Global Variables
        private Classes.enums.ApplicationState _applicationState = enums.ApplicationState.Shown;
        private Classes.enums.ApplicationStatus _applicationStatus = enums.ApplicationStatus.NotRunning;

        private Classes.enums.ApplicationState applicationState
        {
            get { return _applicationState; }
            set
            {
                _applicationState = value;
            }
        }

        private Classes.enums.ApplicationStatus applicationStatus
        {
            get { return _applicationStatus;}
            set
            {
                _applicationStatus = value;
                switch (value)
                {
                    case enums.ApplicationStatus.NotRunning:
                        toolStripStatusLabel1.Text = "Not Running";
                        break;
                    case enums.ApplicationStatus.Running:
                        toolStripStatusLabel1.Text = "Running";
                        break;
                }
            }
        }

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

                if (confirmPw.pwConfirmed == false)
                    disableApplication();
            }

            notifyIcon1.Icon = SystemIcons.Application;
            notifyIcon1.BalloonTipTitle = "MrLock is giving you example of notification.";
            notifyIcon1.BalloonTipText = @"Here you will see notifications when you have 30 minutes 
left until the computer gets automatically locked.";
            notifyIcon1.ShowBalloonTip(1000);
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
        private void disableApplication()
        {
            foreach (Control ctr in this.Controls)
            {
                if (ctr is GroupBox)
                {
                    GroupBox groupbox = (GroupBox) ctr;
                    foreach (Control control in groupbox.Controls)
                    {
                        control.Enabled = false;
                    }
                }
                else if (ctr is MenuStrip)
                {
                    preferencesToolStripMenuItem.Enabled = false;
                    lockApplicationToolStripMenuItem.Enabled = false;
                    enableApplicationToolStripMenuItem.Enabled = true;
                }
                else if (!(ctr is StatusBar))
                    ctr.Enabled = false;
            }
        }

        private void enableApplication()
        {
            foreach (Control ctr in this.Controls)
            {
                if (ctr is GroupBox)
                {
                    GroupBox groupbox = (GroupBox)ctr;
                    foreach (Control control in groupbox.Controls)
                    {
                        control.Enabled = true;
                    }
                }
                else if (ctr is MenuStrip)
                {
                    preferencesToolStripMenuItem.Enabled = true;
                    lockApplicationToolStripMenuItem.Enabled = true;
                    enableApplicationToolStripMenuItem.Enabled = false;
                }
                else if (!(ctr is StatusBar))
                    ctr.Enabled = true;
            }
        }

        private void enableApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Confirm if the password is correct or no
            Dialogs.ConfirmPassword confirmPw = new ConfirmPassword();
            confirmPw.ShowDialog();

            if (confirmPw.pwConfirmed == true)
                enableApplication();
        }

        private void lockApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Confirm if the password is correct or no
            Dialogs.ConfirmPassword confirmPw = new ConfirmPassword();
            confirmPw.ShowDialog();

            if (confirmPw.pwConfirmed == true)
            {
                disableApplication();
                this.Hide();
                applicationState = enums.ApplicationState.Hidden;
                notifyIcon1.BalloonTipTitle = "MrLock Says...";
                notifyIcon1.BalloonTipText = @"The application is now locked and started in the background, no one will be able to stop/exit it unless the password is entered.";
                notifyIcon1.ShowBalloonTip(1000);
            }
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Confirm if the password is correct or no
            Dialogs.ConfirmPassword confirmPw = new ConfirmPassword();
            confirmPw.ShowDialog();

            if (confirmPw.pwConfirmed == true)
            {
                notifyIcon1.Visible = false;
                notifyIcon1.Dispose();
                Environment.Exit(0);
            }
            else
                e.Cancel = true;
        }

        private void showHideApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (applicationState)
            {
                case enums.ApplicationState.Shown:
                    this.Hide();
                    applicationState = enums.ApplicationState.Hidden;
                    break;
                case enums.ApplicationState.Hidden:
                    //Confirm if the password is correct or no
                    Dialogs.ConfirmPassword confirmPw = new ConfirmPassword();
                    confirmPw.ShowDialog();

                    if (confirmPw.pwConfirmed == true)
                        this.Show();

                    applicationState = enums.ApplicationState.Shown;
                    break;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            applicationStatus = enums.ApplicationStatus.Running;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lockApplicationToolStripMenuItem.PerformClick();
        }

        ///// <summary>
        ///// Hide the application from being shown in taskmanager to avoid forcing it to close
        ///// </summary>
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        var cp = base.CreateParams;
        //        cp.ExStyle |= 0x80;  // Turn on WS_EX_TOOLWINDOW
        //        return cp;
        //    }
        //}
    }
}