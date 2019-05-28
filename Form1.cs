using System;
using System.Windows.Forms;

namespace ComputerTimeControl
{
    public partial class Form1 : Form
    {
        private Register reg;
        private WorkTimeControl timeControl;
        public Form1()
        {
            InitializeComponent();

            Register.DeleteKey();
            reg = new Register();
            timeControl = new WorkTimeControl();
            allowedHours.Value = timeControl.GetAllowedHours();
            allowedMinutes.Value = timeControl.GetAllowedMinutes();
            powerOffHours.Value = timeControl.GetPowerOffHours();
            powerOffMinutes.Value = timeControl.GetPowerOffMinutes();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            timeControl.SetAllowedTimeOfWork((int)allowedHours.Value, (int)allowedMinutes.Value);
            timeControl.SetPowerOffPeriod((int)powerOffHours.Value, (int)powerOffMinutes.Value);
            reg.WriteAllowedTimeOfWork(timeControl.GetAllowedTimeOfWork());
            reg.WritePowerOffPeriod(timeControl.GetPowerOffPeriod());
        }
    }
}
