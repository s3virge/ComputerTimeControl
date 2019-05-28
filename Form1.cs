using System;
using System.Windows.Forms;

namespace ComputerTimeControl
{
    public partial class Form1 : Form
    {
        private Register reg;
        public Form1()
        {
            InitializeComponent();

            Register.DeleteKey();
            reg = new Register();
            allowedHours.Value = reg.GetAllowedHours();
            allowedMinutes.Value = reg.GetAllowedMinutes();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            reg.SetAllowedTimeOfWork((int)allowedHours.Value, (int)allowedMinutes.Value);
            reg.WriteAllowedTimeOfWork();
        }
    }
}
