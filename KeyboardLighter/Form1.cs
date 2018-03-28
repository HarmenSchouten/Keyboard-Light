using HidLibrary;
using KeyboardLighter.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using KeyboardLighter;

namespace KeyboardLighter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            KeyboardLedManager.Instance.checkNewDevices();
        }

        private void orange_Click(object sender, EventArgs e)
        {
            label1.BackColor = Color.Orange;
            KeyboardLedManager.Instance.updateMouseIndicator(Program.indicatorModes.ORANGE_ON);
        }

        private void red_Click(object sender, EventArgs e)
        {
            label1.BackColor = Color.Red;
            KeyboardLedManager.Instance.updateMouseIndicator(Program.indicatorModes.RED_ON);
        }

        private void green_Click(object sender, EventArgs e)
        {
            label1.BackColor = Color.Green;
            KeyboardLedManager.Instance.updateMouseIndicator(Program.indicatorModes.GREEN_ON);
        }

    }
}
