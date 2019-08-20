using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerTimeControl {
    /// <summary>
    /// клас занимается контролем времени
    /// </summary>
    class TimeControl {
        public void CheckTimeout() {
            for(int c = 0; c < 100; c++) {
            Debug.WriteLine("() was launched.", System.Reflection.MethodBase.GetCurrentMethod().Name);
                Thread.Sleep(735);
            }
        }        

        public void CheckDayTimePeriod() {
            for (int c = 0; c < 100; c++) {
                Debug.WriteLine("() was launched.", System.Reflection.MethodBase.GetCurrentMethod().Name);
                Thread.Sleep(752);
            }
        }
    }
}