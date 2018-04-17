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
            KeyboardLedManager.Instance.Start();
        }

        private void orange_Click(object sender, EventArgs e)
        {
            label1.BackColor = Color.Orange;
            KeyboardLedManager.Instance.updateIndicator(Program.indicatorModes.ORANGE_ON);
        }

        private void red_Click(object sender, EventArgs e)
        {
            label1.BackColor = Color.Red;
            KeyboardLedManager.Instance.updateIndicator(Program.indicatorModes.RED_ON);
        }

        private void green_Click(object sender, EventArgs e)
        {
            label1.BackColor = Color.Green;
            KeyboardLedManager.Instance.updateIndicator(Program.indicatorModes.GREEN_ON);
        }

    }
}
