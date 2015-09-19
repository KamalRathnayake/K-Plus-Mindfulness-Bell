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
    public partial class SettingsForm : Form
    {
        ISettingsRepository SettingsRepository;
        Form1 handle;
        public SettingsForm(ISettingsRepository settingsRepository,Form1 handlex)
        {
            InitializeComponent();
            this.SettingsRepository = settingsRepository;
            handle = handlex;
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            button1.Click += (s, ev) => { this.Close(); };

            trackBar1.Value = int.Parse(SettingsRepository.Receive("INTERVAL_MINS").Value);
            trackBar1.Scroll += (s, ev) => { label3.Text = trackBar1.Value + " Mins"; };

            trackBar2.Value = int.Parse(SettingsRepository.Receive("DISPLAY_TIME_SECS").Value);
            trackBar2.Scroll += (s, ev) => { label4.Text = trackBar2.Value + " Secs"; };

            panel1.BackColor = Color.FromArgb(255,
                int.Parse(SettingsRepository.Receive("BACK_COLOR_R").Value),
                int.Parse(SettingsRepository.Receive("BACK_COLOR_G").Value),
                int.Parse(SettingsRepository.Receive("BACK_COLOR_B").Value));

            handle.blockClosing = true;
            handle.Opacity = double.Parse(SettingsRepository.Receive("OPACITY").Value);

            this.FormClosed += (s, ev) =>
            {
                handle.Opacity = 0;
                handle.blockClosing = false;
            };
        }


        private void button2_Click(object sender, EventArgs e)
        {
            SettingsRepository.Update(new Setting() { Key = "INTERVAL_MINS", Value = trackBar1.Value + "" });
            SettingsRepository.Update(new Setting() { Key = "DISPLAY_TIME_SECS", Value = trackBar2.Value + "" });
            SettingsRepository.Update(new Setting() { Key = "BACK_COLOR_R", Value = panel1.BackColor.R.ToString() + "" });
            SettingsRepository.Update(new Setting() { Key = "BACK_COLOR_G", Value = panel1.BackColor.G.ToString() + "" });
            SettingsRepository.Update(new Setting() { Key = "BACK_COLOR_B", Value = panel1.BackColor.B.ToString() + "" });
            SettingsRepository.Update(new Setting() { Key = "WINDOWS_TIME", Value = checkBox1.Checked + "" });
            SettingsRepository.Update(new Setting() { Key = "24HOUR_MODE", Value = checkBox2.Checked + "" });
            SettingsRepository.Update(new Setting() { Key = "OPACITY", Value = ((double)trackBar3.Value)/10 + "" });

            handle.LoadSettings();
        }


        private void panel1_Click(object sender, EventArgs e)
        {
            ColorDialog cdi = new ColorDialog();
            if (cdi.ShowDialog() == DialogResult.OK)
            {
                panel1.BackColor = cdi.Color;
            }
        }

    }
}
