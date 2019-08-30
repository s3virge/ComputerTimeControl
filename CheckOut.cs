using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace ComputerTimeControl {
    /// <summary>
    /// клас занимается контролем времени
    /// </summary>
    class CheckOut {

        private Register reg;
        private TimeParameters timeParams;
        
        private System.Timers.Timer timeOutTimer;

        public CheckOut() {
            reg = new Register();
            timeParams = new TimeParameters();

            timeParams.SetTimeLimitPerDayInMinutes(reg.ReadDayTimeLimit());

            //DateTime.Now
            timeParams.SetComputerStartDateTime(reg.ReadPcStartDateTime());
            timeParams.SetTimeBeforBreak(reg.ReadBreakPeriod());

            timeOutTimer = new System.Timers.Timer();

            Debug.WriteLine("Allowed time of work - {0}, PowerOff hours - {1}, power off min - {2}",
                timeParams.GetTimeLimitPerDayInMinutes(),
                timeParams.GetTimeBeforBreakHours(),
                timeParams.GetTimeLimitPerDayMinutes());
        }


        /// <summary>
        /// set timer which will be launch LockDesktop method
        /// </summary>
        public void Timeout() {
            
            int dueTime_ms = timeParams.GetTimeBeforBreakMinutes() * 60000;
            int timeOut_ms = dueTime_ms + timeParams.GetPauseTimeInMilisecons();

            timeOutTimer.Interval = timeOut_ms;
            timeOutTimer.Elapsed += LockDesktop;
            // Have the timer fire repeated events (true is the default)
            timeOutTimer.AutoReset = true;
            timeOutTimer.Enabled = true;

            Debug.WriteLine("CheckTimeout(). timeOut = {0}", timeOut_ms);

            // создаем таймер
            //dueTime - The amount of time to delay before callback is invoked, in milliseconds.            
            //timeOutTimer = new System.Threading.Timer(new TimerCallback(LockDesktop), null, dueTime_ms, timeOut_ms);            
        }

        /// <summary>
        /// control the limit of work per day
        /// </summary>
        public void DayPeriod() {

            //how much time the computer has already worked for today

            //когда компютер начал работать
            DateTime dtComputerStart = timeParams.GetComputerStartDateTime();
            //какой период времени работы в сутки разрешен
            int timeLimitPerDay = timeParams.GetTimeLimitPerDayInMinutes();

            DateTime currentTime = DateTime.Now;

            //TimeSpan - промежуток времени
            TimeSpan timeDiff = currentTime.Subtract(dtComputerStart);
            int waitTime = 0;

            if (timeDiff.TotalMinutes < timeLimitPerDay) {
                //остановить процесс на нужное количество времени
                waitTime = (timeLimitPerDay - (int)timeDiff.TotalMinutes);
                //convert to miliseconds
                waitTime = waitTime * 60000;
                Debug.WriteLine("waitTime = {0} ms", waitTime);                
            }

            ShutDown(waitTime);
        }

        public void ShutDown(int waitingTime) {
            /*
                System.Threading.ThreadInterruptedException: 'Thread was interrupted from a waiting state.'
                generates when main form were closed
                */
            try {
                Thread.Sleep(waitingTime);
            }
            catch (ThreadInterruptedException interruptEx) {
                Debug.WriteLine("{0}() gen an exception {1}", System.Reflection.MethodBase.GetCurrentMethod().Name, interruptEx.Message);
                //do nothing
                return;
            }

            //показать сообщение о том что лимит работы компьютера достигнут
            Debug.WriteLine("() was invoked", System.Reflection.MethodBase.GetCurrentMethod().Name);

            //launch shutdown -s -f -t 0
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C shutdown -s -f -t 60";
            process.StartInfo = startInfo;

#if DEBUG
            MessageBox.Show("The pc will turn off immediately");
#else
            process.Start();
#endif
        }

        /// <summary>
        /// block desktop for an essential time period
        /// </summary>        
        public void LockDesktop(Object source, System.Timers.ElapsedEventArgs e) {
            Debug.WriteLine("() was invoked", System.Reflection.MethodBase.GetCurrentMethod().Name);

            //this method must stop the timeOutTimer timer until he do not finish job

            //stop the timer while lockscreen is work
            timeOutTimer.Enabled = false;

            var lockScreen = new LockScreen();
            Application.Run(lockScreen);

            // start timer again
            timeOutTimer.Enabled = true;
        }
    }
}