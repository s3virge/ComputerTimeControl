using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace ComputerTimeControl {
    public partial class MainForm : Form {

        private Register reg;
        private TimeParameters timeControl;

        Thread dayTimePiriodContol, dayTimeOutContol;

        private int timeBefoBreakInSeconds;

        public MainForm() {
            InitializeComponent();

            //Register.DeleteKey();

            reg = new Register();
            timeControl = new TimeParameters();

            //retrive from reg nesessary values
            timeControl.SetTimeLimitPerDayInMinutes(reg.ReadDayTimeLimit());
            timeControl.SetComputerStartDateTime(DateTime.Now);
            timeControl.SetTimeBeforBreak(reg.ReadBreakPeriod());
            timeControl.SetPauseTimeInMilisecons(1 * 60 * 1000); //5 minutes

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

            TimeParameters tp = new TimeParameters();
            timeBefoBreakInSeconds = (tp.GetTimeBeforBreakInMinutes() * 60) + (tp.GetPauseTimeInMilisecons() / 1000);

            StartTimeControl();
        }


        private void StartTimeControl() {
            CheckOut timeControl = new CheckOut();

            /*You have to be aware of the differences between a Task and a Thread. 
             * A Task is something you want to be done. 
             * A thread is one of the many possible workers which performs that task. 
             * Separate tasks don't have to be performed by separate threads. 
             * The same thread can perform several Tasks. One task might be performed by several threads.
            */

            dayTimePiriodContol = new Thread(timeControl.DayPeriod);
            dayTimePiriodContol.Start();

            dayTimeOutContol = new Thread(timeControl.Timeout);
            dayTimeOutContol.Start();

            //запустить два таймера для отображения в главной форме сколько времени осталось до выклчения и сколько до блокировки.          
        }

        public void TimerTick(object sender, EventArgs e) {
            //определить сколько часов осталось до достижения лимита работы в сутки
            //определить сколько минут осталось до достижения лимита работы в сутки
            //определить сколько секудн осталось до достижения лимита работы в сутки
            //how much time the computer has already worked for today 

            timeBefoBreakInSeconds--;

            string min = string.Format("{0}", timeBefoBreakInSeconds / 60);
            string sec = string.Format("{0}", timeBefoBreakInSeconds % 60);

            if (timeBefoBreakInSeconds % 60 < 10) {
                sec = "0" + sec;
            }

            if (timeBefoBreakInSeconds <= 0) {
                TimeParameters tp = new TimeParameters();
                timeBefoBreakInSeconds = (tp.GetTimeBeforBreakInMinutes() * 60) + (tp.GetPauseTimeInMilisecons() / 1000);
            }          

            labelBeforBlockLeft.Text = string.Format("{0}:{1}", min, sec);
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

            //показывать на панеле задач
            ShowInTaskbar = true;
        }

        private void onClosing(object sender, FormClosingEventArgs e) {

            if (ModifierKeys == Keys.Control) {
                DialogResult result = MessageBox.Show("Application will be close. Do you want to continue?", "What to do?", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes) {
                    dayTimeOutContol.Interrupt();
                    dayTimePiriodContol.Interrupt();
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
            reg.WriteBreakPeriod(timeControl.GetTimeBeforBreakInMinutes());
            HideToSystemArea();
        }

        private void BtnCancel_Click(object sender, EventArgs e) {
            HideToSystemArea();
        }

        private void HideToSystemArea() {
            try {
                Hide(); //System.InvalidOperationException: 
                ShowInTaskbar = false;
            }
            catch (InvalidOperationException operExc) {
                Debug.WriteLine("{0}() gen an exception {1}", System.Reflection.MethodBase.GetCurrentMethod().Name, operExc.Message);
                //throw;
            }

            notifyIcon.Visible = true;
        }
    }
}
