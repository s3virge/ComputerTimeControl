using System;
using System.Diagnostics;
using Microsoft.Win32;

namespace ComputerTimeControl {
    /*класс будет записівать и получать */
    class Register {
        private const string regKey = "ComputerTimeControl";
        private const string regPcStartDateTime = "StartTime";
        private const string regDayTimeLimit = "TimeLimitPerDayInMinutes";
        private const string regBreakPeriod = "BreakPeriod";
        private const int defaultDayTimeLimit = 2 * 60 + 10; //two hours = 120 min
        private const int defaultBreakPeriod = 61; //in minutes

        public Register() {
            /*Сохранить дату в реестр если ее там нет*/
            if (!IsKeyExist()) {
                WritePcStartDateTime();
                WriteDayTimeLimit(defaultDayTimeLimit);
                WriteBreakPeriod(defaultBreakPeriod);
            }
            else {                                
                /*сравнить дату в реестре с текущей датой*/
                /*если даті не совпадают значит комп включили уже в другой день, то*/
                if (!IsStoredRegDateAndTodayAreEquals()) {
                    /*устанавливаем новую дату */
                    WritePcStartDateTime();
                }
            }
        }        

        /// <summary>
        /// сохранить в реестр дату и время запуска комьютера в tiсks
        /// </summary>
        private void WritePcStartDateTime() {
            DateTime dateTime = DateTime.Now;

            //сохранить в реестр дату и время запуска помьютора
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\" + regKey);

            //storing the values  
            key.SetValue(regPcStartDateTime, dateTime.Ticks);
            key.Close();
        }

        /// <summary>
        /// Сохраняет в свойство класса время и дату запука компьютора        
        /// </summary>
        public DateTime ReadPcStartDateTime() {
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
        /// проверить есть ли ключ в реестре        
        /// </summary>
        private bool IsKeyExist() {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\" + regKey);
            return key != null;
        }

        /// <summary>
        /// сравнить дату в реесте с текущей
        /// </summary>
        private bool IsStoredRegDateAndTodayAreEquals() {
            DateTime pcStartDateTime = ReadPcStartDateTime();
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

        public static void DeleteKey() {
            //    RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\" + regKey, true);
            //    key.DeleteValue("key1", false); // теперь при отсутствии ключа исключение не сгенерируется
            //    key.Close();
            try {
                Registry.CurrentUser.DeleteSubKey(@"SOFTWARE\" + regKey);                
            }
            catch (ArgumentException aEx) {
                Debug.WriteLine("{0}() method gen an exception {1}", System.Reflection.MethodBase.GetCurrentMethod().Name, aEx.Message);
            }
        }

        public void WriteBreakPeriod(int period) {
            //сохранить в реестр дату и время запуска помьютора
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\" + regKey);

            //storing the values  
            key.SetValue(regBreakPeriod, period);
            key.Close();
        }

        public int ReadBreakPeriod() {
            //opening the subkey  
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\" + regKey);
            int period = 0;

            if (key != null) {
                period = (Convert.ToInt16(key.GetValue(regBreakPeriod)));
                key.Close();
            }

            return period;
        }
    }
}