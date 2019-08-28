using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace ComputerTimeControl {
    /// <summary>
    /// клас занимается контролем времени
    /// </summary>
    class TimeControl {

        private Register reg;
        private TimeParameters timeParams;

        public TimeControl() {
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

        public void CheckTimeout() {
            //блокировать рабочий стол на определённое время
            //запустить таймер             
            TimerCallback tm = new TimerCallback(LockDesctop);
            int pauseTimeInMS = timeParams.GetPauseTimeInMilisecons(); 
            int timeOut = timeParams.GetTimeBeforBreakMinutes() * 60000 + pauseTimeInMS;

            Debug.WriteLine("CheckTimeout(). timeOut = {0}", timeOut);

            // создаем таймер
            //dueTime - The amount of time to delay before callback is invoked, in milliseconds.            
          System.Threading.Timer timer = new System.Threading.Timer(tm, 0, timeOut, timeOut);
          //System.Threading.Timer timer = new System.Threading.Timer(tm, 0, 0, timeOut);
        }
        public void CheckDayTimePeriod() {

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
            System.Windows.Forms.MessageBox.Show("The pc will shutdown immediately.");
        }

         public void LockDesctop(object state) {
            Debug.WriteLine("() was invoked", System.Reflection.MethodBase.GetCurrentMethod().Name);
                      
            var form = new LockScreen();
            Application.Run(form);
        }
    }
}