using System; 
using System.Collections.Generic;
using System.ComponentModel; 
using System.Data; 
using System.Drawing; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Shop
{
    public partial class LOGIN : Form
    {
        // Constructor for initializing the LOGIN form
        public LOGIN()
        {
            InitializeComponent();
        }

        // Event handler for label click (unused in this implementation)
        private void label2_Click(object sender, EventArgs e)
        {

        }

        // Event handler to exit the application when the label is clicked
        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Event handler to clear the username and password fields
        private void clearbtn_Click(object sender, EventArgs e)
        {
            username.Text = ""; // Clear the username field
            password.Text = ""; // Clear the password field
        }

        // Connection string for connecting to the SQL Server database
        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-LBKQMDG;Initial Catalog=shopdb;Integrated Security=True;Encrypt=False;Connect Timeout=30");

        // Event handler for login button click
        private async void loginbtn_Click(object sender, EventArgs e)
        {
            // Check if username or password fields are empty
            if (username.Text == "" || password.Text == "")
            {
                MessageBox.Show("\tMissing Credentials\t");
            }
            else
            {
                // Check if a role is selected
                if (role.SelectedIndex > -1)
                {
                    // Logic for ADMIN role login
                    if (role.SelectedItem.ToString() == "ADMIN")
                    {
                        if (username.Text == "admin" && password.Text == "admin") // Hardcoded admin credentials
                        {
                            Attendants att = new Attendants(); // Create an instance of the Attendants form
                            Loading l = new Loading(); // Create an instance of the Loading form
                            l.Show(); // Show the loading screen
                            await Task.Delay(2000); // Wait for 2 seconds
                            att.Show(); // Show the Attendants form
                            this.Hide(); // Hide the current form
                        }
                        else
                        {
                            MessageBox.Show("\tAdmin Credentials Wrong\t"); // Show error for incorrect admin credentials
                        }
                    }
                    else
                    {
                        // Logic for non-admin role login
                        SqlDataAdapter sqa = new SqlDataAdapter("select count(*) from AttTable where AttName='" + username.Text + "' and Password='" + password.Text + "'", Con);
                        DataTable dt = new DataTable();
                        sqa.Fill(dt); // Fill the data table with the query result

                        if (dt.Rows[0][0].ToString() == "1") // Check if the user exists in the database
                        {
                            Globals.Set(username.Text); // Set the global username
                            SellingForm sf = new SellingForm(); // Create an instance of the SellingForm
                            Loading l = new Loading(); // Create an instance of the Loading form
                            l.Show(); // Show the loading screen
                            await Task.Delay(2000); // Wait for 2 seconds
                            sf.Show(); // Show the SellingForm
                            this.Hide(); // Hide the current form
                        }
                        else
                        {
                            MessageBox.Show("Username/Password is Incorrect. Please Try Again"); // Show error for incorrect credentials
                        }
                    }
                }
                else
                {
                    MessageBox.Show("\tSelect A Role\t"); // Show error if no role is selected
                }
            }
        }
    }

    // Static class for global variables
    public static class Globals
    {
        static String NameOfUser; // Static variable to store the username

        // Getter for the username
        internal static string Get()
        {
            return NameOfUser;
        }

        // Setter for the username
        internal static void Set(string text)
        {
            NameOfUser = text;
        }
    }
}
