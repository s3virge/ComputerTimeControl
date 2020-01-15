using System;

namespace ComputerTimeControl {
    class TimeParameters {
        private static int timeLimitPerDayInMinutes;
        private static int timeBeforeBreakInMinutes;
        private static DateTime dtComputerStart;
        private static int pauseTimeMS;


        public TimeParameters() {
        }

        public int GetPauseTimeInMilisecons() { return pauseTimeMS; }
        public void SetPauseTimeInMilisecons(int timeOfPauseInMilliseconds) {
            pauseTimeMS = timeOfPauseInMilliseconds; }

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
        
        public int GetTimeBeforBreakInMinutes() {
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
