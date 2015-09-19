using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace MindfulnessBell
{
    public partial class Settings : Form
    {
        ISettingsRepository settings;

        public Settings(ISettingsRepository setrep)
        {
            settings = setrep;
        }
        public Settings()
        {
            InitializeComponent();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label3.Text = trackBar1.Value + " Mins";
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            label4.Text = trackBar2.Value + " Secs";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            settings.Insert(new Setting() { Key = "INTERVAL_MINS", Value = trackBar1.Value + "" });
            settings.Insert(new Setting() { Key = "DISPLAY_TIME_SECS", Value = trackBar2.Value + "" });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            //trackBar2.Value = int.Parse(settings.Receive("DISPLAY_TIME_SECS").Value);
            //trackBar1.Value = int.Parse(settings.Receive("INTERVAL_MINS").Value);

            label4.Text = trackBar2.Value + " Secs";
            label3.Text = trackBar1.Value + " Mins";
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
