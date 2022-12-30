namespace NoSleep
{
    partial class MainWindow
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.cbActivate = new System.Windows.Forms.CheckBox();
            this.cbDisplay = new System.Windows.Forms.CheckBox();
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.trayContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.keepPowerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.keepDisplayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cbJiggle = new System.Windows.Forms.CheckBox();
            this.jiggleTimer = new System.Windows.Forms.Timer(this.components);
            this.jiggleMouseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trayContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbActivate
            // 
            this.cbActivate.AutoSize = true;
            this.cbActivate.Location = new System.Drawing.Point(12, 12);
            this.cbActivate.Name = "cbActivate";
            this.cbActivate.Size = new System.Drawing.Size(83, 17);
            this.cbActivate.TabIndex = 0;
            this.cbActivate.Text = "Keep &power";
            this.cbActivate.UseVisualStyleBackColor = true;
            this.cbActivate.CheckedChanged += new System.EventHandler(this.cbActivate_CheckedChanged);
            // 
            // cbDisplay
            // 
            this.cbDisplay.AutoSize = true;
            this.cbDisplay.Location = new System.Drawing.Point(131, 12);
            this.cbDisplay.Name = "cbDisplay";
            this.cbDisplay.Size = new System.Drawing.Size(79, 17);
            this.cbDisplay.TabIndex = 1;
            this.cbDisplay.Text = "&Display ON";
            this.cbDisplay.UseVisualStyleBackColor = true;
            this.cbDisplay.CheckedChanged += new System.EventHandler(this.cbDisplay_CheckedChanged);
            // 
            // trayIcon
            // 
            this.trayIcon.ContextMenuStrip = this.trayContextMenu;
            this.trayIcon.Text = "NoSleep";
            this.trayIcon.Visible = true;
            this.trayIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.trayIcon_MouseClick);
            // 
            // trayContextMenu
            // 
            this.trayContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.keepPowerToolStripMenuItem,
            this.keepDisplayToolStripMenuItem,
            this.jiggleMouseToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.trayContextMenu.Name = "trayContextMenu";
            this.trayContextMenu.Size = new System.Drawing.Size(181, 114);
            // 
            // keepPowerToolStripMenuItem
            // 
            this.keepPowerToolStripMenuItem.Name = "keepPowerToolStripMenuItem";
            this.keepPowerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.keepPowerToolStripMenuItem.Text = "Keep Power";
            this.keepPowerToolStripMenuItem.Click += new System.EventHandler(this.keepPowerToolStripMenuItem_Click);
            // 
            // keepDisplayToolStripMenuItem
            // 
            this.keepDisplayToolStripMenuItem.Name = "keepDisplayToolStripMenuItem";
            this.keepDisplayToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.keepDisplayToolStripMenuItem.Text = "Keep Display";
            this.keepDisplayToolStripMenuItem.Click += new System.EventHandler(this.keepDisplayToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // cbJiggle
            // 
            this.cbJiggle.AutoSize = true;
            this.cbJiggle.Location = new System.Drawing.Point(13, 49);
            this.cbJiggle.Name = "cbJiggle";
            this.cbJiggle.Size = new System.Drawing.Size(88, 17);
            this.cbJiggle.TabIndex = 2;
            this.cbJiggle.Text = "&Jiggle Mouse";
            this.cbJiggle.UseVisualStyleBackColor = true;
            this.cbJiggle.CheckedChanged += new System.EventHandler(this.cbJiggle_CheckedChanged);
            // 
            // jiggleTimer
            // 
            this.jiggleTimer.Interval = 5000;
            this.jiggleTimer.Tick += new System.EventHandler(this.jiggleTimer_Tick);
            // 
            // jiggleMouseToolStripMenuItem
            // 
            this.jiggleMouseToolStripMenuItem.Name = "jiggleMouseToolStripMenuItem";
            this.jiggleMouseToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.jiggleMouseToolStripMenuItem.Text = "Jiggle Mouse";
            this.jiggleMouseToolStripMenuItem.Click += new System.EventHandler(this.jiggleMouseToolStripMenuItem_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(222, 78);
            this.Controls.Add(this.cbJiggle);
            this.Controls.Add(this.cbDisplay);
            this.Controls.Add(this.cbActivate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.Text = "NoSleep";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainWindow_KeyDown);
            this.Resize += new System.EventHandler(this.MainWindow_Resize);
            this.trayContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbActivate;
        private System.Windows.Forms.CheckBox cbDisplay;
        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.ContextMenuStrip trayContextMenu;
		private System.Windows.Forms.ToolStripMenuItem keepPowerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem keepDisplayToolStripMenuItem;
		private System.Windows.Forms.CheckBox cbJiggle;
		private System.Windows.Forms.Timer jiggleTimer;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jiggleMouseToolStripMenuItem;
    }
}

