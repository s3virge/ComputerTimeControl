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
        private const String regValue = "computerStartTime";
        private String pcStartData = "";
        private String pcStartTime = "";
        private String pcTimeNow = "";
        private String pcDataNow = "";

        public ComputerStartTime()
        {
            if (!RegIsKeyExist())
            {
                RegSet();
            }
            else
            {
                GetDataAndTime();
                GetCurrentDataAndTime();
            }
        }

        public String GetStartData()
        {
            return pcStartData;
        }

        public String GetStartTime()
        {
            return pcStartTime;
        }
        
        //сохранить в реестр дату и время запуска комьютера
        public void RegSet()
        {
            DateTime dateTime = DateTime.Now;

            //сохранить в реестр дату и время запуска помьютора
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\" + regKey);

            //storing the values  
            key.SetValue(regValue, dateTime);
            key.Close();
        }

        /*возвращает строку в виде дата плюс время запука компьютора*/
        public String RegGet()
        {
            //opening the subkey  
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\" + regKey);
            String pcStartTime = null;
            //if it does exist, retrieve the stored values  
            if (key != null)
            {
                pcStartTime = Convert.ToString(key.GetValue(regValue));
                key.Close();
            }

            return pcStartTime;
        }

        /*возвращает массив содержащий дату и время начала работі компьютера*/
        public String[] GetDataAndTime()
        {
            String strData = RegGet();
            String[] arrDataAndTime = strData.Split(' ');

            pcStartData = arrDataAndTime[0];
            pcStartTime = arrDataAndTime[1];

            return arrDataAndTime;
        }

        /*получить текущую дату и время*/
        private void GetCurrentDataAndTime()
        {
            DateTime time = DateTime.Now;
            DateTime date = DateTime.Today;

            pcTimeNow = time.ToString(); // тут получается дата + 00:00:00
            pcDataNow = date.ToString();
        }

        /*проверить есть ли ключ в реестре*/
        public bool RegIsKeyExist()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\" + regKey);
            return key != null;
        }
    }
}