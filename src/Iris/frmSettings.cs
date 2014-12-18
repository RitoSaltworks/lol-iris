using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Iris;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Web;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using System.IO;

namespace Iris
{
    public partial class frmSettings : Form
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        public static System.Windows.Input.Key WinformsToWPFKey(System.Windows.Forms.Keys inputKey)
        {
            try
            {
                return (System.Windows.Input.Key)Enum.Parse(typeof(System.Windows.Input.Key), inputKey.ToString());
            }
            catch
            {
                return System.Windows.Input.Key.None;
            }
        } 

        private string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }

        const long checkLoLDelay =  50000000;

        private bool bReload;

        public frmSettings()
        {
            InitializeComponent();
        }

        bool bRunning = true;

        private void formSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            config.save();
            bRunning = false;
        }
        
        static void errorHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            MessageBox.Show("Error: " + e.Message + Environment.NewLine + Environment.NewLine + "Iris must now exit. Please report this error along with pertinent information to Rito Saltworks (https://sites.google.com/site/ritosaltworksloliris/ or https://github.com/RitoSaltworks/lol-iris/).");
        }

        private void formSettings_Load(object sender, EventArgs e)
        {

            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(errorHandler);

            bReload = false;
         
            this.Show();

            config.initialize();

            //fill the settings form with the settings info

            txtSummonerName.Text = config.summonerName;

            comboRegion.SelectedItem = config.region;

            string hotkeyString = "Hotkey: ";
            if(config.radialHotkeyAlt == true)
            {
                hotkeyString += "Alt + ";
            }
            if(config.radialHotkeyShift == true)
            {
                hotkeyString += "Shift + ";
            }
            if(config.radialHotkeyAlt == true)
            {
                hotkeyString += "Ctrl + ";
            }

            KeysConverter kc = new KeysConverter();
            hotkeyString += kc.ConvertToString(config.radialHotkey);

            hotkeyLabel.Text = hotkeyString;

            hotkeyString = "Quick Ping Hotkey: ";
            if (config.pingHotkeyAlt == true)
            {
                hotkeyString += "Alt + ";
            }
            if (config.pingHotkeyShift == true)
            {
                hotkeyString += "Shift + ";
            }
            if (config.pingHotkeyAlt == true)
            {
                hotkeyString += "Ctrl + ";
            }

            hotkeyString += kc.ConvertToString(config.pingHotkey);

            lblAlertPing.Text = hotkeyString;

            chkCloseOutside.Checked = config.radialCloseOnOutsideClick;

            chkStaticLocation.Checked = config.radialIsStatic;

            txtStaticX.Text = config.radialStaticXY.X.ToString();
            txtStaticY.Text = config.radialStaticXY.Y.ToString();

            while (bRunning)
            {
                Application.DoEvents();

                IntPtr lolHandle = Win32Helper.getWindowHandle("League of Legends (TM) Client");
                if (lolHandle != (IntPtr)0)
                {
                    //initialize all the things!

                    DX9Overlay.SetParam("use_window", "1"); 
                    DX9Overlay.SetParam("window", "League of Legends (TM) Client");
                    radialMenu.initialize(lolHandle);
                    matchQuery.initialize();

                    do
                    {
                        long checkLoL = Environment.TickCount;

                        Application.DoEvents();

                        if(bReload)
                        {
                            config.initialize();
                            bReload = false;
                            break;
                        }

                        if (matchQuery.dataAcquired == false)
                        {
                            matchQuery.checkMatch();
                        }

                        //if (GetActiveWindowTitle() == "League of Legends (TM) Client")
                        //{
                         radialMenu.loop(lolHandle);
                        //}

                        if(System.DateTime.Today.Ticks > (checkLoL + checkLoLDelay))
                        {
                            checkLoL = Environment.TickCount;
                            lolHandle = Win32Helper.getWindowHandle("League of Legends (TM) Client");
                     
                        }
                    } while (lolHandle != (IntPtr)0 & bRunning);

                    radialMenu.deinitialize();
                    matchQuery.dataAcquired = false;
                }
            }

            Application.Exit();
        }

        private void frmSettings_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void btnSetHotkey_Click(object sender, EventArgs e)
        {
            if (btnSetHotkey.Text == "Click to Cancel")
            {
                btnSetHotkey.Text = "Click to Set Hotkey";
                btnSetAlertPing.Enabled = true;
            }
            else
            {
                btnSetHotkey.Text = "Click to Cancel";
                btnSetAlertPing.Enabled = false;
            }
        }

      private void setHotkey()
        {

        }

        private void frmSettings_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void btnSetAlertPing_Click(object sender, EventArgs e)
        {
            if (btnSetAlertPing.Text == "Click to Cancel")
            {
                btnSetAlertPing.Text = "Click to Set Hotkey";
                btnSetHotkey.Enabled = true;
            }
            else
            {
                btnSetAlertPing.Text = "Click to Cancel";
                btnSetHotkey.Enabled = false;
            }
        }

        private void picHotkeySeperator_Click(object sender, EventArgs e)
        {

        }

        private void lblHotkeyNote_Click(object sender, EventArgs e)
        {

        }

        private void tabSummonerInfo_Click(object sender, EventArgs e)
        {

        }

        private void btnSetSummoner_Click(object sender, EventArgs e)
        {
            config.summonerName = txtSummonerName.Text;
            config.region = comboRegion.SelectedItem.ToString();
        }

        private void txtAPIKey_TextChanged(object sender, EventArgs e)
        {

        }

        private void labelSummonerName_Click(object sender, EventArgs e)
        {

        }

        private void comboRegion_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtSummonerName_TextChanged(object sender, EventArgs e)
        {

        }

        private void picIrisLogo_Click(object sender, EventArgs e)
        {

        }

        private void hotkeyLabel_Click(object sender, EventArgs e)
        {

        }

        private void radialMenuTab_Click(object sender, EventArgs e)
        {
        }

        private void btnReloadOverlay_Click(object sender, EventArgs e)
        {
            bReload = true;
            
        }

        private void lblPingNote_Click(object sender, EventArgs e)
        {

        }

        private void chkCloseOutside_CheckedChanged(object sender, EventArgs e)
        {
            config.radialCloseOnOutsideClick = chkCloseOutside.Checked;
            config.save();
        }

        private void chkStaticLocation_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkStaticLocation_CheckedChanged_1(object sender, EventArgs e)
        {
            config.radialIsStatic = chkStaticLocation.Checked;
            config.save();
        }

        private void txtStaticX_TextChanged(object sender, EventArgs e)
        {
            config.radialStaticXY.X = Convert.ToInt32(txtStaticX.Text.ToString());
            config.save();
        }

        private void txtStaticY_TextChanged(object sender, EventArgs e)
        {
            config.radialStaticXY.Y = Convert.ToInt32(txtStaticY.Text.ToString());
            config.save();
        }

        private void frmSettings_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
               notifyIcon.BalloonTipText = "LoL Iris";
               notifyIcon.Visible = true;
               notifyIcon.ShowBalloonTip(500);
               this.Hide();
            }

            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon.Visible = false;
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.Activate();
            this.BringToFront();
        }

        private void frmSettings_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void frmSettings_KeyDown(object sender, KeyEventArgs e)
        {
            if (!(e.KeyCode == Keys.Shift || e.KeyCode == Keys.LShiftKey || e.KeyCode == Keys.RShiftKey || e.KeyCode == Keys.ShiftKey ||
                e.KeyCode == Keys.Alt || e.KeyCode == Keys.Control || e.KeyCode == Keys.RControlKey || e.KeyCode == Keys.LControlKey || e.KeyCode == Keys.ControlKey))
            {

                if (btnSetHotkey.Text == "Click to Cancel" || btnSetAlertPing.Text == "Click to Cancel")
                {
                    string hotkeyString;

                    if (btnSetHotkey.Text == "Click to Cancel")
                    {
                        hotkeyString = "Hotkey: ";
                    }
                    else
                    {
                        hotkeyString = "Quick Ping Hotkey: ";
                    }


                    if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
                    {
                        if (btnSetHotkey.Text == "Click to Cancel")
                        {
                            config.radialHotkeyShift = true;
                        }
                        else
                        {
                            config.pingHotkeyShift = true;
                        }

                        hotkeyString += "Shift + ";
                    }
                    else
                    {
                        if (btnSetHotkey.Text == "Click to Cancel")
                        {
                            config.radialHotkeyShift = false;
                        }
                        else
                        {
                            config.pingHotkeyShift = false;
                        }
                    }

                    if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                    {
                        if (btnSetHotkey.Text == "Click to Cancel")
                        {
                            config.radialHotkeyCtrl = true;
                        }
                        else
                        {
                            config.pingHotkeyCtrl = true;
                        }

                        hotkeyString += "Ctrl + ";
                    }
                    else
                    {
                        if (btnSetHotkey.Text == "Click to Cancel")
                        {
                            config.radialHotkeyCtrl = false;
                        }
                        else
                        {
                            config.pingHotkeyCtrl = false;
                        }
                    }

                    if ((Control.ModifierKeys & Keys.Alt) == Keys.Alt)
                    {
                        if (btnSetHotkey.Text == "Click to Cancel")
                        {

                            config.radialHotkeyAlt = true;
                        }
                        else
                        {
                            config.pingHotkeyAlt = true;
                        }
                        hotkeyString += "Alt + ";
                    }
                    else
                    {
                        if (btnSetHotkey.Text == "Click to Cancel")
                        {
                            config.radialHotkeyAlt = false;
                        }
                        else
                        {
                            config.pingHotkeyAlt = false;
                        }
                    }

                    hotkeyString += e.KeyCode.ToString().ToUpper();

                    if (btnSetHotkey.Text == "Click to Cancel")
                    {
                    

                        hotkeyLabel.Text = hotkeyString;
                        btnSetHotkey.Text = "Click to Set Hotkey";
                        btnSetAlertPing.Enabled = true;

                        config.radialHotkey = e.KeyValue;
                    }
                    else
                    {
                        config.pingHotkey = e.KeyValue;

                        lblAlertPing.Text = hotkeyString;
                        btnSetAlertPing.Text = "Click to Set Hotkey";
                        btnSetHotkey.Enabled = true;
                    }

                    config.save();
                }
            }

        }
    }
}
