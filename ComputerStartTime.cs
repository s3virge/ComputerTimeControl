using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace ComputerTimeControl
{
    /*класс будет записівать и получать */
    class ComputerStartTime
    {
        private const String regKey = "ComputerTimeControl";
        private const String regPcStartDateTime = "StartTime";
        private const String regAllowedTimeOfWork = "regAllowedTimeOfWork"; 
        private static int AllowedTimeOfWork = 2; //let it be 2 hours by default       
        private DateTime pcStartDateTime;

        public ComputerStartTime()
        {
            /*Сохранить дату в реестр если ее там нет*/
            if (!RegIsKeyExist())
            {
                RegSetPcStartDateTime();
                RegSetAllowedTimeOfWork();
            }
            else
            {
                //получить сохраненную в реестре дату и время
                RegGetPcStartDateTime();
                RegGetAllowedTimeOfWork();
                /*сравнить дату в реестре с текущей датой*/
                /*если даті не совпадают значит комп включили уже в другой день, то*/
                if (!IsStoredRegDateAdTodayAreEquals())
                {
                    /*устанавливаем новую дату */
                    RegSetPcStartDateTime();
                }
            }            
        }

        public DateTime GetPcStartDateTime()
        {
            return pcStartDateTime;
        }

        public static void SetAllowedTimeOfWork(int Time)
        {
            AllowedTimeOfWork = Time;
        }

        //public int GetAllowedTimeOfWork()
        //{
        //    return AllowedTimeOfWork;
        //}
        
        //сохранить в реестр дату и время запуска комьютера в tiсks
        private void RegSetPcStartDateTime()
        {
            DateTime dateTime = DateTime.Now;

            //сохранить в реестр дату и время запуска помьютора
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\" + regKey);

            //storing the values  
            key.SetValue(regPcStartDateTime, dateTime.Ticks);
            key.Close();
        }

        /*Сохраняет в свойство класса время и дату запука компьютора*/
        private void RegGetPcStartDateTime()
        {
            //opening the subkey  
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\" + regKey);
            String pcStartTime = null;
            //if it does exist, retrieve the stored values  
            if (key != null)
            {
                pcStartTime = Convert.ToString(key.GetValue(regPcStartDateTime));
                /*строку из реестра конвертим в long и преобразуем в DateTime*/
                pcStartDateTime = new DateTime(Convert.ToInt64(key.GetValue(regPcStartDateTime)));
                key.Close();
            }
        }

        /*проверить есть ли ключ в реестре*/
        private bool RegIsKeyExist()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\" + regKey);
            return key != null;
        }

        /*сравнить дату в реесте с текущей */
        private bool IsStoredRegDateAdTodayAreEquals()
        {
            return (pcStartDateTime.Date == DateTime.Today);
        }

        /*хранить в реестре разрешенній период работі*/
        private void RegSetAllowedTimeOfWork()
        {
            //сохранить в реестр дату и время запуска помьютора
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\" + regKey);

            //storing the values  
            key.SetValue(regAllowedTimeOfWork, AllowedTimeOfWork);
            key.Close();
        }

        /*gets from reg stored value of allowed time of work*/
        private void RegGetAllowedTimeOfWork()
        {
            //opening the subkey  
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\" + regKey);

            if (key != null)
            {
                AllowedTimeOfWork = Convert.ToInt16(key.GetValue(regAllowedTimeOfWork));                
                key.Close();
            }
        }
    }
}