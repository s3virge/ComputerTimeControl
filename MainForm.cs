using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComputerTimeControl {
    public partial class MainForm : Form {

        private Register reg;
        private TimeParameters timeControl;

        public MainForm() {
            InitializeComponent();

            Register.DeleteKey();
                       
            reg = new Register();
            timeControl = new TimeParameters();
            
            //retrive from reg nesessary values
            timeControl.SetTimeLimitPerDayInMinutes(reg.ReadDayTimeLimit());
            timeControl.SetComputerStartDateTime(DateTime.Now);
            timeControl.SetTimeBeforBreak(reg.ReadBreakPeriod());

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

            var dayTimePiriodContol = new Task(timeControl.CheckDayTimePeriod);
            dayTimePiriodContol.Start();

            var dayTimeOutContol = new Task(timeControl.CheckTimeout);
            dayTimeOutContol.Start();

            var tasks = new[] { dayTimePiriodContol, dayTimeOutContol };
            Task.WaitAll(tasks);
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
            Hide();
            notifyIcon.Visible = true;
        }
    }
}
