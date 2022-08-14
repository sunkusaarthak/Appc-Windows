using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication2
{
    public partial class Home : Form
    {
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        public Home()
        {
            InitializeComponent();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            int de;
            Boolean b = InternetGetConnectedState(out de, 0);
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2ControlBox1_MouseHover_1(object sender, EventArgs e)
        {
            guna2ControlBox1.FillColor = Color.FromArgb(251, 98, 118);
        }

        private void guna2ControlBox1_MouseLeave_1(object sender, EventArgs e)
        {
            guna2ControlBox1.FillColor = Color.Transparent;
        }

        private void guna2ControlBox2_MouseHover_1(object sender, EventArgs e)
        {
            guna2ControlBox2.FillColor = Color.FromArgb(107, 45, 102);
        }

        private void guna2ControlBox2_MouseLeave_1(object sender, EventArgs e)
        {
            guna2ControlBox2.FillColor = Color.Transparent;
        }

       
    }
}
