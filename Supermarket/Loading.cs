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
    public partial class Loading : Form
    {
        public Loading()
        {
            InitializeComponent(); // Initializes the components for the Loading form.
        }

        private void Loading_Load(object sender, EventArgs e)
        {
            // This method is triggered when the Loading form loads.
        }

        Timer tmr; // Declares a Timer object.
        private void Loading_Shown(object sender, EventArgs e)
        {
            tmr = new Timer(); // Instantiates the Timer object.

            // Sets the timer interval to 3 seconds (3000 milliseconds).
            tmr.Interval = 3000;

            // Starts the timer.
            tmr.Start();

            // Subscribes the Tick event of the timer to the tmr_Tick method.
            tmr.Tick += tmr_Tick;
        }

        void tmr_Tick(object sender, EventArgs e)
        {
            // Stops the timer after 3 seconds.
            tmr.Stop();

            // Hides the current Loading form.
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // This method is triggered when pictureBox1 is clicked.
            // Currently, it has no functionality implemented.
        }
    }
}
