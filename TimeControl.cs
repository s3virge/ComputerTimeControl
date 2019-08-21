using System;
using System.Diagnostics;
using System.Threading;

namespace ComputerTimeControl {
    /// <summary>
    /// клас занимается контролем времени
    /// </summary>
    class TimeControl {

        private Register reg;
        private TimeParameters timeControl;

        public TimeControl() {
            reg = new Register();
            timeControl = new TimeParameters();

            timeControl.SetAllowedTimeOfWork(reg.ReadAllowedTimeOfWork());
            timeControl.SetComputerStartDateTime(DateTime.Now);
            timeControl.SetPowerOffPeriod(reg.ReadPowerOffPeriod());

            Debug.WriteLine("Allowed time of work - {0}, PowerOff hours - {1}, power off min - {2}",
                timeControl.GetAllowedTimeOfWork(),
                timeControl.GetPowerOffHours(),
                timeControl.GetAllowedMinutes());
        }
        public void CheckTimeout() {
            //блокировать рабочий стол на определённое время
            //запустить таймер 
            // устанавливаем метод обратного вызова     
            TimerCallback tm = new TimerCallback(LockDesctop);
            int pauseTime = 5 * 60 * 1000; //convert minutes to miliseconds 
            int timeOut = timeControl.GetAllowedMinutes() * 1000 + pauseTime;
            // создаем таймер
            Timer timer = new Timer(tm, 0, 0, timeOut);
        }
        public void CheckDayTimePeriod() {
            ShutDown();
        }
                
        public void ShutDown() {
            //можно остановить процесс на нужное количество времени
            //Thread.Sleep(WaitTime);

            //показать сообщение о том что лимит работы компьютера достигнут
            Debug.WriteLine("() was invoked", System.Reflection.MethodBase.GetCurrentMethod().Name);            
        }

         public void LockDesctop(object state) {
            Debug.WriteLine("() was invoked", System.Reflection.MethodBase.GetCurrentMethod().Name);
        }
    }
}