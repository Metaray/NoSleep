using System;
using System.Drawing;
using System.Windows.Forms;

namespace NoSleep
{
    internal partial class MainWindow : Form
    {
        private PowerManager powerManager = new PowerManager();

        private int jiggleTick = 0;

        public MainWindow()
        {
            InitializeComponent();
            trayIcon.Icon = Icon;
            FormClosed += delegate { powerManager.Dispose(); };
        }

        private void cbActivate_CheckedChanged(object sender, EventArgs e)
        {
            powerManager.EnableConstantPower(cbActivate.Checked);
            keepPowerToolStripMenuItem.Checked = cbActivate.Checked;
		}

        private void cbDisplay_CheckedChanged(object sender, EventArgs e)
        {
            powerManager.EnableConstantDisplay(cbDisplay.Checked);
			keepDisplayToolStripMenuItem.Checked = cbDisplay.Checked;
		}

		private void cbJiggle_CheckedChanged(object sender, EventArgs e)
		{
            jiggleTimer.Enabled = cbJiggle.Checked;
		}

		private void keepPowerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var enable = !keepPowerToolStripMenuItem.Checked;
            keepPowerToolStripMenuItem.Checked = enable;
            cbActivate.Checked = enable;
		}

        private void keepDisplayToolStripMenuItem_Click(object sender, EventArgs e)
        {
			var enable = !keepDisplayToolStripMenuItem.Checked;
			keepDisplayToolStripMenuItem.Checked = enable;
			cbDisplay.Checked = enable;
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
            const int Distance = 3;
			int delta = ((jiggleTick++) & 1) == 0 ? Distance : -Distance;
			
            //var pos = Cursor.Position;
            //pos.Y += delta;
            //Cursor.Position = pos;

            MouseJiggler.MoveDelta(delta, delta);
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
    }
}
