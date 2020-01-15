using System;
using System.Diagnostics;
using System.Threading;

namespace TimeLimiter
{
    class TimeLimiter
    {
        private static string enabledNumberOfHoures = "2";

        public void Start(string[] args)
        {
            Register reg = new Register();           

            //if exists command line arguments
            if (args.Length != 0)
            {
                //then get first argument and write to register 
                reg.WriteEnabledNumberOfHours(args[0]);
            }

            if (reg.IsKeyExist())
            {
                string numberH = reg.ReadEnabledNumberOfHours();
                if (numberH == null) { 
                    reg.WriteEnabledNumberOfHours(enabledNumberOfHoures);
                }
            }

            //сравнивать даты.
            DateTime startDate = reg.ReadTimeWhenComputerStartedWorking();
            //если даты разные
            if (IsToday(startDate) != true)
            {
                //то обнулить NumberOfTimes
                reg.WriteNumberOfTimes("0");
            }

            int timeOut = 60 * 60 * 1000; //1 hour

            //check number of times
            int nTimes = Convert.ToInt32(reg.ReadNumberOfTimes());
            int enabledNumOfHours = Convert.ToInt32(reg.ReadEnabledNumberOfHours());

            if (nTimes >= enabledNumOfHours)
            {
                timeOut = 0;
            }

            //wait one houre befor shutdown the pc
            ShutDown(timeOut);

            //make record in register about what time computer is poweroff            
            reg.WriteNumberOfTimes((Convert.ToInt32(reg.ReadNumberOfTimes()) + 1).ToString());
        }

        /// <summary>
        /// compare a DataTimes
        /// </summary>
        private bool IsToday(DateTime time)
        {
            return (time.Date == DateTime.Today);
        }

        public void ShutDown(int waitingTime)
        {
            Thread.Sleep(waitingTime);

            Debug.WriteLine("() was invoked", System.Reflection.MethodBase.GetCurrentMethod().Name);

            //launch shutdown -s -f -t 0
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C shutdown -s -f -t 120";
            process.StartInfo = startInfo;

#if DEBUG
            Console.WriteLine("The pc will turn off immediately");
#else
            process.Start();
#endif
        }
    }
}

