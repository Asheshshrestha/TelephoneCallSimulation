using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace TelephoneCallSimulation_LostCall
{
    public partial class IndexPage : Form
    {

        public static int link;
        
        public IndexPage()
        {
            InitializeComponent();
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        public void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                link = Convert.ToInt32(textBox1.Text);
                System_State st = new System_State();
                st.Show();
                this.Hide();
            }catch(Exception ex)
            {
                MessageBox.Show("Enter the Line first:");
            }

           

           
           


        }

        
    }
}
