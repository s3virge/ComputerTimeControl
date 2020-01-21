using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Microsoft.Win32;

namespace TimeLimiter
{
    /*this class will write and read parametres from regestry*/

    class Register {
        private string regKey; // = "TimeLimiter";
        private const string regPcStartDateTime = "StartTime";
        private const string regDayTimeLimit = "TimeLimitPerDayInMinutes";

        private const string regEnabledNumberOfHours = "EnabledNumberOfHours";
        private const string regNumberOfTimes = "NumberOfTimes";
        private const int timeLimit = 2 * 60 + 10; //two hours = 120 min

        public Register(string keyName) {
            regKey = keyName;
        }

        //установить разрешенное число часов.
        public void WriteEnabledNumberOfHours(string numberOfHoures)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\" + regKey);

            //storing the values  
            key.SetValue(regEnabledNumberOfHours, numberOfHoures);
            key.Close();
        }
        
        //получить резрешенное число часов
        public string ReadEnabledNumberOfHours()
        {
            //opening the subkey  
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\" + regKey);

            string numberOfHours = "";
            //if it does exist, retrieve the stored values  
            if (key != null)
            {
                /*строку из реестра конвертим в long и преобразуем в DateTime*/
                numberOfHours = (string)key.GetValue(regEnabledNumberOfHours);
                key.Close();
            }

            return numberOfHours;
        }

        //записать число часов.
        public void WriteNumberOfTimes(string numberOfTimes)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\" + regKey);

            //storing the values  
            key.SetValue(regNumberOfTimes, numberOfTimes);
            key.Close();
        }

        //прочитать резрешенное число часов
        public string ReadNumberOfTimes()
        {
            //opening the subkey  
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\" + regKey);

            string numberOfTimes = "";
            //if it does exist, retrieve the stored values  
            if (key != null)
            {
                /*строку из реестра конвертим в long и преобразуем в DateTime*/
                numberOfTimes = (string)key.GetValue(regNumberOfTimes);
                key.Close();
            }

            return numberOfTimes;
        }
                
       

        /// <summary>
        /// сохранить в реестр дату и время запуска комьютера в tiсks
        /// </summary>
        public void WriteCurrentDate() {
            DateTime dateTime = DateTime.Now;

            //сохранить в реестр дату и время запуска помьютора
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\" + regKey);

            //storing the values  
            key.SetValue(regPcStartDateTime, dateTime.Ticks);
            key.Close();
        }

        /// <summary>
        /// return the DateTime when computer was started        
        /// </summary>
        public DateTime ReadTimeWhenComputerStartedWorking() {
            //opening the subkey  
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\" + regKey);
            DateTime dt = new DateTime();
            //if it does exist, retrieve the stored values  
            if (key != null) {
                /*строку из реестра конвертим в long и преобразуем в DateTime*/
                dt = new DateTime(Convert.ToInt64(key.GetValue(regPcStartDateTime)));
                key.Close();
            }

            return dt;
        }

        /// <summary>
        /// return true if key is exists
        /// </summary>
        public bool IsKeyExist() {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\" + regKey);
            return key != null;
        }

        /// <summary>
        /// сравнить дату в реесте с текущей
        /// </summary>
        public bool IsStoredDateAndTodayAreEquals()
        {
            DateTime pcStartDateTime = ReadTimeWhenComputerStartedWorking();
            return (pcStartDateTime.Date == DateTime.Today);
        }

        /// <summary>
        /// сохранить в реестре разрешенній период работі
        /// </summary>
        public void WriteDayTimeLimit(int timeLimitInMinutes) {
            //сохранить в реестр дату и время запуска помьютора
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\" + regKey);

            //storing the values  
            key.SetValue(regDayTimeLimit, timeLimitInMinutes);
            key.Close();
        }

        /// <summary>
        /// gets from reg stored value of allowed time of work
        /// </summary>
        public int ReadDayTimeLimit() {
            //opening the subkey  
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\" + regKey);
            int time = 0;

            if (key != null) {
                time = Convert.ToInt16(key.GetValue(regDayTimeLimit));
                key.Close();
            }

            return time;
        }

        public void DeleteKey() {
            //    RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\" + regKey, true);
            //    key.DeleteValue("key1", false); // теперь при отсутствии ключа исключение не сгенерируется
            //    key.Close();
            try {
                Registry.CurrentUser.DeleteSubKey($@"SOFTWARE\{regKey}");                
            }
            catch (ArgumentException aEx) {
                Debug.WriteLine("{0}() method gen an exception {1}", System.Reflection.MethodBase.GetCurrentMethod().Name, aEx.Message);
            }
        }
        
        public void AddToRun()
        {
            //сохранить в реестр дату и время запуска помьютора
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", RegistryKeyPermissionCheck.ReadWriteSubTree);

            string GetAsseblyName()
            {
                return System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            }

            string GetAssemblyPath()
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }

            //storing the values  
            try
            {
                string exeName = GetAsseblyName();
                string exePath = GetAssemblyPath();
                key.SetValue(exeName, $@"{exePath}\{exeName}.exe");
            }
            catch (Exception exception) 
            {
                Console.WriteLine(exception.Message);
            }

            key.Close();
        }
    }
}