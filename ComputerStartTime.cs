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

        //сохранить в реестр дату и время запуска комьютера
        public void Set()
        {
            DateTime dateTime = DateTime.Now;

            //сохранить в реестр дату и время запуска помьютора
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\" + regKey);

            //storing the values  
            key.SetValue(regValue, dateTime);
            key.Close();
        }       
        
        public String Get()
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
    }
}