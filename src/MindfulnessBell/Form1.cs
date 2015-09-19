using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

using System.Windows.Forms;

namespace MindfulnessBell
{
    public partial class Form1 : Form
    {
        ISettingsRepository SettingsRepository;
        public int interval
        {
            get
            {
                return 1000 * 60* 5;
            }
        }
        public bool Paused { get; set; }
        public Form1()
        {
            InitializeComponent();
            SettingsRepository = new XMLSettingsRepository();
        }
        public void LoadSettings()
        {

            this.BackColor = Color.FromArgb(255,
                int.Parse(SettingsRepository.Receive("BACK_COLOR_R").Value),
                int.Parse(SettingsRepository.Receive("BACK_COLOR_G").Value),
                int.Parse(SettingsRepository.Receive("BACK_COLOR_B").Value));

            this.Opacity = double.Parse(SettingsRepository.Receive("OPACITY").Value);
            label2.Visible = bool.Parse(SettingsRepository.Receive("WINDOWS_TIME").Value);
            if (label2.Visible)
            {
                Thread thread = new Thread(new ThreadStart(() =>
                {
                    while (true)
                    {
                        label2.Invoke(new Action(() =>
                        {
                            label2.Text = DateTime.Now.ToString("HH:mm tt");
                        }));
                        Thread.Sleep(1000);
                    }
                }));
                thread.Start();
            }
        }
        public bool blockClosing = false;
        private void Form1_Load(object sender, EventArgs e)
        {
            ContextMenu cm = new System.Windows.Forms.ContextMenu();
            cm.MenuItems.Add(new MenuItem("Settings", (s, ev) =>
            {
                SettingsForm settings = new SettingsForm(new XMLSettingsRepository(), this);
                settings.Show();
            }));
            cm.MenuItems.Add(new MenuItem("Pause / Resume", (s, ev) => { Paused = !Paused; }));
            cm.MenuItems.Add(new MenuItem("Close", (s, ev) => { this.Close(); }));

            notifyIcon1.ContextMenu = cm;
            notifyIcon1.Click += (s, ev) =>
            {
                SettingsForm settings = new SettingsForm(new XMLSettingsRepository(), this);
                settings.Show();
            };
            Thread thread = new Thread(new ThreadStart(() =>
            {
                while (true)
                {
                    act(() =>
                    {
                        if (!Paused)
                        {
                            this.Opacity = double.Parse(SettingsRepository.Receive("OPACITY").Value);
                            Thread.Sleep(2200);
                            if (!blockClosing)
                                this.Opacity = 0;
                        }
                    });
                    Thread.Sleep(interval);
                }

            }));
            thread.Start();
            LoadSettings();
        }

        public void act(Action action)
        {
            this.Invoke(action);
        }
    }
}
