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
            Register reg = new Register("TimeLimiter");
            reg.AddToRun();

            if (!reg.IsKeyExist()) {
                reg.WriteCurrentDate();
                //WriteDayTimeLimit(timeLimit);                
            }
            else {
                /*сравнить дату в реестре с текущей датой*/
                /*если даті не совпадают значит комп включили уже в другой день, то*/
                if (reg.IsStoredRegDateAndTodayAreEquals() != true) {
                    /*устанавливаем новую дату */
                    reg.WriteCurrentDate();
                    reg.WriteNumberOfTimes("0");
                }
            }

            //if exists command line arguments
            if (args.Length != 0)
            {                
                //argument is a number?
                if (CheckArguments(args[0]) == false) {
                    Console.WriteLine("Command line arguments is incorrect.");
                }

                //then get first argument and write to register 
                //that will be the enabled Number of hours
                reg.WriteEnabledNumberOfHours(args[0]);
            }

            if (reg.IsKeyExist())
            {
                string numberH = reg.ReadEnabledNumberOfHours();
                if (numberH == null) { 
                    reg.WriteEnabledNumberOfHours(enabledNumberOfHoures);
                }
            }

            //compare dates
            //DateTime startDate = reg.ReadTimeWhenComputerStartedWorking();
            //Console.WriteLine($"{startDate.Date}");
            //if dates is different
            //if (IsToday(startDate) != true)
            //{
            //    //then make NumberOfTimes equals zero
            //    //reg.WriteNumberOfTimes("0");
            //    //reg.WriteTimeWhenComputerStartedWorking();
            //}

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

            //make record in register how many times the computer is poweroff      
            reg.WriteNumberOfTimes((Convert.ToInt32(reg.ReadNumberOfTimes()) + 1).ToString());
        }

        private bool CheckArguments(string arg) {
            if (arg.Length > 1) {                
                return false;
            }

            if (Char.IsDigit(arg, 0)){
                return true;
            }
           
            return false;
        }

        /// <summary>
        /// compare a DataTimes
        /// </summary>
        private bool IsToday(DateTime time)
        {
            Console.WriteLine($"time.Date = {time.Date} DateTime.Today = {DateTime.Today}");
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

