﻿using System;
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
        static Dictionary<int, int> end = new Dictionary<int, int>();
        static int inuse=0;
        int min = 0;
        int blockcount = 0;
        int processedcount = 0;
        int busycount = 0;
        int completecount = 0;

        int A =2;
        int B = 6;
        int C = 6;
        int D = 6;
        public static int time;
        int xt = IndexPage.link;
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
            time = time + 1;
            int cn = From.Count;
            
           
            clocktime.Text = time.ToString();
            label19.Text = callinqueue.ToString();
            //--------------------------------------------------------------------------------------  
           for(int i=0; i<n;i++ )
            this.Controls["textboxline"+(i).ToString()].Text = LineStatus[i].ToString();
            textBox6.Text = inuse.ToString();

           

           
            
            //-----------------------------------------------------------------------------
            if (cn >0 )
            {
                int s=0;
                
                
                foreach (KeyValuePair<int, int> item in AT)
                {
                    

                    if (item.Value >= time)
                    {
                        if (item.Value < min)
                        {
                            s = item.Key;
                            min = item.Value;
                            

                        }
                    }

                }
                textBox1.Text = From[s].ToString();
                textBox2.Text = To[s].ToString();
                textBox3.Text = length[s].ToString();
                textBox4.Text = AT[s].ToString();




            }
            //------------------------------------------------------------------------------------- 
           

            foreach (KeyValuePair<int, int> kvp in AT)
            {
               // Console.WriteLine("Key={0} and Value ={1}", kvp.Key, kvp.Value);
                if(kvp.Value==time)
                {
                    processedcount = processedcount + 1;
                    textBox7.Text = processedcount.ToString();
                    if (inuse < 3)

                    {
                        if (LineStatus[To[kvp.Key]]==0 && LineStatus[From[kvp.Key]]==0)
                        {
                            this.Controls["textboxTo" + (xt).ToString()].Text = To[kvp.Key].ToString();
                            this.Controls["textboxFrom" + (xt).ToString()].Text = From[kvp.Key].ToString();
                            this.Controls["textboxEnd" + (xt).ToString()].Text = (time + length[kvp.Key]).ToString();
                            end.Add(kvp.Key, (time + length[kvp.Key]));
                            LineStatus[To[kvp.Key] - 1] = 1;
                            LineStatus[From[kvp.Key] - 1] = 1; 
                            inuse = inuse + 1;
                            xt = xt - 1;
                        }
                        else
                        {
                            busycount = busycount + 1;
                            textBox9.Text = busycount.ToString();
                        }
                    }
                    else
                    {
                        blockcount = blockcount + 1;
                        textBox10.Text = blockcount.ToString();
                    }

                    



                }

                
            }
            //-----------------------------------------------------------------------------
            foreach (KeyValuePair<int, int> itr in end)
            {
                if (itr.Value == time)
                {
                    LineStatus[To[itr.Key] - 1] = 0;
                    LineStatus[From[itr.Key] - 1] = 0;
                    To.Remove(itr.Key);
                    From.Remove(itr.Key);
                    AT.Remove(itr.Key);
                    length.Remove(itr.Key);
                    inuse = inuse - 1;
                    xt = xt + 1;
                    completecount = completecount + 1;
                    textBox8.Text = completecount.ToString();
                    callinqueue = callinqueue - 1;


                }
            }







        }


        public void InsertCall(int f,int t ,int a,int l)
        {
           

            
          
                    From.Add(dc, f);
                    To.Add(dc, t);
                    AT.Add(dc, a);
                    length.Add(dc, l);
                   
                  
                    
                    dc = dc + 1;
              


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
