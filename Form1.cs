using System;
using System.Windows.Forms;

namespace ComputerTimeControl
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ComputerStartTime.SetAllowedTimeOfWork(2);
            ComputerStartTime pcStartTime = new ComputerStartTime();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
