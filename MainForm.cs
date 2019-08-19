using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ComputerTimeControl {
    public partial class MainForm : Form {
        private Register reg;
        private WorkTimeControl timeControl;
        public MainForm() {
            InitializeComponent();

            //Register.DeleteKey();
            
            //retrive from reg nesessary values
            reg = new Register();
            timeControl = new WorkTimeControl();

            timeControl.SetAllowedTimeOfWork(reg.ReadAllowedTimeOfWork());
            //timeControl.SetComputerStartDateTime(reg.Read);
            timeControl.SetPowerOffPeriod(reg.ReadPowerOffPeriod());

            Debug.WriteLine("Allowed time of work - {0}, PowerOff hours - {1}, power off min - {2}",
                timeControl.GetAllowedTimeOfWork(),
                timeControl.GetPowerOffHours(),
                timeControl.GetAllowedMinutes());

            /*shows the values in controls*/
            allowedHours.Value = timeControl.GetAllowedHours();
            allowedMinutes.Value = timeControl.GetAllowedMinutes();

            powerOffHours.Value = timeControl.GetPowerOffHours();
            powerOffMinutes.Value = timeControl.GetPowerOffMinutes();

            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;
        }

        private void OnResize(object sender, EventArgs e) {
            //if the form is minimized  
            //hide it from the task bar  
            //and show the system tray icon (represented by the NotifyIcon control)  
            if (this.WindowState == FormWindowState.Minimized) {
                HideToSystemArea();
            }
        }

        private void OnSystemAriaIconMouseDoubleClick(object sender, MouseEventArgs e) {
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
            timeControl.SetAllowedTimeOfWork((int)allowedHours.Value, (int)allowedMinutes.Value);
            timeControl.SetPowerOffPeriod((int)powerOffHours.Value, (int)powerOffMinutes.Value);
            reg.WriteAllowedTimeOfWork(timeControl.GetAllowedTimeOfWork());
            reg.WritePowerOffPeriod(timeControl.GetPowerOffPeriod());
            HideToSystemArea();
        }

        private void BtnCancel_Click(object sender, EventArgs e) {
            HideToSystemArea();
        }

        private void HideToSystemArea() {
            Hide();
            notifyIcon.Visible = true;
        }
    }
}
