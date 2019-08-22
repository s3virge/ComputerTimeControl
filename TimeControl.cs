using System;
using System.Diagnostics;
using System.Threading;

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
            int pauseTime = 5 * 60 * 1000; //convert minutes to miliseconds 
            int timeOut = timeParams.GetTimeLimitPerDayMinutes() * 1000 + pauseTime;
            // создаем таймер
            Timer timer = new Timer(tm, 0, 0, timeOut);
        }
        public void CheckDayTimePeriod() {
            ShutDown();
        }
                
        public void ShutDown() {
            //когда компютер начал работать
            DateTime dtComputerStart = timeParams.GetComputerStartDateTime();
            //какой период времени работы в сутки разрешен
            int timeLimitPerDay = timeParams.GetTimeLimitPerDayInMinutes();

            //int breakPeriod = timeParams.GetTimeBeforBreak();
                       
            //можно остановить процесс на нужное количество времени
            //Thread.Sleep(WaitTime);

            //показать сообщение о том что лимит работы компьютера достигнут
            Debug.WriteLine("() was invoked", System.Reflection.MethodBase.GetCurrentMethod().Name);
            System.Windows.Forms.MessageBox.Show("The pc will shutdown immediately.");
        }

         public void LockDesctop(object state) {
            Debug.WriteLine("() was invoked", System.Reflection.MethodBase.GetCurrentMethod().Name);
        }
    }
}