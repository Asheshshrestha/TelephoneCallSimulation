using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TelephoneCallSimulation_LostCall
{
    public partial class Caller : Form
    {
        int from, to, len , at;

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public Caller()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            System_State s = new System_State();
            try { 
            from = Convert.ToInt32(textBox1.Text);
            to = Convert.ToInt32(textBox2.Text);
            len = Convert.ToInt32(textBox3.Text);
            at = Convert.ToInt32(textBox4.Text);
            System_State.callinqueue = System_State.callinqueue + 1;
            s.InsertCall(from, to, at, len);
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Please Fill All the Box.");
            }
      
           
           
           

      



        }
    }
}
