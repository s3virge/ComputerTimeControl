using System;
using System.Windows.Forms;

namespace ComputerTimeControl {
    public partial class LockScreen : Form {

        private int intervalSeconds; 
        public LockScreen() {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            TopMost = true;

            intervalSeconds = new TimeParameters().GetPauseTimeInMilisecons() / 1000;// * 60;
            ShowTime();
        }

        private void ShowTime() {
            string min = string.Format("{0}", intervalSeconds / 60);
            string sec = string.Format("{0}", intervalSeconds % 60);

            if (intervalSeconds % 60 < 10) {
                sec = "0" + sec;
            }

            label1.Text = string.Format("{0}:{1}", min, sec);
        }

        private void putOff_Click(object sender, EventArgs e) {
            this.Close();
        }

        /// <summary>
        /// shows timer on lock screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerTick(object sender, EventArgs e) {
            // Set the caption to the current time.  
            //label1.Text = DateTime.Now.ToString();          

            intervalSeconds--;

            ShowTime();

            if (intervalSeconds < 0) {                
                timer1.Enabled = false;
                this.Close();
            }
        }
    }
}
