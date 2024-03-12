using NoSleep.Properties;
using System;
using System.Windows.Forms;

namespace NoSleep
{
    internal partial class MainWindow : Form
    {
        private Settings Settings => Settings.Default;

        private readonly PowerManager powerManager = new PowerManager();

        private readonly MouseJiggler mouseJiggler = new MouseJiggler();

        public MainWindow()
        {
            InitializeComponent();
            trayIcon.Icon = Icon;

            LoadSettings();

            FormClosed += delegate
            {
                SaveSettings();
                powerManager.Dispose();
            };
        }

        private void SaveSettings()
        {
            Settings.KeepPowerEnabled = cbActivate.Checked;
            Settings.KeepDisplayEnabled = cbDisplay.Checked;
            Settings.JiggleMouseEnabled = cbJiggle.Checked;
            Settings.Save();
        }

        private void LoadSettings()
        {
            SetKeepPower(Settings.KeepPowerEnabled);
            SetKeepDisplay(Settings.KeepDisplayEnabled);
            SetJiggleEnabled(Settings.JiggleMouseEnabled);
        }

        private void SetKeepPower(bool keep)
        {
            keepPowerToolStripMenuItem.Checked = keep;
            cbActivate.Checked = keep;
            powerManager.EnableConstantPower(keep);
        }

        private void SetKeepDisplay(bool keep)
        {
            keepDisplayToolStripMenuItem.Checked = keep;
            cbDisplay.Checked = keep;
            powerManager.EnableConstantDisplay(keep);
        }

        private void SetJiggleEnabled(bool enabled)
        {
            cbJiggle.Checked = enabled;
            jiggleMouseToolStripMenuItem.Checked = enabled;
            jiggleTimer.Enabled = enabled;
        }

        private void cbActivate_CheckedChanged(object sender, EventArgs e)
        {
            SetKeepPower(cbActivate.Checked);
        }

        private void cbDisplay_CheckedChanged(object sender, EventArgs e)
        {
            SetKeepDisplay(cbDisplay.Checked);
        }

        private void cbJiggle_CheckedChanged(object sender, EventArgs e)
        {
            SetJiggleEnabled(cbJiggle.Checked);
        }

        private void keepPowerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetKeepPower(!keepPowerToolStripMenuItem.Checked);
        }

        private void keepDisplayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetKeepDisplay(!keepDisplayToolStripMenuItem.Checked);
        }

        private void jiggleMouseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetJiggleEnabled(!jiggleMouseToolStripMenuItem.Checked);
        }

        private void MainWindow_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
            }
        }

        private void jiggleTimer_Tick(object sender, EventArgs e)
        {
            mouseJiggler.Jiggle();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void trayIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (WindowState == FormWindowState.Minimized)
                {
                    Show();
                    WindowState = FormWindowState.Normal;
                }
                else
                {
                    WindowState = FormWindowState.Minimized;
                    Hide();
                }
            }
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                WindowState = FormWindowState.Minimized;
            }
        }
    }
}
