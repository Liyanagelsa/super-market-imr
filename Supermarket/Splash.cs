using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shop
{
    // Splash screen form class that shows when the application starts
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
        }

        // Counter for progress bar
        int start = 0;

        // Timer event that updates progress bar and launches login form
        private async void timer1_Tick(object sender, EventArgs e)
        {
            start += 1;
            progress.Value = start;
            if (progress.Value == 100)
            {
                progress.Value = 0;
                timer1.Stop();
                LOGIN log = new LOGIN();
                log.Show();
                await Task.Delay(500);    // Half second delay before hiding splash screen
                this.Hide();
            }
        }

        // Start the timer when the splash form loads
        private void Splash_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        // Empty click event handler for label3
        private void label3_Click(object sender, EventArgs e)
        {
        }

        // Empty click event handler for label1
        private void label1_Click(object sender, EventArgs e)
        {
        }
    }
}