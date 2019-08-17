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
using System.Collections;

namespace TelephoneCallSimulation_LostCall
{
    public partial class System_State : Form
    {
       static Dictionary<int, int> LineStatus = new Dictionary<int, int>();
        static Dictionary<int, int> From = new Dictionary<int, int>();
        static Dictionary<int, int> To = new Dictionary<int, int>();
        static Dictionary<int, int> AT = new Dictionary<int, int>();
        static Dictionary<int, int> length = new Dictionary<int, int>();
        static int inuse=0;

        int A =2;
        int B = 6;
        int C = 6;
        int D = 6;
        public static int time;
        int i=0,n;
        static int dc = 0;
        public static int callinqueue=0;

       
        public System_State()
        {
            InitializeComponent();
           
            
            
            
        }
        

        public void System_State_Load(object sender, EventArgs e)
        {
            time = 0;
            timer1.Start();
            textBox5.Text = Convert.ToString(IndexPage.link);
            n = LineCalculate(Convert.ToInt32(textBox5.Text));
            for(int x=0; x<n;x++)
            {
                LineStatus.Add(x, 0);
                Linebox(x);
                

            }
            for(int y=1; y<=Convert.ToInt32(IndexPage.link);y++)
            {
                CallInProgress_From(y);
                CallInProgress_To(y);
                CallInProgress_End(y);
            }
            Console.WriteLine(LineStatus[5]);

        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void Label9_Click(object sender, EventArgs e)
        {

        }

        public void Label17_Click(object sender, EventArgs e)
        {
            
        }
        public int LineCalculate(int x)
        {
            int line;
            line = 2 * x + 2;
            return line;
        }
        public System.Windows.Forms.TextBox Linebox(int x)
        {
            System.Windows.Forms.TextBox txt = new System.Windows.Forms.TextBox();
            this.Controls.Add(txt);
            txt.Top = A * 28;
            txt.Left = 15;
            txt.Name = "textboxline" + x;
            txt.Text = LineStatus[x].ToString();
            txt.ReadOnly = true;
            txt.Width = 30;
           
            A = A + 1;
            i = i + 1;
            return txt;
          
        }
        public System.Windows.Forms.TextBox CallInProgress_From(int x)
        {
            System.Windows.Forms.TextBox txt = new System.Windows.Forms.TextBox();
            this.Controls.Add(txt);
            txt.Top = B*28;
            txt.Left = 370;
            txt.Name = "textboxFrom" + x;
            txt.Text="";
            txt.ReadOnly = true;
            txt.Width = 40;
            B = B + 1;
            return txt;

        }
        public System.Windows.Forms.TextBox CallInProgress_To(int x)
        {
            System.Windows.Forms.TextBox txt = new System.Windows.Forms.TextBox();
            this.Controls.Add(txt);
            txt.Top = C * 28;
            txt.Left = 420;
            txt.Name = "textboxTo" + x;
            txt.Text = "";
            txt.ReadOnly = true;
            txt.Width = 40;
            C = C + 1;
            return txt;

        }
        public System.Windows.Forms.TextBox CallInProgress_End(int x)
        {
            System.Windows.Forms.TextBox txt = new System.Windows.Forms.TextBox();
            this.Controls.Add(txt);
            txt.Top = D * 28;
            txt.Left = 470;
            txt.Name = "textboxEnd" + x;
            txt.Text = "";
            txt.ReadOnly = true;
            txt.Width = 40;
            D = D + 1;
            return txt;

        }

        public void Button1_Click(object sender, EventArgs e)
        {
            Caller call = new Caller();
            call.Show();
            
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            int x = IndexPage.link;
            int cn = dc;
            time = time + 1;
            clocktime.Text = time.ToString();
            label19.Text = callinqueue.ToString();
            //--------------------------------------------------------------------------------------  
           for(int i=0; i<n;i++ )
            this.Controls["textboxline"+(i).ToString()].Text = LineStatus[i].ToString();
            textBox6.Text = inuse.ToString();
            if(From.Count>0)
            {
                textBox1.Text = From[cn-1].ToString();
                textBox2.Text = To[cn - 1].ToString();
                textBox3.Text = length[cn - 1].ToString();
                textBox4.Text = AT[cn - 1].ToString();
            }
            foreach (KeyValuePair<int, int> kvp in AT)
            {
               // Console.WriteLine("Key={0} and Value ={1}", kvp.Key, kvp.Value);
                if(kvp.Value==time)
                {
                    Calculation calc = new Calculation();

                    this.Controls["textboxTo" + (x).ToString()].Text = To[kvp.Key].ToString();
                    this.Controls["textboxFrom" + (x).ToString()].Text = From[kvp.Key].ToString();
                    this.Controls["textboxEnd" + (x).ToString()].Text = (time+length[kvp.Key]).ToString();

                }
            }

          


            
        }


        public void InsertCall(int f,int t ,int a,int l)
        {
           // LineStatus[7] = 1;

           if(LineStatus[f]==0 && LineStatus[t]==0)
            {

                if (inuse < 3)
                {
                    From.Add(dc, f);
                    To.Add(dc, t);
                    AT.Add(dc, a);
                    length.Add(dc, l);
                    LineStatus[f] = 1;
                    LineStatus[t] = 1;
                    inuse = inuse + 1;
                    dc = dc + 1;
                }
                else
                {
                    throw new ArgumentException("Line is Full please wait for another call");
                }


                
            }
            else
            {
                throw new ArgumentNullException("Port is occupied try another port: ");
            }
            //---------------------------------------yaha pugyaxas ---------------------

        }

        public void Label19_Click(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
            IndexPage f = new IndexPage();
            f.Show();

        }
    }
}
