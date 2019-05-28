using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerTimeControl
{
    class WorkTimeControl
    {
        private int allowedTimeOfWork;        
        private int powerOffPeriod;
        private DateTime computerStartDateTime;

        /// <summary>
        /// присвоить свойству класса AllowedTimeOfWork значения часов и минут 
        /// </summary>
        /// <param name="iHours">разрешено часов</param>
        /// <param name="iMinutes">разрешено минут</param>
        public void SetAllowedTimeOfWork(int iHours, int iMinutes)
        {
            allowedTimeOfWork = iHours * 60 + iMinutes;
        }
        public void SetAllowedTimeOfWork(int timeOfWork)
        {
            allowedTimeOfWork = timeOfWork;
        }

        public int GetAllowedTimeOfWork()
        {
            return allowedTimeOfWork;
        }

        public DateTime GetComputerStartDateTime()
        {
            return computerStartDateTime;
        }

        public int GetAllowedHours()
        {
            return allowedTimeOfWork / 60;
        }

        public int GetAllowedMinutes()
        {
            return allowedTimeOfWork - ((allowedTimeOfWork / 60) * 60);
        }

        public void SetComputerStartDateTime(DateTime pcStartDateTime)
        {
            computerStartDateTime = pcStartDateTime;
        }

        public int GetPowerOffPeriod()
        {
            return powerOffPeriod;
        }

        public int GetPowerOffHours()
        {
            return powerOffPeriod / 60;
        }

        public int GetPowerOffMinutes()
        {
            return powerOffPeriod - ((powerOffPeriod / 60) * 60);
        }

        public void SetPowerOffPeriod(int timePeriod)
        {
            powerOffPeriod = timePeriod;
        }

        public void SetPowerOffPeriod(int iHours, int iMinutes)
        {
            powerOffPeriod = iHours * 60 + iMinutes;
        }
    }
}
