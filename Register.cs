using System;
using System.Diagnostics;
using Microsoft.Win32;

namespace ComputerTimeControl {
    /*класс будет записівать и получать */
    class Register {
        private const String regKey = "ComputerTimeControl";
        private const String regPcStartDateTime = "StartTime";
        private const String regAllowedTimeOfWork = "AllowedTimeOfWork";
        private const String regPowerOffPeriod = "PowerOffPeriod";
        private const int defaultAllowedTimeOfWork = 2;

        public Register() {
            /*Сохранить дату в реестр если ее там нет*/
            if (!IsKeyExist()) {
                WritePcStartDateTime();
                WriteAllowedTimeOfWork(defaultAllowedTimeOfWork);
            }
            else {
                //получить сохраненную в реестре дату и время
                ReadPcStartDateTime();
                ReadAllowedTimeOfWork();
                ReadPowerOffPeriod();
                /*сравнить дату в реестре с текущей датой*/
                /*если даті не совпадают значит комп включили уже в другой день, то*/
                if (!IsStoredRegDateAdTodayAreEquals()) {
                    /*устанавливаем новую дату */
                    WritePcStartDateTime();
                }
            }
        }

        //сохранить в реестр дату и время запуска комьютера в tiсks
        private void WritePcStartDateTime() {
            DateTime dateTime = DateTime.Now;

            //сохранить в реестр дату и время запуска помьютора
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\" + regKey);

            //storing the values  
            key.SetValue(regPcStartDateTime, dateTime.Ticks);
            key.Close();
        }

        /*Сохраняет в свойство класса время и дату запука компьютора*/
        private DateTime ReadPcStartDateTime() {
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

        /*проверить есть ли ключ в реестре*/
        private bool IsKeyExist() {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\" + regKey);
            return key != null;
        }

        /// <summary>
        /// сравнить дату в реесте с текущей
        /// </summary>
        private bool IsStoredRegDateAdTodayAreEquals() {
            DateTime pcStartDateTime = ReadPcStartDateTime();
            return (pcStartDateTime.Date == DateTime.Today);
        }

        /// <summary>
        /// сохранить в реестре разрешенній период работі
        /// </summary>
        public void WriteAllowedTimeOfWork(int timeOfWork) {
            //сохранить в реестр дату и время запуска помьютора
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\" + regKey);

            //storing the values  
            key.SetValue(regAllowedTimeOfWork, timeOfWork);
            key.Close();
        }

        /*gets from reg stored value of allowed time of work*/
        private int ReadAllowedTimeOfWork() {
            //opening the subkey  
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\" + regKey);
            int time = 0;

            if (key != null) {
                time = Convert.ToInt16(key.GetValue(regAllowedTimeOfWork));
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

        public void WritePowerOffPeriod(int period) {
            //сохранить в реестр дату и время запуска помьютора
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\" + regKey);

            //storing the values  
            key.SetValue(regPowerOffPeriod, period);
            key.Close();
        }

        public int ReadPowerOffPeriod() {
            //opening the subkey  
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\" + regKey);
            int period = 0;

            if (key != null) {
                period = (Convert.ToInt16(key.GetValue(regPowerOffPeriod)));
                key.Close();
            }

            return period;
        }
    }
}