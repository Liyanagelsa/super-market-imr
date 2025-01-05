using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shop
{
    static class Program
    {
        /// <summary>  
        /// The main entry point for the application.  
        /// </summary>  
        [STAThread] // Indicates that the COM threading model for the application is single-threaded apartment.  
        static void Main()
        {
            Application.EnableVisualStyles(); // Enables visual styles for the application, giving it a modern look.
            Application.SetCompatibleTextRenderingDefault(false); // Sets whether the application uses GDI+ or GDI for text rendering.
            Application.Run(new Splash()); // Starts the application with the Splash form as the main form.
        }
    }
}
