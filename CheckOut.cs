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
        
        public CheckOut() {
            reg = new Register();
            timeParams = new TimeParameters();

            timeParams.SetTimeLimitPerDayInMinutes(reg.ReadDayTimeLimit());

            //DateTime.Now
            timeParams.SetComputerStartDateTime(reg.ReadPcStartDateTime());
            timeParams.SetTimeBeforBreak(reg.ReadBreakPeriod());

            Debug.WriteLine("Allowed time of work - {0}, PowerOff hours - {1}, power off min - {2}",
                timeParams.GetTimeLimitPerDayInMinutes(),
                timeParams.GetTimeBeforBreakHours(),
                timeParams.GetTimeLimitPerDayMinutes());
        }

        public void Timeout() {
            //блокировать рабочий стол на определённое время
            int timeOut = timeParams.GetTimeBeforBreakMinutes() * 60000 + timeParams.GetPauseTimeInMilisecons();

            Debug.WriteLine("CheckTimeout(). timeOut = {0}", timeOut);

            // создаем таймер
            //dueTime - The amount of time to delay before callback is invoked, in milliseconds.            
          System.Threading.Timer timer = new System.Threading.Timer(new TimerCallback(LockDesktop), null, 0, timeOut);
          //System.Threading.Timer timer = new System.Threading.Timer(tm, 0, 0, timeOut);
        }

        public void DayPeriod() {

            //how much time the computer has already worked for today

            //когда компютер начал работать
            DateTime dtComputerStart = timeParams.GetComputerStartDateTime();
            //какой период времени работы в сутки разрешен
            int timeLimitPerDay = timeParams.GetTimeLimitPerDayInMinutes();

            DateTime currentTime = DateTime.Now;

            //TimeSpan - промежуток времени
            TimeSpan timeDiff = currentTime.Subtract(dtComputerStart);

            if (timeDiff.TotalMinutes < timeLimitPerDay) {
                //остановить процесс на нужное количество времени
                int waitTime = (timeLimitPerDay - (int)timeDiff.TotalMinutes);
                //convert to miliseconds
                waitTime = waitTime * 60000;
                Debug.WriteLine("waitTime = {0} ms", waitTime);

                /*
                 System.Threading.ThreadInterruptedException: 'Thread was interrupted from a waiting state.'
                 generates when main form were closed
                 */
                try {
                    Thread.Sleep(waitTime);
                }
                catch (ThreadInterruptedException interruptEx) {
                    Debug.WriteLine("{0}() gen an exception {1}", System.Reflection.MethodBase.GetCurrentMethod().Name, interruptEx.Message);
                    //do nothing
                    return;
                }
            }

            ShutDown();
        }
                
        public void ShutDown() {
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

        public void LockDesktop(object state) {
            Debug.WriteLine("() was invoked", System.Reflection.MethodBase.GetCurrentMethod().Name);
                      
            var form = new LockScreen();
            Application.Run(form);
        }
    }
}