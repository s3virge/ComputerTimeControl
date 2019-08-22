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
            int pauseTimeInMS = 5 * 60 * 1000; //convert minutes to miliseconds 
            int timeOut = timeParams.GetTimeLimitPerDayMinutes() * 60000 + pauseTimeInMS;
            // создаем таймер
            System.Threading.Timer timer = new System.Threading.Timer(tm, 0, 0, timeOut);
        }
        public void CheckDayTimePeriod() {
            ShutDown();
        }
                
        public void ShutDown() {

            //вычистилть сколько времени за сегодня уже отработал комп

            //когда компютер начал работать
            DateTime dtComputerStart = timeParams.GetComputerStartDateTime();
            //какой период времени работы в сутки разрешен
            int timeLimitPerDay = timeParams.GetTimeLimitPerDayInMinutes();

            DateTime currentTime = DateTime.Now;

            //TimeSpan - промежуток времени
            TimeSpan timeDiff = currentTime.Subtract(dtComputerStart);
                       
            if (timeDiff.TotalMinutes < timeLimitPerDay) {
                //можно остановить процесс на нужное количество времени
                //convert to miliseconds
                int waitTime = (timeLimitPerDay - (int)timeDiff.TotalMinutes);
                waitTime = waitTime * 60000;
                Debug.WriteLine("waitTime = {0} ms", waitTime);

                Thread.Sleep(waitTime);
            }

            //показать сообщение о том что лимит работы компьютера достигнут
            Debug.WriteLine("() was invoked", System.Reflection.MethodBase.GetCurrentMethod().Name);
            System.Windows.Forms.MessageBox.Show("The pc will shutdown immediately.");
        }

         public void LockDesctop(object state) {
            Debug.WriteLine("() was invoked", System.Reflection.MethodBase.GetCurrentMethod().Name);

            Application.EnableVisualStyles();
            var form = new Form();

            form.FormBorderStyle = FormBorderStyle.None;
            form.WindowState = FormWindowState.Maximized;

            var button = new Button() {
                Text = "Close"
            };

            button.Click += (sender, e) => Application.Exit();

            form.Controls.Add(button);
            Application.Run(form);
        }
    }
}