﻿using System;

namespace ComputerTimeControl {
    class TimeParameters {
        private int timeLimitPerDayInMinutes;
        private int timeBeforeBreakInMinutes;
        private DateTime dtComputerStart;

        public TimeParameters() {
        }

        public void SetTimeLimitPerDay(int iHours, int iMinutes) {
            timeLimitPerDayInMinutes = iHours * 60 + iMinutes;
        }
        public void SetTimeLimitPerDayInMinutes(int timeLimitInMinutes) {
            timeLimitPerDayInMinutes = timeLimitInMinutes;
        }

        public int GetTimeLimitPerDayInMinutes() {
            return timeLimitPerDayInMinutes;
        }

        public DateTime GetComputerStartDateTime() {
            return dtComputerStart;
        }

        public void SetComputerStartDateTime(DateTime pcStartDateTime) {
            dtComputerStart = pcStartDateTime;
        }

        public int GetTimeLimitPerDayHours() {
            return timeLimitPerDayInMinutes / 60;
        }

        public int GetTimeLimitPerDayMinutes() {
            return timeLimitPerDayInMinutes - ((timeLimitPerDayInMinutes / 60) * 60);
        }
        
        public int GetTimeBeforBreak() {
            return timeBeforeBreakInMinutes;
        }

        public int GetTimeBeforBreakHours() {
            return timeBeforeBreakInMinutes / 60;
        }

        public int GetTimeBeforBreakMinutes() {
            return timeBeforeBreakInMinutes - ((timeBeforeBreakInMinutes / 60) * 60);
        }

        public void SetTimeBeforBreak(int timePeriod) {
            timeBeforeBreakInMinutes = timePeriod;
        }

        public void SetTimeBeforBreak(int iHours, int iMinutes) {
            timeBeforeBreakInMinutes = iHours * 60 + iMinutes;
        }

        ///// <summary>
        ///// сравнить дату в реесте с текущей
        ///// </summary>
        //private bool IsStoredRegDateAndTodayAreEquals() {
        //    DateTime pcStartDateTime = ReadPcStartDateTime();
        //    return (pcStartDateTime.Date == DateTime.Today);
        //}
    }
}
