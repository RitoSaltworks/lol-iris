namespace Iris
{
    partial class frmSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            this.picIrisLogo = new System.Windows.Forms.PictureBox();
            this.btnReloadOverlay = new System.Windows.Forms.Button();
            this.tabSummonerInfo = new System.Windows.Forms.TabPage();
            this.btnSetSummoner = new System.Windows.Forms.Button();
            this.comboRegion = new System.Windows.Forms.ComboBox();
            this.labelRegion = new System.Windows.Forms.Label();
            this.labelSummonerName = new System.Windows.Forms.Label();
            this.txtSummonerName = new System.Windows.Forms.TextBox();
            this.radialMenuTab = new System.Windows.Forms.TabPage();
            this.picHotkeySeperator = new System.Windows.Forms.PictureBox();
            this.lblPingNote = new System.Windows.Forms.Label();
            this.lblHotkeyNote = new System.Windows.Forms.Label();
            this.btnSetAlertPing = new System.Windows.Forms.Button();
            this.lblAlertPing = new System.Windows.Forms.Label();
            this.hotkeyLabel = new System.Windows.Forms.Label();
            this.btnSetHotkey = new System.Windows.Forms.Button();
            this.settingsTabs = new System.Windows.Forms.TabControl();
            this.chkCloseOutside = new System.Windows.Forms.CheckBox();
            this.txtStaticX = new System.Windows.Forms.TextBox();
            this.txtStaticY = new System.Windows.Forms.TextBox();
            this.lblStaticX = new System.Windows.Forms.Label();
            this.lblStaticY = new System.Windows.Forms.Label();
            this.chkStaticLocation = new System.Windows.Forms.CheckBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picIrisLogo)).BeginInit();
            this.tabSummonerInfo.SuspendLayout();
            this.radialMenuTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHotkeySeperator)).BeginInit();
            this.settingsTabs.SuspendLayout();
            this.SuspendLayout();
            // 
            // picIrisLogo
            // 
            this.picIrisLogo.Image = global::Iris.Properties.Resources.logo;
            this.picIrisLogo.Location = new System.Drawing.Point(1, 2);
            this.picIrisLogo.Name = "picIrisLogo";
            this.picIrisLogo.Size = new System.Drawing.Size(381, 113);
            this.picIrisLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picIrisLogo.TabIndex = 0;
            this.picIrisLogo.TabStop = false;
            this.picIrisLogo.Click += new System.EventHandler(this.picIrisLogo_Click);
            // 
            // btnReloadOverlay
            // 
            this.btnReloadOverlay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReloadOverlay.Location = new System.Drawing.Point(1, 425);
            this.btnReloadOverlay.Name = "btnReloadOverlay";
            this.btnReloadOverlay.Size = new System.Drawing.Size(381, 52);
            this.btnReloadOverlay.TabIndex = 2;
            this.btnReloadOverlay.Text = "Reload Overlay";
            this.btnReloadOverlay.UseVisualStyleBackColor = true;
            this.btnReloadOverlay.Click += new System.EventHandler(this.btnReloadOverlay_Click);
            // 
            // tabSummonerInfo
            // 
            this.tabSummonerInfo.BackColor = System.Drawing.Color.Orange;
            this.tabSummonerInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tabSummonerInfo.Controls.Add(this.btnSetSummoner);
            this.tabSummonerInfo.Controls.Add(this.comboRegion);
            this.tabSummonerInfo.Controls.Add(this.labelRegion);
            this.tabSummonerInfo.Controls.Add(this.labelSummonerName);
            this.tabSummonerInfo.Controls.Add(this.txtSummonerName);
            this.tabSummonerInfo.Location = new System.Drawing.Point(4, 29);
            this.tabSummonerInfo.Name = "tabSummonerInfo";
            this.tabSummonerInfo.Size = new System.Drawing.Size(373, 168);
            this.tabSummonerInfo.TabIndex = 1;
            this.tabSummonerInfo.Text = "Summoner Info";
            this.tabSummonerInfo.Click += new System.EventHandler(this.tabSummonerInfo_Click);
            // 
            // btnSetSummoner
            // 
            this.btnSetSummoner.Location = new System.Drawing.Point(158, 84);
            this.btnSetSummoner.Name = "btnSetSummoner";
            this.btnSetSummoner.Size = new System.Drawing.Size(208, 34);
            this.btnSetSummoner.TabIndex = 4;
            this.btnSetSummoner.Text = "Set Summoner";
            this.btnSetSummoner.UseVisualStyleBackColor = true;
            this.btnSetSummoner.Click += new System.EventHandler(this.btnSetSummoner_Click);
            // 
            // comboRegion
            // 
            this.comboRegion.AllowDrop = true;
            this.comboRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboRegion.FormattingEnabled = true;
            this.comboRegion.Items.AddRange(new object[] {
            "BR",
            "EUNE",
            "EUW",
            "LAN",
            "LAS",
            "NA",
            "OCE",
            "KR",
            "TR"});
            this.comboRegion.Location = new System.Drawing.Point(158, 50);
            this.comboRegion.Name = "comboRegion";
            this.comboRegion.Size = new System.Drawing.Size(208, 28);
            this.comboRegion.TabIndex = 3;
            this.comboRegion.SelectedIndexChanged += new System.EventHandler(this.comboRegion_SelectedIndexChanged);
            // 
            // labelRegion
            // 
            this.labelRegion.AutoSize = true;
            this.labelRegion.Location = new System.Drawing.Point(7, 48);
            this.labelRegion.Name = "labelRegion";
            this.labelRegion.Size = new System.Drawing.Size(69, 20);
            this.labelRegion.TabIndex = 2;
            this.labelRegion.Text = "Region:";
            // 
            // labelSummonerName
            // 
            this.labelSummonerName.AutoSize = true;
            this.labelSummonerName.Location = new System.Drawing.Point(6, 17);
            this.labelSummonerName.Name = "labelSummonerName";
            this.labelSummonerName.Size = new System.Drawing.Size(151, 20);
            this.labelSummonerName.TabIndex = 1;
            this.labelSummonerName.Text = "Summoner Name:";
            this.labelSummonerName.Click += new System.EventHandler(this.labelSummonerName_Click);
            // 
            // txtSummonerName
            // 
            this.txtSummonerName.Location = new System.Drawing.Point(158, 12);
            this.txtSummonerName.Name = "txtSummonerName";
            this.txtSummonerName.Size = new System.Drawing.Size(208, 32);
            this.txtSummonerName.TabIndex = 0;
            this.txtSummonerName.TextChanged += new System.EventHandler(this.txtSummonerName_TextChanged);
            // 
            // radialMenuTab
            // 
            this.radialMenuTab.BackColor = System.Drawing.Color.Orange;
            this.radialMenuTab.Controls.Add(this.picHotkeySeperator);
            this.radialMenuTab.Controls.Add(this.lblPingNote);
            this.radialMenuTab.Controls.Add(this.lblHotkeyNote);
            this.radialMenuTab.Controls.Add(this.btnSetAlertPing);
            this.radialMenuTab.Controls.Add(this.lblAlertPing);
            this.radialMenuTab.Controls.Add(this.hotkeyLabel);
            this.radialMenuTab.Controls.Add(this.btnSetHotkey);
            this.radialMenuTab.Location = new System.Drawing.Point(4, 29);
            this.radialMenuTab.Name = "radialMenuTab";
            this.radialMenuTab.Size = new System.Drawing.Size(373, 168);
            this.radialMenuTab.TabIndex = 0;
            this.radialMenuTab.Text = "Radial Menu";
            this.radialMenuTab.Click += new System.EventHandler(this.radialMenuTab_Click);
            // 
            // picHotkeySeperator
            // 
            this.picHotkeySeperator.BackColor = System.Drawing.Color.Black;
            this.picHotkeySeperator.Location = new System.Drawing.Point(3, 83);
            this.picHotkeySeperator.Name = "picHotkeySeperator";
            this.picHotkeySeperator.Size = new System.Drawing.Size(368, 6);
            this.picHotkeySeperator.TabIndex = 6;
            this.picHotkeySeperator.TabStop = false;
            this.picHotkeySeperator.Click += new System.EventHandler(this.picHotkeySeperator_Click);
            // 
            // lblPingNote
            // 
            this.lblPingNote.BackColor = System.Drawing.Color.Transparent;
            this.lblPingNote.Location = new System.Drawing.Point(226, 96);
            this.lblPingNote.Name = "lblPingNote";
            this.lblPingNote.Size = new System.Drawing.Size(144, 66);
            this.lblPingNote.TabIndex = 5;
            this.lblPingNote.Text = "Set this to your in-game Quick Alert Ping hotkey";
            this.lblPingNote.Click += new System.EventHandler(this.lblPingNote_Click);
            // 
            // lblHotkeyNote
            // 
            this.lblHotkeyNote.BackColor = System.Drawing.Color.Transparent;
            this.lblHotkeyNote.Location = new System.Drawing.Point(229, 32);
            this.lblHotkeyNote.Name = "lblHotkeyNote";
            this.lblHotkeyNote.Size = new System.Drawing.Size(144, 61);
            this.lblHotkeyNote.TabIndex = 4;
            this.lblHotkeyNote.Text = "Opens the radial context menu";
            this.lblHotkeyNote.Click += new System.EventHandler(this.lblHotkeyNote_Click);
            // 
            // btnSetAlertPing
            // 
            this.btnSetAlertPing.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSetAlertPing.Location = new System.Drawing.Point(11, 126);
            this.btnSetAlertPing.Name = "btnSetAlertPing";
            this.btnSetAlertPing.Size = new System.Drawing.Size(198, 32);
            this.btnSetAlertPing.TabIndex = 3;
            this.btnSetAlertPing.Text = "Click to Set Hotkey";
            this.btnSetAlertPing.UseVisualStyleBackColor = true;
            this.btnSetAlertPing.Click += new System.EventHandler(this.btnSetAlertPing_Click);
            // 
            // lblAlertPing
            // 
            this.lblAlertPing.AutoSize = true;
            this.lblAlertPing.Location = new System.Drawing.Point(7, 96);
            this.lblAlertPing.Name = "lblAlertPing";
            this.lblAlertPing.Size = new System.Drawing.Size(162, 20);
            this.lblAlertPing.TabIndex = 2;
            this.lblAlertPing.Text = "Quick Alert Hotkey:";
            // 
            // hotkeyLabel
            // 
            this.hotkeyLabel.AutoSize = true;
            this.hotkeyLabel.Location = new System.Drawing.Point(7, 12);
            this.hotkeyLabel.Name = "hotkeyLabel";
            this.hotkeyLabel.Size = new System.Drawing.Size(69, 20);
            this.hotkeyLabel.TabIndex = 1;
            this.hotkeyLabel.Text = "Hotkey:";
            this.hotkeyLabel.Click += new System.EventHandler(this.hotkeyLabel_Click);
            // 
            // btnSetHotkey
            // 
            this.btnSetHotkey.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSetHotkey.Location = new System.Drawing.Point(7, 41);
            this.btnSetHotkey.Name = "btnSetHotkey";
            this.btnSetHotkey.Size = new System.Drawing.Size(198, 32);
            this.btnSetHotkey.TabIndex = 0;
            this.btnSetHotkey.Text = "Click to Set Hotkey";
            this.btnSetHotkey.UseVisualStyleBackColor = true;
            this.btnSetHotkey.Click += new System.EventHandler(this.btnSetHotkey_Click);
            // 
            // settingsTabs
            // 
            this.settingsTabs.Controls.Add(this.radialMenuTab);
            this.settingsTabs.Controls.Add(this.tabSummonerInfo);
            this.settingsTabs.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.settingsTabs.Location = new System.Drawing.Point(1, 121);
            this.settingsTabs.Name = "settingsTabs";
            this.settingsTabs.SelectedIndex = 0;
            this.settingsTabs.Size = new System.Drawing.Size(381, 201);
            this.settingsTabs.TabIndex = 1;
            // 
            // chkCloseOutside
            // 
            this.chkCloseOutside.AutoSize = true;
            this.chkCloseOutside.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCloseOutside.Location = new System.Drawing.Point(6, 322);
            this.chkCloseOutside.Name = "chkCloseOutside";
            this.chkCloseOutside.Size = new System.Drawing.Size(379, 24);
            this.chkCloseOutside.TabIndex = 3;
            this.chkCloseOutside.Text = "Close radial menu when hotkey is pressed outside";
            this.chkCloseOutside.UseVisualStyleBackColor = true;
            this.chkCloseOutside.CheckedChanged += new System.EventHandler(this.chkCloseOutside_CheckedChanged);
            // 
            // txtStaticX
            // 
            this.txtStaticX.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStaticX.Location = new System.Drawing.Point(36, 382);
            this.txtStaticX.Name = "txtStaticX";
            this.txtStaticX.Size = new System.Drawing.Size(153, 26);
            this.txtStaticX.TabIndex = 5;
            this.txtStaticX.TextChanged += new System.EventHandler(this.txtStaticX_TextChanged);
            // 
            // txtStaticY
            // 
            this.txtStaticY.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStaticY.Location = new System.Drawing.Point(224, 382);
            this.txtStaticY.Name = "txtStaticY";
            this.txtStaticY.Size = new System.Drawing.Size(154, 26);
            this.txtStaticY.TabIndex = 6;
            this.txtStaticY.TextChanged += new System.EventHandler(this.txtStaticY_TextChanged);
            // 
            // lblStaticX
            // 
            this.lblStaticX.AutoSize = true;
            this.lblStaticX.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStaticX.Location = new System.Drawing.Point(6, 384);
            this.lblStaticX.Name = "lblStaticX";
            this.lblStaticX.Size = new System.Drawing.Size(24, 20);
            this.lblStaticX.TabIndex = 7;
            this.lblStaticX.Text = "X:";
            // 
            // lblStaticY
            // 
            this.lblStaticY.AutoSize = true;
            this.lblStaticY.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStaticY.Location = new System.Drawing.Point(194, 384);
            this.lblStaticY.Name = "lblStaticY";
            this.lblStaticY.Size = new System.Drawing.Size(24, 20);
            this.lblStaticY.TabIndex = 8;
            this.lblStaticY.Text = "Y:";
            // 
            // chkStaticLocation
            // 
            this.chkStaticLocation.AutoSize = true;
            this.chkStaticLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkStaticLocation.Location = new System.Drawing.Point(6, 342);
            this.chkStaticLocation.Name = "chkStaticLocation";
            this.chkStaticLocation.Size = new System.Drawing.Size(312, 24);
            this.chkStaticLocation.TabIndex = 9;
            this.chkStaticLocation.Text = "Open the radial menu at a static location";
            this.chkStaticLocation.UseVisualStyleBackColor = true;
            this.chkStaticLocation.CheckedChanged += new System.EventHandler(this.chkStaticLocation_CheckedChanged_1);
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.BalloonTipText = "LoL Iris is Mnimized";
            this.notifyIcon.BalloonTipTitle = "Restore";
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Notification Icon";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Highlight;
            this.ClientSize = new System.Drawing.Size(383, 489);
            this.Controls.Add(this.chkStaticLocation);
            this.Controls.Add(this.lblStaticY);
            this.Controls.Add(this.lblStaticX);
            this.Controls.Add(this.txtStaticY);
            this.Controls.Add(this.txtStaticX);
            this.Controls.Add(this.chkCloseOutside);
            this.Controls.Add(this.btnReloadOverlay);
            this.Controls.Add(this.settingsTabs);
            this.Controls.Add(this.picIrisLogo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmSettings";
            this.Text = "Iris";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formSettings_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSettings_FormClosed);
            this.Load += new System.EventHandler(this.formSettings_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSettings_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmSettings_KeyPress);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.frmSettings_MouseClick);
            this.Resize += new System.EventHandler(this.frmSettings_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.picIrisLogo)).EndInit();
            this.tabSummonerInfo.ResumeLayout(false);
            this.tabSummonerInfo.PerformLayout();
            this.radialMenuTab.ResumeLayout(false);
            this.radialMenuTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHotkeySeperator)).EndInit();
            this.settingsTabs.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picIrisLogo;
        private System.Windows.Forms.Button btnReloadOverlay;
        private System.Windows.Forms.TabPage tabSummonerInfo;
        private System.Windows.Forms.Button btnSetSummoner;
        private System.Windows.Forms.ComboBox comboRegion;
        private System.Windows.Forms.Label labelRegion;
        private System.Windows.Forms.Label labelSummonerName;
        private System.Windows.Forms.TextBox txtSummonerName;
        private System.Windows.Forms.TabPage radialMenuTab;
        private System.Windows.Forms.PictureBox picHotkeySeperator;
        private System.Windows.Forms.Label lblPingNote;
        private System.Windows.Forms.Label lblHotkeyNote;
        private System.Windows.Forms.Button btnSetAlertPing;
        private System.Windows.Forms.Label lblAlertPing;
        private System.Windows.Forms.Label hotkeyLabel;
        private System.Windows.Forms.Button btnSetHotkey;
        private System.Windows.Forms.TabControl settingsTabs;
        private System.Windows.Forms.CheckBox chkCloseOutside;
        private System.Windows.Forms.TextBox txtStaticX;
        private System.Windows.Forms.TextBox txtStaticY;
        private System.Windows.Forms.Label lblStaticX;
        private System.Windows.Forms.Label lblStaticY;
        private System.Windows.Forms.CheckBox chkStaticLocation;
        private System.Windows.Forms.NotifyIcon notifyIcon;
    }
}

