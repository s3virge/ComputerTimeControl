﻿using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComputerTimeControl {
    public partial class MainForm : Form {

        private Register reg;
        private TimeParameters timeControl;

        public MainForm() {
            InitializeComponent();

            //Register.DeleteKey();

            reg = new Register();
            timeControl = new TimeParameters();

            //retrive from reg nesessary values
            timeControl.SetTimeLimitPerDayInMinutes(reg.ReadDayTimeLimit());
            timeControl.SetComputerStartDateTime(DateTime.Now);
            timeControl.SetTimeBeforBreak(reg.ReadBreakPeriod());
            timeControl.SetPauseTimeInMilisecons(5 * 60 * 1000); //5 minutes

            Debug.WriteLine("Allowed time of work - {0}, PowerOff hours - {1}, power off min - {2}",
                timeControl.GetTimeLimitPerDayInMinutes(),
                timeControl.GetTimeBeforBreakHours(),
                timeControl.GetTimeLimitPerDayMinutes());

            /*shows the values in controls*/
            allowedHours.Value = timeControl.GetTimeLimitPerDayHours();
            allowedMinutes.Value = timeControl.GetTimeLimitPerDayMinutes();

            powerOffHours.Value = timeControl.GetTimeBeforBreakHours();
            powerOffMinutes.Value = timeControl.GetTimeBeforBreakMinutes();

            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;

            StartTimeControl();
        }

        
        private void StartTimeControl() {
            TimeControl timeControl = new TimeControl();

            /*You have to be aware of the differences between a Task and a Thread. 
             * A Task is something you want to be done. 
             * A thread is one of the many possible workers which performs that task. 
             * Separate tasks don't have to be performed by separate threads. 
             * The same thread can perform several Tasks. One task might be performed by several threads.
            */

            var dayTimePiriodContol = new Thread(timeControl.CheckDayTimePeriod);
            dayTimePiriodContol.Start();

            var dayTimeOutContol = new Thread(timeControl.CheckTimeout);
            dayTimeOutContol.Start();
        }

        private void OnResize(object sender, EventArgs e) {
            //if the form is minimized  
            //hide it from the task bar  
            //and show the system tray icon (represented by the NotifyIcon control)  
            if (this.WindowState == FormWindowState.Minimized) {
                HideToSystemArea();
            }
        }

        private void OnSystemAreaIconMouseDoubleClick(object sender, MouseEventArgs e) {
            Show();
            WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }

        private void onClosing(object sender, FormClosingEventArgs e) {
            if (ModifierKeys == Keys.Control) {
                DialogResult result = MessageBox.Show("Application will be close. Do you want to continue?", "What to do?", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes) {
                    return;
                }
            }

            HideToSystemArea();
            /*event should be canceled*/
            e.Cancel = true;
        }

        private void BtnOk_Click(object sender, EventArgs e) {
            timeControl.SetTimeLimitPerDay((int)allowedHours.Value, (int)allowedMinutes.Value);
            timeControl.SetTimeBeforBreak((int)powerOffHours.Value, (int)powerOffMinutes.Value);
            reg.WriteDayTimeLimit(timeControl.GetTimeLimitPerDayInMinutes());
            reg.WriteBreakPeriod(timeControl.GetTimeBeforBreak());
            HideToSystemArea();
        }

        private void BtnCancel_Click(object sender, EventArgs e) {
            HideToSystemArea();
        }

        private void HideToSystemArea() {
            try {
                Hide(); //System.InvalidOperationException: 
            }
            catch (InvalidOperationException operExc) {
                Debug.WriteLine("{0}() gen an exception {1}", System.Reflection.MethodBase.GetCurrentMethod().Name, operExc.Message);
                //throw;
            }

            notifyIcon.Visible = true;
        }
    }
}
